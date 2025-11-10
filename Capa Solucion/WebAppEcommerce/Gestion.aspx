<%@ Page Title="Gestion de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="WebAppEcommerce.Gestion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <h3>ABML de Productos</h3>
        <h4>Agregar, modificar y eliminar productos</h4>

        <!-- agregar nuevo producto -->
        <div style="margin: 15px 0;">
            <asp:Button ID="btnAgregar" runat="server" Text="AGREGAR NUEVO PRODUCTO"
                        CssClass="btn btn-success" OnClick="btnAgregar_Click" />
        </div>

        <!-- repite los productos -->
        <asp:Repeater ID="rptArticulos" runat="server" OnItemCommand="rptArticulos_ItemCommand">
            <ItemTemplate>
                <div style="border:1px solid #ccc; border-radius:10px; padding:15px; margin:10px; display:inline-block; width:300px; vertical-align:top;">
                    <h4><%# Eval("nombre") %></h4>
                    <p><strong>Codigo:</strong> <%# Eval("codigoArticulo") %></p>
                    <p><strong>Marca:</strong> <%# Eval("Marca.Nombre") %></p>
                    <p><strong>Categoria:</strong> <%# Eval("tipo.Nombre") %></p>
                    <p><strong>Precio:</strong> $<%# Eval("precio", "{0:N2}") %></p>

                    <!-- Galeria de imágenes -->
                    <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("ListaUrls") %>'>
                        <ItemTemplate>
                            <img src='<%# Container.DataItem %>' 
                                 alt="Imagen" 
                                 style="width:90px; height:90px; object-fit:cover; margin:5px; border-radius:8px;" />
                        </ItemTemplate>
                    </asp:Repeater>

                    <!-- Botones en Galeria-->
                    <div style="margin-top:10px;">
                        <asp:Button ID="btnModificar" runat="server" Text="MODIFICAR" CssClass="btn btn-warning btn-sm"
                                    CommandName="Modificar" CommandArgument='<%# Eval("id") %>' />
                        <asp:Button ID="btnEliminar" runat="server" Text="ELIMINAR" CssClass="btn btn-danger btn-sm"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("id") %>' 
                                    OnClientClick="return confirm('¿Seguro que desea eliminar este producto?');" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <div class ="lblMensaje">
        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
</div>
    </main>

</asp:Content>
