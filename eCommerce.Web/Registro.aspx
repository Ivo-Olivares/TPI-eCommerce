<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="eCommerce.Web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <h1 class="h3 mb-3">Registro de cliente</h1>
        <div class="row g-3">
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="form-label" Text="Nombre" />
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtApellido" CssClass="form-label" Text="Apellido" />
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="form-label" Text="Email" />
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtTelefono" CssClass="form-label" Text="Telefono" />
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtClave" CssClass="form-label" Text="Clave" />
                <asp:TextBox runat="server" ID="txtClave" CssClass="form-control" TextMode="Password" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtConfirmarClave" CssClass="form-label" Text="Confirmar clave" />
                <asp:TextBox runat="server" ID="txtConfirmarClave" CssClass="form-control" TextMode="Password" />
            </div>
        </div>

        <h2 class="h5 mt-4">Direccion principal</h2>
        <div class="row g-3">
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtCalle" CssClass="form-label" Text="Calle" />
                <asp:TextBox runat="server" ID="txtCalle" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <asp:Label runat="server" AssociatedControlID="txtAltura" CssClass="form-label" Text="Numero" />
                <asp:TextBox runat="server" ID="txtAltura" CssClass="form-control" TextMode="Number" />
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" AssociatedControlID="txtLocalidad" CssClass="form-label" Text="Localidad" />
                <asp:TextBox runat="server" ID="txtLocalidad" CssClass="form-control" />
            </div>
            <div class="col-md-4">
                <asp:Label runat="server" AssociatedControlID="txtProvincia" CssClass="form-label" Text="Provincia" />
                <asp:TextBox runat="server" ID="txtProvincia" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="txtCp" CssClass="form-label" Text="Codigo postal" />
                <asp:TextBox runat="server" ID="txtCp" CssClass="form-control" />
            </div>
            <div class="col-md-5">
                <asp:Label runat="server" AssociatedControlID="txtObservaciones" CssClass="form-label" Text="Observaciones" />
                <asp:TextBox runat="server" ID="txtObservaciones" CssClass="form-control" />
            </div>
        </div>

        <div class="mt-4">
            <asp:Button runat="server" ID="btnRegistrarse" CssClass="btn btn-primary" Text="Registrarse" />
        </div>
    </main>
</asp:Content>
