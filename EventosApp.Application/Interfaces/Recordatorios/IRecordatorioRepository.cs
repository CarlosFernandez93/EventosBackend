using EventosApp.Domain.Eventos;

namespace EventosApp.Application.Interfaces.Recordatorios;

public interface IRecordatorioRepository : IRepository<Recordatorio>
{
    Task<Recordatorio> ObtenerDetalleRecordatorioAsync(Guid id);
    Task<IEnumerable<Recordatorio>> ObtenerInfoCompletaRecordatoriosAsync();
    
    Task<IEnumerable<Recordatorio>> ObtenerRecordatoriosPorUsuarioAsync(Guid usuarioId);

    Task<Recordatorio> ObtenerRecordatoriosPorUsuarioPorEventoAsync(Guid usuarioId, Guid eventoId);
}