<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarMarca.aspx.cs" Inherits="WebAppEcommerce.AgregarMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:Label ID="lblNombreMarca" runat="server" Text="Nombre de la marca:"></asp:Label>
    <br />
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br />  
    <br />
    <asp:Button ID="btnAgregarMarca" runat="server"  CssClass="btn btn-primary mt-3" Text="Agregar" OnClick="btnAgregar_Click"   />
</asp:Content>
