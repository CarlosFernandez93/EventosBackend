using EventosApp.Domain.Eventos;

namespace EventosApp.Application.Interfaces.Usuarios;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<Usuario> ObtenerDetalleUsuarioAsync(string correo);
}