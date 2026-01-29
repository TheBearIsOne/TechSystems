using DataService.Application.Interfaces;
using DataService.Domain.Entities;
using DataService.Infrastructure.Data;

namespace DataService.Infrastructure.Repositories;

public sealed class PaymentRepository : Repository<Payment, long>, IPaymentRepository
{
    public PaymentRepository(DataServiceDbContext dbContext) : base(dbContext)
    {
    }
}
