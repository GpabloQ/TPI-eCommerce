<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionCategoria.aspx.cs" Inherits="WebAppEcommerce.GestionCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-2">

        <div class="col-md-3">
            <asp:TextBox runat="server" ID="txtId" CssClass="form-control" placeholder="ID" ReadOnly="true" />
        </div>
            
        <asp:TextBox runat="server" ID="txtNombreCategoria" CssClass="form-control" placeholder="Nombre de Categoria" />
    

        </div>

        
        <div class="col-4 ">

            <asp:Button runat="server" Text="AGREGAR" ID="btnAgregar" OnClick="btnAgregar_Click"  CssClass="btn btn-outline-success" />

            <asp:Button runat="server" Text="MODIFICAR" ID="btnModificar" OnClick="btnModificar_Click" CssClass="btn btn-outline-primary" />

            <asp:Button runat="server" Text="ELIMINAR" ID="btnEliminar" OnClick="btnEliminar_Click"  CssClass="btn btn-outline-danger" />


        </div>
</asp:Content>
