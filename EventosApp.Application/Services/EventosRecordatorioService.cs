using System.Globalization;
using EventosApp.Application.DTOs.Recordatorios;
using Hangfire;

namespace EventosApp.Application.Services;

public class EventosRecordatorioService(EmailService emailService)
{
    public void ProgramarEnvioRecordatorio(DetallesRecordatorioDto recordatorio)
    {
        var culture = new CultureInfo("es-SV");
        var fechaFormateda = recordatorio.FechaEvento.ToString("D", culture);
        var horaFormateada = recordatorio.FechaEvento.ToString("t", culture);
        
        var htmlBody = $$"""
                         <!DOCTYPE html>
                         <html>
                         <head>
                             <meta charset="UTF-8">
                             <title>Recordatorio de Evento</title>
                             <style>
                                 body {
                                     font-family: Arial, sans-serif;
                                     background-color: #f0f0f0;
                                 }
                         
                                 .container {
                                     max-width: 600px;
                                     margin: 0 auto;
                                     padding: 20px;
                                     background-color: #fff;
                                     border-radius: 5px;
                                     box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
                                 }
                         
                                 h1 {
                                     text-align: center;
                                 }
                         
                                 p {
                                     line-height: 1.5;
                                 }
                             </style>
                         </head>
                         <body>
                             <div class="container">
                                 <h1>Recordatorio de Evento {{recordatorio.NombreEvento}}</h1>
                                 <p>Te recordamos que tienes un evento el día: <strong>{{fechaFormateda}}</strong> a las <strong>{{horaFormateada}}</strong>.</p>
                                 <p>La ubicación es: <strong>{{recordatorio.UbicacionEvento}}</strong>.</p>
                             </div>
                         </body>
                         </html>
                         """;
        
        ScheduleEmailReminder(recordatorio.Email, $"Recordatorio del Evento {recordatorio.NombreEvento}", htmlBody, recordatorio.FechaInicioRecordatorio);
    }

    
    // Método para programar el recordatorio
    private void ScheduleEmailReminder(string toEmail, string subject, string body, DateTime sendAt)
    {
        // Programar el correo para que se envíe en la fecha y hora que el usuario haya elegido
        BackgroundJob.Schedule(() => emailService.SendEmailAsync(toEmail, subject, body), sendAt);
    }
}