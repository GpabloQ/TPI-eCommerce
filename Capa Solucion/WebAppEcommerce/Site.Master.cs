using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEcommerce
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario user = Session["Usuario"] as Usuario;

                if (user != null)
                {
                    // Usuario logueado
                    lblSaludo.InnerText = "Hola, " + user.Nombre;
                    lblAccion.InnerText = "Cuenta";

                    if (btnLogout != null)
                        btnLogout.Visible = true;

                    // Redirigir a página de cuenta
                    if (lnkCuenta != null)
                        lnkCuenta.HRef = "~/CuentaUsuario.aspx";
                }
                else
                {
                    // Usuario NO logueado
                    lblSaludo.InnerText = "Hola,";
                    lblAccion.InnerText = "Iniciar Sesión";

                    if (btnLogout != null)
                        btnLogout.Visible = false;

                    // Redirigir a inicio de sesión
                    if (lnkCuenta != null)
                        lnkCuenta.HRef = "~/Signin.aspx";
                }
            }
        }



        // Estos son metodos integrados en el framework de ASP.NET, no son creados por mi
        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();   // Limpia los valores de la sesión
            Session.Abandon(); // Termina la sesión (propiedad interna de ASP.NET)
            Response.Redirect("~/Default.aspx"); // Redirige al usuario a la página de inicio
        }
    }
}