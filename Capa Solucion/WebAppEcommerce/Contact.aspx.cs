using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace WebAppEcommerce
{
    public partial class Contact : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        //Para gmail
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("franbueno681@gmail.com"); // tu dirección Gmail
            mail.To.Add(txtEmailContact.Text); // o cualquier destinatario real
            mail.Subject = txtMensajeContact.Text;
            mail.Body = "Nombre: " + txtNombreContact.Text + "\n" +
                        "Email: " + txtEmailContact.Text + "\n" +
                        "Teléfono: " + txtTelefonoContact.Text + "\n" +
                        "Mensaje: " + txtMensajeContact.Text;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("franbueno681@gmail.com", "udhp zgut mjcp kngu");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);
                lblMensajeContact.Text = "Correo del formulario del contacto enviado correctamente.";
            }
            catch (Exception ex)
            {
                lblMensajeContact.Text = "Error al enviar: " + ex.Message;
            }
        }

        /* para mail trap
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            EmailService emailService = new EmailService();
            emailService.armarCorreo(txtEmailContact.Text, txtNombreContact.Text, txtMensajeContact.Text);
            try
            {
                emailService.enviarEmail();
            }
            catch (Exception ex)
            {

                Session.Add("Error", ex);
            }
        }
        */
    }
}