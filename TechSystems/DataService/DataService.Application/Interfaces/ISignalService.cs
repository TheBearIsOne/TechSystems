using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Requests;

namespace DataService.Application.Interfaces;

public interface ISignalService
{
    Task<PagedResult<SignalDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<SignalDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<SignalDto> CreateAsync(CreateSignalRequest request, CancellationToken cancellationToken = default);
    Task<SignalDto?> UpdateAsync(long id, UpdateSignalRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
