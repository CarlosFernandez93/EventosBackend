using EventosApp.Domain.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace EventosApp.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly string _smtpServer = "smtp.gmail.com";
    private readonly int _smtpPort = 587;
    private readonly string _smtpUser = "eventosappdev@gmail.com";
    private readonly string _smtpPassword = "rdqu fnov zkaz eegu";
    
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("Recordatorio de Evento", _smtpUser));
        emailMessage.To.Add(new MailboxAddress("", to));
        emailMessage.Subject = subject;
        
        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var smtpClient = new SmtpClient();
        
        // Conexión con el servidor SMTP
        await smtpClient.ConnectAsync(_smtpServer, _smtpPort, SecureSocketOptions.StartTlsWhenAvailable);
        await smtpClient.AuthenticateAsync(_smtpUser, _smtpPassword);
            
        // Enviar el correo
        await smtpClient.SendAsync(emailMessage);

        // Desconectarse después de enviar
        await smtpClient.DisconnectAsync(true);
    }
}