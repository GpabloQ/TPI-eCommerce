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
                var master = (SiteMaster)Master;
                master.SetBreadcrumb(new List<(string, string)>
                {
                    ("Inicio", "Default.aspx"),
                    ("Tienda", null)
                });

                CargarCategoria();
                CargarMarca();
                CargarArticulos();
            }

            btnLimpiar.Visible = Filtros();


        }

        private void CargarArticulos()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                List<Articulo> lista = negocio.Listar2();


                // FILTRAR POR MARCA

                if (Request.QueryString["marca"] != null)
                {
                    int idMarca = int.Parse(Request.QueryString["marca"]);
                    lista = lista.Where(x => x.Marca.IdMarca == idMarca).ToList();
                }

                // FILTRAR POR CATEGORIA

                if (Request.QueryString["categoria"] != null)
                {
                    int idCategoria = int.Parse(Request.QueryString["categoria"]);
                    lista = lista.Where(x => x.Categoria.IdCategoria == idCategoria).ToList();
                }


                // FILTRAR POR BÚSQUEDA
                if (!string.IsNullOrEmpty(txtBuscar.Value))
                {
                    string txt = txtBuscar.Value.ToLower();

                    lista = lista.Where(a =>
                        (a.Nombre != null && a.Nombre.ToLower().Contains(txt)) ||
                        (a.Marca != null && a.Marca.Nombre != null && a.Marca.Nombre.ToLower().Contains(txt)) ||
                        (a.Categoria != null && a.Categoria.Nombre != null && a.Categoria.Nombre.ToLower().Contains(txt))
                    ).ToList();
                }


                // ORDENAR POR PRECIO
                if (!string.IsNullOrEmpty(ddlOrdenPrecio.SelectedValue))
                {
                    if (ddlOrdenPrecio.SelectedValue == "ASC")
                        lista = lista.OrderBy(a => a.Precio).ToList();
                    else
                        lista = lista.OrderByDescending(a => a.Precio).ToList();
                }


                // PROCESAR IMÁGENES
                foreach (var art in lista)
                {
                    if (string.IsNullOrEmpty(art.UrlImagen) &&
                        art.ListaUrls != null &&
                        art.ListaUrls.Count > 0)
                    {
                        art.UrlImagen = art.ListaUrls[0];
                        art.ListaUrls.RemoveAt(0);
                    }
                }



                rptArticulos.DataSource = lista;
                rptArticulos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void CargarCategoria()
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                var lista = negocio.listar();
                rptCategorias.DataSource = lista;
                rptCategorias.DataBind();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        protected void repCategorias_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "FiltrarCategoria")
            {
                int idCat = int.Parse(e.CommandArgument.ToString());
                ArticuloNegocio negocio = new ArticuloNegocio();

                rptArticulos.DataSource = negocio.Listar2()
                    .Where(x => x.Categoria.IdCategoria == idCat)
                    .ToList();

                rptArticulos.DataBind();
            }
        }



        private void CargarMarca()
        {
            try
            {
                MarcaNegocio Negocio = new MarcaNegocio();
                var lista = Negocio.ListarConConteo();
                rptMarcas.DataSource = lista;
                rptMarcas.DataBind();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        protected void repMarcas_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "FiltrarMarca")
            {
                int idMarca = int.Parse(e.CommandArgument.ToString());
                ArticuloNegocio negocio = new ArticuloNegocio();

                rptArticulos.DataSource = negocio.Listar2()
                    .Where(x => x.Marca.IdMarca == idMarca)
                    .ToList();

                rptArticulos.DataBind();
            }
        }

        private void FiltrarPorCategoria()
        {
            int idCat = int.Parse(Request.QueryString["categoria"]);
            ArticuloNegocio negocio = new ArticuloNegocio();

            var lista = negocio.Listar2().Where(x => x.Categoria.IdCategoria == idCat).ToList();

            rptArticulos.DataSource = lista;
            rptArticulos.DataBind();
        }

        private void FiltrarPorMarca()
        {
            int idMarca = int.Parse(Request.QueryString["marca"]);
            ArticuloNegocio negocio = new ArticuloNegocio();

            var lista = negocio.Listar2().Where(x => x.Marca.IdMarca == idMarca).ToList();

            rptArticulos.DataSource = lista;
            rptArticulos.DataBind();
        }

        private bool Filtros()
        {
            bool hayBusqueda = !string.IsNullOrWhiteSpace(txtBuscar.Value);
            bool hayOrden = !string.IsNullOrWhiteSpace(ddlOrdenPrecio.SelectedValue);
            bool hayMarca = Request.QueryString["marca"] != null;
            bool hayCategoria = Request.QueryString["categoria"] != null;

            return hayBusqueda || hayOrden || hayMarca || hayCategoria;
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
                    if (usuario == null)
                    {
                        string script = @"Swal.fire({
        title: 'Inicia sesión',
        text: 'Para agregar el artículo al carrito debes iniciar sesión',
        icon: 'info',
        position: 'top',
        showCancelButton: true,
        confirmButtonText: 'Aceptar',
        cancelButtonText: 'Cancelar'
    }).then((result) => {
        if (result.isConfirmed) {
            window.location.href = 'Signin.aspx';
        } else if (result.dismiss === Swal.DismissReason.cancel) {
            console.log('El usuario canceló');
        }
    });";

                        ScriptManager.RegisterStartupScript(this, GetType(), "msg", script, true);
                        return;
                    }
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

                    // Buscar si ya existe el artículo en el carrito
                    var existente = carrito.Items.FirstOrDefault(x => x.IdArticulo == idArticulo);
                    if (existente != null)
                    {
                        // Si ya existe, solo aumento la cantidad
                        existente.Cantidad++;
                    }
                    else
                    {
                        // Crear nuevo elemento

                        ElementoCarrito item = new ElementoCarrito
                        {
                            IdArticulo = idArticulo,
                            Cantidad = 1, // o lo que seleccione el usuario
                            PrecioUnitario = artnegocio.ObtenerPrecioArticulo(idArticulo)
                        };
                        if (artnegocio.ListarPorIDArticulo(idArticulo).Cantidad > 0)
                        {
                            carrito.Items.Add(item);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(),
                 "msg", "Swal.fire({ text: 'No hay stock de este producto.', position: 'top', timer: 2000, showConfirmButton: false });", true);

                        }
                    }
                    // Guardar carrito en sesión
                    Session["Carrito"] = carrito;

                    // Podés redirigir al carrito o mostrar un mensaje
                    //   Response.Redirect("CarritoPage.aspx");
                    ScriptManager.RegisterStartupScript(this, GetType(),
         "msg", "Swal.fire({ text: 'Producto agregado', position: 'top', timer: 1000, showConfirmButton: false });", true);

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

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarArticulos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Value = "";
            ddlOrdenPrecio.SelectedIndex = 0;

            Response.Redirect("Tienda.aspx");
        }


        protected void ddlOrdenPrecio_SelectedIndexChanged(object sender, EventArgs e)
        {
            CargarArticulos();
        }


        /*
        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            
            Usuario usuario = Session["Usuario"] as Usuario;
            if(usuario == null)
            {
                Response.Redirect("Signin.aspx");
                return;
            }

            ArticuloNegocio artnegocio  = new ArticuloNegocio();
                      
            int idArticulo = Convert.ToInt32(Request.QueryString["id"]);
            System.Diagnostics.Debug.WriteLine("ID recibido: " + idArticulo);



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
        */




    }
}