namespace EventosApp.Domain.Eventos;

public class Invitacion
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EventoId { get; set; }
    public Guid UsuarioId { get; set; }
    public string Estado { get; set; }

    public Evento Evento { get; set; }
    public Usuario Usuario { get; set; }
}