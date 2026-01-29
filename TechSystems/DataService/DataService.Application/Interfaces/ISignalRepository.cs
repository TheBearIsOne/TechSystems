using DataService.Domain.Entities;

namespace DataService.Application.Interfaces;

public interface ISignalRepository : IRepository<Signal, long>
{
}
