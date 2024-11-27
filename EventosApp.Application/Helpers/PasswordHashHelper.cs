namespace EventosApp.Application.Helpers;

public static class PasswordHashHelper
{
    // Método para generar el hash de una contraseña
    public static string HashPassword(string password)
    {
        // Usamos el método 'HashPassword' que genera el hash con un salt aleatorio
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    // Método para verificar si la contraseña ingresada coincide con el hash almacenado
    public static bool VerifyPassword(string password, string hashedPassword)
    {
        // Verificamos si la contraseña ingresada coincide con el hash
        return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
    }
}