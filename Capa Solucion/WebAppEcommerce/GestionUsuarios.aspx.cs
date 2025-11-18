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
            if (!IsPostBack)
            {
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