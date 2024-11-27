using EventosApp.Application.Interfaces.Recordatorios;
using EventosApp.Application.Interfaces.Usuarios;
using EventosApp.Domain.Eventos;

namespace EventosApp.Application.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IRepository<Evento> Eventos { get; } 
    IUsuarioRepository Usuarios { get; } 
    IRepository<Invitacion> Invitaciones { get; } 
    IRecordatorioRepository Recordatorios { get; } 
    IRepository<Calendario> Calendarios { get; } 
    IRepository<EventoCalendario> EventosCalendario { get; } 
    Task<int> SaveAsync();
    void DetachEntity(object entity);
}