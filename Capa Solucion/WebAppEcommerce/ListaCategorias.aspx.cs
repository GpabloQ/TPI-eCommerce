using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Negocio;
using Dominio;

namespace WebAppEcommerce
{
    public partial class ListaCategorias : System.Web.UI.Page
    {
 
        protected void Page_Load(object sender, EventArgs e)
        {   
            

            Usuario user = Session["Usuario"] as Usuario;

            if (user == null)
            {
                Response.Redirect("Signin.aspx");
                return;
            }

            if (user.TipoUsuario != 1)
            {
                Response.Redirect("Default.aspx");
                return;
            }

            // Si es admin, cargamos la grilla
            if (!IsPostBack)
            {
                var master = (SiteMaster)Master;
                master.SetBreadcrumb(new List<(string, string)>
                {
                    ("Inicio", "Default.aspx"),
                    ("Panel Administrativo", "PanelAdmin.aspx"),
                    ("Lista Categoria", null),
                });


                CategoriaNegocio negocio = new CategoriaNegocio();
                dgvCategorias.DataSource = negocio.listar();
                dgvCategorias.DataBind();
            }
        }

        protected void dgvCategorias_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvCategorias.SelectedDataKey.Value.ToString();
            Response.Redirect("GestionCategoria.aspx?id=" + id);
        }

        protected void dgvCategorias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvCategorias.PageIndex = e.NewPageIndex;

            CategoriaNegocio negocio = new CategoriaNegocio();
            dgvCategorias.DataSource = negocio.listar();
            dgvCategorias.DataBind();
        }

        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionCategoria.aspx");
        }

        protected void dgvCategorias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Eliminar")
            {
                long id = Convert.ToInt64(e.CommandArgument);
                CategoriaNegocio negocio = new CategoriaNegocio();

                try
                {
                    negocio.Eliminar(id);   // ← acá se lanza la excepción si tiene artículos
                    dgvCategorias.DataSource = negocio.listar();
                    dgvCategorias.DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(
                        this,
                        this.GetType(),
                        "errorEliminarCategoria",
                        $"Swal.fire('Error', '{ex.Message}', 'error');",
                        true
                    );
                }
            }
        }

    }
}