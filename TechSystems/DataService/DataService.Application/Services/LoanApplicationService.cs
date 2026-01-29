using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Interfaces;
using DataService.Application.Mappers;
using DataService.Application.Requests;

namespace DataService.Application.Services;

public sealed class LoanApplicationService(IUnitOfWork unitOfWork, ICacheService cacheService) : ILoanApplicationService
{
    public async Task<PagedResult<LoanApplicationDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"loan-applications:{request.PageNumber}:{request.PageSize}";
        var cached = await cacheService.GetAsync<PagedResult<LoanApplicationDto>>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var items = await unitOfWork.LoanApplications.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var total = await unitOfWork.LoanApplications.CountAsync(cancellationToken);
        var result = new PagedResult<LoanApplicationDto>(items.Select(item => item.ToDto()).ToList(), request.PageNumber, request.PageSize, total);
        await cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }

    public async Task<LoanApplicationDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"loan-applications:{id}";
        var cached = await cacheService.GetAsync<LoanApplicationDto>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var entity = await unitOfWork.LoanApplications.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var dto = entity.ToDto();
        await cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(10), cancellationToken);
        return dto;
    }

    public async Task<LoanApplicationDto> CreateAsync(CreateLoanApplicationRequest request, CancellationToken cancellationToken = default)
    {
        var entity = request.ToEntity();
        await unitOfWork.LoanApplications.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync("loan-applications:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<LoanApplicationDto?> UpdateAsync(long id, UpdateLoanApplicationRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.LoanApplications.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.ApplyUpdate(request);
        unitOfWork.LoanApplications.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"loan-applications:{id}", cancellationToken);
        await cacheService.RemoveAsync("loan-applications:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.LoanApplications.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        unitOfWork.LoanApplications.Remove(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"loan-applications:{id}", cancellationToken);
        await cacheService.RemoveAsync("loan-applications:1:20", cancellationToken);
        return true;
    }
}
