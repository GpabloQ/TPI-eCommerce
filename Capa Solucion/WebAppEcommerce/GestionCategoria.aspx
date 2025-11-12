<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionCategoria.aspx.cs" Inherits="WebAppEcommerce.GestionCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col-3">
            <div class="mb-3">

                <div class="col-2">
                    <asp:TextBox runat="server" ID="txtId" CssClass="form-control" placeholder="ID" ReadOnly="true" />
                </div>
                <asp:TextBox runat="server" ID="txtNombreCategoria" CssClass="form-control" placeholder="Nombre de Categoria" />
            </div>
        </div>

    </div>

    <div class="row-2">

        <div class="col-3">

            <div class="mb-4">

                <asp:Button runat="server" Text="AGREGAR" ID="btnAgregar" OnClick="btnAgregar_Click" CssClass="btn btn-outline-primary" />
                <asp:Button runat="server" Text="CANCELAR" ID="btnCancelar" OnClick="btnCancelar_Click" CssClass="btn btn-outline-primary" />

            </div>

            <div class="mb-3">

                <asp:Button runat="server" Text="MODIFICAR" ID="btnModificar" OnClick="btnModificar_Click" CssClass="btn btn-outline-warning" />

                <asp:Button runat="server" Text="ELIMINAR" ID="btnEliminar" OnClick="btnEliminar_Click" CssClass="btn btn-outline-danger" />

            </div>


        </div>
    </div>

    <div class="col-3">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <%if (ConfirmarEliminacion)
                    {%>
                <div class="mb-3">
                    <asp:CheckBox Text="Confirmar Eliminacion" ID="chkConfimaEliminacion" runat="server" />
                    <asp:Button runat="server" Text="ELIMINAR" ID="btnConfirmacion" OnClick="btnConfirmacion_Click" CssClass="btn btn-outline-danger" />

                </div>
                <%} %>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>



</asp:Content>
