using DataService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataService.Infrastructure.Data;

public sealed class DataServiceDbContext(DbContextOptions<DataServiceDbContext> options) : DbContext(options)
{
    public DbSet<ApplicationLock> ApplicationLocks => Set<ApplicationLock>();
    public DbSet<Client> Clients => Set<Client>();
    public DbSet<LoanApplication> LoanApplications => Set<LoanApplication>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<PaymentSchedule> PaymentSchedules => Set<PaymentSchedule>();
    public DbSet<Payment> Payments => Set<Payment>();
    public DbSet<Signal> Signals => Set<Signal>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ApplicationLock>()
            .HasKey(lockEntity => lockEntity.LockId);

        modelBuilder.Entity<LoanApplication>()
            .HasKey(application => application.ApplicationId);

        modelBuilder.Entity<PaymentSchedule>()
            .HasKey(schedule => schedule.ScheduleId);

        modelBuilder.Entity<Client>()
            .HasMany(client => client.LoanApplications)
            .WithOne(application => application.Client)
            .HasForeignKey(application => application.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Client>()
            .HasMany(client => client.Loans)
            .WithOne(loan => loan.Client)
            .HasForeignKey(loan => loan.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<LoanApplication>()
            .HasOne(application => application.Loan)
            .WithOne(loan => loan.Application)
            .HasForeignKey<Loan>(loan => loan.ApplicationId);

        modelBuilder.Entity<Loan>()
            .HasMany(loan => loan.PaymentScheduleEntries)
            .WithOne(schedule => schedule.Loan)
            .HasForeignKey(schedule => schedule.LoanId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Loan>()
            .HasMany(loan => loan.Payments)
            .WithOne(payment => payment.Loan)
            .HasForeignKey(payment => payment.LoanId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}
