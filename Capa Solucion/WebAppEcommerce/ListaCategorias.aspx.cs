using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace WebAppEcommerce
{
    public partial class ListaCategorias : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            dgvCategorias.DataSource = negocio.listar();
            dgvCategorias.DataBind();
        }

        protected void btnGestionar_Click(object sender, EventArgs e)
        {

        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvCategorias.SelectedDataKey.Value.ToString();
            Response.Redirect("GestionCategoria.aspx?id=" + id);
        }

        protected void dgvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCategorias.PageIndex = e.NewPageIndex;
            dgvCategorias.DataBind();
        }
    }
}