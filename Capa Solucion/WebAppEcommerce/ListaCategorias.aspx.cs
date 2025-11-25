using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace WebAppEcommerce
{
    public partial class ListaCategorias : System.Web.UI.Page
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario user = Session["Usuario"] as Usuario;

            if (user == null)
            {
                Response.Redirect("Signin.aspx");
                return;
            }

            if (user.TipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Si es admin, cargamos la grilla
            if (!IsPostBack)
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                dgvCategorias.DataSource = negocio.listar();
                dgvCategorias.DataBind();
            }
        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvCategorias.SelectedDataKey.Value.ToString();
            Response.Redirect("GestionCategoria.aspx?id=" + id);
        }

        protected void dgvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCategorias.PageIndex = e.NewPageIndex;

            CategoriaNegocio negocio = new CategoriaNegocio();
            dgvCategorias.DataSource = negocio.listar();
            dgvCategorias.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionCategoria.aspx");
        }

        protected void dgvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                int id = Convert.ToInt32(e.CommandArgument);

                CategoriaNegocio negocio = new CategoriaNegocio();
                negocio.eliminar(id);

                dgvCategorias.DataSource = negocio.listar();
                dgvCategorias.DataBind();
            }
        }

    }
}