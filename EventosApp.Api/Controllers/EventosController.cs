using EventosApp.Application.DTOs;
using EventosApp.Application.DTOs.Eventos;
using EventosApp.Application.Interfaces.Eventos;
using EventosApp.Domain.Eventos;
using Microsoft.AspNetCore.Mvc;

namespace EventosApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventosController(IEventosService service) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllEventosAsync()
    {
        var eventos = await service.ObtenerTodosEventos().ConfigureAwait(false); 
        return Ok(eventos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEventoByIdAsync(Guid id)
    {
        try
        {
            var evento = await service.ObtenerDetallesEvento(id).ConfigureAwait(false);
        
            return Ok(evento);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (NullReferenceException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateEventoAsync(CrearEventosDto evento)
    {
        try
        {
            var result = await service.GuardarEvento(evento);
            return StatusCode(201, "Evento creado");
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPut]
    public async Task<IActionResult> UpdateEventoAsync(ActualizarEventosDto evento)
    {
        try
        {
            var result = await service.ActualizarEvento(evento);
            return StatusCode(204);
        }
        catch (ArgumentException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEventoAsync(Guid id)
    {
        try
        {
            await service.EliminarEvento(id).ConfigureAwait(false);
            return StatusCode(204);
        }
        catch (NullReferenceException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("evento-resumen/{id:guid}")]
    public async Task<IActionResult> GetInfoEliminarEventosAsync(Guid id)
    {
        try
        {
            var evento = await service.ObtenerInfoEliminarEvento(id).ConfigureAwait(false);
            return Ok(evento);
        }
        catch (NullReferenceException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet("usuario/{id:guid}")]
    public async Task<IActionResult> GetEventosPorUsuarioAsync(Guid id)
    {
        try
        {
            var eventos = await service.ObtenerEventosPorUsuario(id).ConfigureAwait(false);
            return Ok(eventos);
        }
        catch (NullReferenceException e)
        {
            return NotFound(e.Message);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}