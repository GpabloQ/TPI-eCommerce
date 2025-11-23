<%@ Page Title="Mi Cuenta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuentaUsuario.aspx.cs" Inherits="WebAppEcommerce.CuentaUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
        <%--Libreria SweetAlert2 para mensajes emergentes--%>
<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <main class="container mt-4">
        <h2 class="text-center mb-4">MI CUENTA</h2>
        <hr />

        <!-- Si no hay usuario logueado -->
        <asp:Panel ID="pnlNoLogueado" runat="server" Visible="false" CssClass="text-center">
            <h4>Para ver tu cuenta necesitás iniciar sesión.</h4>
            <a href="Signin.aspx" class="btn btn-primary mt-3">IR A LOGIN</a>
        </asp:Panel>

        <!-- Si hay usuario logueado -->
        <asp:Panel ID="pnlLogueado" runat="server" Visible="false">

            <div class="row">
                <div class="col-md-6">
                    <h4>Datos del Usuario</h4>
                    <div class="card p-3">

                        <p><strong>Nombre:</strong> <asp:Label ID="lblNombre" runat="server" /></p>
                        <p><strong>Email:</strong> <asp:Label ID="lblEmail" runat="server" /></p>
                        <p><strong>Tipo de Usuario:</strong> <asp:Label ID="lblTipoUsuario" runat="server" /></p>
                        <p><strong>Telefono: </strong> <asp:Label ID="lblTelefono" runat="server" /></p>
                        <p> <strong>Direccion: </strong> <asp:Label ID="lblDireccion" runat="server" /></p>

                        <asp:Button ID="btnEditarCuenta" runat="server" Text="Editar Datos" CssClass="btn btn-outline-dark mt-3" />
                    </div>
                </div>

                <div class="col-md-6">
                    <h4>Últimos Movimientos</h4>
                    <div class="card p-3">
                        <asp:Label ID="lblMovimientos" runat="server" Text="No hay registros aún." />

                        <!-- Mas adelante podemos agregar un repeater de compras -->
                    </div>

                    <!-- Carrito del Usuario -->
                    <h4 class="mt-4">Mi Carrito</h4>
                    <div class="card p-3">

                        <!-- Repeater de items del carrito -->
                        <asp:Repeater ID="rptCarritoCuenta" runat="server">
                            <ItemTemplate>
                                <div class="row border-bottom py-2 align-items-center">

                                    <!-- Imagen -->
                                    <div class="col-3">
                                        <img src='<%# Eval("ImagenUrl") %>' class="img-fluid rounded" />
                                    </div>

                                    <!-- Datos -->
                                    <div class="col-9">
                                        <p class="fw-bold"><%# Eval("Nombre") %></p>
                                        <p>Precio: $<%# Eval("PrecioUnitario", "{0:N2}") %></p>
                                        <p>Cantidad: <%# Eval("Cantidad") %></p>
                                        <p class="fw-bold">Subtotal: $<%# Eval("Subtotal", "{0:N2}") %></p>
                                    </div>

                                </div>
                            </ItemTemplate>
                        </asp:Repeater>

                        <!-- Total del carrito -->
                        <div class="text-end mt-3">
                            <asp:Label ID="lblTotalCuenta" runat="server" CssClass="fw-bold fs-5"></asp:Label>
                        </div>                        
                        <asp:Button ID="btnFinalizarCompra" runat="server"
                                    Text="FINALIZAR COMPRA"
                                    CssClass="btn btn-success mt-3"
                                    Visible="false"
                                    OnClick="btnFinalizarCompra_Click"
                                    OnClientClick="return confirmarCompra();" />
                    </div>
                </div>
            </div>
        </asp:Panel>
    </main>
</asp:Content>

