using DataService.Application.Interfaces;
using DataService.Domain.Entities;
using DataService.Infrastructure.Data;

namespace DataService.Infrastructure.Repositories;

public sealed class SignalRepository : Repository<Signal, long>, ISignalRepository
{
    public SignalRepository(DataServiceDbContext dbContext) : base(dbContext)
    {
    }
}
