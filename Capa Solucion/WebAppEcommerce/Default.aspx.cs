using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEcommerce
{
    public partial class _Default : Page
    {
        public List<Articulo> listaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulo = negocio.Listar2();

               
                if (!IsPostBack)
                {
                    //rptArticulos.DataSource = listaArticulo;
                    //rptArticulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        //Para gmail
        protected void btnEnviar_Click(object sender, EventArgs e)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("franbueno681@gmail.com"); // tu dirección Gmail
            mail.To.Add("franbueno681@gmail.com"); // o cualquier destinatario real
            mail.Subject = txtMensajeContact.Text;
            mail.Body = "Nombre del cliente: " + txtNombreContact.Text + "\n" +
                        "Email: " + txtEmailContact.Text + "\n" +
                        "Teléfono: " + txtTelefonoContact.Text + "\n" +
                        "Mensaje: " + txtMensajeContact.Text;

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("franbueno681@gmail.com", "udhp zgut mjcp kngu");
            smtp.EnableSsl = true;

            try
            {
                smtp.Send(mail);

                // SweetAlert en éxito
                ClientScript.RegisterStartupScript(this.GetType(),
                    "alert",
                    "Swal.fire('Correo enviado','Correo del formulario de contacto enviado correctamente.','success');",
                    true);

                txtNombreContact.Text = string.Empty;
                txtEmailContact.Text = string.Empty;
                txtTelefonoContact.Text = string.Empty;
                txtMensajeContact.Text = string.Empty;

            }
            catch (Exception)
            {
                // SweetAlert en error
                ClientScript.RegisterStartupScript(this.GetType(),
                    "alert",
                    "Swal.fire('Error','Error al enviar el correo. Por favor, contáctenos por WhatsApp o email, gracias.','error');",
                    true);
            }
        }

        protected void BtnVermas_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuienesSomos.aspx");
        }
    }
}
