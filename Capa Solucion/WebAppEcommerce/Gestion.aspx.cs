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
    public partial class Gestion : System.Web.UI.Page
    {
        
        protected Repeater rptArticulos;

        public List<Articulo> listaArticulo { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            listaArticulo = negocio.listar2();
            if (!IsPostBack)
            {
                rptArticulos.DataSource = listaArticulo;
                rptArticulos.DataBind();
            }
        }
    }
}


