<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaCategorias.aspx.cs" Inherits="WebAppEcommerce.ListaCategorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="dgvCategorias" runat="server" AutoGenerateColumns="false"
        CssClass="table"
        DataKeyNames="IdCategoria"
        AllowPaging="true"
        PageSize="8"
        OnPageIndexChanging="dgvCategorias_PageIndexChanging"
        OnSelectedIndexChanged="dgvCategorias_SelectedIndexChanged"
        OnRowCommand="dgvCategorias_RowCommand">

        <Columns>
            <asp:BoundField HeaderText="Categoría" DataField="Nombre" />

            <asp:CommandField HeaderText="Editar"
                ShowSelectButton="true"
                SelectText="✏️" />

            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEliminarCat"
                        runat="server"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("IdCategoria") %>'
                        CssClass="icon-btn"
                        OnClientClick='<%# "confirmarEliminacionCategoria(this, \"" + Eval("IdCategoria") + "\"); return false;" %>'>
                    🗑️
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>

    <br />

    <div class="botonera">
        <asp:Button ID="btnAgregar" runat="server"
            CssClass="btn btn-primary"
            Text="Agregar"
            OnClick="btnAgregar_Click" />
    </div>


    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script type="text/javascript">
        function confirmarEliminacionCategoria(boton, id) {
            event.preventDefault();

            Swal.fire({
                title: "¿Eliminar categoría?",
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
                        var postBackID = match[1];
                        __doPostBack(postBackID, "");
                    }
                }
            });
            return false;
        }
    </script>





</asp:Content>
