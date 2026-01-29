using DataService.Application.Interfaces;
using DataService.Infrastructure.Caching;
using DataService.Infrastructure.Data;
using DataService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DataService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Database") ?? configuration["DATABASE_CONNECTION_STRING"];
        services.AddDbContext<DataServiceDbContext>(options =>
            options.UseNpgsql(connectionString));

        services.AddScoped<IApplicationLockRepository, ApplicationLockRepository>();
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<ILoanApplicationRepository, LoanApplicationRepository>();
        services.AddScoped<ILoanRepository, LoanRepository>();
        services.AddScoped<IPaymentScheduleRepository, PaymentScheduleRepository>();
        services.AddScoped<IPaymentRepository, PaymentRepository>();
        services.AddScoped<ISignalRepository, SignalRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis") ?? configuration["REDIS_CONNECTION_STRING"];
        });
        services.AddScoped<ICacheService, RedisCacheService>();

        return services;
    }
}
