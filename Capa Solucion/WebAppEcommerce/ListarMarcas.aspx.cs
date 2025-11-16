using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;

namespace WebAppEcommerce
{
    public partial class ListarMarcas : System.Web.UI.Page
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {

            MarcaNegocio negocio = new MarcaNegocio();
            dgvMarcas.DataSource = negocio.listar();
            dgvMarcas.DataBind();
        }

        protected void dgvMarcas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMarcas.PageIndex = e.NewPageIndex;
            dgvMarcas.DataBind();
        }

        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvMarcas.SelectedDataKey.Value.ToString();
            Response.Redirect("AgregarMarca.aspx?id=" + id);
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarMarca.aspx", false);
        }      
    }
}