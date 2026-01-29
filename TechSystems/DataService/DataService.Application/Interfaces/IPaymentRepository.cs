using DataService.Domain.Entities;

namespace DataService.Application.Interfaces;

public interface IPaymentRepository : IRepository<Payment, long>
{
}
