using System;
using System.Configuration; // Para leer el archivo App.config
using System.Net;
using System.Net.Mail;

namespace EmailNotificationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Llamamos al método que envía el correo
            SendEmailNotification();
        }

        // Método para enviar notificaciones por correo electrónico
        static void SendEmailNotification()
        {
            try
            {
                // Leer las credenciales y configuraciones desde el archivo App.config
                string smtpEmail = ConfigurationManager.AppSettings["SmtpEmail"];
                string smtpPassword = ConfigurationManager.AppSettings["SmtpPassword"];
                string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
                int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
                bool enableSsl = bool.Parse(ConfigurationManager.AppSettings["EnableSsl"]);

                string fromEmail = ConfigurationManager.AppSettings["FromEmail"];
                string fromName = ConfigurationManager.AppSettings["FromName"];
                string toEmail = ConfigurationManager.AppSettings["ToEmail"];
                string subject = ConfigurationManager.AppSettings["Subject"];

                // Crear tabla HTML con los datos proporcionados
                string htmlBody = @"
                    <html>
                    <body>
                        <h2>EDI File Information</h2>
                        <table border='1' cellpadding='10'>
                            <tr>
                                <th>EDI file name received</th>
                                <th>Received Date</th>
                                <th>PDF/XML pair count</th>
                                <th>PDF File Creation Time</th>
                                <th>ZIP count</th>
                                <th>ZIP creation Time</th>
                            </tr>
                            <tr>
                                <td>edi_file_123.edi</td>
                                <td>2023-09-01 12:30:00</td>
                                <td>10</td>
                                <td>2023-09-01 12:35:00</td>
                                <td>5</td>
                                <td>2023-09-01 12:40:00</td>
                            </tr>
                        </table>
                    </body>
                    </html>";

                // Configuración del cliente SMTP
                SmtpClient smtpClient = new SmtpClient(smtpHost)
                {
                    Port = smtpPort,
                    Credentials = new NetworkCredential(smtpEmail, smtpPassword),
                    EnableSsl = enableSsl
                };

                // Configurar el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(fromEmail, fromName),
                    Subject = subject,
                    Body = htmlBody,
                    IsBodyHtml = true // Importante: Especificar que el cuerpo es HTML
                };

                // Añadir destinatario
                mailMessage.To.Add(toEmail);

                // Enviar el correo electrónico
                smtpClient.Send(mailMessage);
                Console.WriteLine("Correo enviado exitosamente.");

            }
            catch (Exception ex)
            {
                // Captura de errores en caso de que el envío falle
                Console.WriteLine("Error enviando el correo: " + ex.Message);
            }
        }
    }
}
