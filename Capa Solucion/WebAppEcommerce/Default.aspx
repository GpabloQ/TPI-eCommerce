<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebAppEcommerce._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main>
     <hr/>
        
        <%--QUIENES SOMOS--%>
        <div class="module-image">
            <div class="cuadroImagen"> </div>            
         <%--<img src="data:image/gif;base64,R0lGODlhAQABAAAAACH5BAEKAAEALAAAAAABAAEAAAICTAEAOw==" data-srcset="//acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 480w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 640w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 1024w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 1920w" data-sizes="auto" class="textbanner-image-background lazyautosizes fade-in ls-is-cached lazyloaded" alt="¿QUIENES SOMOS?" sizes="720px" srcset="//acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 480w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 640w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 1024w, //acdn-us.mitiendanube.com/stores/002/936/119/themes/style/img-596067975-1736528106-c4ae845a20c394ca2d2df71fa9c22e741736528107.png?1300625852 1920w">--%>
             <div class="placeholder-overlay placeholder-container"></div>
                
            <div class="Info">
                    <h3 >¿QUIENES SOMOS?</h3>
                    <p>Somos una empresa dedicada a la venta de componentes e insumos electrónicos. Con más de 1 año de experiencia, nuestro objetivo es ofrecer soluciones que cumplan las necesidades de cada cliente.</p>
                   
                <div class="BOTON">
                        <asp:Button ID="BtnVermas" class="module-text-button btn btn-dark" OnClick="BtnVermas_Click" runat="server" Text="VER MAS" />
                    </div>

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

       <%-- Seccion de contacto--%>    
        <section id="contacto" class="arsumo-contacto py-5 text-white" style="background:#111;">
                    <div class="container">

    <h2 class="mb-4">Contacto</h2>

    <div class="contenedor-contacto d-flex justify-content-between align-items-start">

        <!-- IZQUIERDA (WhatsApp + Dirección + Mapa) -->
        <div class="izquierda">

            <h4>Atención personalizada, envianos un mensaje por WhatsApp.</h4>
            <br />

            <address>
                <i class="bi bi-whatsapp fs-4 text-success"></i>
                <a href="https://wa.me/541138467136" target="_blank" rel="noopener" 
                   class="text-white text-decoration-none ms-1">
                    WhatsApp: +54 11 3846-7136
                </a>
            </address>

            <br />

            <!-- Icono globo -->
            <svg xmlns="http://www.w3.org/2000/svg" width="20" height="20" fill="currentColor"
                 class="bi bi-globe-americas-fill text-info mb-1" viewBox="0 0 16 16">
                <path fill-rule="evenodd"
                      d="m8 0 .412.01A7.97 7.97 0 0 1 13.29 2a8.04 8.04 0 0 1 2.548 4.382 8 8 0 1 1-15.674 0 8 8 0 0 1 1.361-3.078A8 8 0 0 1 2.711 2 7.96 7.96 0 0 1 8 0m0 1a7 7 0 0 0-5.958 3.324C2.497 6.192 6.669 7.827 6.5 8c-.5.5-1.034.884-1 1.5.07 1.248 2.259.774 2.5 2 .202 1.032-1.051 3 0 3 1.5-.5 3.798-3.186 4-5 .138-1.242-2-2-3.5-2.5-.828-.276-1.055.648-1.5.5S4.5 5.5 5.5 5s1 0 1.5.5c1 .5.5-1 1-1.5.838-.838 3.16-1.394 3.605-2.001A6.97 6.97 0 0 0 8 1"/>
            </svg>

            Valparaíso 1051 - Ing. Pablo Nogués - Malvinas Argentinas - Buenos Aires  
            <br /><br />

            <!-- Mapa -->
            <div style="margin-top:20px;">
                <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d52618.491172448725!2d-58.78568307832031!3d-34.48625759999999!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x95bca3448b7ff733%3A0x47810c946685719b!2sArsumo%20Tecnolog%C3%ADa%20e%20Innovaci%C3%B3n!5e0!3m2!1ses!2sus!4v1761868723940!5m2!1ses!2sus"
                        width="100%" height="350" style="border:0;" loading="lazy">
                </iframe>
            </div>

            <br />

            <address>
                <strong>Ventas:</strong>
                <a href="mailto:arsumoventas@gmail.com" class="text-white">
                    arsumoventas@gmail.com
                </a>
            </address>

        </div>

        <!-- DERECHA (Formulario COMPLETO) -->
        <div class="derecha text-white">

            <asp:Label ID="lblNombreContact" runat="server" Text="Nombre:" ></asp:Label>
            <br />
            <asp:TextBox ID="txtNombreContact" runat="server" CssClass="form-control mb-2"></asp:TextBox>

            <asp:Label ID="lblEmailContact" runat="server" Text="Email:" ></asp:Label>
            <br />
            <asp:TextBox ID="txtEmailContact" runat="server" CssClass="form-control mb-2"></asp:TextBox>

            <asp:Label ID="lblTelefonoContact" runat="server" Text="Teléfono (opcional):" ></asp:Label>
            <br />
            <asp:TextBox ID="txtTelefonoContact" runat="server" CssClass="form-control mb-2"></asp:TextBox>

            <asp:Label ID="lblMensajeContact" runat="server" Text="Escribí tu mensaje:" />
            <br />
            <asp:TextBox ID="txtMensajeContact" runat="server" TextMode="MultiLine" Rows="5" Columns="50" 
                         CssClass="form-control mb-3" />

            <asp:Button ID="btnEnviar" OnClick="btnEnviar_Click" runat="server" 
                        Text="Enviar" CssClass="btn btn-success mt-2" />

        </div>

    </div>

</div>

        </section>
       

    </main>

</asp:Content>
