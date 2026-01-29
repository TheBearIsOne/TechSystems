using DataService.Domain.Entities;

namespace DataService.Application.Interfaces;

public interface IUnitOfWork
{
    IApplicationLockRepository ApplicationLocks { get; }
    IClientRepository Clients { get; }
    ILoanApplicationRepository LoanApplications { get; }
    ILoanRepository Loans { get; }
    IPaymentScheduleRepository PaymentSchedules { get; }
    IPaymentRepository Payments { get; }
    ISignalRepository Signals { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
