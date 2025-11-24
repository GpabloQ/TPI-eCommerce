using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace WebAppEcommerce
{
    public partial class CuentaUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validacion si hay usuario logueado
                Usuario usuario = Session["Usuario"] as Usuario;

                if (usuario == null)
                {
                    pnlNoLogueado.Visible = true;
                    pnlLogueado.Visible = false;
                }
                else
                {
                    pnlNoLogueado.Visible = false;
                    pnlLogueado.Visible = true;

                    // Completar info
                    lblNombre.Text = usuario.NombreCompleto;
                    lblEmail.Text = usuario.Mail;
                    lblTipoUsuario.Text = usuario.TipoUsuarioNombre;
                    lblTelefono.Text = usuario.Telefono;

                    if (usuario.Domicilio != null)
                    {
                        lblDireccion.Text =
                            $"{usuario.Domicilio.Calle} {usuario.Domicilio.Numero}, {usuario.Domicilio.Piso} {usuario.Domicilio.Departamento}, " +
                            $"{usuario.Domicilio.Ciudad}, {usuario.Domicilio.Provincia}, CP: {usuario.Domicilio.CodigoPostal}";
                    }
                    else
                    {
                        lblDireccion.Text = "Sin domicilio registrado";
                    }

                    // Cargar pedidos del usuario
                    CargarPedidos(usuario.IdUsuario);
                }
            }

            // ---- Cargar carrito ----
            Dominio.Carrito carrito = Session["Carrito"] as Dominio.Carrito;

            if (carrito != null && carrito.Items.Any())
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                var datos = carrito.Items.Select(i =>
                {
                    Articulo art = negocio.ListarPorIDArticulo((int)i.IdArticulo);

                    return new
                    {
                        IdArticulo = art.IdArticulo,
                        ImagenUrl = art.UrlImagen,
                        Nombre = art.Nombre,
                        PrecioUnitario = i.PrecioUnitario,
                        Cantidad = i.Cantidad,
                        Subtotal = i.Cantidad * i.PrecioUnitario
                    };
                }).ToList();

                rptCarritoCuenta.DataSource = datos;
                rptCarritoCuenta.DataBind();

                decimal total = carrito.Items.Sum(x => x.Cantidad * x.PrecioUnitario);
                lblTotalCuenta.Text = "Total: $" + total.ToString("N2");

                btnFinalizarCompra.Visible = true;
            }
            else
            {
                lblTotalCuenta.Text = "No tenés productos en tu carrito.";
                btnFinalizarCompra.Visible = false;
            }
        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinDeCompra.aspx");
        }

        // -------------------------------
        // MÉTODO PARA CARGAR PEDIDOS
        // -------------------------------
        private void CargarPedidos(long idUsuario)
        {
            OrdenNegocio negocio = new OrdenNegocio();

            // Trae pedidos finalizados
            List<Dominio.Carrito> pedidos = negocio.ListarOrdenesPorUsuario(idUsuario);

            // Para cada pedido, cargar sus items
            foreach (Dominio.Carrito ped in pedidos)
            {
                ped.Items = negocio.ListarElementosPorCarrito(ped.IdCarrito);
            }

            // carga lista simple
            rptPedidos.DataSource = pedidos;
            rptPedidos.DataBind();
        }
       
        public static List<Dominio.ElementoCarrito> ObtenerDetallePedido(long idCarrito)
        {
            OrdenNegocio negocio = new OrdenNegocio();
            return negocio.ListarElementosPorCarrito(idCarrito);
        }
        protected void rptPedidos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerMas")
            {
                long idCarrito = Convert.ToInt64(e.CommandArgument);
                Response.Redirect("DetallePedido.aspx?idCarrito=" + idCarrito);
            }
        }

    }
}
