using DataService.Application.Interfaces;
using DataService.Domain.Entities;
using DataService.Infrastructure.Data;

namespace DataService.Infrastructure.Repositories;

public sealed class PaymentScheduleRepository : Repository<PaymentSchedule, long>, IPaymentScheduleRepository
{
    public PaymentScheduleRepository(DataServiceDbContext dbContext) : base(dbContext)
    {
    }
}
