using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEcommerce
{
    public partial class GestionCategoria : System.Web.UI.Page
    {
      
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                string id = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(id))
                {
                    titulo.InnerText = "Modificar Categoría";

                    CategoriaNegocio negocio = new CategoriaNegocio();
                    Categoria seleccionada = negocio.listar(id)[0];

                    txtId.Text = seleccionada.IdCategoria.ToString();
                    txtId.Visible = true;
                    txtNombreCategoria.Text = seleccionada.Nombre;

                    btnEliminar.Visible = true;
                }
                else
                {
                    titulo.InnerText = "Agregar Categoría";
                    btnEliminar.Visible = false;
                }
            }
        }



        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                CategoriaNegocio negocio = new CategoriaNegocio();
                Categoria nuevo = new Categoria();

                nuevo.Nombre = txtNombreCategoria.Text;
                nuevo.Estado = true;

                if (!string.IsNullOrEmpty(txtId.Text))
                {
                    nuevo.IdCategoria = int.Parse(txtId.Text);
                    negocio.modificar(nuevo);
                }
                else
                {
                    negocio.agregar(nuevo);
                }

                Response.Redirect("ListaCategorias.aspx"); 

                return;
            }

            catch (Exception)
            {

                throw;
            }
    }

        

        protected void btnModificar_Click(object sender, EventArgs e)
        {

            try
            {
                    CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                    Categoria seleccionado = new Categoria();
                    

                    //modificacion de datos
                    seleccionado.Nombre=txtNombreCategoria.Text;
                    seleccionado.Estado = true;

                    if (Request.QueryString["id"] != null) {
                    
                        seleccionado.IdCategoria = int.Parse(Request.QueryString["id"].ToString());
                        categoriaNegocio.modificar(seleccionado);
                        Response.Redirect("ListaCategorias.aspx");
                    }

                

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            panelConfirmacion.Visible = true;
        }

        protected void btnConfirmacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfirmaEliminacion.Checked)
                {
                    CategoriaNegocio negocio = new CategoriaNegocio();
                    negocio.eliminar(int.Parse(txtId.Text));
                    Response.Redirect("ListaCategorias.aspx");
                }

            }
            catch (Exception ex)
            {

                Session.Add("error", ex);
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ListaCategorias.aspx");
        }
    }
}