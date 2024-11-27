using EventosApp.Application.Interfaces;
using EventosApp.Application.Interfaces.Recordatorios;
using EventosApp.Application.Interfaces.Usuarios;
using EventosApp.Domain.Eventos;
using EventosApp.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EventosApp.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly EventosDbContext _context; 

    public UnitOfWork(EventosDbContext context)
    {
        _context = context;
        Eventos = new EventosAppRepository<Evento>(_context); 
        Usuarios = new UsuarioRepository(_context); 
        Invitaciones = new EventosAppRepository<Invitacion>(_context); 
        Recordatorios = new RecordatoriosRepository(_context); 
        Calendarios = new EventosAppRepository<Calendario>(_context);
        EventosCalendario = new EventosAppRepository<EventoCalendario>(_context);
    }

    public IRepository<Evento> Eventos { get; }
    public IUsuarioRepository Usuarios { get; }
    public IRepository<Invitacion> Invitaciones { get; }
    public IRecordatorioRepository Recordatorios { get; }
    public IRepository<Calendario> Calendarios { get; }
    public IRepository<EventoCalendario> EventosCalendario { get; }
    
    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public void DetachEntity(object entity)
    {
        _context.Remove(entity);
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }
}