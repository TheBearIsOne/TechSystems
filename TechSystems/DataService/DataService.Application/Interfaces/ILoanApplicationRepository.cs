using DataService.Domain.Entities;

namespace DataService.Application.Interfaces;

public interface ILoanApplicationRepository : IRepository<LoanApplication, long>
{
}
