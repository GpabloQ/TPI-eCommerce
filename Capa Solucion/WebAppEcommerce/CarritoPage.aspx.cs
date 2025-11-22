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
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Dominio.Carrito carrito = Session["Carrito"] as Dominio.Carrito;

                if (carrito != null && carrito.Items.Any())
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();

                    // Armamos el datasource con todos los campos que necesita el Repeater
                    var datos = carrito.Items.Select(i =>
                    {
                        // Traemos el artículo completo
                        Articulo art = negocio.ListarPorIDArticulo(Convert.ToInt32(i.IdArticulo));


                        if (art == null)
                        {
                            return new
                            {
                                IdArticulo = i.IdArticulo,
                                ImagenUrl = "src=\"//acdn-us.mitiendanube.com/stores/002/936/119/themes/common/logo-22781428-1736359691-1181e8b8417134564b3585ce75180c481736359691-480-0.webp\"", // imagen por defecto
                                Nombre = "Artículo no encontrado",
                                PrecioUnitario = i.PrecioUnitario,
                                Cantidad = i.Cantidad,
                                Subtotal = i.Cantidad * i.PrecioUnitario
                            };
                        }
                        return new
                        {
                            IdArticulo = art.IdArticulo,
                            ImagenUrl = art.UrlImagen,
                            Nombre = art.Nombre,
                            PrecioUnitario = i.PrecioUnitario,
                            Cantidad = i.Cantidad,
                            Subtotal = i.Cantidad * i.PrecioUnitario
                        };
                    });

                    rptCarrito.DataSource = datos;
                    //el siguiente for each hay que borrarlo
                    foreach (var item in datos)
                    {
                        System.Diagnostics.Debug.WriteLine(item.ImagenUrl);
                        Response.Write("<p>ImagenUrl: " + item.ImagenUrl + "</p>");
                    }

                    rptCarrito.DataBind();

                    // Calcular total del carrito
                    decimal total = carrito.Items.Sum(x => x.Cantidad * x.PrecioUnitario);
                    lblTotal.Text = "Total: $" + total.ToString("N2");
                }
                else
                {
                    lblTotal.Text = "Tu carrito está vacío.";
                }
            }
        }



        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            Dominio.Carrito carrito = Session["Carrito"] as Dominio.Carrito;
            if (carrito == null) return;

            long idArticulo = Convert.ToInt64(e.CommandArgument);

            var item = carrito.Items.FirstOrDefault(x => x.IdArticulo == idArticulo);
            if (item == null) return;

            switch (e.CommandName)
            {
                case "Sumar":
                    item.Cantidad++;
                    break;

                case "Restar":
                    if (item.Cantidad > 1)
                        item.Cantidad--;
                    else
                        carrito.Items.Remove(item); // si llega a 0, lo eliminamos
                    break;

                case "Eliminar":
                    carrito.Items.Remove(item);
                    break;
            }

            // Guardar cambios en sesión
            Session["Carrito"] = carrito;

            // Recalcular y mostrar
            ActualizarCarrito(carrito);
        }

        private void ActualizarCarrito(Dominio.Carrito carrito)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();

            var datos = carrito.Items.Select(i =>
            {
                Articulo art = negocio.ListarPorIDArticulo((int)i.IdArticulo);
                return new
                {
                    ImagenUrl = art.UrlImagen,
                    Nombre = art.Nombre,
                    PrecioUnitario = i.PrecioUnitario,
                    Cantidad = i.Cantidad,
                    Subtotal = i.Cantidad * i.PrecioUnitario,
                    IdArticulo = i.IdArticulo
                };
            });

            rptCarrito.DataSource = datos;
            rptCarrito.DataBind();

            decimal total = carrito.Items.Sum(x => x.Cantidad * x.PrecioUnitario);
            lblTotal.Text = "Total: $" + total.ToString("N2");
        }







    }
}