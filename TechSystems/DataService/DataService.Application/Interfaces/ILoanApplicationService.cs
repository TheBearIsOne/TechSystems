using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Requests;

namespace DataService.Application.Interfaces;

public interface ILoanApplicationService
{
    Task<PagedResult<LoanApplicationDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<LoanApplicationDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<LoanApplicationDto> CreateAsync(CreateLoanApplicationRequest request, CancellationToken cancellationToken = default);
    Task<LoanApplicationDto?> UpdateAsync(long id, UpdateLoanApplicationRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
