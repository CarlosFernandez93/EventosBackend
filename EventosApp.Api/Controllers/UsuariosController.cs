using EventosApp.Application.DTOs.Usuarios;
using EventosApp.Application.Interfaces.Usuarios;
using EventosApp.Application.Services;
using EventosApp.Domain.Eventos;
using Microsoft.AspNetCore.Mvc;

namespace EventosApp.Api.Controllers;

[ApiController]
[Route("api")]
public class UsuariosController(IUsuarioService service, EmailService emailService) : ControllerBase
{
    [HttpPost("[controller]")]
    public async Task<IActionResult> RegistrarUsuario(RegistrarUsuarioDto usuario)
    {
        try
        {
            int filasInsertadas = await service.Registrar(usuario);
            return filasInsertadas > 0
                ? Created("", "Usuario Registrado")
                : Accepted("", "Peticion procesada, Usuario no registrado");
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("auth/login")]
    public async Task<IActionResult> Login(LoginDto usuario)
    {
        try
        {
            DetalleUsuarioDto resultado = await service.Login(usuario);
            return Ok(resultado);
        }
        catch (ArgumentException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}