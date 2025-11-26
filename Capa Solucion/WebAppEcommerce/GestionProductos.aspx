<%@ Page Title="Gestion de Productos" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="GestionProductos.aspx.cs" Inherits="WebAppEcommerce.Gestion" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="container mt-4">
        <h2 class="text-center mb-4">GESTION DE PRODUCTOS</h2>

        <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

        <!-- Boton agregar -->
        <div class="text-center mb-4">
            <asp:Button ID="btnAgregar" runat="server" 
                Text="AGREGAR NUEVO PRODUCTO"
                CssClass="btn btn-success"
                OnClick="btnAgregar_Click" />
        </div>

        <!-- TABLA DE PRODUCTOS -->
        <asp:GridView ID="dgvProductos" runat="server" AutoGenerateColumns="False"
            CssClass="table table-striped table-bordered table-hover"
            DataKeyNames="IdArticulo"
            OnRowCommand="dgvProductos_RowCommand">
            <Columns>
                <asp:BoundField DataField="Codigo" HeaderText="Código" />
                <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
                <asp:BoundField DataField="Marca.Nombre" HeaderText="Marca" />
                <asp:BoundField DataField="Categoria.Nombre" HeaderText="Categoría" />
                <asp:BoundField DataField="Cantidad" HeaderText="Stock" />
                <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="ARS {0:N2}" HtmlEncode="false" />

    
                <asp:TemplateField HeaderText="Editar">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEditar" runat="server"
                            CommandName="Modificar"
                            CommandArgument='<%# Eval("IdArticulo") %>'
                            CssClass="icon-btn"
                            Text="✏️" />
                    </ItemTemplate>
                </asp:TemplateField>

    
                <asp:TemplateField HeaderText="Detalle">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnDetalle" runat="server"
                            CommandName="Detalle"
                            CommandArgument='<%# Eval("IdArticulo") %>'
                            CssClass="icon-btn"
                            Text="📄" />
                    </ItemTemplate>
                </asp:TemplateField>

    
                <asp:TemplateField HeaderText="Eliminar">
                    <ItemTemplate>
                        <asp:LinkButton ID="btnEliminar" runat="server"
                            CssClass="icon-btn text-danger"
                            Text="🗑️"
                            OnClientClick='<%# "confirmarEliminacion(this, \"" + Eval("IdArticulo") + "\"); return false;" %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>


        </asp:GridView>

        <div class="text-center mt-3">
            <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
        </div>

    </main>

    <!-- CONFIRMACION SWEETALERT -->
    <script>
        function confirmarEliminacion(boton) {
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
                    __doPostBack(boton.name, '');
                }
            });

            return false;
        }
    </script>

</asp:Content>
