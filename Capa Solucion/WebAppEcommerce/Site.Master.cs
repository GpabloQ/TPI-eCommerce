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
                // Obtiene el usuario guardado en sesión (si existe)
                Usuario user = Session["Usuario"] as Usuario;

                // Ocultamos por defecto el panel administrativo
                // (por seguridad, solo un ADMIN lo podrá ver)
                if (navAdmin != null)
                    navAdmin.Visible = false;

                if (user != null)
                {
                    // ------------------------------------------
                    // USUARIO LOGUEADO
                    // ------------------------------------------

                    // Muestra saludo personalizado con el nombre
                    lblSaludo.InnerText = "Hola, " + user.Nombre;

                    // Cambia el texto a "Cuenta" ya que está logueado
                    lblAccion.InnerText = "Cuenta";

                    // Si existe el botón de cierre de sesión → mostrarlo
                    if (btnLogout != null)
                        btnLogout.Visible = true;

                    // Link de menú que debe llevar a la página de cuenta
                    if (lnkCuenta != null)
                        lnkCuenta.HRef = "~/CuentaUsuario.aspx";

                    // ------------------------------------------
                    // MOSTRAR PANEL ADMINISTRATIVO SI ES ADMIN
                    // ------------------------------------------
                    // Habilita el botón/menu del Panel Admin en el navbar
                    if (user.TipoUsuario == 1)
                    {
                        navAdmin.Visible = true;
                    }                    
                }
                else
                {
                    // ------------------------------------------
                    // USUARIO NO LOGUEADO
                    // ------------------------------------------

                    // Texto por defecto sin usuario
                    lblSaludo.InnerText = "Hola,";

                    // Texto de acción "Iniciar Sesión"
                    lblAccion.InnerText = "Iniciar Sesión";

                    // Oculta el botón de logout
                    if (btnLogout != null)
                        btnLogout.Visible = false;

                    // Link lleva a Signin porque no hay sesión activa
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