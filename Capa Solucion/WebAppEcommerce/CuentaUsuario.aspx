<%@ Page Title="Mi Cuenta" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CuentaUsuario.aspx.cs" Inherits="WebAppEcommerce.CuentaUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">      

     <%--Libreria de JS SweetAlert, para mostrar carteles copados--%>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        <script src="Javascript/FuncionesJS.js"></script>

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
                        <p><strong>Dirección:</strong><asp:Label ID="lblDireccion" runat="server" />    
                            <!-- ÍCONO DE ELIMINAR DOMICILIO -->
                            <asp:LinkButton ID="btnEliminarDomicilio" runat="server"
                                            OnClick="btnEliminarDomicilio_Click"
                                            CssClass="text-danger ms-2"
                                            ToolTip="Eliminar domicilio"
                                            Visible="false">
                                <i class="fa fa-trash"></i>
                            </asp:LinkButton>
                        </p>



                         <!-- BOTÓN PARA AGREGAR DOMICILIO -->
                         <asp:Button ID="btnAgregarDomicilio" runat="server"
                                     Text="Agregar Domicilio"
                                     CssClass="btn btn-info mt-2"
                                     OnClientClick="abrirModalDomicilio(); return false;" />

                        <asp:Button ID="btnEditarCuenta" runat="server" 
                                Text="Editar Datos" 
                                CssClass="btn btn-outline-dark mt-3"
                                OnClick="btnEditarCuenta_Click" />
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
        <!-- MODAL DOMICILIO -->
<div id="modalDomicilio" class="modal" style="display:none;
     position:fixed; top:0; left:0; width:100%; height:100%;
     background:rgba(0,0,0,0.6); justify-content:center; align-items:center;">

    <div class="card p-4" style="width:450px;">
        <h4>Agregar Domicilio</h4>

        <div class="mb-3">
            <label>Calle:</label>
            <asp:TextBox ID="txtCalle" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Número:</label>
            <asp:TextBox ID="txtNumero" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Piso (opcional):</label>
            <asp:TextBox ID="txtPiso" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Departamento:</label>
            <asp:TextBox ID="txtDepto" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Ciudad:</label>
            <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Provincia:</label>
            <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" />
        </div>

        <div class="mb-3">
            <label>Código Postal:</label>
            <asp:TextBox ID="txtCP" runat="server" CssClass="form-control" />
        </div>

        <!-- BOTONES DEL MODAL -->
        <div class="text-end">
            <asp:Button ID="btnGuardarDomicilio" runat="server"
                        Text="Guardar"
                        CssClass="btn btn-success"
                        OnClick="btnGuardarDomicilio_Click" />

            <button type="button" class="btn btn-secondary" onclick="cerrarModalDomicilio()">Cerrar</button>
        </div>
    </div>
</div>
</asp:Content>
