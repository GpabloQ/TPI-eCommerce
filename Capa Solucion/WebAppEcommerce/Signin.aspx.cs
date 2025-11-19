using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEcommerce
{
    public partial class Signin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string mail = txtEmail.Text.Trim();
                string pass = txtPassword.Text.Trim();

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = negocio.Login(mail, pass);

                if (usuario != null && usuario.Estado)
                {
                    // Guardar usuario en sesión
                    Session["usuario"] = usuario;

                    // Si es ADMIN (1) → ir al panel
                    if (usuario.TipoUsuario == 1) // ADMINISTRADOR
                    {
                        Response.Redirect("Gestion.aspx", false);
                    }
                    else
                    {
                        // Si es CLIENTE (2) → ir a Home o donde vos quieras
                        Response.Redirect("Default.aspx", false);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "loginError",
                        "Swal.fire('Error', 'Usuario o contraseña incorrectos', 'error');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "loginError",
                   $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
        }

    }
}