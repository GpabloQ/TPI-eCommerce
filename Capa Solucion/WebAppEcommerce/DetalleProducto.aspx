<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="WebAppEcommerce.DetalleProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



     <main class="container-fluid">
         <br />
     <h2 class="text-center mb-4">Detalle del producto</h2>
         <hr class="mb-4" />
         
         <div class="text-center mt-3">
    <asp:Label ID="Label2" runat="server" ForeColor="Red"></asp:Label>
</div>
     <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
     


     <!-- Repeater de productos -->
     <asp:Repeater ID="rptArticulos" runat="server" OnItemCommand="rptArticulos_ItemCommand">
         <HeaderTemplate>
             <div class="row ">
         </HeaderTemplate>

         <ItemTemplate>
       <!--        <div class="col-md-12">      -->
               
                     <!-- que puedo modificar aca??-->
                     <div class="col-md-6">
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
            </div>
       

                      <!-- que puedo modificar aca?? (alinear ala derecha)-->
                     <div class="col-md-6">
                     <!-- Info del producto -->
                     <div class="card-body">
                         <h5 class="card-title text-center"><%# Eval("Nombre") %></h5>
                         <br />
                         <p><strong>Marca:</strong> <%# Eval("Marca.Nombre") %></p>
                         <p><strong>Categoría:</strong> <%# Eval("Categoria.Nombre") %></p>
                            <h5 style="font-family: Poppins, sans-serif; color: #333333CC; "> $<%# Eval("Precio", "{0:N2}") %></h5>
      <h6 style="font-family: Poppins, sans-serif; color: #EC6746; ">$<%# string.Format("{0:N2}", Convert.ToDecimal(Eval("Precio")) * 0.97m) %> CON TRANSFERENCIA O DEPÓSITO</h6>
                          <p><strong>Cantidad:</strong> <%# Eval("Cantidad") %></p>

                         <p><strong>Descripción:</strong> <%# Eval("Descripcion") %></p>
                     </div>
              </div>

           
        <!--       </div>   -->
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
