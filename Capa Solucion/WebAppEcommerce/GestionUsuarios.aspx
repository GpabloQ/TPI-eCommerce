<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="GestionUsuarios.aspx.cs" Inherits="WebAppEcommerce.GestionUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
 
<h2>Listado de usuarios</h2>
<br />

<asp:GridView ID="dgvUsuarios" runat="server" AutoGenerateColumns="false"
              CssClass="table"
              DataKeyNames="IdUsuario"
              AllowPaging="true"
              PageSize="8"
              OnPageIndexChanging="dgvUsuarios_PageIndexChanging"
              OnSelectedIndexChanged="dgvUsuarios_SelectedIndexChanged"
              OnRowCommand="dgvUsuarios_RowCommand">

    <Columns>

        
        <asp:BoundField HeaderText="Usuario" DataField="NombreCompleto" />       
        <asp:BoundField HeaderText="Tipo" DataField="TipoUsuarioNombre" />        
        <asp:CommandField HeaderText="Editar"
                          ShowSelectButton="true"
                          SelectText="✏️" />
       
        <asp:TemplateField HeaderText="Eliminar">
            <ItemTemplate>
                <%-- Llama a la función de JS para mostrar el msj de confirmacion antes de eliminar --%>
                <asp:LinkButton ID="btnEliminar"
                    runat="server"
                    CommandName="Eliminar"
                    CommandArgument='<%# Eval("IdUsuario") %>'
                    CssClass="icon-btn"                    
                    OnClientClick='<%# "confirmarEliminacion(this, \"" + Eval("IdUsuario") + "\"); return false;" %>'>🗑️        
                </asp:LinkButton>


            </ItemTemplate>
        </asp:TemplateField>

    </Columns>

</asp:GridView>

<br />

<div class="botonera">
    <asp:Button ID="btnAgregarUsuario" runat="server"
                CssClass="btn btn-primary"
                Text="Agregar"
                OnClick="btnAgregarUsuario_Click" />
</div>


<script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>
    <%--FRAN, BALTA Investigando encontre esta libreria de JS SweetAlert: muestra ventanas emergentes  y permite confirmar acciones --%>
    <%--Ventana de JS para mostrar el mensaje de confirmación antes de eliminar un usuario--%>
<script type="text/javascript">
    function confirmarEliminacion(boton, id) {
        event.preventDefault();

        Swal.fire({
            title: "¿Eliminar usuario?",
            text: "Esta acción no se puede deshacer",
            icon: "warning",
            showCancelButton: true,
            confirmButtonColor: "#d33",
            cancelButtonColor: "#3085d6",
            confirmButtonText: "Sí, eliminar",
            cancelButtonText: "Cancelar"
        }).then((result) => {
            if (result.isConfirmed) {

                // toma el href del LinkButton
                var href = boton.getAttribute("href");                
                var match = href && href.match(/__doPostBack\('([^']+)'/);

                if (match && match[1]) {
                    var postBackID = match[1];
                    // No hace falta pasar el id aca: el CommandArgument lo aporta el LinkButton
                    __doPostBack(postBackID, "");
                }
            }
        });
        return false;
    }
</script>


</asp:Content>
