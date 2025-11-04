using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace Negocio
{
    public class EmailService
    {

        private MailMessage email;
        private SmtpClient server;

        public EmailService()
        {
            //Los siguientes datos se completan según el servidor que utilicemos
            server = new SmtpClient();
            server.Credentials = new NetworkCredential("7eb6c61e1851c6", "a4cf0d4ca53eb3");
            server.EnableSsl = true;
            server.Port = 2525;
            server.Host = "sandbox.smtp.mailtrap.io";
                }

        public void armarCorreo(string emailDestino, string nombre, string mensaje)
        {
            email = new MailMessage();
            //El correo de respuesta llega desde el siguiente email:
            email.From = new MailAddress("noresponder@ecommerceprogramacioniii.com");
            email.To.Add(emailDestino);
            email.Subject = nombre;
            email.IsBodyHtml = true;
            email.Body = mensaje;
        }

        public void enviarEmail()
        {
            try
            {
                server.Send(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
