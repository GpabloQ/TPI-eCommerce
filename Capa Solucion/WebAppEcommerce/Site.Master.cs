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
                Usuario user = Session["usuario"] as Usuario;

                if (user != null)
                {
                    // Usuario logueado
                    lblSaludo.InnerText = "Hola, " + user.Nombre;
                    lblAccion.InnerText = "Cuenta";

                    // Se mostrar boton de cerrar sesion
                    if (btnLogout != null)
                        btnLogout.Visible = true;
                }
                else
                {
                    // Usuario NO logueado
                    lblSaludo.InnerText = "Hola,";
                    lblAccion.InnerText = "Iniciar Sesion";

                    // Se oculta el boton de cerrar sesion
                    if (btnLogout != null)
                        btnLogout.Visible = false;
                }
            }
        }



        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();
            Response.Redirect("~/Default.aspx");
        }

        
       
    }
}