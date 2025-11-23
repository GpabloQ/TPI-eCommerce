<%@ Page Title="Inicio de sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signin.aspx.cs" Inherits="WebAppEcommerce.Signin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="Content/sign-in.css" rel="stylesheet" />    


    <main class="form-signin w-100 m-auto">
        <div runat="server">
            <div style="text-align:center">
            <svg xmlns="http://www.w3.org/2000/svg" width="80" height="80" fill="currentColor" class="bi bi-people-fill" viewBox="0 0 16 16">
                <path d="M7 14s-1 0-1-1 1-4 5-4 5 3 5 4-1 1-1 1zm4-6a3 3 0 1 0 0-6 3 3 0 0 0 0 6m-5.784 6A2.24 2.24 0 0 1 5 13c0-1.355.68-2.75 1.936-3.72A6.3 6.3 0 0 0 5 9c-4 0-5 3-5 4s1 1 1 1zM4.5 8a2.5 2.5 0 1 0 0-5 2.5 2.5 0 0 0 0 5" />
            </svg>
                </div>

            <h1 class="h3 mb-3 fw-normal">Por favor inicia sesión</h1>

            <div class="form-floating">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="name@example.com" />
                <label for="txtEmail">Ingrese email</label>
            </div>

            <div class="form-floating">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password" />
                <label for="txtPassword">Contraseña</label>
            </div>
                <p class="text-center mt-3">
                  ¿No tenés cuenta?
                  <a href="AgregarUsuario.aspx">Registrate acá</a>
                </p>

            <asp:Button ID="btnLogin" runat="server" Text="Iniciar sesión" CssClass="btn btn-primary w-100 py-2" OnClick="btnLogin_Click" />

            <p class="mt-5 mb-3 text-body-secondary">&copy; 2017–2025</p>
        </div>
    </main>
</asp:Content>
