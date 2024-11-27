using AutoMapper;
using EventosApp.Application.DTOs;
using EventosApp.Application.DTOs.Eventos;
using EventosApp.Application.DTOs.Recordatorios;
using EventosApp.Application.DTOs.Usuarios;
using EventosApp.Application.Helpers;
using EventosApp.Domain.Eventos;

namespace EventosApp.Application.Mapper;

public class EventosAppProfile : Profile
{
    public EventosAppProfile()
    {
        CreateMap<ActualizarEventosDto, Evento>();
        CreateMap<CrearEventosDto, Evento>();
        CreateMap<Evento, DetallesEventosDto>();
        CreateMap<Evento, EliminarEventosDto>();

        CreateMap<ActualizarRecordatorioDto, Recordatorio>();
        CreateMap<CrearRecordatioDto, Recordatorio>();
        CreateMap<Recordatorio, DetallesRecordatorioDto>()
            .ForMember(dest => dest.NombreEvento, opt => opt.MapFrom(src => src.Evento.Nombre))
            .ForMember(dest => dest.FechaEvento, opt => opt.MapFrom(src => src.Evento.Fecha))
            .ForMember(dest => dest.UbicacionEvento, opt => opt.MapFrom(src => src.Evento.Ubicacion))
            .ForMember(dest => dest.Usuaroio, opt => opt.MapFrom(src => src.Usuario.Nombre))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Usuario.Correo));
            
        CreateMap<Recordatorio, EliminarRecordatorioDto>()
            .ForMember(dest => dest.NombreEvento, opt => opt.MapFrom(src => src.Evento.Nombre))
            .ForMember(dest => dest.FechaEvento, opt => opt.MapFrom(src => src.Evento.Fecha))
            .ForMember(dest => dest.UbicacionEvento, opt => opt.MapFrom(src => src.Evento.Ubicacion))
            .ForMember(dest => dest.Usuario, opt => opt.MapFrom(src => src.Usuario.Nombre));

        CreateMap<RegistrarUsuarioDto, Usuario>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => PasswordHashHelper.HashPassword(src.Password)));
        CreateMap<Usuario, DetalleUsuarioDto>() 
            .ForCtorParam("Correo", opt => opt.MapFrom(src => src.Correo)) 
            .ForCtorParam("Nombre", opt => opt.MapFrom(src => src.Nombre)) 
            .ForCtorParam("Id", opt => opt.MapFrom(src => src.Id)) 
            .ForCtorParam("Recordatorios", opt => opt.MapFrom(src => src.Recordatorios.Select(r =>r.Id)));
    }
}