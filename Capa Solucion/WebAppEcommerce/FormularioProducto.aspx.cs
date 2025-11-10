using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;

namespace WebAppEcommerce
{
    public partial class FormularioProducto : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarListas();

                string id = Request.QueryString["id"];
                if (id != null)
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();
                    Articulo seleccionado = negocio.listar2().Find(x => x.id == int.Parse(id));
                    Session.Add("articuloSeleccionado", seleccionado);
                    cargarFormulario(seleccionado);
                    lblModo.Text = "EDITAR PRODUCTO";
                }
                else
                {
                    lblModo.Text = "AGREGAR NUEVO PRODUCTO";
                }
            }
        }

        private void cargarListas()
        {
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();

            ddlMarca.DataSource = marcaNegocio.listar();
            ddlMarca.DataTextField = "Nombre";   
            ddlMarca.DataValueField = "Id";      
            ddlMarca.DataBind();

            ddlCategoria.DataSource = categoriaNegocio.listar();
            ddlCategoria.DataTextField = "Nombre"; 
            ddlCategoria.DataValueField = "Id";    
            ddlCategoria.DataBind();
        }

        private void cargarFormulario(Articulo art)
        {
            txtCodigo.Text = art.codigoArticulo;
            txtNombre.Text = art.nombre;
            txtDescripcion.Text = art.descripcion;
            txtPrecio.Text = art.precio.ToString("N2");
            txtUrlImagen.Text = art.UrlImagen;

            ddlMarca.SelectedValue = art.Marca.Id.ToString();
            ddlCategoria.SelectedValue = art.tipo.Id.ToString();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = new Articulo();

            try
            {
                // Validaciones 
                if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text) ||
                    string.IsNullOrEmpty(txtPrecio.Text) || ddlMarca.SelectedValue == "" || ddlCategoria.SelectedValue == "")
                {
                    
                    lblError.Text = "Todos los campos obligatorios deben completarse.";
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
                {
                    lblError.Text = "Precio debe ser un número positivo.";
                    return;
                }

                articulo.codigoArticulo = txtCodigo.Text;
                articulo.nombre = txtNombre.Text;
                articulo.descripcion = txtDescripcion.Text;
                articulo.precio = precio;
                articulo.UrlImagen = txtUrlImagen.Text;

                
                articulo.Marca = new Marca { Id = int.Parse(ddlMarca.SelectedValue) };
                articulo.tipo = new Categoria { Id = int.Parse(ddlCategoria.SelectedValue) };

                if (Request.QueryString["id"] != null)
                {
                    articulo.id = int.Parse(Request.QueryString["id"]);
                    negocio.modificarProducto(articulo);
                }
                else
                {
                    negocio.agregarArticulo(articulo);
                }

                Response.Redirect("Gestion.aspx", false);
            }
            catch (Exception ex)
            {
                Session.Add("error", "Error al guardar el producto. Intente nuevamente.");
                Response.Redirect("Error.aspx", false);
            }
        }


        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Gestion.aspx", false);
        }
    }
}
