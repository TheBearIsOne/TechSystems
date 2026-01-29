using DataService.Application.Interfaces;
using DataService.Domain.Entities;
using DataService.Infrastructure.Data;

namespace DataService.Infrastructure.Repositories;

public sealed class ClientRepository : Repository<Client, long>, IClientRepository
{
    public ClientRepository(DataServiceDbContext dbContext) : base(dbContext)
    {
    }
}
