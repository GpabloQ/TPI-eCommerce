using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEcommerce
{
    public partial class _Default : Page
    {
        public List<Articulo> listaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // 🔹 Instancia del negocio
            ArticuloNegocio negocio = new ArticuloNegocio();

            try
            {
                listaArticulo = negocio.Listar2();

                // 🔹 Solo se cargan los datos la primera vez
                if (!IsPostBack)
                {
                    rptArticulos.DataSource = listaArticulo;
                    rptArticulos.DataBind();
                }
            }
            catch (Exception ex)
            {
                // 🔹 Mostrar el error si algo falla
                Response.Write("Error: " + ex.Message);
            }
        }
    }
}
