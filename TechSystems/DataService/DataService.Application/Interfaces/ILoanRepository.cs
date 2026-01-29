using DataService.Domain.Entities;

namespace DataService.Application.Interfaces;

public interface ILoanRepository : IRepository<Loan, long>
{
}
