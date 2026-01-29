using DataService.Domain.Entities;

namespace DataService.Application.Interfaces;

public interface IPaymentScheduleRepository : IRepository<PaymentSchedule, long>
{
}
