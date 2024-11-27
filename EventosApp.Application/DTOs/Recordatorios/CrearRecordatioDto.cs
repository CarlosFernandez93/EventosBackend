using EventosApp.Domain.Eventos;

namespace EventosApp.Application.DTOs.Recordatorios;

public record CrearRecordatioDto
{
    public Guid EventoId { get; init; }
    public Guid UsuarioId { get; init; }
    public DateTime FechaInicioRecordatorio { get; init; } 
    public DateTime? FechaFinRecordatorio { get; init; }
    public MetodoNotificacion Metodo { get; init; } 
    public bool RecordatorioRecurrente { get; init; } 
    public PeriodoTiempo? PeriodoTiempo { get; init; }
    public int Repeticiones { get; init; }
}