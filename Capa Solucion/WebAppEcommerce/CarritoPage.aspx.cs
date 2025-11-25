using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Globalization;

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

            Articulo art;
            ArticuloNegocio artNegocio = new ArticuloNegocio();

           

            var item = carrito.Items.FirstOrDefault(x => x.IdArticulo == idArticulo);
            if (item == null) return;

           art = artNegocio.ListarPorIDArticulo(Convert.ToInt32(item.IdArticulo));
          
            switch (e.CommandName)
            {
                case "Sumar":
                    if(art.Stock != 0 && art != null) 
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


        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            if (usuario == null)
            {
                ScriptManager.RegisterStartupScript(this, GetType(),
                    "msg", "Swal.fire({ title: 'Inicia sesión', text: 'Debes iniciar sesión para finalizar la compra', icon: 'info', confirmButtonText: 'Aceptar' });", true);
                return;
            }

            Dominio.Carrito carrito = Session["Carrito"] as Dominio.Carrito;
            if (carrito == null || carrito.Items.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(),
                    "msg", "Swal.fire({ text: 'Tu carrito está vacío', icon: 'info', confirmButtonText: 'Aceptar' });", true);
                return;
            }

            // Crear orden, descontar stock y guardar en BD
            OrdenNegocio ordenNegocio = new OrdenNegocio();
            ordenNegocio.CrearOrden(usuario.IdUsuario, carrito.Items);

            // Paso 5: marcar carrito como finalizado
            carrito.Estado = "Finalizado";
            //despues de enviar el correo se pone el carrito como null

            // Paso 6: mostrar solo SweetAlert de éxito
            ScriptManager.RegisterStartupScript(this, GetType(),
                Guid.NewGuid().ToString(),
                "Swal.fire({ title: 'Compra realizada', text: 'Tu pedido fue registrado correctamente', icon: 'success', confirmButtonText: 'Aceptar' });",
                true);


            //Paso 7: envío de correo
          Dominio.Carrito carro =  ordenNegocio.BuscarCarritoPorId(carrito.IdCarrito);
           List <ElementoCarrito> ListaDeelementosDelCarro = new List<ElementoCarrito> ();
            ListaDeelementosDelCarro = ordenNegocio.ListarElementosPorCarrito(carrito.IdCarrito);


            // Construir detalle de la compra
            StringBuilder detalleCompra = new StringBuilder();
            detalleCompra.AppendLine("¡Gracias por comprar en ARSUMO!\n");
            detalleCompra.AppendLine("=== Detalle de la compra ===");
            decimal TOTAL = 0;
            ArticuloNegocio articuloNegocio = new ArticuloNegocio();
            foreach (var item in carrito.Items)
            {
                detalleCompra.AppendLine(
                   $"Artículo: {articuloNegocio.ListarPorIDArticulo(Convert.ToInt32(item.IdArticulo)).Nombre} | " +
                   $"Cantidad: {item.Cantidad} |" +
                   $" Precio: {item.PrecioUnitario.ToString("C", new CultureInfo("es-AR"))} |" +
                   $" Subtotal: {(item.Cantidad * item.PrecioUnitario).ToString("C", new CultureInfo("es-AR"))}"
                );
                   TOTAL +=  item.Cantidad * item.PrecioUnitario;
            }
            detalleCompra.AppendLine("============================");
            detalleCompra.AppendLine($"TOTAL: {TOTAL.ToString("C", new CultureInfo("es-AR"))}\n");
            detalleCompra.AppendLine("Estamos esperando la confirmación del pago, que puede demorar hasta 72 hs hábiles " +
                "(esto puede variar dependiendo del medio de pago elegido).\n\n" +
                "Seguí el estado de envío de tu pedido desde el siguiente enlace:\n" +
                "https://www.correoargentino.com.ar/formularios/e-commerce \n\n" +
                "Además de nuestra web puedes encontrarnos en Valparaíso 1051 - Ing. Pablo Nogués - Malvinas Argentinas - Buenos Aires\n"
                );
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("franbueno681@gmail.com"); // tu dirección Gmail
            mail.To.Add(usuario.Mail); // destinatario real
            mail.Subject = "Detalle de compra";

// Cuerpo del correo con datos de contacto + detalle de compra
mail.Body =
    "Cliente: " + usuario.Nombre + " " + usuario.Apellido + "\n" +

    detalleCompra.ToString();
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.Credentials = new NetworkCredential("franbueno681@gmail.com", "udhp zgut mjcp kngu");
            smtp.EnableSsl = true;


            Session["Carrito"] = null;
           

            try
            {
                smtp.Send(mail);

                // Enviar mensaje a la consola del navegador
                ClientScript.RegisterStartupScript(this.GetType(), "correoOk",
                    "console.log('Correo enviado correctamente.');", true);
                Response.Redirect("FinDeCompra.aspx");
            }
            catch (Exception ex)
            {
                // Enviar mensaje de error a la consola del navegador
                ClientScript.RegisterStartupScript(this.GetType(), "correoError",
                    $"console.error('Error al enviar: {ex.Message}');", true);
            }


        }





    }
}