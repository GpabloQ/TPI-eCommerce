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
    public partial class DetalleProducto : System.Web.UI.Page
    {
        public List<Articulo> listaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idArticuloStr = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(idArticuloStr))
                {
                    int idArticulo = int.Parse(idArticuloStr);
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo art = negocio.ListarPorIDArticulo(idArticulo);

                    // ✅ Paso 1: Validar UrlImagen
                    if (string.IsNullOrEmpty(art.UrlImagen) && art.ListaUrls != null && art.ListaUrls.Count > 0)
                    {
                        // Usar la primera imagen de la lista como principal
                        art.UrlImagen = art.ListaUrls[0];
                        // Opcional: remover esa primera de la lista para no duplicarla
                        art.ListaUrls.RemoveAt(0);
                    }

                    rptArticulos.DataSource = new List<Articulo> { art };
                    rptArticulos.DataBind();
                }
                else
                {
                    lblMensaje.Text = "No se recibió un id válido en la URL.";
                }
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
                               
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}