using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Interfaces;
using DataService.Application.Mappers;
using DataService.Application.Requests;

namespace DataService.Application.Services;

public sealed class LoanService(IUnitOfWork unitOfWork, ICacheService cacheService) : ILoanService
{
    public async Task<PagedResult<LoanDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"loans:{request.PageNumber}:{request.PageSize}";
        var cached = await cacheService.GetAsync<PagedResult<LoanDto>>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var items = await unitOfWork.Loans.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var total = await unitOfWork.Loans.CountAsync(cancellationToken);
        var result = new PagedResult<LoanDto>(items.Select(item => item.ToDto()).ToList(), request.PageNumber, request.PageSize, total);
        await cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }

    public async Task<LoanDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"loans:{id}";
        var cached = await cacheService.GetAsync<LoanDto>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var entity = await unitOfWork.Loans.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var dto = entity.ToDto();
        await cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(10), cancellationToken);
        return dto;
    }

    public async Task<LoanDto> CreateAsync(CreateLoanRequest request, CancellationToken cancellationToken = default)
    {
        var entity = request.ToEntity();
        await unitOfWork.Loans.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync("loans:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<LoanDto?> UpdateAsync(long id, UpdateLoanRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.Loans.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.ApplyUpdate(request);
        unitOfWork.Loans.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"loans:{id}", cancellationToken);
        await cacheService.RemoveAsync("loans:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.Loans.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        unitOfWork.Loans.Remove(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"loans:{id}", cancellationToken);
        await cacheService.RemoveAsync("loans:1:20", cancellationToken);
        return true;
    }
}
