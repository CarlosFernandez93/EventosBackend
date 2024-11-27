namespace EventosApp.Domain.Eventos;

public class Calendario
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public string Nombre { get; set; }

    public Usuario Usuario { get; set; }
    public ICollection<EventoCalendario> EventosCalendario { get; set; }
}