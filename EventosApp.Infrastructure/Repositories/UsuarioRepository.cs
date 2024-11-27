using EventosApp.Application.Interfaces.Usuarios;
using EventosApp.Domain.Eventos;
using Microsoft.EntityFrameworkCore;

namespace EventosApp.Infrastructure.Repositories;

public class UsuarioRepository : EventosAppRepository<Usuario>, IUsuarioRepository
{
    public UsuarioRepository(EventosDbContext context) : base(context)
    {
    }

    public async Task<Usuario> ObtenerDetalleUsuarioAsync(string correo)
    {
        var user = await Context
            .Usuarios
            .Include(u => u.Recordatorios)
            //.ThenInclude(r => r.Id)
            .FirstOrDefaultAsync(u => u.Correo == correo);
        
        return user ?? new Usuario();
    }
}