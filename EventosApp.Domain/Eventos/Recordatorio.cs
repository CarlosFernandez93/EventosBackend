namespace EventosApp.Domain.Eventos;

public class Recordatorio
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid EventoId { get; set; }
    public Guid UsuarioId { get; set; }
    public MetodoNotificacion Metodo { get; set; } = MetodoNotificacion.Correo;
    public DateTime FechaInicioRecordatorio { get; set; } = DateTime.Now;
    public DateTime? FechaFinRecordatorio { get; set; }
    public bool RecordatorioRecurrente { get; set; } = false;
    public PeriodoTiempo? PeriodoTiempo { get; set; }
    public int Repeticiones { get; set; } = 0;

    public virtual Evento? Evento { get; init; } 
    public virtual Usuario? Usuario { get; init; } 
}

public enum MetodoNotificacion
{
    Correo = 1,
    NotificacionWeb = 2,
    Celular = 4,
    Otro = 4
}

public enum PeriodoTiempo
{
    Dias = 2,
    Meses = 1,
    Horas = 3,
    Minutos = 4
}