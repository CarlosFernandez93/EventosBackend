namespace EventosApp.Application.DTOs.Eventos;

public class EliminarEventosDto
{
    public string Nombre { get; init; } = string.Empty;
    public DateTime Fecha { get; init; }
    public string Ubicacion { get; init; } = string.Empty;
    public bool Recurrente { get; init; }
}