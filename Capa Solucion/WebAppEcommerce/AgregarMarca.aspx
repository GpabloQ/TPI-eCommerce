<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarMarca.aspx.cs" Inherits="WebAppEcommerce.AgregarMarca" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .botonera {
            display: flex;
            gap: 8px; /* espacio entre los hijos */
        }

        .checkbox-align {
            vertical-align: middle;
        }
    </style>

    <h2 id="titulo" runat="server"></h2>

    <div class="mb-3">
        <label>ID</label>
        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
    </div>

    <div class="mb-3">
        <label>Nombre</label>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
    </div>

    <asp:Label ID="lblError"
        runat="server"
        ForeColor="Red"
        Font-Bold="true"
        Visible="false"
        CssClass="mt-2 d-block">
    </asp:Label>


    <div class="d-flex gap-2">
        <asp:Button ID="btnAceptar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnAceptar_Click" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />

        <asp:Button ID="btnEliminarMarca"
            runat="server"
            Text="Eliminar"
            CssClass="btn btn-danger"
            Visible="false"
            OnClientClick="return confirmarEliminacion();" />
    </div>

    
    <div class="mt-3" id="panelConfirmacion" runat="server" visible="false">
        <label>Confirma eliminar</label>
        <asp:CheckBox ID="chkConfirma" runat="server" />
        <asp:Button ID="btnEliminar" runat="server"
            Text="Confirmar"
            CssClass="btn btn-danger"
            OnClick="btnEliminar_Click" />
    </div>




<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

<script type="text/javascript">
    function confirmarEliminacion() {

        Swal.fire({
            title: "¿Eliminar marca?",
            text: "Esta acción no se puede deshacer.",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar"
        }).then((result) => {
            if (result.isConfirmed) {
                __doPostBack('<%= btnEliminar.UniqueID %>', '');
            }
        });

        return false; // Previene postback automático
    }
</script>


</asp:Content>

