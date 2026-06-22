<%@ Page Title="Ingresar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="eCommerce.Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <div class="row justify-content-center">
            <div class="col-md-6 col-lg-5">
                <h1 class="h3 mb-3">Ingreso de usuario</h1>
                <div class="mb-3">
                    <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="form-label" Text="Email" />
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" />
                </div>
                <div class="mb-3">
                    <asp:Label runat="server" AssociatedControlID="txtClave" CssClass="form-label" Text="Clave" />
                    <asp:TextBox runat="server" ID="txtClave" CssClass="form-control" TextMode="Password" />
                </div>
                <asp:Label runat="server" ID="lblError" CssClass="alert alert-danger d-block" Visible="false" />
                <div class="d-flex gap-2">
                    <asp:Button runat="server" ID="btnIngresar" CssClass="btn btn-primary" Text="Ingresar" OnClick="btnIngresar_Click" />
                    <a runat="server" href="~/Registro" class="btn btn-outline-secondary">Crear cuenta</a>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
