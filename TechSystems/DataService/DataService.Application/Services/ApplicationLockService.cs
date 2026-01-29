using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Interfaces;
using DataService.Application.Mappers;
using DataService.Application.Requests;

namespace DataService.Application.Services;

public sealed class ApplicationLockService(IUnitOfWork unitOfWork, ICacheService cacheService) : IApplicationLockService
{
    public async Task<PagedResult<ApplicationLockDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"application-locks:{request.PageNumber}:{request.PageSize}";
        var cached = await cacheService.GetAsync<PagedResult<ApplicationLockDto>>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var items = await unitOfWork.ApplicationLocks.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var total = await unitOfWork.ApplicationLocks.CountAsync(cancellationToken);
        var result = new PagedResult<ApplicationLockDto>(items.Select(item => item.ToDto()).ToList(), request.PageNumber, request.PageSize, total);
        await cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }

    public async Task<ApplicationLockDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"application-locks:{id}";
        var cached = await cacheService.GetAsync<ApplicationLockDto>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var entity = await unitOfWork.ApplicationLocks.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var dto = entity.ToDto();
        await cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(10), cancellationToken);
        return dto;
    }

    public async Task<ApplicationLockDto> CreateAsync(CreateApplicationLockRequest request, CancellationToken cancellationToken = default)
    {
        var entity = request.ToEntity();
        await unitOfWork.ApplicationLocks.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync("application-locks:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<ApplicationLockDto?> UpdateAsync(string id, UpdateApplicationLockRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.ApplicationLocks.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.ApplyUpdate(request);
        unitOfWork.ApplicationLocks.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"application-locks:{id}", cancellationToken);
        await cacheService.RemoveAsync("application-locks:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.ApplicationLocks.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        unitOfWork.ApplicationLocks.Remove(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"application-locks:{id}", cancellationToken);
        await cacheService.RemoveAsync("application-locks:1:20", cancellationToken);
        return true;
    }
}
