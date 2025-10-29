<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registrarse.aspx.cs" Inherits="WebAppEcommerce.Registrarse" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <script>    
        function ValidarForm(form) {
            form.classList.add('was-validated');
            return form.checkValidity();
        }
    </script>


    <br />
    <h1>INGRESA TUS DATOS</h1>
    <br />

    <div class="row g-3">
        <div class=" col-md-4">
            <asp:TextBox ID="txtDni" ClientIDMode="Static" CssClass="form-control" onkeypress="return soloNumeros(event);" AutoPostBack="true" OnTextChanged="txtDni_TextChanged" placeholder="DNI" aria-label="DNI" runat="server" required="required" />
            <div class="invalid-feedback">Ingresá un DNI válido.</div>
        </div>
        <div class="row g-3">
            <div class="col-3">
                <asp:TextBox ID="txtNombre" ClientIDMode="Static" CssClass="form-control" onkeypress="return soloLetras(event);" placeholder="Nombre" aria-label="Nombre" runat="server" required="required" />
                <div class="invalid-feedback">El nombre es obligatorio.</div>
            </div>
        </div>
        <div class="col-3">
            <asp:TextBox ID="txtApellido" ClientIDMode="Static" CssClass="form-control" onkeypress="return soloLetras(event);" placeholder="Apellido" aria-label="Apellido" runat="server" required="required" />
            <div class="invalid-feedback">El apellido es obligatorio.</div>
        </div>
        <div class="col-md-4">
            <asp:TextBox ID="txtEmail" ClientIDMode="Static" CssClass="form-control" onkeypress="return validarEmail(event);" placeholder="Email" aria-label="Email" runat="server" TextMode="Email" required="required" />
            <div class="invalid-feedback">Ingresá un email válido.</div>
        </div>
        <div class="col-8">
            <label for="txtDireccion" class="form-label">Direccion</label>
            <asp:TextBox ID="txtDireccion" ClientIDMode="Static" CssClass="form-control" onkeypress="return validarDireccion(event);" placeholder="Direcion" aria-label="Direccion" runat="server" required="required" />
            <div class="invalid-feedback">La dirección es obligatoria.</div>
        </div>
        <div class="col-md-6">
            <label for="txtCiudad" class="form-label" >Ciudad</label>
            <asp:TextBox ID="txtCiudad" ClientIDMode="Static" CssClass="form-control" onkeypress="return soloLetras(event);" placeholder="Ciudad" aria-label="Ciudad" runat="server" required="required" />
            <div class="invalid-feedback">La ciudad es obligatoria.</div>
        </div>
        <div class="col-md-2">
            <label for="txtCP" class="form-label">C.P</label>
            <asp:TextBox ID="txtCP" ClientIDMode="Static" CssClass="form-control" onkeypress="return soloNumeros(event);" placeholder="C.P" aria-label="CP" runat="server" required="required" />
            <div class="invalid-feedback">Ingresá el código postal.</div>
        </div>
        <div class="col-12">
            <asp:Button Text="Registrarse"  CssClass="btn btn-primary" ID="btnRegistrarse"  runat="server" />
        </div>
    </div>

    <script type="text/javascript">
        function soloLetras(e) {
            var tecla = e.key;
            var regex = /^[a-zA-Z\s]$/;
            return regex.test(tecla);
        }
    </script>

    <script type="text/javascript">
        function soloNumeros(e) {
            var tecla = e.key;
            var regex = /^[0-9]$/;
            return regex.test(tecla);
        }
    </script>

    <script type="text/javascript">
        function validarDireccion(e) {
            var tecla = e.key;
            var regex = /^[a-zA-Z0-9\s]$/;
            return regex.test(tecla);
        }
    </script>

    <script type="text/javascript">
        function validarEmail(e) {
            var tecla = e.key;
            var regex = /^[a-zA-Z0-9@._-]$/;
            return regex.test(tecla);
        }
    </script>




</asp:Content>





