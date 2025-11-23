<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarMarcas.aspx.cs" Inherits="WebAppEcommerce.ListarMarcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



    <h2>Listado de marcas.</h2>
    <br />
    <%--  Listar usando SP --%>
    <asp:GridView ID="dgvMarcas" runat="server" CssClass="table table-striped" AutoGenerateColumns="false"
        DataKeyNames="IdMarca"
        AllowPaging="true"
        PageSize="5"
        PagerSettings-Mode="Numeric"
        PagerStyle-CssClass="pager-numeros"
        OnPageIndexChanging="dgvMarcas_PageIndexChanging">

        <Columns>
            <asp:BoundField HeaderText="Nombre de la marca" DataField="Nombre" />
            <asp:BoundField HeaderText="Id" DataField="IdMarca" Visible="false" />
            <asp:BoundField HeaderText="Estado" DataField="Estado" Visible="false" />
            <asp:CommandField HeaderText="Modificar" ShowSelectButton="true" SelectText="✏️" />

        </Columns>
    </asp:GridView>

    <div class="botonera">
        <asp:Button runat="server" CssClass="btn btn-primary mt-3" Text="Agregar" OnClick="btnAgregarMarca_Click" ID="btnAgregarMarca" />
    </div>
    <br />

</asp:Content>
