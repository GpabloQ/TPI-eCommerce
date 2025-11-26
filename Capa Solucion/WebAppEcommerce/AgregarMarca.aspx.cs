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
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtId.Enabled = false;
            
            try
            {
                //Configuración si estamos modificando
                if (!IsPostBack)
                {
                    string id = Request.QueryString["id"];

                    if (!string.IsNullOrEmpty(id))
                    {
                        // Modo modificar
                        titulo.InnerText = "Modificar Marca";

                        MarcaNegocio negocio = new MarcaNegocio();
                        Marca seleccionada = negocio.listar(id)[0];

                        txtId.Text = seleccionada.IdMarca.ToString();
                        txtId.Visible = true;
                        txtNombre.Text = seleccionada.Nombre;

                        btnEliminar.Visible = true;
                    }
                    else
                    {
                        // Modo agregar
                        titulo.InnerText = "Agregar Marca";
                        btnEliminar.Visible = false;
                    }
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
                MarcaNegocio negocio = new MarcaNegocio();
                Marca nuevo = new Marca();

                if (negocio.ExisteNombre(txtNombre.Text.Trim()))
                {
                    lblError.Text = "La marca ya existe.";
                    lblError.Visible = true;  
                    return;
                }


                nuevo.Nombre = txtNombre.Text;
                nuevo.Estado = true;

                if (!string.IsNullOrEmpty(txtId.Text))
                {
                    nuevo.IdMarca = int.Parse(txtId.Text);
                    negocio.modificar(nuevo);
                }
                else
                {
                    negocio.agregar(nuevo);
                }

                Response.Redirect("ListarMarcas.aspx");

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
            panelConfirmacion.Visible = true;
        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                MarcaNegocio negocio = new MarcaNegocio();
                negocio.eliminar(long.Parse(txtId.Text));
                Response.Redirect("Marcas.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "errorEliminar",
                    $"Swal.fire('Error', '{ex.Message}', 'error');",
                    true
                );
            }
        }



    }
}