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
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertCampos",
                        "Swal.fire('Campos incompletos', 'Debe completar todos los campos obligatorios.', 'warning');", true);
                    return;
                }

                if (!decimal.TryParse(txtPrecio.Text, out decimal precio) || precio <= 0)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertPrecio",
                        "Swal.fire('Precio inválido', 'El precio debe ser un número positivo.', 'warning');", true);
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

                    // SweetAlert éxito edición
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertModificado",
                        "Swal.fire('Articulo modificado', 'Los cambios se guardaron correctamente.', 'success')" +
                        ".then(() => { window.location = 'GestionProductos.aspx'; });", true);
                }
                else
                {
                    // Alta
                    negocio.AgregarArticulo(articulo);

                    // SweetAlert éxito alta
                    ScriptManager.RegisterStartupScript(this, GetType(), "alertAgregado",
                        "Swal.fire('Articulo agregado', 'El articulo fue creado correctamente.', 'success')" +
                        ".then(() => { window.location = 'GestionProductos.aspx'; });", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alertError",
                    $"Swal.fire('Error técnico', '{ex.Message}', 'error');", true);
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionProductos.aspx", false);
        }

        protected void btnVerImagen_Click(object sender, EventArgs e)
        {
            string url = txtUrlImagen.Text.Trim();

            if (string.IsNullOrEmpty(url))
            {
                lblError.Text = "INGRESE UNA URL PARA PREVISUALIZAR.";
                imgPreview.Style["display"] = "none";
                return;
            }

            try
            {
                imgPreview.ImageUrl = url;
                imgPreview.Style["display"] = "block";
                lblError.Text = "";
            }
            catch
            {
                imgPreview.Style["display"] = "none";
                lblError.Text = "LA URL NO ES VALIDA O LA IMAGEN NO SE CARGÓ.";
            }
        }

    }
}
