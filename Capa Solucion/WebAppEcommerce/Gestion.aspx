<%@ Page Title="Gestión de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Gestion.aspx.cs" Inherits="WebAppEcommerce.Gestion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main aria-labelledby="title">
        <h2 id="title"><%: Title %></h2>
        <h3>ABML de Productos</h3>
        <h4>Agregar, modificar y eliminar productos</h4>

        <!-- Botón para agregar nuevo producto -->
        <div style="margin: 15px 0;">
            <asp:Button ID="btnAgregar" runat="server" Text="AGREGAR NUEVO PRODUCTO"
                        CssClass="btn btn-success" OnClick="btnAgregar_Click" />
        </div>

        <!-- Listado de productos -->
        <asp:Repeater ID="rptArticulos" runat="server" OnItemCommand="rptArticulos_ItemCommand">
            <ItemTemplate>
                <div style="border:1px solid #ccc; border-radius:10px; padding:15px; margin:10px; display:inline-block; width:300px; vertical-align:top;">
                    <h4><%# Eval("Nombre") %></h4>
                    <p><strong>Código:</strong> <%# Eval("Codigo") %></p>
                    <p><strong>Marca:</strong> <%# Eval("Marca.Nombre") %></p>
                    <p><strong>Categoría:</strong> <%# Eval("Categoria.Nombre") %></p>
                    <p><strong>Precio:</strong> $<%# Eval("Precio", "{0:N2}") %></p>

                    <!-- Imagen principal (si existe) -->
                    <asp:Image ID="imgArticulo" runat="server"
                               ImageUrl='<%# Eval("UrlImagen") %>'
                               AlternateText="Sin imagen"
                               Width="120px" Height="120px"
                               Style="object-fit:cover; margin-top:8px; border-radius:8px;" />

                    <!-- Galería de imágenes adicionales -->
                    <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("ListaUrls") %>'>
                        <ItemTemplate>
                            <img src='<%# Container.DataItem %>' 
                                 alt="Imagen adicional" 
                                 style="width:70px; height:70px; object-fit:cover; margin:3px; border-radius:6px;" />
                        </ItemTemplate>
                    </asp:Repeater>

                    <!-- Botones -->
                    <div style="margin-top:10px;">
                        <asp:Button ID="btnModificar" runat="server" Text="MODIFICAR"
                                    CssClass="btn btn-warning btn-sm"
                                    CommandName="Modificar" CommandArgument='<%# Eval("IdArticulo") %>' />

                        <asp:Button ID="btnEliminar" runat="server" Text="ELIMINAR"
                                    CssClass="btn btn-danger btn-sm"
                                    CommandName="Eliminar" CommandArgument='<%# Eval("IdArticulo") %>'
                                    OnClientClick="return confirm('¿Seguro que desea eliminar este producto?');" />
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>

        <!-- Mensaje informativo -->
        <div class="lblMensaje" style="margin-top:15px;">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </main>

</asp:Content>
