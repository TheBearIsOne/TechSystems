using DataService.Application.Interfaces;
using DataService.Application.Services;
using FluentValidation;
//using FluentValidation.DependencyInjectionExtensions; // Добавлено для расширения AddValidatorsFromAssembly
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DataService.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IClientService, ClientService>();
        services.AddScoped<ILoanApplicationService, LoanApplicationService>();
        services.AddScoped<ILoanService, LoanService>();
        services.AddScoped<IPaymentScheduleService, PaymentScheduleService>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<ISignalService, SignalService>();
        services.AddScoped<IApplicationLockService, ApplicationLockService>();
        //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);
        return services;
    }
}
