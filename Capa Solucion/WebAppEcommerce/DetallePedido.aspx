<%@ Page Title="Detalle del Pedido" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetallePedido.aspx.cs" Inherits="WebAppEcommerce.DetallePedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container mt-4">

        <h2>Detalle del Pedido</h2>
        <hr />

        <asp:Panel ID="pnlPedido" runat="server" Visible="false">

            <p><strong>Número de Pedido:</strong> <asp:Label ID="lblIdCarrito" runat="server" /></p>
            <p><strong>Fecha:</strong> <asp:Label ID="lblFecha" runat="server" /></p>
            <p><strong>Estado:</strong> <asp:Label ID="lblEstado" runat="server" /></p>

            <h4 class="mt-4">Productos</h4>

            <asp:Repeater ID="rptItems" runat="server">
                <ItemTemplate>
                    <div class="border p-3 mb-2">

                        <p><strong>Artículo:</strong> <%# Eval("IdArticulo") %></p>
                        <p><strong>Nombre:</strong> <strong><%# Eval("Nombre") %></strong></p>
                        <p><strong>Descripción:</strong> <%# Eval("Descripcion") %></p>
                        <p><strong>Marca:</strong> <%# Eval("Marca") %></p>                        
                        <p><strong>Categoria:</strong> <%# Eval("Categoria") %></p>     

                        <p><strong>Imagen:</strong><br />
                            <img src='<%# Eval("UrlImagen") %>' alt="Imagen del producto"
                                 style="max-width: 150px; height: auto;" />
                        </p>

                        <p><strong>Cantidad:</strong> <%# Eval("Cantidad") %></p>
                        <p><strong>Precio Unitario:</strong> $ <%# Eval("PrecioUnitario") %></p>

                        <p><strong>Subtotal:</strong> 
                            $ <%# (Convert.ToDecimal(Eval("Cantidad")) 
                                  * Convert.ToDecimal(Eval("PrecioUnitario"))).ToString("N2") %>
                        </p>

                    </div>
                </ItemTemplate>
            </asp:Repeater>


            <h4 class="mt-4 text-end">Total:  
                <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold"></asp:Label>
            </h4>

        </asp:Panel>

        <asp:Panel ID="pnlError" runat="server" Visible="false">
            <h3>No se encontró el pedido</h3>
        </asp:Panel>

    </main>

</asp:Content>
