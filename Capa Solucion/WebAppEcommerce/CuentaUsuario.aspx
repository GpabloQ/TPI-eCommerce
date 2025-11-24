<%@ Page Title="Mi Cuenta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuentaUsuario.aspx.cs" Inherits="WebAppEcommerce.CuentaUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">      

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

                <!-- DATOS DEL USUARIO -->
                <div class="col-md-6">
                    <h4>Datos del Usuario</h4>
                    <div class="card p-3">

                        <p><strong>Nombre:</strong> <asp:Label ID="lblNombre" runat="server" /></p>
                        <p><strong>Email:</strong> <asp:Label ID="lblEmail" runat="server" /></p>
                        <p><strong>Tipo de Usuario:</strong> <asp:Label ID="lblTipoUsuario" runat="server" /></p>
                        <p><strong>Teléfono:</strong> <asp:Label ID="lblTelefono" runat="server" /></p>
                        <p><strong>Dirección:</strong> <asp:Label ID="lblDireccion" runat="server" /></p>

                        <asp:Button ID="btnEditarCuenta" runat="server" Text="Editar Datos" CssClass="btn btn-outline-dark mt-3" />
                    </div>
                </div>

                <!-- MIS PEDIDOS -->
                <div class="col-md-6">                    
                    <h4 class="mt-4">Historial de Pedidos</h4>                    

                    <asp:Repeater ID="rptPedidos" runat="server" OnItemCommand="rptPedidos_ItemCommand">
                        <ItemTemplate>

                            <div class="card p-3 mb-3">

                                <!-- FILA 1 -->
                                <div class="row mb-2">

                                    <div class="col-md-4">
                                        <h5 class="fw-bold">Pedido Nº <%# Eval("IdCarrito") %></h5>
                                    </div>

                                    <div class="col-md-4">
                                        <p class="m-0"><strong>Fecha:</strong> <%# Eval("FechaCreacion", "{0:dd/MM/yyyy HH:mm}") %></p>
                                    </div>

                                    <div class="col-md-4">
                                        <p class="m-0"><strong>Estado:</strong> <%# Eval("EstadoCarrito") %></p>
                                    </div>

                                </div>

                                <hr />

                                <!-- FILA 2 -->
                                <div class="row align-items-center">

                                    <div class="col-md-6">
                                        <p class="fw-bold m-0">
                                            Total: $
                                            <%# ((List<Dominio.ElementoCarrito>)Eval("Items"))
                                                  .Sum(x => x.Cantidad * x.PrecioUnitario).ToString("N2") %>
                                        </p>
                                    </div>

                                    <div class="col-md-6 text-end">
                                        <asp:Button runat="server"
                                                    CssClass="btn btn-dark"
                                                    Text="Ver más"
                                                    CommandName="VerMas"
                                                    CommandArgument='<%# Eval("IdCarrito") %>' />
                                    </div>

                                </div>

                            </div>

                        </ItemTemplate>
                    </asp:Repeater>

                    <!-- MI CARRITO ACTUAL -->
                    <h4 class="mt-4">Mi Carrito</h4>
                    <div class="card p-3">

                        <asp:Repeater ID="rptCarritoCuenta" runat="server">
                            <ItemTemplate>

                                <div class="row border-bottom py-2 align-items-center">

                                    <!-- Imagen -->
                                    <div class="col-3">
                                        <img src='<%# Eval("ImagenUrl") %>' class="img-fluid rounded" />
                                    </div>

                                    <!-- Datos del producto -->
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

                        <!-- Botón para finalizar compra -->
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
