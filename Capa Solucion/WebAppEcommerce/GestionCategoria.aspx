<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionCategoria.aspx.cs" Inherits="WebAppEcommerce.GestionCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 id="titulo" runat="server"></h2>

    <div class="mb-3">
        <label>ID</label>
        <asp:TextBox ID="txtId" runat="server" CssClass="form-control" Enabled="false" Visible="false" />
    </div>

    <div class="mb-3">
        <label>Nombre</label>
        <asp:TextBox ID="txtNombreCategoria" runat="server" CssClass="form-control" />
    </div>

    <div class="d-flex gap-2">
        <asp:Button ID="btnAgregar" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnAgregar_Click" />
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click" />

        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar"
            CssClass="btn btn-danger"
            Visible="false"
            OnClick="btnEliminar_Click" />
    </div>

    <div class="mt-3" id="panelConfirmacion" runat="server" visible="false">
        <label>Confirma eliminar</label>
        <asp:CheckBox ID="chkConfirmaEliminacion" runat="server" />
        <asp:Button ID="btnConfirmacion" runat="server"
            Text="Confirmar"
            CssClass="btn btn-danger"
            OnClick="btnConfirmacion_Click" />
    </div>



</asp:Content>
