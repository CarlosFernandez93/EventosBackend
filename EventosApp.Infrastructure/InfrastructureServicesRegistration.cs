using EventosApp.Application.Interfaces;
using EventosApp.Domain.Interfaces;
using EventosApp.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventosApp.Infrastructure;

public static class InfrastructureServicesRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var serverVersion = new MySqlServerVersion(new Version(8,4,3));
        services.AddDbContext<EventosDbContext>(options =>
        {
            options.UseMySql(
                configuration.GetConnectionString("EventosDbConnection"), 
                serverVersion);
        });

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddTransient<IEmailSender, EmailSender>();
        return services;
    }
}