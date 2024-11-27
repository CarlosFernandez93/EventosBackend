namespace EventosApp.Application.DTOs.Eventos;

public record ActualizarEventosDto
{
    public Guid Id { get; init; }
    public Guid UsuarioId { get; init; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; }
    public int Duracion { get; set; }
    public string Ubicacion { get; set; } = string.Empty;
    public string Notas { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public bool Recurrente { get; set; }
}