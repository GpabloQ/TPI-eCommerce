using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEcommerce
{
    public partial class Gestion : System.Web.UI.Page
    {
        public List<Articulo> listaArticulo { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            // Validación de acceso
            Usuario user = Session["Usuario"] as Usuario;

            // Si NO está logueado CHAU
            if (user == null)
            {
                Response.Redirect("Signin.aspx");
                return;
            }

            // Si NO es ADMIN (TipoUsuario = 1) CHAU
            if (user.TipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Si es admin, ADENTRO
            if (!IsPostBack)
            {
                var master = (SiteMaster)Master;
                master.SetBreadcrumb(new List<(string, string)>
                {
                    ("Inicio", "Default.aspx"),
                    ("Panel Administrativo", "PanelAdmin.aspx"),
                    ("Gestion de Productos", null),
                });

                cargarArticulos();
            }
        }


        private void cargarArticulos()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                listaArticulo = negocio.Listar2();   // Obtiene la lista completa
                dgvProductos.DataSource = listaArticulo;
                dgvProductos.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // Maneja botones DETALLE / MODIFICAR / ELIMINAR
        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            int idArticulo = Convert.ToInt32(e.CommandArgument);

            ArticuloNegocio negocio = new ArticuloNegocio();

            switch (e.CommandName)
            {
                case "Detalle":
                    Response.Redirect("DetalleProducto.aspx?id=" + idArticulo);
                    break;

                case "Modificar":
                    Response.Redirect("FormularioProducto.aspx?id=" + idArticulo);
                    break;

                case "Eliminar":
                    negocio.EliminarArticulo(idArticulo);
                    cargarArticulos();
                    lblMensaje.Text = "ARTICULO ELIMINADO CORRECTAMENTE";
                    break;

                case "SumarStock":
                    negocio.ModificarStock(idArticulo, +1);
                    cargarArticulos();
                    break;

                case "RestarStock":
                    negocio.ModificarStock(idArticulo, -1);
                    cargarArticulos();
                    break;
            }
        }


        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("FormularioProducto.aspx");
        }
    }
}
