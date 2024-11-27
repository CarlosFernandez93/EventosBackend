namespace EventosApp.Application.DTOs.Usuarios;

public record LoginDto(string Correo, string Password);

public record RegistrarUsuarioDto(string Nombre, string Password, string Correo);

public record DetalleUsuarioDto(string Correo, string Nombre, Guid Id, IEnumerable<Guid> Recordatorios);