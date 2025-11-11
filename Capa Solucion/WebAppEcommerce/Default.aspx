<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppEcommerce._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
     <hr/>
        <div class="module-image">
         <img src="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" data-srcset="//acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 480w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 640w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 1024w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 1920w" data-sizes="auto" class="textbanner-image-background lazyautosizes fade-in ls-is-cached lazyloaded" alt="¿QUIENES SOMOS?" sizes="720px" srcset="//acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 480w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 640w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 1024w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 1920w">
             <div class="placeholder-overlay placeholder-container"></div>
                <div class="module-text pull-left">
                    <h3 class="module-text-title">¿QUIENES SOMOS?</h3>
                    <div class="module-text-paragraph">Somos una empresa dedicada a la venta de componentes e insumos electrónicos. Con más de 1 año de experiencia, nuestro objetivo es ofrecer soluciones que cumplan las necesidades de cada cliente.</div>
                    <div class="module-text-button btn btn-primary">VER MAS</div>
                </div>
             </div>
        <asp:Repeater ID="rptArticulos" runat="server">
            <ItemTemplate>
                <div style="border:1px solid #ccc; 
                            border-radius:10px; 
                            padding:15px; 
                            margin:10px; 
                            display:inline-block; 
                            width:300px; 
                            vertical-align:top;
                            box-shadow:2px 2px 6px rgba(0,0,0,0.1);">
            
                    <h4 style="margin-top:0;"><%# Eval("Nombre") %></h4>
                    <p><strong>Codigo:</strong> <%# Eval("Codigo") %></p>
                    <p><strong>Marca:</strong> <%# Eval("Marca.Nombre") %></p>
                    <p><strong>Categoria:</strong> <%# Eval("Categoria.Nombre") %></p>
                    <p><strong>Precio:</strong> $<%# Eval("Precio", "{0:N2}") %></p>

                    <!-- Galería de imágenes -->
                    <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("ListaUrls") %>'>
                        <ItemTemplate>
                            <img src='<%# Container.DataItem %>' 
                                 alt="Imagen del producto" 
                                 style="width:90px; 
                                        height:90px; 
                                        object-fit:cover; 
                                        margin:5px; 
                                        border-radius:8px; 
                                        border:1px solid #ddd;" />
                        </ItemTemplate>
                    </asp:Repeater>
                 </div>
            </ItemTemplate>
        </asp:Repeater>
    </main>

</asp:Content>
