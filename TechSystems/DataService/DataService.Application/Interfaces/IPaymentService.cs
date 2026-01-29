using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Requests;

namespace DataService.Application.Interfaces;

public interface IPaymentService
{
    Task<PagedResult<PaymentDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<PaymentDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PaymentDto> CreateAsync(CreatePaymentRequest request, CancellationToken cancellationToken = default);
    Task<PaymentDto?> UpdateAsync(long id, UpdatePaymentRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
