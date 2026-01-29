using DataService.Application.Interfaces;

namespace DataService.Infrastructure.Data;

public sealed class UnitOfWork(
    DataServiceDbContext dbContext,
    IApplicationLockRepository applicationLocks,
    IClientRepository clients,
    ILoanApplicationRepository loanApplications,
    ILoanRepository loans,
    IPaymentScheduleRepository paymentSchedules,
    IPaymentRepository payments,
    ISignalRepository signals) : IUnitOfWork
{
    public IApplicationLockRepository ApplicationLocks { get; } = applicationLocks;
    public IClientRepository Clients { get; } = clients;
    public ILoanApplicationRepository LoanApplications { get; } = loanApplications;
    public ILoanRepository Loans { get; } = loans;
    public IPaymentScheduleRepository PaymentSchedules { get; } = paymentSchedules;
    public IPaymentRepository Payments { get; } = payments;
    public ISignalRepository Signals { get; } = signals;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }
}
