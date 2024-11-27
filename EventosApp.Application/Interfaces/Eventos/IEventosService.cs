using System.Linq.Expressions;
using EventosApp.Application.DTOs;
using EventosApp.Application.DTOs.Eventos;

namespace EventosApp.Application.Interfaces.Eventos;

public interface IEventosService
{
    Task<IEnumerable<DetallesEventosDto>> ObtenerTodosEventos();
    
    Task<IEnumerable<DetallesEventosDto>> ObtenerEventosPorUsuario(Guid usuarioId);
    
    Task<DetallesEventosDto> ObtenerDetallesEvento(Guid id);
    
    Task<int> GuardarEvento(CrearEventosDto dto);
    
    Task<int> EliminarEvento(Guid id);
    
    Task<int> ActualizarEvento(ActualizarEventosDto dto);
    
    Task<EliminarEventosDto> ObtenerInfoEliminarEvento(Guid id);
}