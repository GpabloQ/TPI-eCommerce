<%@ Page Title="Gestion de Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="" Inherits="WebAppEcommerce.Gestion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    
    <main aria-labelledby="title">
            <h2 id="title"><%: Title %>.</h2>
            <h3>ABML de Productos</h3>
            <h4>agregar, modificar, eliminar productos</h4>
        
        
                <asp:Repeater ID="rptArticulos" runat="server">
                        <ItemTemplate>
                            <div style="border:1px solid #ccc; border-radius:10px; padding:15px; margin:10px; display:inline-block; width:300px; vertical-align:top;">
                                <h4><%# Eval("nombre") %></h4>
                                <p><strong>Codigo:</strong> <%# Eval("codigoArticulo") %></p>
                                <p><strong>Marca:</strong> <%# Eval("Marca.Descripcion") %></p>
                                <p><strong>Categoria:</strong> <%# Eval("tipo.Descripcion") %></p>
                                <p><strong>Precio:</strong> $<%# Eval("precio", "{0:N2}") %></p>

                                <!-- Galeria -->
                                <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("ListaUrls") %>'>
                                    <ItemTemplate>
                                        <img src='<%# Container.DataItem %>' 
                                             alt="Imagen" 
                                             style="width:90px; height:90px; object-fit:cover; margin:5px; border-radius:8px;" />
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ItemTemplate>
                </asp:Repeater>

    </main>
</asp:Content>