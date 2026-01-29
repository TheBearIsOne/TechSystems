using DataService.Application.Interfaces;
using DataService.Domain.Entities;
using DataService.Infrastructure.Data;

namespace DataService.Infrastructure.Repositories;

public sealed class LoanRepository : Repository<Loan, long>, ILoanRepository
{
    public LoanRepository(DataServiceDbContext dbContext) : base(dbContext)
    {
    }
}
