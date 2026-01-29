using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Requests;

namespace DataService.Application.Interfaces;

public interface IClientService
{
    Task<PagedResult<ClientDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<ClientDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<ClientDto> CreateAsync(CreateClientRequest request, CancellationToken cancellationToken = default);
    Task<ClientDto?> UpdateAsync(long id, UpdateClientRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
