using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Interfaces;
using DataService.Application.Mappers;
using DataService.Application.Requests;

namespace DataService.Application.Services;

public sealed class PaymentService(IUnitOfWork unitOfWork, ICacheService cacheService) : IPaymentService
{
    public async Task<PagedResult<PaymentDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"payments:{request.PageNumber}:{request.PageSize}";
        var cached = await cacheService.GetAsync<PagedResult<PaymentDto>>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var items = await unitOfWork.Payments.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var total = await unitOfWork.Payments.CountAsync(cancellationToken);
        var result = new PagedResult<PaymentDto>(items.Select(item => item.ToDto()).ToList(), request.PageNumber, request.PageSize, total);
        await cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }

    public async Task<PaymentDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"payments:{id}";
        var cached = await cacheService.GetAsync<PaymentDto>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var entity = await unitOfWork.Payments.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var dto = entity.ToDto();
        await cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(10), cancellationToken);
        return dto;
    }

    public async Task<PaymentDto> CreateAsync(CreatePaymentRequest request, CancellationToken cancellationToken = default)
    {
        var entity = request.ToEntity();
        await unitOfWork.Payments.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync("payments:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<PaymentDto?> UpdateAsync(long id, UpdatePaymentRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.Payments.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.ApplyUpdate(request);
        unitOfWork.Payments.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"payments:{id}", cancellationToken);
        await cacheService.RemoveAsync("payments:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.Payments.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        unitOfWork.Payments.Remove(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"payments:{id}", cancellationToken);
        await cacheService.RemoveAsync("payments:1:20", cancellationToken);
        return true;
    }
}
