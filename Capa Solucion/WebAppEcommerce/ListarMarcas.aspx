<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListarMarcas.aspx.cs" Inherits="WebAppEcommerce.ListarMarcas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">



         
<%--  Listar usando SP --%>
        <asp:GridView ID="dgvMarcas" runat="server" CssClass="table" AutoGenerateColumns="false">

            <Columns>
                <asp:BoundField HeaderText="Nombre de la marca" DataField="Nombre" />
                <asp:BoundField HeaderText="Estado" DataField="Estado" />
                
            </Columns>
            
        </asp:GridView>

</asp:Content>
