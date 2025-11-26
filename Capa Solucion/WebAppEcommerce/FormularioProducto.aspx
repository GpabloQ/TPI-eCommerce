<%@ Page Title="Formulario de Producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormularioProducto.aspx.cs" Inherits="WebAppEcommerce.FormularioProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <%--Libreria SweetAlert2 para mensajes emergentes--%>
    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>


    <main>
        <h2>Formulario de Producto</h2>
        <h4><asp:Label ID="lblModo" runat="server" /></h4>
        <hr />

        <div class="form-group">
            <label>Codigo:</label>
            <asp:TextBox ID="txtCodigo" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Nombre:</label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
        </div>

        <div class="form-group">
            <label>Descripcion:</label>
            <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" />
        </div>

        <div class="form-group">
            <label>Precio:</label>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" />
        </div>



        <h4>IMAGENES ACTUALES</h4>

    <asp:Repeater ID="repImagenes" runat="server" OnItemCommand="repImagenes_ItemCommand">
        <ItemTemplate>
            <div style="display:flex; gap:12px; align-items:center; margin-bottom:10px;">
                <img src='<%# Eval("UrlImagen") %>'
                     style="width:100px;height:100px;border-radius:6px;object-fit:cover;" />

                <asp:LinkButton ID="btnEliminarImg"
                    runat="server"
                    Text="🗑️"
                    CssClass="btn btn-danger btn-sm"
                    CommandName="EliminarImg"
                    CommandArgument='<%# Eval("IdImagen") %>'
                    OnClientClick="return confirmarEliminacion(this);" />
            </div>
        </ItemTemplate>
    </asp:Repeater>
        <h4>Agregar IMAGENES</h4>
        <div class="form-group">
            <label>URL Imagen:</label>
            <div class="input-group">
                <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" />
                <asp:Button ID="btnVerImagen" runat="server"
                            CssClass="btn btn-info"
                            Text="VER IMAGEN"
                            OnClick="btnVerImagen_Click" />
            </div>
        </div>           
        <!-- Vista previa de la imagen -->
        <div class="mt-3">
            <asp:Image ID="imgPreview" runat="server"
                       Width="220px"
                       Height="220px"
                       Style="object-fit:cover; border-radius:8px; border:1px solid #ddd; display:none;" />
        </div>
        <div class="form-group">
            <label>Marca:</label>
            <asp:DropDownList ID="ddlMarca" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>

        <div class="form-group">
            <label>Categoria:</label>
            <asp:DropDownList ID="ddlCategoria" runat="server" CssClass="form-control"></asp:DropDownList>
        </div>

        <div style="margin-top:20px;">
            <asp:Button ID="btnAceptar" 
            runat="server" 
            Text="GUARDAR" 
            CssClass="btn btn-primary"
            OnClick="btnAceptar_Click"
            OnClientClick="return confirmarGuardado();" />

            <asp:Button ID="btnCancelar" runat="server" Text="CANCELAR" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" CausesValidation="false" />
        </div>
        <div class ="lblError">
            <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        </div>
       
        <script>
            function confirmarGuardado() {
                return Swal.fire({
                    title: '¿Confirmar guardado?',
                    text: 'Se va a guardar el articulo.',
                    icon: 'question',
                    showCancelButton: true,
                    confirmButtonText: 'Sí, guardar',
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {
                
                        __doPostBack('<%= btnAceptar.UniqueID %>', '');
                    }
                }), false; 
            }
        
         </script>
        <script type="text/javascript">
            function confirmarEliminacion(boton) {
                event.preventDefault();

                Swal.fire({
                    title: "¿Eliminar Imagen?",
                    text: "Esta acción no se puede deshacer",
                    icon: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#d33",
                    cancelButtonColor: "#3085d6",
                    confirmButtonText: "Sí, eliminar",
                    cancelButtonText: "Cancelar"
                }).then((result) => {
                    if (result.isConfirmed) {

                        var href = boton.getAttribute("href");
                        var match = href && href.match(/__doPostBack\('([^']+)'/);

                        if (match && match[1]) {
                            __doPostBack(match[1], "");
                        }
                    }
                });
                return false;
            }
        </script>
    </main>
</asp:Content>
