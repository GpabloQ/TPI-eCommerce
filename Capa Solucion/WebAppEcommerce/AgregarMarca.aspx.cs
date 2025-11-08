using Dominio;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppEcommerce
{
    public partial class AgregarMarca : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnAgregar_Click(object sender, EventArgs e)
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

                negocio.agregar(nuevo);
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
    }
}