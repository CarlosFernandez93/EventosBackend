using System.Linq.Expressions;
using AutoMapper;
using EventosApp.Application.DTOs.Usuarios;
using EventosApp.Application.Helpers;
using EventosApp.Application.Interfaces;
using EventosApp.Application.Interfaces.Usuarios;
using EventosApp.Domain.Eventos;

namespace EventosApp.Application.Services.Eventos;

public class UsuarioService(IUnitOfWork unitOfWork, IMapper mapper) : IUsuarioService
{
    public async Task<DetalleUsuarioDto> Login(LoginDto login)
    {
        var usuarioDb = await BuscarUsuario(login.Correo).ConfigureAwait(false);
        
        if (usuarioDb.Correo == string.Empty)
        {
            throw new ArgumentException("Usuario o password invalido");
        }

        if (!PasswordHashHelper.VerifyPassword(login.Password, usuarioDb.Password))
        {
            throw new ArgumentException("No se pudo autenticar, revise que los datos sean correctos.");
        }
        
        var user = mapper.Map<DetalleUsuarioDto>(usuarioDb);
        return user;
    }


    public async Task<int> Registrar(RegistrarUsuarioDto usuario)
    {
        var usuarioExisteDb = await BuscarUsuarios(usuario.Correo).ConfigureAwait(false);
        if (usuarioExisteDb.Any())
        {
            throw new ArgumentException("Ya existe un usuario con ese correo");
        }
        await unitOfWork.Usuarios.AddAsync(mapper.Map<Usuario>(usuario));
        return await unitOfWork.SaveAsync();
    }
    
    private Task<IEnumerable<Usuario>> BuscarUsuarios(string correo)
    {
        return unitOfWork.Usuarios.FindAsync(u => u.Correo == correo);
    }

    private Task<Usuario> BuscarUsuario(string correo)
    {
        return unitOfWork.Usuarios.ObtenerDetalleUsuarioAsync(correo);
    }
}
