using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace TestNotificationService
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new Service1()
            //};
            //ServiceBase.Run(ServicesToRun);
            SendEmailNotification();            
        }

        // Método para enviar notificaciones por correo electrónico
        static void SendEmailNotification()
        {
            try
            {
                // Configuración del cliente SMTP para Gmail
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587, // Puerto para TLS
                    Credentials = new NetworkCredential("tu_correo@gmail.com", "tu_contraseña_o_contraseña_de_aplicacion"),
                    EnableSsl = true, // Gmail requiere SSL
                };

                // Configurar el mensaje de correo electrónico
                MailMessage mailMessage = new MailMessage
                {
                    From = new MailAddress("tu_correo@gmail.com", "Nombre Remitente"),
                    Subject = "Prueba de Notificación",
                    Body = "Este es un correo de prueba enviado desde una aplicación de consola en C#.",
                    IsBodyHtml = false // Cambia a true si deseas enviar HTML
                };

                // Añadir destinatario
                mailMessage.To.Add("ohgovilla@gmail.com"); // Puedes enviar a múltiples destinatarios si lo deseas

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
