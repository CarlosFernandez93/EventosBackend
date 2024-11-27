using EventosApp.Domain.Interfaces;

namespace EventosApp.Application.Services;

public class EmailService(IEmailSender emailSender)
{
    public async Task SendEmailAsync(string email, string subject, string message) 
        => await emailSender.SendEmailAsync(email, subject, message);
}