using DataService.Application.Common;
using DataService.Application.Interfaces;
using DataService.Application.Requests;
using DataService.Application.Services;
using DataService.Domain.Entities;
using Xunit;

namespace DataService.Tests;

public class ClientServiceTests
{
    [Fact]
    public async Task CreateAsync_AddsClient()
    {
        var unitOfWork = new FakeUnitOfWork();
        var cache = new InMemoryCacheService();
        var service = new ClientService(unitOfWork, cache);

        var result = await service.CreateAsync(new CreateClientRequest(
            ExternalId: "ext-1",
            FirstName: "Test",
            LastName: "User",
            MiddleName: null,
            BirthDate: DateOnly.FromDateTime(DateTime.UtcNow.AddYears(-30)),
            PassportSeries: null,
            PassportNumber: null,
            PhoneNumber: "+100000000",
            Email: "test@example.com",
            RegistrationAddress: null,
            ResidentialAddress: null,
            Income: 1000m,
            EmploymentStatus: "Employed"));

        Assert.Equal("ext-1", result.ExternalId);
        Assert.Equal(1, await unitOfWork.Clients.CountAsync());
    }

    private sealed class FakeUnitOfWork : IUnitOfWork
    {
        public FakeUnitOfWork()
        {
            Clients = new FakeRepository<Client, long>(client => client.ClientId);
            ApplicationLocks = new FakeRepository<ApplicationLock, string>(lockEntity => lockEntity.LockId);
            LoanApplications = new FakeRepository<LoanApplication, long>(application => application.ApplicationId);
            Loans = new FakeRepository<Loan, long>(loan => loan.LoanId);
            PaymentSchedules = new FakeRepository<PaymentSchedule, long>(schedule => schedule.ScheduleId);
            Payments = new FakeRepository<Payment, long>(payment => payment.PaymentId);
            Signals = new FakeRepository<Signal, long>(signal => signal.SignalId);
        }

        public IApplicationLockRepository ApplicationLocks { get; }
        public IClientRepository Clients { get; }
        public ILoanApplicationRepository LoanApplications { get; }
        public ILoanRepository Loans { get; }
        public IPaymentScheduleRepository PaymentSchedules { get; }
        public IPaymentRepository Payments { get; }
        public ISignalRepository Signals { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) => Task.FromResult(0);
    }

    private sealed class FakeRepository<TEntity, TKey>(Func<TEntity, TKey> keySelector) : IRepository<TEntity, TKey>
        where TEntity : class
    {
        private readonly List<TEntity> _items = new();
        private long _nextId = 1;

        public Task<TEntity?> GetByIdAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(_items.FirstOrDefault(item => EqualityComparer<TKey>.Default.Equals(keySelector(item), id)));
        }

        public Task<IReadOnlyList<TEntity>> GetPagedAsync(int pageNumber, int pageSize, CancellationToken cancellationToken = default)
        {
            var skip = Math.Max(0, (pageNumber - 1) * pageSize);
            var page = _items.Skip(skip).Take(pageSize).ToList();
            return Task.FromResult<IReadOnlyList<TEntity>>(page);
        }

        public Task<long> CountAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult((long)_items.Count);
        }

        public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
        {
            if (entity is Client client)
            {
                client.ClientId = _nextId++;
            }

            _items.Add(entity);
            return Task.CompletedTask;
        }

        public void Update(TEntity entity)
        {
        }

        public void Remove(TEntity entity)
        {
            _items.Remove(entity);
        }
    }

    private sealed class InMemoryCacheService : ICacheService
    {
        private readonly Dictionary<string, object> _cache = new();

        public Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default)
        {
            if (_cache.TryGetValue(key, out var value) && value is T typed)
            {
                return Task.FromResult<T?>(typed);
            }

            return Task.FromResult<T?>(default);
        }

        public Task SetAsync<T>(string key, T value, TimeSpan? timeToLive = null, CancellationToken cancellationToken = default)
        {
            _cache[key] = value!;
            return Task.CompletedTask;
        }

        public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
        {
            _cache.Remove(key);
            return Task.CompletedTask;
        }
    }
}
