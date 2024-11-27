using EventosApp.Domain.Eventos;

namespace EventosApp.Application.DTOs.Recordatorios;

public class DetallesRecordatorioDto
{
    public Guid Id { get; set; }
    public Guid EventoId { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime FechaInicioRecordatorio { get; init; } 
    public DateTime? FechaFinRecordatorio { get; init; }
    public MetodoNotificacion Metodo { get; init; } 
    public bool RecordatorioRecurrente { get; init; } 
    public PeriodoTiempo? PeriodoTiempo { get; init; }
    public int Repeticiones { get; init; }

    public string NombreEvento { get; init; }
    public DateTime FechaEvento { get; init; }
    public string UbicacionEvento { get; init; }
    public string Usuaroio     { get; init; }
    public string Email { get; init; }
}