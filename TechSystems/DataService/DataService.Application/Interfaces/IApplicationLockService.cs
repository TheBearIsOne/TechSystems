using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Requests;

namespace DataService.Application.Interfaces;

public interface IApplicationLockService
{
    Task<PagedResult<ApplicationLockDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<ApplicationLockDto?> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    Task<ApplicationLockDto> CreateAsync(CreateApplicationLockRequest request, CancellationToken cancellationToken = default);
    Task<ApplicationLockDto?> UpdateAsync(string id, UpdateApplicationLockRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(string id, CancellationToken cancellationToken = default);
}
