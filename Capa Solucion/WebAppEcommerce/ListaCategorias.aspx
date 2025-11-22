<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaCategorias.aspx.cs" Inherits="WebAppEcommerce.ListaCategorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="dgvCategorias" cssclass="table table-striped"  runat="server" DataKeyNames="IdCategoria"  AutoGenerateColumns="false"  
        OnSelectedIndexChanged="dgvCategorias_SelectedIndexChanged" OnPageIndexChanging="dgvCategorias_PageIndexChanging" 
        AllowPaging="true" PageSize="5">
        <Columns>   
            <asp:BoundField  HeaderText="ID"  DataField="IdCategoria"/>
            <asp:BoundField  HeaderText="Nombre de Categoria"  DataField="Nombre"/>
            <asp:BoundField  HeaderText="Estado"  DataField="Estado" Visible="false"/>
            <asp:CommandField  HeaderText="Modificar"  ShowSelectButton="true" SelectText="✏️"/>

        </Columns>        

    </asp:GridView>
       

    <div>
        <asp:Button runat="server" Text="Agregar" ID="btnAgregar" OnClick="btnAgregar_Click"  CssClass="btn btn-primary" />
    </div>
    
</asp:Content>
