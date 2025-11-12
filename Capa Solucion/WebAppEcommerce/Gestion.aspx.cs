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
                ArticuloNegocio negocio = new ArticuloNegocio();
                rptArticulos.DataSource = negocio.ListarConImagenes();
                rptArticulos.DataBind();
                cargarArticulos();
            }
        }

        

        private void cargarArticulos()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulo = negocio.Listar2();

                rptArticulos.DataSource = listaArticulo;
                rptArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void rptArticulos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            try
            {
                int idArticulo = Convert.ToInt32(e.CommandArgument);

                if (idArticulo <= 0) return;

                ArticuloNegocio negocio = new ArticuloNegocio();

                switch (e.CommandName)
                {
                    case "Modificar":
                        Response.Redirect("FormularioProducto.aspx?id=" + idArticulo, false);
                        break;

                    case "Eliminar":
                        negocio.EliminarArticulo(idArticulo);
                        cargarArticulos();                        
                        lblMensaje.Text = "ARTICULO ELIMINADO CORRECTAMENTE";
                        break;
                }
            }
            catch (Exception ex)
            {
               
                throw;
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {            
            Response.Redirect("FormularioProducto.aspx", false);
        }
    }
}
