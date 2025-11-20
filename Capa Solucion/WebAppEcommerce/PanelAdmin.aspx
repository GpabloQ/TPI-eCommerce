<%@ Page Title="Panel Administrativo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PanelAdmin.aspx.cs" Inherits="WebAppEcommerce.PanelAdmin" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <script src="https://cdn.jsdelivr.net/npm/sweetalert2@11"></script>

    <div class="container mt-4">

        <h2 class="mb-4">Panel Administrativo</h2>

        <div class="row">

            <!-- Gestión de Usuarios -->
            <div class="col-md-3 mb-3">
                <a href="GestionUsuarios.aspx" class="btn btn-primary w-100 p-3">
                    Gestión de Usuarios
                </a>
            </div>

            <!-- Gestión de Productos -->
            <div class="col-md-3 mb-3">
                <a href="GestionProductos.aspx" class="btn btn-primary w-100 p-3">
                    Gestión de Productos
                </a>
            </div>

            <!-- Gestión de Marcas -->
            <div class="col-md-3 mb-3">
                <a href="ListarMarcas.aspx" class="btn btn-primary w-100 p-3">
                    Gestión de Marcas
                </a>
            </div>

            <!-- Gestión de Categorías -->
            <div class="col-md-3 mb-3">
                <a href="ListaCategorias.aspx" class="btn btn-primary w-100 p-3">
                    Gestión de Categorías
                </a>
            </div>

        </div>
    </div>

</asp:Content>
