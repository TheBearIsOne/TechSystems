using DataService.Application.Interfaces;
using DataService.Domain.Entities;
using DataService.Infrastructure.Data;

namespace DataService.Infrastructure.Repositories;

public sealed class LoanApplicationRepository : Repository<LoanApplication, long>, ILoanApplicationRepository
{
    public LoanApplicationRepository(DataServiceDbContext dbContext) : base(dbContext)
    {
    }
}
