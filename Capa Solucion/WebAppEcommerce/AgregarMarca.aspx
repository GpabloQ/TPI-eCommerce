<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AgregarMarca.aspx.cs" Inherits="WebAppEcommerce.AgregarMarca" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
        .botonera {
          display: flex;
          gap: 8px; /* espacio entre los hijos */
        }

    </style>
    <asp:Label ID="lblTitulo" runat="server" CssClass="h2"></asp:Label>
    <br />
    <br />
    <asp:Label ID="lblNombreMarca" runat="server" Text="Nombre de la marca:"></asp:Label>
    <br />
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <br />
    <div class="botonera">
    <asp:Button ID="btnAceptarMarca" runat="server"  CssClass="btn btn-primary mt-3" Text="Aceptar" OnClick="btnAceptar_Click"   />  
    <asp:Button ID="btnCancelarMarca" runat="server"  CssClass="btn btn-primary mt-3" Text="Cancelar" OnClick="btnCancelar_Click"   />
    </div>
       <div class="botonera">
       <asp:Button ID="btnEliminarMarca" runat="server" Text="Eliminar" CssClass="btn btn-danger mt-3" OnClick="btnEliminarMarca_Click"/>
       </div>
   <br />


      <%if (ConfirmaEliminacion) {  %>       
  <div class="botonera">
  <asp:CheckBox ID="ChkConfirmaEliminacion" runat="server" Text="Confirmar Eliminación" />
  <asp:Button ID="btnConfirmarEliminar" runat="server" Text="Eliminar" CssClass="btn btn-outline-danger mt-3"
      OnClick="btnEliminar_Click"/>
  </div>
       <%  } %>
</asp:Content>
