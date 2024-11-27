using AutoMapper;
using EventosApp.Application.DTOs.Recordatorios;
using EventosApp.Application.Interfaces;
using EventosApp.Application.Interfaces.Recordatorios;
using EventosApp.Domain.Eventos;

namespace EventosApp.Application.Services.Eventos;

public class RecordatoriosService(IUnitOfWork unitOfWork, IMapper mapper) : IRecordatoriosService
{
    public async Task<IEnumerable<DetallesRecordatorioDto>> ObtenerRecordatoriosAsync()
    {
        var recordatorios = await unitOfWork.Recordatorios.ObtenerInfoCompletaRecordatoriosAsync().ConfigureAwait(false);
        var recordatiosDto = mapper.Map<IEnumerable<DetallesRecordatorioDto>>(recordatorios);
        
        return recordatiosDto;
    }

    public async Task<DetallesRecordatorioDto> ObtenerRecordatorioAsync(Guid id)
    {
        var recordatorioDb = await unitOfWork.Recordatorios.ObtenerDetalleRecordatorioAsync(id).ConfigureAwait(false);

        if (recordatorioDb.Id == new Guid() || recordatorioDb.Evento.Id == new Guid() || recordatorioDb.UsuarioId == new Guid())
        {
            throw new NullReferenceException("Recordatorio no valido o no asociado a ningun evento/usuario");
        }
        
        var recordatorioDto = mapper.Map<DetallesRecordatorioDto>(recordatorioDb);
        
        return recordatorioDto;
    }

    public async Task<int> GuardarRecordatorioAsync(CrearRecordatioDto recordatorio)
    {
        var recordatorioDb = mapper.Map<Recordatorio>(recordatorio);
        
        await unitOfWork.Recordatorios.AddAsync(recordatorioDb);
        
        return await unitOfWork.SaveAsync().ConfigureAwait(false);
    }

    public async Task<int> EliminarRecordatorioAsync(Guid id)
    {
        var recordatorioDb = await unitOfWork.Recordatorios.GetByIdAsync(id).ConfigureAwait(false);
        if (recordatorioDb.Id == new Guid())
        {
            throw new NullReferenceException("Evento no encontrado");
        }
        unitOfWork.DetachEntity(recordatorioDb);
        unitOfWork.Recordatorios.Delete(recordatorioDb);
        
        return await unitOfWork.SaveAsync().ConfigureAwait(false);
    }

    public async Task<int> ActualizarRecordatorioAsync(ActualizarRecordatorioDto recordatorio)
    {
        var recordatorioDb = await unitOfWork.Recordatorios.GetByIdAsync(recordatorio.Id).ConfigureAwait(false);

        if (recordatorio.Id == new Guid())
        {
            throw new NullReferenceException("Recordatorio no encontrado");
        }

        if (recordatorio.UsuarioId == new Guid())
        {
            recordatorio.UsuarioId = recordatorioDb.UsuarioId;
        }

        if (recordatorio.EventoId == new Guid())
        {
            recordatorio.EventoId = recordatorioDb.EventoId;
        }

        if (recordatorio.FechaInicioRecordatorio < DateTime.Now || recordatorio.FechaInicioRecordatorio < recordatorioDb.FechaInicioRecordatorio)
        {
            recordatorio.FechaInicioRecordatorio = recordatorioDb.FechaInicioRecordatorio;
        }

        if (string.IsNullOrWhiteSpace(recordatorio.Metodo.ToString()))
        {
            recordatorio.Metodo = recordatorioDb.Metodo;
        }
        
        unitOfWork.DetachEntity(recordatorioDb);
        unitOfWork.Recordatorios.Update(mapper.Map<Recordatorio>(recordatorio));
        
        return await unitOfWork.SaveAsync().ConfigureAwait(false);
    }

    public async Task<EliminarRecordatorioDto> ObtenerInfoEliminarRecordatorioAsync(Guid id)
    {
        var recordatorioDb = await unitOfWork.Recordatorios.ObtenerDetalleRecordatorioAsync(id).ConfigureAwait(false);
        if (recordatorioDb.Id == new Guid())
        {
            throw new NullReferenceException("Recordatorio no encontrado");
        }
        //unitOfWork.DetachEntity(eventoDb);
        return mapper.Map<EliminarRecordatorioDto>(recordatorioDb);
    }

    public async Task<IEnumerable<DetallesRecordatorioDto>> ObtenerRecordatoriosPorUsuarioAsync(Guid usuarioId)
    {
        if (usuarioId == Guid.Empty)
        {
            throw new ArgumentException("Usuario no valido");
        }
        var recordatorios = await unitOfWork
            .Recordatorios
            .ObtenerRecordatoriosPorUsuarioAsync(usuarioId)
            .ConfigureAwait(false);
        
        var recordatorioDtos = mapper.Map<IEnumerable<DetallesRecordatorioDto>>(recordatorios);
        
        return recordatorioDtos;
    }

    public async Task<DetallesRecordatorioDto> ObtenerRecordatorioPorUsuarioPorEvento(Guid usuarioId, Guid eventoId)
    {
        var recordatorio = await unitOfWork
            .Recordatorios
            .ObtenerRecordatoriosPorUsuarioPorEventoAsync(usuarioId, eventoId)
            .ConfigureAwait(false);
        
        var recordatorioDto = mapper.Map<DetallesRecordatorioDto>(recordatorio);
        return recordatorioDto;
    }
}