using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEcommerce
{
    public partial class Gestion : System.Web.UI.Page
    {
        public List<Articulo> listaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarArticulos();
            }
        }

        private void cargarArticulos()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulo = negocio.Listar2();   // Obtiene la lista completa
                dgvProductos.DataSource = listaArticulo;
                dgvProductos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Maneja botones DETALLE / MODIFICAR / ELIMINAR
        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int rowIndex = Convert.ToInt32(e.CommandArgument);
            int idArticulo = Convert.ToInt32(dgvProductos.DataKeys[rowIndex].Value);

            ArticuloNegocio negocio = new ArticuloNegocio();

            switch (e.CommandName)
            {
                case "Detalle":
                    Response.Redirect("DetalleProducto.aspx?id=" + idArticulo);
                    break;

                case "Modificar":
                    Response.Redirect("FormularioProducto.aspx?id=" + idArticulo);
                    break;

                case "Eliminar":
                    negocio.EliminarArticulo(idArticulo);
                    cargarArticulos();
                    lblMensaje.Text = "ARTICULO ELIMINADO CORRECTAMENTE";
                    break;
            }
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioProducto.aspx");
        }
    }
}
