using DataService.Application.Common;
using DataService.Application.DTOs;
using DataService.Application.Requests;

namespace DataService.Application.Interfaces;

public interface ILoanService
{
    Task<PagedResult<LoanDto>> GetAsync(PageRequest request, CancellationToken cancellationToken = default);
    Task<LoanDto?> GetByIdAsync(long id, CancellationToken cancellationToken = default);
    Task<LoanDto> CreateAsync(CreateLoanRequest request, CancellationToken cancellationToken = default);
    Task<LoanDto?> UpdateAsync(long id, UpdateLoanRequest request, CancellationToken cancellationToken = default);
    Task<bool> DeleteAsync(long id, CancellationToken cancellationToken = default);
}
