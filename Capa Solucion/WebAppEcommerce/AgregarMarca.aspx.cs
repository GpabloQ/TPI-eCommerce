using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEcommerce
{
    public partial class AgregarMarca : System.Web.UI.Page
    {
        public bool ConfirmaEliminacion { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtId.Enabled = false;
            ConfirmaEliminacion = false;

            try
            {
                //Configuración si estamos modificando
                if (Request.QueryString["id"] != null && !IsPostBack)
                {
                    lblTitulo.Text = "Modificar Marca";
                    MarcaNegocio negocio = new MarcaNegocio();
                    List<Marca> lista = negocio.listar(Request.QueryString["id"].ToString());
                    Marca seleccionada = lista[0];

                    //pre cargar todos los campos...
                    txtNombre.Text = seleccionada.Nombre;

                }
                else
                {
                    lblTitulo.Text = "Agregar Marca";
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                {
                    
                    // El TextBox tiene contenido válido
                Marca nuevo = new Marca();
                MarcaNegocio negocio = new MarcaNegocio();


                nuevo.Nombre = txtNombre.Text;
                nuevo.Estado = true;

                    if (Request.QueryString["id"] != null)
                    {
                        nuevo.IdMarca = long.Parse(Request.QueryString["id"].ToString());
                        negocio.modificar(nuevo);
                    }
                    else
                    {
                        negocio.agregar(nuevo);
                    }


                Response.Redirect("ListarMarcas.aspx", false);
                }
                else
                {
                    return;
                    // Está vacío o solo tiene espacios
                }

            }
            catch (Exception ex)
            {
                Session.Add("error", ex);
                throw;
                //redirreccion a pantalla de error
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListarMarcas.aspx", false);
        }

        protected void btnEliminarMarca_Click(object sender, EventArgs e)
        {
            ConfirmaEliminacion = true;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ChkConfirmaEliminacion.Checked)
                {
                MarcaNegocio negocio = new MarcaNegocio();
                Marca modificar = new Marca();
                    long id = long.Parse(Request.QueryString["id"]);
                //negocio.eliminacionFisica(id);


                    if (!string.IsNullOrWhiteSpace(txtNombre.Text))
                    { 
                        // El TextBox tiene contenido válido

                        modificar.Nombre = txtNombre.Text;
                        modificar.Estado = false;

                        if (Request.QueryString["id"] != null)
                        {
                            modificar.IdMarca = long.Parse(Request.QueryString["id"].ToString());
                            negocio.eliminacionLogica(modificar);
                            Response.Redirect("ListarMarcas.aspx", false);
                        }
                    }
                }
            }
            catch (Exception ex )
            {
                Session.Add("error", ex);
                throw;
            }
        }



    }
}