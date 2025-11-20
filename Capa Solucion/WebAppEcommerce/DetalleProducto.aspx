<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="WebAppEcommerce.DetalleProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



     <main class="container-fluid">
         <br />
     <h2 class="text-center mb-4">DETALLE DEL PRODUCTO</h2>
         <br />
         <br />

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
                         <p><strong>Marca:</strong> <%# Eval("Marca.Nombre") %></p>
                         <p><strong>Categoría:</strong> <%# Eval("Categoria.Nombre") %></p>
                         <p><strong>Precio:</strong> $<%# Eval("Precio", "{0:N2}") %></p>
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

 <!-- Estilos para flechas -->
 <style>
     .custom-arrow {
         position: absolute;
         top: 50%;
         transform: translateY(-50%);
         background-color: rgba(0, 0, 0, 0.6);
         color: white;
         font-size: 28px;
         border: none;
         border-radius: 50%;
         width: 38px;
         height: 38px;
         text-align: center;
         line-height: 32px;
         cursor: pointer;
         z-index: 2;
     }

     .custom-arrow:hover {
         background-color: rgba(255, 255, 255, 0.8);
         color: black;
     }

     .carousel-control-prev.custom-arrow { left: 10px; }
     .carousel-control-next.custom-arrow { right: 10px; }
 </style>


</asp:Content>
