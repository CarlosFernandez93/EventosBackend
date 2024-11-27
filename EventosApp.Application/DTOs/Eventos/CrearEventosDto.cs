namespace EventosApp.Application.DTOs.Eventos;

public record CrearEventosDto
{
    public string Nombre { get; init; } = string.Empty;
    public Guid   UsuarioId { get; set; }
    public string Descripcion { get; init; } = string.Empty; 
    public DateTime Fecha { get; init; }
    public int Duracion { get; init; }
    public string Ubicacion { get; init; } = string.Empty;
    public string Notas { get; init; } = string.Empty;
    public string Tipo { get; init; } = string.Empty;
    public bool Recurrente { get; init; }
}