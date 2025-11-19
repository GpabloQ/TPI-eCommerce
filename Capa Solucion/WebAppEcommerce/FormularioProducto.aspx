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

        <div class="form-group">
            <label>URL Imagen:</label>
            <asp:TextBox ID="txtUrlImagen" runat="server" CssClass="form-control" />
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


    </main>
</asp:Content>
