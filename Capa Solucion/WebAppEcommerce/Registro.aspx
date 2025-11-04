<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="WebAppEcommerce.Registro" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    
        <div class="container">
            <div class="row">
                <!-- Columna izquierda -->
                <div class="col-md-6">
                    <div class="mb-3">
                        <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombre" Text="Nombre:" />
                        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Nombre" />
                    </div>
                    <div class="mb-3">
    <asp:Label ID="lblApellido" runat="server" AssociatedControlID="txtApellido" Text="Apellido:" />
    <asp:TextBox ID="txtApellido" runat="server" CssClass="form-control" placeholder="Apellido" />
</div>
<div class="mb-3">
    <asp:Label ID="lblEmail" runat="server" Text="Email:" />
    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="juan@correo.com" />
</div>
                      <div class="mb-3">
      <asp:Label ID="lblFechaNacimiento" runat="server" Text="Fecha de Nacimiento:" />
      <asp:TextBox ID="txtFechaNacimiento" runat="server" TextMode="Date" CssClass="form-control" />
  </div>
<div class="mb-3">
    <asp:Label ID="lblDNI" runat="server" Text="DNI:" />
    <asp:TextBox ID="txtDNI" runat="server" CssClass="form-control" placeholder="DNI sin puntos" />
</div>
                  
                    <div class="mb-3">
                        <asp:Label ID="lblTelefono" runat="server" Text="Teléfono:" />
                        <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" placeholder="Incluir el código de área" />
                    </div>
                   
                </div>

                <!-- Columna derecha -->
                <div class="col-md-6">
                     <div class="mb-3">
     <asp:Label ID="lblProvincia" runat="server" AssociatedControlID="txtProvincia" Text="Provincia:" />
     <asp:TextBox ID="txtProvincia" runat="server" CssClass="form-control" placeholder="Provincia" />
 </div>
                    <div class="mb-3">
                        <asp:Label ID="lblCiudad" runat="server" AssociatedControlID="txtCiudad" Text="Ciudad:" />
                        <asp:TextBox ID="txtCiudad" runat="server" CssClass="form-control" placeholder="Ciudad" />
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="lblCodigoPostal" runat="server" AssociatedControlID="txtCodigoPostal" Text="Código postal:" />
                        <asp:TextBox ID="txtCodigoPostal" runat="server" CssClass="form-control" placeholder="Código Postal" />
                    </div>
                </div>
            </div>

            <asp:Button ID="btnRegistrarse" runat="server" CssClass="btn btn-primary mt-3" Text="Registrarse" />
        </div>
    




</asp:Content>
