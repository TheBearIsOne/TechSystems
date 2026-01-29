using DataService.Application.Interfaces;
using DataService.Domain.Entities;
using DataService.Infrastructure.Data;

namespace DataService.Infrastructure.Repositories;

public sealed class ApplicationLockRepository : Repository<ApplicationLock, string>, IApplicationLockRepository
{
    public ApplicationLockRepository(DataServiceDbContext dbContext) : base(dbContext)
    {
    }
}
