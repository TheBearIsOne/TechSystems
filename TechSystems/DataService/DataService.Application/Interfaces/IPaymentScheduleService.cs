using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Requests;

namespace DataService.Application.Interfaces;

public interface IPaymentScheduleService
{
    Task<PagedResult<PaymentScheduleDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<PaymentScheduleDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<PaymentScheduleDto> CreateAsync(CreatePaymentScheduleRequest request, CancellationToken cancellationToken = default);
    Task<PaymentScheduleDto?> UpdateAsync(long id, UpdatePaymentScheduleRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
