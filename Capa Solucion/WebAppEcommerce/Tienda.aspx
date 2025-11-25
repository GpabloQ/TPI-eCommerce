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

       

        <div class="filtros-superiores">

            <!-- BUSCADOR -->
            <div class="input-group mb-3 search-box">

                <span class="input-group-text bg-white">
                    <i class="bi bi-search"></i>
                </span>

                <input type="text" id="txtBuscar"
                    runat="server"
                    class="form-control"
                    placeholder="Buscar artículos..." />

                <asp:Button ID="btnBuscar" runat="server"
                    Text="Buscar"
                    CssClass="btn btn-dark btn-filtro"
                    OnClick="btnBuscar_Click" />
            </div>
        </div>

        <div class="filtro-inferior">

            <!-- DROPDOWN -->
            <div class="dropdown-container">
                <asp:DropDownList ID="ddlOrdenPrecio" runat="server"
                    CssClass="ddlOrdenPrecio"
                    AutoPostBack="true"
                    OnSelectedIndexChanged="ddlOrdenPrecio_SelectedIndexChanged">
                    <asp:ListItem Text="Ordenar por:" Value="" />
                    <asp:ListItem Text="Menor a mayor" Value="ASC" />
                    <asp:ListItem Text="Mayor a menor" Value="DESC" />
                </asp:DropDownList>
            </div>

           
            <!-- BOTÓN LIMPIAR FILTROS -->
            <div class="limpiar-container">
                <asp:Button ID="btnLimpiar" runat="server"
                    Text="Limpiar Filtros"
                    CssClass="btn btn-dark btn-filtro"
                    OnClick="btnLimpiar_Click" />
            </div>

        </div>




        <div class="container">
            <div class="row">

                <!-- COLUMNA IZQUIERDA – FILTROS -->
                <div class="col-md-3">
                    <div class="filtros-sidebar">

                        <!-- FILTRO MARCAS -->
                        <div class="filtro">
                            <h3>MARCAS</h3>

                            <div class="filtro-scroll">
                                <asp:Repeater ID="rptMarcas" runat="server">
                                    <ItemTemplate>
                                        <a class="tag" href='Tienda.aspx?marca=<%# Eval("IdMarca") %>'>
                                            <%# Eval("Nombre") %> (<%# Eval("Cantidad") %>)
                                        </a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                        <!-- FILTRO CATEGORÍAS -->
                        <div class="filtro">
                            <h3>CATEGORÍAS</h3>

                            <div class="filtro-scroll">
                                <asp:Repeater ID="rptCategorias" runat="server">
                                    <ItemTemplate>
                                        <a class="tag" href='Tienda.aspx?categoria=<%# Eval("IdCategoria") %>'>
                                            <%# Eval("Nombre") %>
                                        </a>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </div>

                    </div>
                    <!-- cierre filtros-sidebar -->
                </div>
    





                <!-- COLUMNA DERECHA – PRODUCTOS -->
                <div class="col-md-9">

                    <asp:Repeater ID="rptArticulos" runat="server" OnItemCommand="rptArticulos_ItemCommand">
                        <HeaderTemplate>
                            <div class="row justify-content-start">
                        </HeaderTemplate>

                        <ItemTemplate>
                            <div class="col-md-4 mb-4">
                                <div class="card shadow-sm h-100">

                                    <!-- Carrusel -->
                                    <div id="carousel_<%# Eval("IdArticulo") %>" class="carousel slide" data-bs-ride="carousel">
                                        <div class="carousel-inner">

                                            <div class="carousel-item active">
                                                <img src='<%# Eval("UrlImagen") %>'
                                                    class="d-block w-100 img-fluid"
                                                    style="object-fit: contain; border-radius: 6px;" />
                                            </div>

                                            <asp:Repeater ID="rptImagenes" runat="server" DataSource='<%# Eval("ListaUrls") %>'>
                                                <ItemTemplate>
                                                    <div class="carousel-item">
                                                        <img src='<%# Container.DataItem %>'
                                                            class="d-block w-100 img-fluid"
                                                            style="object-fit: contain; border-radius: 6px;" />
                                                    </div>
                                                </ItemTemplate>
                                            </asp:Repeater>

                                        </div>

                                        <button class="carousel-control-prev custom-arrow"
                                            type="button"
                                            data-bs-target="#carousel_<%# Eval("IdArticulo") %>" data-bs-slide="prev">
                                            &#10094;
                                        </button>

                                        <button class="carousel-control-next custom-arrow"
                                            type="button"
                                            data-bs-target="#carousel_<%# Eval("IdArticulo") %>" data-bs-slide="next">
                                            &#10095;
                                        </button>
                                    </div>

                                    <!-- Información -->
                                    <div class="card-body text-center">
                                        <h5 class="card-title"><%# Eval("Nombre") %></h5>

                                        <p style="font-family: Poppins; color: #333333CC; font-size: 12px;">
                                            $<%# Eval("Precio", "{0:N2}") %>
                                        </p>

                                        <p style="font-family: Poppins; color: #EC6746; font-size: 12px;">
                                            $<%# string.Format("{0:N2}", Convert.ToDecimal(Eval("Precio")) * 0.97m) %> 
                                        CON TRANSFERENCIA O DEPÓSITO
                                        </p>
                                    </div>

                                    <!-- Botones -->
                                    <div class="card-footer text-center">

                                        <asp:Button ID="btnAgregarCarrito" runat="server"
                                            Text="Agregar al carrito"
                                            CssClass="btn btn-primary"
                                            CommandName="Agregar"
                                            CommandArgument='<%# Eval("IdArticulo") %>' />

                                        <asp:Button ID="btnDetalleArticulo" runat="server"
                                            Text="Detalle Artículo"
                                            CssClass="btn btn-outline-dark"
                                            OnClick="btnDetalleArticulo_Click"
                                            CommandArgument='<%# Eval("IdArticulo") %>' />

                                    </div>

                                </div>
                            </div>
                        </ItemTemplate>

                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>

                </div>
                <!-- FIN COL 9 -->

            </div>
            <!-- row -->
        </div>
        <!-- container -->


        <div class="text-center mt-3">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
        </div>
    </main>





</asp:Content>
