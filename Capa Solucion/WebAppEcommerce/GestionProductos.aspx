<%@ Page Title="Gestion de Productos" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="GestionProductos.aspx.cs" Inherits="WebAppEcommerce.Gestion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container mt-4">
        <h2 class="text-center mb-4">GESTION DE PRODUCTOS</h2>

        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
        


        <!-- Botón para agregar nuevo producto -->
        <div class="text-center mb-4">
            <asp:Button ID="btnAgregar" runat="server" Text="AGREGAR NUEVO PRODUCTO"
                        CssClass="btn btn-success" OnClick="btnAgregar_Click" />
        </div>

        <!-- Repeater de productos -->
        <asp:Repeater ID="rptArticulos" runat="server" OnItemCommand="rptArticulos_ItemCommand">
            <HeaderTemplate>
                <div class="row justify-content-center">
            </HeaderTemplate>

            <ItemTemplate>
                <div class="col-md-3 mb-4">
                    <div class="card shadow-sm h-100">

                        <!-- Carrusel -->
                        <div id="carousel_<%# Eval("IdArticulo") %>" class="carousel slide" data-bs-ride="carousel">
                            <div class="carousel-inner">
                                <div class="carousel-item active">
                                    <img src='<%# Eval("UrlImagen") %>'
                                         alt="Imagen principal"
                                         class="d-block w-100"
                                         style="height:200px; object-fit:cover; border-radius:6px;" />
                                </div>

                                <!-- Imágenes adicionales -->
                                <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("ListaUrls") %>'>
                                    <ItemTemplate>
                                        <div class="carousel-item">
                                            <img src='<%# Container.DataItem %>'
                                                 alt="Imagen adicional"
                                                 class="d-block w-100"
                                                 style="height:200px; object-fit:cover; border-radius:6px;" />
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
                        <div class="card-body">
                            <h5 class="card-title text-center"><%# Eval("Nombre") %></h5>
                            <p><strong>Código:</strong> <%# Eval("Codigo") %></p>
                            <p><strong>Marca:</strong> <%# Eval("Marca.Nombre") %></p>
                            <p><strong>Categoría:</strong> <%# Eval("Categoria.Nombre") %></p>
                            <p><strong>Precio:</strong> $<%# Eval("Precio", "{0:N2}") %></p>
                        </div>

                        <!-- Botones -->
                        <div class="card-footer text-center">
                            <asp:Button ID="btnModificar" runat="server" Text="MODIFICAR"
                                        CssClass="btn btn-warning btn-sm mx-1"
                                        CommandName="Modificar" CommandArgument='<%# Eval("IdArticulo") %>' />

                            <asp:Button ID="btnEliminar" runat="server" Text="ELIMINAR"
                                        CssClass="btn btn-danger btn-sm mx-1"
                                        CommandName="Eliminar" CommandArgument='<%# Eval("IdArticulo") %>'
                                        OnClientClick="return confirmarEliminacion(this);" />
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
    <script>
        function confirmarEliminacion(boton) {

            // Detener el postback inmediato
            event.preventDefault();

            Swal.fire({
                title: '¿Eliminar artículo?',
                text: 'Esta acción no se puede deshacer.',
                icon: 'warning',
                showCancelButton: true,
                confirmButtonText: 'Sí, eliminar',
                cancelButtonText: 'Cancelar'
            }).then((result) => {
                if (result.isConfirmed) {

                    // Hacemos que el botón ASP.NET ejecute el Command normalmente
                    __doPostBack(boton.name, '');
                }
            });

            return false;
        }
    </script>

</asp:Content>
