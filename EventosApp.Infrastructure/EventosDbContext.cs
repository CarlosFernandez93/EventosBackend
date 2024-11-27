using EventosApp.Domain.Eventos;
using Microsoft.EntityFrameworkCore;

namespace EventosApp.Infrastructure;

public class EventosDbContext : DbContext
{
    public EventosDbContext(DbContextOptions<EventosDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Invitacion> Invitaciones { get; set; }
    public DbSet<Recordatorio> Recordatorios { get; set; }
    public DbSet<Calendario> Calendarios { get; set; }
    public DbSet<EventoCalendario> EventosCalendario { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Evento>()
            .ToTable("Evento");
        
        modelBuilder.Entity<Usuario>()
            .ToTable("Usuario");
        
        modelBuilder.Entity<Invitacion>()
            .ToTable("Invitacion");
        
        modelBuilder.Entity<Recordatorio>()
            .ToTable("Recordatorio");
        
        modelBuilder.Entity<Calendario>()
            .ToTable("Calendario");
        
        modelBuilder.Entity<EventoCalendario>()
            .ToTable("EventoCalendario");
        
        modelBuilder.Entity<EventoCalendario>()
            .HasKey(ec => new { ec.EventoId, ec.CalendarioId });

        modelBuilder.Entity<EventoCalendario>()
            .HasOne(ec => ec.Evento)
            .WithMany(e => e.EventosCalendario)
            .HasForeignKey(ec => ec.EventoId);

        modelBuilder.Entity<EventoCalendario>()
            .HasOne(ec => ec.Calendario)
            .WithMany(c => c.EventosCalendario)
            .HasForeignKey(ec => ec.CalendarioId);
    }
}