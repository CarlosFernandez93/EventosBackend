namespace EventosApp.Domain.Eventos;

public class Usuario
{
    public Guid Id { get; set; } = new();
    public string Nombre { get; set; } = string.Empty;
    public string Correo { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    
    public ICollection<Evento> Eventos { get; set; }
    public ICollection<Invitacion> Invitaciones { get; set; }
    public ICollection<Recordatorio> Recordatorios { get; set; }
    public ICollection<Calendario> Calendarios { get; set; }
}