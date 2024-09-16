using System;
using System.Configuration; // Importar para leer App.config
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
                // Leer las credenciales del archivo App.config
                string email = ConfigurationManager.AppSettings["SmtpEmail"];
                string password = ConfigurationManager.AppSettings["SmtpPassword"];

                // Configuración del cliente SMTP para Gmail
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Puerto para TLS
                    Credentials = new NetworkCredential(email, password),
                    EnableSsl = true, // Gmail requiere SSL
                };

                // Configurar el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress(email, "Tu Nombre"),
                    Subject = "Prueba de Notificación",
                    Body = "Este es un correo de prueba enviado desde una aplicación de consola en C#.",
                    IsBodyHtml = false
                };

                // Añadir destinatario
                mailMessage.To.Add("destinatario@gmail.com");

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
