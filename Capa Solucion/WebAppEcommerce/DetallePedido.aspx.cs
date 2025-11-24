using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebAppEcommerce
{
    public partial class DetallePedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarPedido();
        }

        private void CargarPedido()
        {
            if (Request.QueryString["idCarrito"] == null)
            {
                pnlError.Visible = true;
                return;
            }

            long idCarrito;
            if (!long.TryParse(Request.QueryString["idCarrito"], out idCarrito))
            {
                pnlError.Visible = true;
                return;
            }

            OrdenNegocio negocio = new OrdenNegocio();

            // BUSCAR SOLO CARRITO FINALIZADO
            Dominio.Carrito pedido = negocio.BuscarCarritoPorId(idCarrito);

            if (pedido == null || pedido.EstadoCarrito != "Finalizado")
            {
                pnlError.Visible = true;
                return;
            }

            // Datos generales del pedido
            lblIdCarrito.Text = pedido.IdCarrito.ToString();
            lblFecha.Text = pedido.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
            lblEstado.Text = pedido.EstadoCarrito;

            // Items del pedido
            List<ElementoCarrito> items = negocio.ListarElementosPorCarrito(idCarrito);
            rptItems.DataSource = items;
            rptItems.DataBind();

            // Total
            decimal total = items.Sum(x => x.Cantidad * x.PrecioUnitario);
            lblTotal.Text = "$ " + total.ToString("N2");

            pnlPedido.Visible = true;
        }


    }
}

