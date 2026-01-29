using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Interfaces;
using DataService.Application.Mappers;
using DataService.Application.Requests;

namespace DataService.Application.Services;

public sealed class SignalService(IUnitOfWork unitOfWork, ICacheService cacheService) : ISignalService
{
    public async Task<PagedResult<SignalDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"signals:{request.PageNumber}:{request.PageSize}";
        var cached = await cacheService.GetAsync<PagedResult<SignalDto>>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var items = await unitOfWork.Signals.GetPagedAsync(request.PageNumber, request.PageSize, cancellationToken);
        var total = await unitOfWork.Signals.CountAsync(cancellationToken);
        var result = new PagedResult<SignalDto>(items.Select(item => item.ToDto()).ToList(), request.PageNumber, request.PageSize, total);
        await cacheService.SetAsync(cacheKey, result, TimeSpan.FromMinutes(5), cancellationToken);
        return result;
    }

    public async Task<SignalDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default)
    {
        var cacheKey = $"signals:{id}";
        var cached = await cacheService.GetAsync<SignalDto>(cacheKey, cancellationToken);
        if (cached is not null)
        {
            return cached;
        }

        var entity = await unitOfWork.Signals.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        var dto = entity.ToDto();
        await cacheService.SetAsync(cacheKey, dto, TimeSpan.FromMinutes(10), cancellationToken);
        return dto;
    }

    public async Task<SignalDto> CreateAsync(CreateSignalRequest request, CancellationToken cancellationToken = default)
    {
        var entity = request.ToEntity();
        await unitOfWork.Signals.AddAsync(entity, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync("signals:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<SignalDto?> UpdateAsync(long id, UpdateSignalRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.Signals.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.ApplyUpdate(request);
        unitOfWork.Signals.Update(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"signals:{id}", cancellationToken);
        await cacheService.RemoveAsync("signals:1:20", cancellationToken);
        return entity.ToDto();
    }

    public async Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default)
    {
        var entity = await unitOfWork.Signals.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return false;
        }

        unitOfWork.Signals.Remove(entity);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        await cacheService.RemoveAsync($"signals:{id}", cancellationToken);
        await cacheService.RemoveAsync("signals:1:20", cancellationToken);
        return true;
    }
}
