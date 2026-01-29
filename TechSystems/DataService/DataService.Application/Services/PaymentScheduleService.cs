using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Interfaces;
using DataService.Application.Mappers;
using DataService.Application.Requests;

namespace DataService.Application.Services;

public sealed class PaymentScheduleService(IUnitOfWork unitOfWork, ICacheService cacheService) : IPaymentScheduleService
{
    public async Task<PagedResult<PaymentScheduleDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"payment-schedules:{request.PageNumber}:{request.PageSize}";
        var cached = await cacheService.GetAsync<PagedResult<PaymentScheduleDto>>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var items = await unitOfWork.PaymentSchedules.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var total = await unitOfWork.PaymentSchedules.CountAsync(cancellationToken);
        var result = new PagedResult<PaymentScheduleDto>(items.Select(item => item.ToDto()).ToList(), request.PageNumber, request.PageSize, total);
        await cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }

    public async Task<PaymentScheduleDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"payment-schedules:{id}";
        var cached = await cacheService.GetAsync<PaymentScheduleDto>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var entity = await unitOfWork.PaymentSchedules.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var dto = entity.ToDto();
        await cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(10), cancellationToken);
        return dto;
    }

    public async Task<PaymentScheduleDto> CreateAsync(CreatePaymentScheduleRequest request, CancellationToken cancellationToken = default)
    {
        var entity = request.ToEntity();
        await unitOfWork.PaymentSchedules.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync("payment-schedules:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<PaymentScheduleDto?> UpdateAsync(long id, UpdatePaymentScheduleRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.PaymentSchedules.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.ApplyUpdate(request);
        unitOfWork.PaymentSchedules.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"payment-schedules:{id}", cancellationToken);
        await cacheService.RemoveAsync("payment-schedules:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.PaymentSchedules.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        unitOfWork.PaymentSchedules.Remove(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"payment-schedules:{id}", cancellationToken);
        await cacheService.RemoveAsync("payment-schedules:1:20", cancellationToken);
        return true;
    }
}
