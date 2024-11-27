using EventosApp.Application.DTOs.Recordatorios;
using EventosApp.Domain.Eventos;

namespace EventosApp.Application.Interfaces.Recordatorios;

public interface IRecordatoriosService
{
    Task<IEnumerable<DetallesRecordatorioDto>> ObtenerRecordatoriosAsync();
    
    Task<DetallesRecordatorioDto> ObtenerRecordatorioAsync(Guid id);
    
    Task<int> GuardarRecordatorioAsync(CrearRecordatioDto recordatorio);
    
    Task<int> EliminarRecordatorioAsync(Guid id);
    
    Task<int> ActualizarRecordatorioAsync(ActualizarRecordatorioDto recordatorio);
    
    Task<EliminarRecordatorioDto> ObtenerInfoEliminarRecordatorioAsync(Guid id);
    Task<IEnumerable<DetallesRecordatorioDto>> ObtenerRecordatoriosPorUsuarioAsync(Guid usuarioId);
    
    Task<DetallesRecordatorioDto> ObtenerRecordatorioPorUsuarioPorEvento(Guid usuarioId, Guid eventoId);
}