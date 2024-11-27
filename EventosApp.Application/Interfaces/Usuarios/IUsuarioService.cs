using System.Linq.Expressions;
using EventosApp.Application.DTOs.Usuarios;
using EventosApp.Domain.Eventos;

namespace EventosApp.Application.Interfaces.Usuarios;

public interface IUsuarioService
{
    Task<DetalleUsuarioDto> Login(LoginDto login);
    Task<int> Registrar(RegistrarUsuarioDto usuario);
}