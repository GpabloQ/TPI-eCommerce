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
                if (!string.IsNullOrEmpty(id))
                {
                    ArticuloNegocio negocio = new ArticuloNegocio();

                    // Busca el artículo por ID 
                    Articulo seleccionado = negocio.Listar2()
                        .Find(x => x.IdArticulo == long.Parse(id));

                    if (seleccionado != null)
                    {
                        Session["articuloSeleccionado"] = seleccionado;
                        CargarFormulario(seleccionado);
                        lblModo.Text = "EDITAR PRODUCTO";
                    }
                    else
                    {
                        lblError.Text = "No se encontró el artículo especificado.";
                    }
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
            ddlMarca.DataValueField = "IdMarca";      
            ddlMarca.DataBind();

            ddlCategoria.DataSource = categoriaNegocio.listar();
            ddlCategoria.DataTextField = "Nombre"; 
            ddlCategoria.DataValueField = "IdCategoria";    
            ddlCategoria.DataBind();
        }

        private void CargarFormulario(Articulo art)
        {
            txtCodigo.Text = art.Codigo;
            txtNombre.Text = art.Nombre;
            txtDescripcion.Text = art.Descripcion;
            txtPrecio.Text = art.Precio.ToString("N2");
            txtUrlImagen.Text = art.UrlImagen;

            ddlMarca.SelectedValue = art.Marca.IdMarca.ToString();
            ddlCategoria.SelectedValue = art.Categoria.IdCategoria.ToString();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo articulo = new Articulo();

            try
            {
                // Validaciones básicas
                if (string.IsNullOrEmpty(txtCodigo.Text) || string.IsNullOrEmpty(txtNombre.Text) ||
                    string.IsNullOrEmpty(txtPrecio.Text) || string.IsNullOrEmpty(ddlMarca.SelectedValue) ||
                    string.IsNullOrEmpty(ddlCategoria.SelectedValue))
                {
                    lblError.Text = "Todos los campos obligatorios deben completarse.";
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
                {
                    lblError.Text = "El precio debe ser un número positivo.";
                    return;
                }

                // Se asignan valores al objeto Articulo
                articulo.Codigo = txtCodigo.Text.Trim();
                articulo.Nombre = txtNombre.Text.Trim();
                articulo.Descripcion = txtDescripcion.Text.Trim();
                articulo.Precio = precio;
                articulo.UrlImagen = txtUrlImagen.Text.Trim();
                articulo.Estado = true;
                articulo.Cantidad = 1; 

                articulo.Marca = new Marca { IdMarca = int.Parse(ddlMarca.SelectedValue) };
                articulo.Categoria = new Categoria { IdCategoria = int.Parse(ddlCategoria.SelectedValue) };

                string id = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(id))
                {
                    // Edición
                    articulo.IdArticulo = int.Parse(id);

                    string urlVieja = "";
                    if (Session["articuloSeleccionado"] != null)
                    {
                        Articulo seleccionado = (Articulo)Session["articuloSeleccionado"];
                        urlVieja = seleccionado.UrlImagen;
                    }

                    negocio.ModificarArticulo(articulo, urlVieja, articulo.UrlImagen);
                }
                else
                {
                    // Alta
                    negocio.AgregarArticulo(articulo);
                }

                Response.Redirect("Gestion.aspx", false);
            }
            catch (Exception ex)
            {
                lblError.Text = "Error técnico: " + ex.Message;
            }
        }




        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("Gestion.aspx", false);
        }
    }
}
