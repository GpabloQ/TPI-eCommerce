<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarMarcas.aspx.cs" Inherits="WebAppEcommerce.ListarMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <h2>Listado de marcas.</h2>
    <br />
    <%--  Listar usando SP --%>
    <asp:GridView ID="dgvMarcas" runat="server" AutoGenerateColumns="false"
        CssClass="table"
        DataKeyNames="IdMarca"
        AllowPaging="true"
        PageSize="8"
        OnPageIndexChanging="dgvMarcas_PageIndexChanging"
        OnSelectedIndexChanged="dgvMarcas_SelectedIndexChanged"
        OnRowCommand="dgvMarcas_RowCommand">

        <Columns>

         
            <asp:BoundField HeaderText="Marca" DataField="Nombre" />

           
            <asp:CommandField HeaderText="Editar"
                ShowSelectButton="true"
                SelectText="✏️" />

          
            <asp:TemplateField HeaderText="Eliminar">
                <ItemTemplate>
                    <asp:LinkButton ID="btnEliminarMarca"
                        runat="server"
                        CommandName="Eliminar"
                        CommandArgument='<%# Eval("IdMarca") %>'
                        CssClass="icon-btn"
                        OnClientClick='<%# "confirmarEliminacionMarca(this, \"" + Eval("IdMarca") + "\"); return false;" %>'>
                    🗑️
                    </asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>

    </asp:GridView>


    <br />

    <div class="botonera">
        <asp:Button ID="btnAgregarMarca" runat="server"
            CssClass="btn btn-primary"
            Text="Agregar"
            OnClick="btnAgregarMarca_Click" />
    </div>


    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <script type="text/javascript">
        function confirmarEliminacionMarca(boton, id) {
            event.preventDefault();

            Swal.fire({
                title: "¿Eliminar marca?",
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
