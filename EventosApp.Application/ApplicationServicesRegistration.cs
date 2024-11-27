using EventosApp.Application.Interfaces.Eventos;
using EventosApp.Application.Interfaces.Recordatorios;
using EventosApp.Application.Interfaces.Usuarios;
using EventosApp.Application.Services;
using EventosApp.Application.Services.Eventos;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.DependencyInjection;

namespace EventosApp.Application;

public static class ApplicationServicesRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(ApplicationServicesRegistration).Assembly);
        services.AddScoped<IEventosService, EventosService>();
        services.AddScoped<IRecordatoriosService, RecordatoriosService>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        
        services.AddTransient<EmailService>();
        services.AddTransient<EventosRecordatorioService>();

        services.AddHangfire(config => config.UseMemoryStorage());
        services.AddHangfireServer();
        
        return services;
    }
}