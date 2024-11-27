using EventosApp.Domain.Eventos;

namespace EventosApp.Application.DTOs.Recordatorios;

public class EliminarRecordatorioDto
{
    public Guid Id { get; set; }
    public Guid EventoId { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime FechaInicioRecordatorio { get; init; } 
    public DateTime? FechaFinRecordatorio { get; init; }
    public string Metodo { get; init; } 
    public bool RecordatorioRecurrente { get; init; } 
    public string? PeriodoTiempo { get; init; }
    public int Repeticiones { get; init; }
    
    public string NombreEvento { get; init; }
    public DateTime FechaEvento { get; init; }
    public string UbicacionEvento { get; init; }
    public string Usuario     { get; init; }
}