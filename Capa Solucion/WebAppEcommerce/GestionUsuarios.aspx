<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" 
    CodeBehind="GestionUsuarios.aspx.cs" Inherits="WebAppEcommerce.GestionUsuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<style>
    .botonera {
        display: flex;
        gap: 16px;
    }
    .icon-btn {
        font-size: 20px;
        cursor: pointer;
        text-decoration: none;
        background: none;
        border: none;
    }
</style>

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
                <asp:LinkButton ID="btnEliminar"
                                runat="server"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("IdUsuario") %>'
                                CssClass="icon-btn"
                                OnClientClick='return confirmarEliminacion(<%# Eval("IdUsuario") %>, this);'>
                    🗑️
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

<script type="text/javascript">
    function confirmarEliminacion(id, boton) {
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

                
                let postBackID = boton.id.replace(/_/g, "$");

                __doPostBack(postBackID, id);
            }
        });

        return false;
    }
</script>

</asp:Content>
