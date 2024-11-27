using AutoMapper;
using EventosApp.Application.DTOs;
using EventosApp.Application.DTOs.Eventos;
using EventosApp.Application.Interfaces;
using EventosApp.Application.Interfaces.Eventos;
using EventosApp.Domain.Eventos;

namespace EventosApp.Application.Services.Eventos;

public class EventosService(IUnitOfWork unitOfWork, IMapper mapper) : IEventosService
{
    public async Task<IEnumerable<DetallesEventosDto>> ObtenerTodosEventos()
    {
        var eventosDb = await unitOfWork.Eventos.GetAllAsync().ConfigureAwait(false);
        var eventosDto = mapper.Map<IEnumerable<DetallesEventosDto>>(eventosDb);
        
        return eventosDto;
    }

    public async Task<IEnumerable<DetallesEventosDto>> ObtenerEventosPorUsuario(Guid usuarioId)
    {
        var eventosDb = await unitOfWork
            .Eventos
            .FindAsync(x => x.UsuarioId == usuarioId)
            .ConfigureAwait(false);
        if (!eventosDb.Any())
        {
            throw new NullReferenceException($"No se encontraron eventos para el usuario {usuarioId}");
        }
        var eventosDto = mapper.Map<IEnumerable<DetallesEventosDto>>(eventosDb);
        
        return eventosDto;
    }

    public async Task<DetallesEventosDto> ObtenerDetallesEvento(Guid id)
    {
        var eventoDb = await unitOfWork.Eventos.GetByIdAsync(id).ConfigureAwait(false);
        if (eventoDb.Id == new Guid())
        {
            throw new NullReferenceException("Evento no encontrado");
        }
        var eventoDto = mapper.Map<DetallesEventosDto>(eventoDb);
        
        return eventoDto;
    }

    public async Task<int> GuardarEvento(CrearEventosDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nombre))
        {
            throw new ArgumentException("Nombre del evento no proporcionado");
        }

        if (string.IsNullOrWhiteSpace(dto.Descripcion))
        {
            throw new ArgumentException("Descripcion del evento no proporcionado");
        }

        if (string.IsNullOrWhiteSpace(dto.Ubicacion))
        {
            throw new ArgumentException("Ubicacion del evento no proporcionado");
        }

        if (dto.Duracion <= 0)
        {
            throw new ArgumentException("Duracion del evento incorrecta");
        }

        if (dto.Fecha < DateTime.Now)
        {
            throw new ArgumentException("Fecha del evento debe ser una fecha futura");
        }
        
        var eventoDb = mapper.Map<Evento>(dto);
        
        await unitOfWork.Eventos.AddAsync(eventoDb);
        
        return await unitOfWork.SaveAsync().ConfigureAwait(false);
    }

    public async Task<int> EliminarEvento(Guid id)
    {
        var eventoDb = await unitOfWork.Eventos.GetByIdAsync(id).ConfigureAwait(false);
        if (eventoDb.Id == new Guid())
        {
            throw new NullReferenceException("Evento no encontrado");
        }
        unitOfWork.DetachEntity(eventoDb);
        unitOfWork.Eventos.Delete(eventoDb);
        
        return await unitOfWork.SaveAsync().ConfigureAwait(false);
    }

    public async Task<int> ActualizarEvento(ActualizarEventosDto dto)
    {
        var eventoDb = await unitOfWork.Eventos.GetByIdAsync(dto.Id).ConfigureAwait(false);

        if (eventoDb.Id == new Guid())
        {
            throw new NullReferenceException("Evento no encontrado");
        }
        
        if (string.IsNullOrWhiteSpace(dto.Nombre))
        {
            dto.Nombre = eventoDb.Nombre;
        }

        if (string.IsNullOrWhiteSpace(dto.Descripcion))
        {
            dto.Descripcion = eventoDb.Descripcion;
        }

        if (string.IsNullOrWhiteSpace(dto.Ubicacion))
        {
            dto.Ubicacion = eventoDb.Ubicacion;
        }

        if (dto.Duracion <= 0)
        {
            dto.Duracion = eventoDb.Duracion;
        }

        if (dto.Fecha < DateTime.Now)
        {
            dto.Fecha = eventoDb.Fecha;
        }

        if (string.IsNullOrWhiteSpace(dto.Tipo))
        {
            dto.Tipo = eventoDb.Tipo;
        }

        if (string.IsNullOrWhiteSpace(dto.Notas))
        {
            dto.Notas = eventoDb.Notas;
        }
        unitOfWork.DetachEntity(eventoDb);
        unitOfWork.Eventos.Update(mapper.Map<Evento>(dto));
        
        return await unitOfWork.SaveAsync().ConfigureAwait(false);
    }

    public async Task<EliminarEventosDto> ObtenerInfoEliminarEvento(Guid id)
    {
        var eventoDb = await unitOfWork.Eventos.GetByIdAsync(id).ConfigureAwait(false);
        if (eventoDb.Id == new Guid())
        {
            throw new NullReferenceException("Evento no encontrado");
        }
        //unitOfWork.DetachEntity(eventoDb);
        return mapper.Map<EliminarEventosDto>(eventoDb);
    }
}