<%@ Page Title="Inicio de sesión" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Signin.aspx.cs" Inherits="WebAppEcommerce.Signin" %>



<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="form-signin w-100 m-auto">
        <form runat="server">
            <img class="mb-4" src="../assets/brand/bootstrap-logo.svg" alt="" width="72" height="57" />
            <h1 class="h3 mb-3 fw-normal">Please sign in</h1>

            <div class="form-floating">
                <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="name@example.com" />
                <label for="txtEmail">Email address</label>
            </div>

            <div class="form-floating">
                <asp:TextBox ID="txtPassword" runat="server" CssClass="form-control" TextMode="Password" placeholder="Password" />
                <label for="txtPassword">Password</label>
            </div>

            <div class="form-check text-start my-3">
                <asp:CheckBox ID="chkRemember" runat="server" CssClass="form-check-input" />
                <label class="form-check-label" for="chkRemember">Remember me</label>
            </div>

            <asp:Button ID="btnLogin" runat="server" Text="Sign in" CssClass="btn btn-primary w-100 py-2" OnClick="btnLogin_Click" />

            <p class="mt-5 mb-3 text-body-secondary">&copy; 2017–2025</p>
        </form>
    </main>
</asp:Content>
