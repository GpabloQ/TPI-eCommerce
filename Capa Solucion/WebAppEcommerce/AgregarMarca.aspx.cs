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
                if (chkConfirma.Checked)
                {
                    MarcaNegocio negocio = new MarcaNegocio();
                    int id = int.Parse(txtId.Text);

                    negocio.eliminacionLogica(new Marca
                    {
                        IdMarca = id,
                        Nombre = txtNombre.Text,
                        Estado = false
                    });

                    Response.Redirect("ListarMarcas.aspx");
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