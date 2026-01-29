using DataService.Domain.Entities;

namespace DataService.Application.Interfaces;

public interface IApplicationLockRepository : IRepository<ApplicationLock, string>
{
}
