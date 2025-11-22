<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CarritoPage.aspx.cs" Inherits="WebAppEcommerce.Carrito" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    
  <div class="container mt-4">
        <h2>Carrito de Compras</h2>

        <!-- Repeater para mostrar los productos -->
        <asp:Repeater ID="rptCarrito" runat="server" OnItemCommand="rptCarrito_ItemCommand">
            <ItemTemplate>
                <div class="row border-bottom py-3 align-items-center">
                    <!-- Imagen -->
                    <div class="col-md-3">
                        <asp:Image ID="imgArticulo" runat="server" 
                                   ImageUrl='<%# Eval("ImagenUrl") %>' 
                                   CssClass="img-fluid" />
                    </div>

                    <!-- Datos -->
                    <div class="col-md-9">
                        <asp:Label ID="lblNombre" runat="server" 
                                   Text='<%# Eval("Nombre") %>' CssClass="fw-bold" />
                        <br />
                        <asp:Label ID="lblPrecio" runat="server" 
                                   Text='<%# "Precio: $" + Eval("PrecioUnitario", "{0:N2}") %>' />
                        <br />
                        <asp:Label ID="lblCantidad" runat="server" 
                                   Text='<%# "Cantidad: " + Eval("Cantidad") %>' />
                        <br />
                        <asp:Label ID="lblSubtotal" runat="server" 
                                   Text='<%# "Subtotal: $" + Eval("Subtotal", "{0:N2}") %>' />
                        <!-- Botones de acción -->
                <div class="mt-2 d-flex justify-content-between">
                    <div>
                    <asp:Button ID="btnMenos" runat="server" Text="-" 
                                CommandName="Restar" CommandArgument='<%# Eval("IdArticulo") %>' CssClass="btn btn-outline-secondary btn-sm" />
                    <asp:Button ID="btnMas" runat="server" Text="+" 
                                CommandName="Sumar" CommandArgument='<%# Eval("IdArticulo") %>' CssClass="btn btn-outline-secondary btn-sm" />
                   </div>
                    <div>
                        <asp:Button ID="btnEliminar" runat="server" Text="🗑" 
                                CommandName="Eliminar" CommandArgument='<%# Eval("IdArticulo") %>' CssClass="btn btn-danger btn-sm; align-items-center;" />
                </div>
                </div>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <!-- Total -->
        <div class="mt-3 text-end">
            <asp:Label ID="lblTotal" runat="server" CssClass="fw-bold fs-5"></asp:Label>
        </div>
    </div>



</asp:Content>
