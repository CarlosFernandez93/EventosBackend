namespace EventosApp.Domain.Eventos;

public class Evento
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UsuarioId { get; set; }
    public string Nombre { get; set; } = string.Empty;
    public string Descripcion { get; set; } = string.Empty;
    public DateTime Fecha { get; set; } = DateTime.Now;
    public int Duracion { get; set; } 
    public string Ubicacion { get; set; } = string.Empty;
    public string Notas { get; set; } = string.Empty;
    public string Tipo { get; set; } = string.Empty;
    public bool Recurrente { get; set; }

    public Usuario? Usuario { get; set; }
    public ICollection<Invitacion> Invitaciones { get; set; } = [];
    public ICollection<Recordatorio> Recordatorios { get; set; } = [];
    public ICollection<EventoCalendario> EventosCalendario { get; set; } = [];
}