using EventosApp.Application.Interfaces.Recordatorios;
using EventosApp.Domain.Eventos;
using Microsoft.EntityFrameworkCore;

namespace EventosApp.Infrastructure.Repositories;

public class RecordatoriosRepository : EventosAppRepository<Recordatorio>, IRecordatorioRepository
{
    public RecordatoriosRepository(EventosDbContext context) : base(context)
    {
    }

    public async Task<Recordatorio> ObtenerDetalleRecordatorioAsync(Guid id)
    {
        var recordatorio = await Context
            .Recordatorios
            .Include(x => x.Evento)
            .Include(x => x.Usuario)
            .FirstOrDefaultAsync(r => r.Id == id);
        return recordatorio ?? new Recordatorio();
    }

    public async Task<IEnumerable<Recordatorio>> ObtenerInfoCompletaRecordatoriosAsync()
    {
        var recordatorio = await Context
            .Recordatorios
            .Include(x => x.Evento)
            .Include(x => x.Usuario)
            .ToListAsync();
        return recordatorio;
    }

    public async Task<IEnumerable<Recordatorio>> ObtenerRecordatoriosPorUsuarioAsync(Guid usuarioId)
    {
        IEnumerable<Recordatorio> recordatorios = await Context
            .Recordatorios
            .Include(x => x.Evento)
            .Include(x => x.Usuario)
            .Where(r => r.UsuarioId == usuarioId)
            .ToListAsync();
        
        return recordatorios;
    }

    public async Task<Recordatorio> ObtenerRecordatoriosPorUsuarioPorEventoAsync(Guid usuarioId,
        Guid eventoId)
    {
        var recordatorio = await Context
            .Recordatorios
            .Include(x => x.Evento)
            .Include(x => x.Usuario)
            .Where(r => r.UsuarioId == usuarioId && r.EventoId == eventoId)
            .FirstOrDefaultAsync();
        return recordatorio ?? new Recordatorio();
    }
}