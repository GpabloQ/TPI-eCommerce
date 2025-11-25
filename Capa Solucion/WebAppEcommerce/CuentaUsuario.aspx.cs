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
    public partial class CuentaUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Validacion si hay usuario logueado
                Usuario usuario = Session["Usuario"] as Usuario;

                if (usuario == null)
                {
                    pnlNoLogueado.Visible = true;
                    pnlLogueado.Visible = false;
                }
                else
                {
                    pnlNoLogueado.Visible = false;
                    pnlLogueado.Visible = true;

                    // Completar info
                    lblNombre.Text = usuario.NombreCompleto;
                    lblEmail.Text = usuario.Mail;
                    lblTipoUsuario.Text = usuario.TipoUsuarioNombre;
                    lblTelefono.Text = usuario.Telefono;

                    if (usuario.Domicilio != null)
                    {
                        lblDireccion.Text =
                            $"{usuario.Domicilio.Calle} {usuario.Domicilio.Numero}, {usuario.Domicilio.Piso} {usuario.Domicilio.Departamento}, " +
                            $"{usuario.Domicilio.Ciudad}, {usuario.Domicilio.Provincia}, CP: {usuario.Domicilio.CodigoPostal}";
                        // CAMBIAR TEXTO DEL BOTÓN
                            btnAgregarDomicilio.Text = "Actualizar Domicilio";
                            btnEliminarDomicilio.Visible = true;   // <-- MOSTRAR EL ÍCONO

                        // Generar script con valores pre-cargados
                        string script = $@"
                                        abrirModalDomicilio(
                                            '{usuario.Domicilio.Calle}',
                                            '{usuario.Domicilio.Numero}',
                                            '{usuario.Domicilio.Piso}',
                                            '{usuario.Domicilio.Departamento}',
                                            '{usuario.Domicilio.Ciudad}',
                                            '{usuario.Domicilio.Provincia}',
                                            '{usuario.Domicilio.CodigoPostal}'
                                        ); return false;";

                        btnAgregarDomicilio.OnClientClick = script;
                    }
                    else
                    {
                        lblDireccion.Text = "Sin domicilio registrado";
                        // TEXTO POR DEFECTO
                        btnEliminarDomicilio.Visible = false;  // <-- OCULTARLO
                        btnAgregarDomicilio.Text = "Agregar Domicilio";
                        btnAgregarDomicilio.OnClientClick = "abrirModalDomicilio(); return false;";
                    }

                    // Cargar pedidos del usuario
                    CargarPedidos(usuario.IdUsuario);
                }
            }

            // ---- Cargar carrito ----
            Dominio.Carrito carrito = Session["Carrito"] as Dominio.Carrito;

            if (carrito != null && carrito.Items.Any())
            {
                ArticuloNegocio negocio = new ArticuloNegocio();

                var datos = carrito.Items.Select(i =>
                {
                    Articulo art = negocio.ListarPorIDArticulo((int)i.IdArticulo);

                    return new
                    {
                        IdArticulo = art.IdArticulo,
                        ImagenUrl = art.UrlImagen,
                        Nombre = art.Nombre,
                        PrecioUnitario = i.PrecioUnitario,
                        Cantidad = i.Cantidad,
                        Subtotal = i.Cantidad * i.PrecioUnitario
                    };
                }).ToList();

                rptCarritoCuenta.DataSource = datos;
                rptCarritoCuenta.DataBind();

                decimal total = carrito.Items.Sum(x => x.Cantidad * x.PrecioUnitario);
                lblTotalCuenta.Text = "Total: $" + total.ToString("N2");

                btnFinalizarCompra.Visible = true;
            }
            else
            {
                lblTotalCuenta.Text = "No tenés productos en tu carrito.";
                btnFinalizarCompra.Visible = false;
            }
        }

        protected void btnFinalizarCompra_Click(object sender, EventArgs e)
        {
            Response.Redirect("FinDeCompra.aspx");
        }
        // -------------------------------
        // MÉTODO PARA CARGAR PEDIDOS
        // -------------------------------
        private void CargarPedidos(long idUsuario)
        {
            OrdenNegocio negocio = new OrdenNegocio();

            // Trae pedidos finalizados
            List<Dominio.Carrito> pedidos = negocio.ListarOrdenesPorUsuario(idUsuario);

            // Para cada pedido, cargar sus items
            foreach (Dominio.Carrito ped in pedidos)
            {
                ped.Items = negocio.ListarElementosPorCarrito(ped.IdCarrito);
            }

            // carga lista simple
            rptPedidos.DataSource = pedidos;
            rptPedidos.DataBind();
        }
       
        public static List<Dominio.ElementoCarrito> ObtenerDetallePedido(long idCarrito)
        {
            OrdenNegocio negocio = new OrdenNegocio();
            return negocio.ListarElementosPorCarrito(idCarrito);
        }
        protected void rptPedidos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "VerMas")
            {
                long idCarrito = Convert.ToInt64(e.CommandArgument);
                Response.Redirect("DetallePedido.aspx?idCarrito=" + idCarrito);
            }
        }

        protected void btnEditarCuenta_Click(object sender, EventArgs e)
        {
            Usuario usuario = Session["Usuario"] as Usuario;

            if (usuario != null)
            {
                Response.Redirect("AgregarUsuario.aspx?id=" + usuario.IdUsuario);
            }
        }

        protected void btnGuardarDomicilio_Click(object sender, EventArgs e)
        {
            try
            {
                // 1) Crear objeto domicilio
                Domicilio nuevo = new Domicilio
                {
                    Calle = txtCalle.Text.Trim(),
                    Numero = txtNumero.Text.Trim(),
                    Piso = txtPiso.Text.Trim(),
                    Departamento = txtDepto.Text.Trim(),
                    Ciudad = txtCiudad.Text.Trim(),
                    Provincia = txtProvincia.Text.Trim(),
                    CodigoPostal = txtCP.Text.Trim(),
                    Estado = true
                };

                DomicilioNegocio domNeg = new DomicilioNegocio();
                UsuarioNegocio userNeg = new UsuarioNegocio();

                // 2) Guardar domicilio en BD
                long idDom = domNeg.Agregar(nuevo);

                // 3) Obtener usuario en sesión
                Usuario usuario = Session["Usuario"] as Usuario;

                // 4) Asignar domicilio al usuario en BD
                userNeg.ActualizarDomicilio(usuario.IdUsuario, idDom);

                // 5) REFRESCAR USUARIO en sesión
                usuario = userNeg.buscarPorId((int)usuario.IdUsuario);
                usuario.Domicilio = nuevo; // por si el buscarPorId no trae join

                Session["Usuario"] = usuario;

                // 6) Mensaje OK + cerrar modal
                ScriptManager.RegisterStartupScript(this, GetType(), "ok",
                    "Swal.fire('Domicilio agregado', 'Se actualizó correctamente.', 'success');", true);

                ScriptManager.RegisterStartupScript(this, GetType(), "cerrar",
                    "cerrarModalDomicilio();", true);

                // 7) Recargar la página para ver los cambios
                Response.Redirect("CuentaUsuario.aspx");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "error",
                    $"Swal.fire('Error', '{ex.Message}', 'error');", true);
            }
        }

        protected void btnEliminarDomicilio_Click(object sender, EventArgs e)
{
    try
    {
        Usuario usuario = Session["Usuario"] as Usuario;

        if (usuario?.Domicilio == null)
            return;

        long idDom = usuario.Domicilio.IdDomicilio;

        // 1) Quitar domicilio del usuario
        UsuarioNegocio un = new UsuarioNegocio();
        un.ActualizarDomicilio(usuario.IdUsuario, 0); // Lo limpiamos

        // 2) Eliminar domicilio de la tabla
        DomicilioNegocio dn = new DomicilioNegocio();
        dn.Eliminar(idDom);

        // 3) Actualizar Session
        usuario.Domicilio = null;
        Session["Usuario"] = usuario;

        // 4) Mostrar mensaje
        ScriptManager.RegisterStartupScript(this, GetType(), "ok",
            "Swal.fire('Domicilio eliminado', 'Se quitó correctamente.', 'success');",
            true);

        // 5) Refrescar pantalla
        Response.Redirect("CuentaUsuario.aspx");
    }
    catch (Exception ex)
    {
        ScriptManager.RegisterStartupScript(this, GetType(), "error",
            $"Swal.fire('Error', '{ex.Message}', 'error');", true);
    }
}


    }
}
