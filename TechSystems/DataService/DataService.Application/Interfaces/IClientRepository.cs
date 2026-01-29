using DataService.Domain.Entities;

namespace DataService.Application.Interfaces;

public interface IClientRepository : IRepository<Client, long>
{
}
