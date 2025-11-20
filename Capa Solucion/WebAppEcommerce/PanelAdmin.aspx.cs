using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dominio;

namespace WebAppEcommerce
{
    public partial class PanelAdmin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario user = (Usuario)Session["usuario"];

                if (user == null || user.TipoUsuario != 1) // 1 = ADMINISTRADOR
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "noAuth",
                        "Swal.fire('Acceso denegado','Solo administradores pueden acceder al panel.','error')" +
                        ".then(() => { window.location='Default.aspx'; });", true);
                }
            }
        }
    }
}
