using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;
using Negocio;

namespace WebAppEcommerce
{
    public partial class GestionCategoria : System.Web.UI.Page
    {
        public bool   ConfirmarEliminacion { get; set; }
      
        protected void Page_Load(object sender, EventArgs e)
        {
           
            string id = Request.QueryString["id"] != null ? Request.QueryString["id"].ToString() : "";
            if (id != "" )
            {
                ConfirmarEliminacion = false;
                //precarga de datos
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                Categoria seleccionado = (categoriaNegocio.listar(id))[0];
                //List<Categoria> lista = categoriaNegocio.listar(id);
                txtId.Text = seleccionado.IdCategoria.ToString();
                txtNombreCategoria.Text = seleccionado.Nombre;
            
            }
            

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Categoria nuevo = new Categoria();
                CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
                
                nuevo.Nombre = txtNombreCategoria.Text;
                nuevo.Estado = true;
               
                categoriaNegocio.agregar(nuevo);
                Response.Redirect("ListaCategorias.aspx");

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
                //List<Categoria> lista = categoriaNegocio.listar(id);
                Categoria seleccionado = new Categoria();

                    //modificacion de datos
                    seleccionado.Nombre=txtNombreCategoria.Text;

                    if (Request.QueryString["id"] != null) 
                    categoriaNegocio.modificar(seleccionado);
                    Response.Redirect("ListaCategorias.aspx");

            }
            catch (Exception)
            {

                throw;
            }

        }

        protected void btnEliminar_Click(object sender, EventArgs e)
        {
            ConfirmarEliminacion = true;
        }

        protected void btnConfirmacion_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkConfimaEliminacion.Checked) { 
                CategoriaNegocio  negocio = new CategoriaNegocio();
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