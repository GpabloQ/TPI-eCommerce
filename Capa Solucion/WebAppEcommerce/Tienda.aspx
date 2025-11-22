<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Tienda.aspx.cs" Inherits="WebAppEcommerce.Tienda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <main class="container mt-4">
                <br />
     <h2 class="text-center mb-4">Tienda</h2>
        <hr class="mb-4" />
         <br />
         
         <div class="text-center mt-3">
    <asp:Label ID="MainContent_Label3" runat="server" ForeColor="Red"></asp:Label>
</div>
        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        
        <!-- Repeater de productos -->
        <asp:Repeater ID="rptArticulos" runat="server" OnItemCommand="rptArticulos_ItemCommand">
            <HeaderTemplate>
                <div class="row justify-content-center">
            </HeaderTemplate>

            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card shadow-sm h-100">

                                           <!-- Carrusel -->
<!-- Imagen principal solo si existe UrlImagen -->
    
    <div id="carousel_<%# Eval("IdArticulo") %>" class="carousel slide" data-bs-ride="carousel">
                         <div class="carousel-inner">
                             <div class="carousel-item active">
                                 <img src='<%# Eval("UrlImagen") %>'
                                      alt="Imagen principal"
                                      class="d-block w-100 img-fluid"
                                      style="object-fit:contain; border-radius:6px;" />
                             </div>
                             

                             <!-- Imágenes adicionales -->
                             <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("ListaUrls") %>'>
                                 <ItemTemplate>
                                     <div class="carousel-item">
                                         <img src='<%# Container.DataItem %>'
                                              alt="Imagen adicional"
                                              class="d-block w-100 img-fluid"
                                              style=" object-fit:contain; border-radius:6px;" />
                                     </div>
                                 </ItemTemplate>
                             </asp:Repeater>
                         </div>

                         <!-- Botones personalizados -->
                         <button class="carousel-control-prev custom-arrow" type="button"
                                 data-bs-target="#carousel_<%# Eval("IdArticulo") %>" data-bs-slide="prev">
                             &#10094;
                         </button>
                         <button class="carousel-control-next custom-arrow" type="button"
                                 data-bs-target="#carousel_<%# Eval("IdArticulo") %>" data-bs-slide="next">
                             &#10095;
                         </button>
                     </div>

                        <!-- Info del producto -->
                        <div class="card-body; text-center">
                            <h5 class="card-title text-center"><%# Eval("Nombre") %></h5>
                            <p style="font-family: Poppins, sans-serif; color: #333333CC; font-size:12px;"> $<%# Eval("Precio", "{0:N2}") %></p>
      <p style="font-family: Poppins, sans-serif; color: #EC6746; font-size:12px;">$<%# string.Format("{0:N2}", Convert.ToDecimal(Eval("Precio")) * 0.97m) %> CON TRANSFERENCIA O DEPÓSITO</p>
                        </div>

                        <!-- Boton Detalle Articulo -->
                        <div class="card-footer text-center">
                            <asp:Button ID="btnDetalleArticulo" runat="server" Text="Detalle Artículo" 
                                OnClick="btnDetalleArticulo_Click"  CommandArgument='<%# Eval("IdArticulo") %>'
                                CssClass="btn btn-outline-dark"/>
                            </div>

                       
                    </div>
                </div>
            </ItemTemplate>

            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

        <div class="text-center mt-3">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </main>


    


</asp:Content>
