using EventosApp.Domain.Eventos;

namespace EventosApp.Application.DTOs.Recordatorios;

public record ActualizarRecordatorioDto
{
    public Guid Id { get; init; }
    public Guid EventoId { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime FechaInicioRecordatorio { get; set; } 
    public DateTime? FechaFinRecordatorio { get; set; }
    public MetodoNotificacion Metodo { get; set; } 
    public bool RecordatorioRecurrente { get; set; } 
    public PeriodoTiempo? PeriodoTiempo { get; set; }
    public int Repeticiones { get; set; }
}