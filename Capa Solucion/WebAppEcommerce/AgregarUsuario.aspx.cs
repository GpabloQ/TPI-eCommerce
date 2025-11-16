using Dominio;
using Negocio;
using System;

namespace WebAppEcommerce
{
    public partial class AgregarUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    cargarUsuario(id);
                    lblTitulo.Text = "Modificar Usuario";
                    divPass.Visible = false;
                }
                else
                {
                    lblTitulo.Text = "Agregar Usuario";
                    divPass.Visible = true;
                }
            }
        }

        private void cargarUsuario(int id)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();
            Usuario u = negocio.buscarPorId(id);

            txtNombre.Text = u.Nombre;
            txtApellido.Text = u.Apellido;
            txtMail.Text = u.Mail;
            txtTelefono.Text = u.Telefono;
            txtDNI.Text = u.DNI;

            if (u.FechaNacimiento != DateTime.MinValue)
                txtFechaNacimiento.Text = u.FechaNacimiento.ToString("yyyy-MM-dd");

            
            if (ddlTipoUsuario.Items.FindByValue(u.TipoUsuario.ToString()) != null)
                ddlTipoUsuario.SelectedValue = u.TipoUsuario.ToString();
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario u = new Usuario();

                if (Request.QueryString["id"] != null)
                    u.IdUsuario = int.Parse(Request.QueryString["id"]);

                // Validaciones básicas
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtApellido.Text) ||
                    string.IsNullOrWhiteSpace(txtMail.Text))
                {
                    lblMensaje.Text = "Los campos Nombre, Apellido y Mail son obligatorios.";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                    return;
                }

                // Validar mail duplicado (solo en alta)
                if (u.IdUsuario == 0 && negocio.ExisteMail(txtMail.Text))
                {
                    lblMensaje.Text = "El mail ya está registrado.";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                    return;
                }

                // Validar fecha
                DateTime fecha;
                if (!DateTime.TryParse(txtFechaNacimiento.Text, out fecha))
                {
                    lblMensaje.Text = "Debe ingresar una fecha válida.";
                    lblMensaje.CssClass = "alert alert-danger";
                    lblMensaje.Visible = true;
                    return;
                }

                // Seteo de propiedades
                u.Nombre = txtNombre.Text;
                u.Apellido = txtApellido.Text;
                u.Mail = txtMail.Text;
                u.Telefono = txtTelefono.Text;
                u.DNI = txtDNI.Text;
                u.FechaNacimiento = fecha;
                u.TipoUsuario = int.Parse(ddlTipoUsuario.SelectedValue);
                u.Estado = true;

                if (u.IdUsuario == 0)
                {
                    if (string.IsNullOrWhiteSpace(txtContrasenia.Text))
                    {
                        lblMensaje.Text = "La contraseña es obligatoria para crear un usuario.";
                        lblMensaje.CssClass = "alert alert-danger";
                        lblMensaje.Visible = true;
                        return;
                    }

                    u.Contrasenia = txtContrasenia.Text;
                    negocio.AgregarUsuario(u);

                    lblMensaje.Text = "Usuario agregado correctamente.";
                    lblMensaje.CssClass = "alert alert-success";
                }
                else
                {
                    negocio.ModificarUsuario(u);
                    lblMensaje.Text = "Usuario modificado correctamente.";
                    lblMensaje.CssClass = "alert alert-success";
                }

                lblMensaje.Visible = true;
            }
            catch (Exception ex)
            {
                lblMensaje.Text = "Ocurrió un error: " + ex.Message;
                lblMensaje.CssClass = "alert alert-danger";
                lblMensaje.Visible = true;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Response.Redirect("GestionUsuarios.aspx");
        }
    }
}
