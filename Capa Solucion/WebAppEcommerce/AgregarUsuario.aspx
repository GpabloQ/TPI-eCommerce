<%@ Page Title="Usuario" Language="C#" MasterPageFile="~/Site.Master"
         AutoEventWireup="true"
         CodeBehind="AgregarUsuario.aspx.cs"
         Inherits="WebAppEcommerce.AgregarUsuario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

<h2><asp:Label ID="lblTitulo" runat="server" Text="Nuevo Usuario"></asp:Label></h2>
<hr />

<div class="container col-md-6">

    <!-- NOMBRE -->
    <div class="mb-3">
        <label>Nombre:</label>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
    </div>

    <!-- APELLIDO -->
    <div class="mb-3">
        <label>Apellido:</label>
        <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" />
    </div>

    <!-- MAIL -->
    <div class="mb-3">
        <label>Email:</label>
        <asp:TextBox ID="txtMail" runat="server" CssClass="form-control" />
    </div>

    <!-- TELEFONO -->
    <div class="mb-3">
        <label>Telefono:</label>
        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" />
    </div>

    <!-- DNI -->
    <div class="mb-3">
        <label>DNI:</label>
        <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" />
    </div>

    <!-- FECHA NACIMIENTO -->
    <div class="mb-3">
        <label>Fecha Nacimiento:</label>
        <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="form-control" />
    </div>

    <!-- CONTRASEÑA -->
    <div class="mb-3" id="divPass" runat="server">
        <label>Contraseña:</label>
        <asp:TextBox ID="txtContrasenia" runat="server" TextMode="Password" CssClass="form-control" />
    </div>

    <!-- TIPO DE USUARIO -->
    <div class="mb-3">
        <label>Tipo Usuario:</label>
        <asp:DropDownList ID="ddlTipoUsuario" runat="server" CssClass="form-select">
            <asp:ListItem Value="1">ADMINISTRADOR</asp:ListItem>
            <asp:ListItem Value="2">CLIENTE</asp:ListItem>
        </asp:DropDownList>
    </div>

    <!-- BOTONES -->
    <asp:Button ID="btnAceptar" runat="server" CssClass="btn btn-success" Text="Guardar" OnClick="btnAceptar_Click" />
    <asp:Button ID="btnCancelar" runat="server" CssClass="btn btn-secondary" Text="Cancelar" OnClick="btnCancelar_Click" />

    <br /><br />
    <asp:Label ID="lblMensaje" runat="server" ForeColor="Green" Visible="false"></asp:Label>

</div>

</asp:Content>

