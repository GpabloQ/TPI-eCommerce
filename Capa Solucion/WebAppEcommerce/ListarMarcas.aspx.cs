using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace WebAppEcommerce
{
    public partial class ListarMarcas : System.Web.UI.Page
    {
    
        protected void Page_Load(object sender, EventArgs e)
        {
            // Validación de acceso (igual que en GestionUsuarios)
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
                MarcaNegocio negocio = new MarcaNegocio();
                dgvMarcas.DataSource = negocio.listar();
                dgvMarcas.DataBind();
            }
        }

        protected void dgvMarcas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvMarcas.PageIndex = e.NewPageIndex;

            MarcaNegocio negocio = new MarcaNegocio();
            dgvMarcas.DataSource = negocio.listar();
            dgvMarcas.DataBind();
        }

        protected void dgvMarcas_SelectedIndexChanged(object sender, EventArgs e)
        {
            string id = dgvMarcas.SelectedDataKey.Value.ToString();
            Response.Redirect("AgregarMarca.aspx?id=" + id);
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            Response.Redirect("AgregarMarca.aspx", false);
        }

        protected void dgvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = Convert.ToInt32(e.CommandArgument);

            MarcaNegocio negocio = new MarcaNegocio();

            try
            {
                negocio.eliminar(id);  // ← ✔ Usa la validación correcta
                dgvMarcas.DataSource = negocio.listar();
                dgvMarcas.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(
                    this,
                    this.GetType(),
                    "errorEliminarGrilla",
                    $"Swal.fire('Error','{ex.Message}','error');",
                    true
                );
            }
        }



    }
}