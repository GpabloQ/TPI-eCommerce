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
    public partial class GestionUsuarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validación de acceso
            Usuario user = Session["Usuario"] as Usuario;

            // Si NO está logueado Chau
            if (user == null)
            {
                Response.Redirect("Signin.aspx");
                return;
            }

            // Si NO es ADMIN (TipoUsuario = 1) Chau
            if (user.TipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Si es ADMIN Adentro
            if (!IsPostBack)
            {
                var master = (SiteMaster)Master;
                master.SetBreadcrumb(new List<(string, string)>
                {
                    ("Inicio", "Default.aspx"),
                    ("Panel Administrativo", "PanelAdmin.aspx"),
                    ("Gestion de Productos", null),
                });

                UsuarioNegocio negocio = new UsuarioNegocio();
                dgvUsuarios.DataSource = negocio.listar();
                dgvUsuarios.DataBind();
            }
        }


        protected void dgvUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvUsuarios.SelectedDataKey.Value.ToString();
            Response.Redirect("AgregarUsuario.aspx?id=" + id);
        }
        
        protected void dgvUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvUsuarios.PageIndex = e.NewPageIndex;
            UsuarioNegocio negocio = new UsuarioNegocio();
            dgvUsuarios.DataSource = negocio.listar();
            dgvUsuarios.DataBind();
        }

        protected void btnAgregarUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarUsuario.aspx");
        }

        // se ejecuta al presiona la confirmacion de eliminar en la ventana ejergente de JS.
        protected void dgvUsuarios_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                long id = Convert.ToInt64(e.CommandArgument);
                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.EliminarUsuario(id);

                // Actualiza la lista de usuarios después de eliminar
                dgvUsuarios.DataSource = negocio.listar();
                dgvUsuarios.DataBind();

                // Muestra mensaje de éxito
                ScriptManager.RegisterStartupScript(this, GetType(), "UsuarioEliminado",
                    "Swal.fire('Eliminado', 'El usuario fue eliminado correctamente', 'success');", true);
            }                                     
        }        
    }
}