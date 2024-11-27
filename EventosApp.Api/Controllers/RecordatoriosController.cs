using EventosApp.Application.DTOs.Recordatorios;
using EventosApp.Application.Interfaces.Recordatorios;
using EventosApp.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventosApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecordatoriosController(IRecordatoriosService service, 
                                     EventosRecordatorioService eventosRecordatorioService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetRecordatoriosAsync()
    {
        var recordatorios = await service.ObtenerRecordatoriosAsync().ConfigureAwait(false);
        return Ok(recordatorios);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetRecordatoriosAsync(Guid id)
    {
        try
        {
            var recordatorioDto = await service.ObtenerRecordatorioAsync(id).ConfigureAwait(false);
            return Ok(recordatorioDto);
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

    [HttpPost]
    public async Task<IActionResult> CreateRecordatorioAsync(CrearRecordatioDto recordatorioDto)
    {
        try
        {
            _ = await service.GuardarRecordatorioAsync(recordatorioDto).ConfigureAwait(false);
            var infoRecordatorio = await service
                .ObtenerRecordatorioPorUsuarioPorEvento(recordatorioDto.UsuarioId, recordatorioDto.EventoId)
                .ConfigureAwait(false);
            
            eventosRecordatorioService.ProgramarEnvioRecordatorio(infoRecordatorio);
            
            return StatusCode(201, "Recordatorio creado");
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
    public async Task<IActionResult> UpdateRecordatorioAsync(ActualizarRecordatorioDto dto)
    {
        try
        {
            var result = await service.ActualizarRecordatorioAsync(dto);
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
    public async Task<IActionResult> DeleteRecordatorioAsync(Guid id)
    {
        try
        {
            await service.EliminarRecordatorioAsync(id).ConfigureAwait(false);
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

    [HttpGet("recordatorio-resumen/{id:guid}")]
    public async Task<IActionResult> GetInfoEliminarRecordatorioAsync(Guid id)
    {
        try
        {
            var recordatorio = await service.ObtenerInfoEliminarRecordatorioAsync(id).ConfigureAwait(false);
            return Ok(recordatorio);
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
    
    [HttpGet("usuario/{usuarioId:guid}")]
    public async Task<IActionResult> GetRecordatoriosPorUsuario(Guid usuarioId)
    {
        try
        {
            var recordatorios = await service
                .ObtenerRecordatoriosPorUsuarioAsync(usuarioId)
                .ConfigureAwait(false);
            return Ok(recordatorios);
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
}