namespace EventosApp.Domain.Eventos;

public class EventoCalendario
{
    public Guid EventoId { get; set; }
    public Guid CalendarioId { get; set; }

    public Evento Evento { get; set; }
    public Calendario Calendario { get; set; }
}