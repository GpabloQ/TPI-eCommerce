using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;


namespace WebAppEcommerce
{
    public partial class Tienda : System.Web.UI.Page
    {
        public List<Articulo> listaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string idCategoriaStr = Request.QueryString["id"];
                int idCategoria = Convert.ToInt32(idCategoriaStr);

                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.ListarArticulosPorCategoria(idCategoria);

                // Validar imágenes de cada artículo
                foreach (var art in lista)
                {
                    if (string.IsNullOrEmpty(art.UrlImagen) && art.ListaUrls != null && art.ListaUrls.Count > 0)
                    {
                        art.UrlImagen = art.ListaUrls[0];
                        art.ListaUrls.RemoveAt(0); // opcional, para no duplicar
                    }
                }

                rptArticulos.DataSource = lista;
                rptArticulos.DataBind();
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
            /*
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
            */

            if (e.CommandName == "Agregar")
            {
                int idArticulo;
                if (int.TryParse(e.CommandArgument.ToString(), out idArticulo))
                {
                    // Recuperar usuario de sesión
                    Usuario usuario = Session["Usuario"] as Usuario;

                    ArticuloNegocio artnegocio = new ArticuloNegocio();

                    // Recuperar carrito de sesión
                    Dominio.Carrito carrito = Session["Carrito"] as Dominio.Carrito;
                    if (carrito == null)
                    {
                        carrito = new Dominio.Carrito
                        {
                            IdUsuario = usuario.IdUsuario,
                            FechaCreacion = DateTime.Now,
                            Estado = "Activo",
                            Items = new List<ElementoCarrito>()
                        };
                    }

                    // Crear nuevo elemento
                    ElementoCarrito item = new ElementoCarrito
                    {
                        IdArticulo = idArticulo,
                        Cantidad = 1, // o lo que seleccione el usuario
                        PrecioUnitario = artnegocio.ObtenerPrecioArticulo(idArticulo)
                    };

                    carrito.Items.Add(item);

                    // Guardar carrito en sesión
                    Session["Carrito"] = carrito;

                    // Podés redirigir al carrito o mostrar un mensaje
                    Response.Redirect("CarritoPage.aspx");
                }
                else
                {
                    // Si el argumento no era numérico, manejar el error
                    // Ejemplo: mostrar un mensaje o ignorar
                }
            }
        }

        protected void btnDetalleArticulo_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string idArticulo = btn.CommandArgument;

            Response.Redirect("DetalleProducto.aspx?id=" + idArticulo, false);
        }


        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] as Usuario;

            ArticuloNegocio artnegocio  = new ArticuloNegocio();

            int idArticulo = Convert.ToInt32(Request.QueryString["id"]);
            int cantidad = 1; // o lo que seleccione el usuario

            // Recuperar carrito de sesión
            Dominio.Carrito carrito = Session["Carrito"] as Dominio.Carrito;
            if (carrito == null)
            {
                carrito = new Dominio.Carrito
                {
                    IdUsuario = usuario.IdUsuario, 
                    FechaCreacion = DateTime.Now,
                    Estado = "Activo",
                    Items = new List<ElementoCarrito>()
                };
            }

            // Crear elemento
            ElementoCarrito item = new ElementoCarrito
            {
                IdArticulo = idArticulo,
                Cantidad = cantidad,
                PrecioUnitario = artnegocio.ObtenerPrecioArticulo(idArticulo) 
            };

            carrito.Items.Add(item);

            // Guardar en sesión
            Session["Carrito"] = carrito;
        }


    }
}