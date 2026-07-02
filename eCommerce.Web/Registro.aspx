<%@ Page Title="Registro" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="eCommerce.Web.Registro" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-card mb-4 text-center">
            <h1 id="tituloRegistro" class="app-title mb-2 fs-2">Crear cuenta</h1>

            <p class="app-subtitle mb-0 fs-6">Registrate para comprar y consultar tus pedidos.</p>
        </section>

        <div class="row justify-content-center">
            <div class="col-lg-9">

                <section class="app-card mb-4">
                    <h2 class="app-card-title mb-4">Datos personales
                    </h2>

                    <div class="row g-3">
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="app-form-label" Text="Nombre" />
                            <asp:TextBox runat="server" ID="txtNombre" CssClass="app-input" />
                        </div>

                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtApellido" CssClass="app-form-label" Text="Apellido" />
                            <asp:TextBox runat="server" ID="txtApellido" CssClass="app-input" />
                        </div>

                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtDni" CssClass="app-form-label" Text="DNI" />
                            <asp:TextBox runat="server" ID="txtDni" CssClass="app-input" />
                        </div>

                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtFechaNacimiento" CssClass="app-form-label" Text="Fecha de nacimiento" />
                            <asp:TextBox runat="server" ID="txtFechaNacimiento" CssClass="app-input" TextMode="Date" />
                        </div>

                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="app-form-label" Text="Email" />
                            <asp:TextBox runat="server" ID="txtEmail" CssClass="app-input" TextMode="Email" />
                        </div>

                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtTelefono" CssClass="app-form-label" Text="Telefono" />
                            <asp:TextBox runat="server" ID="txtTelefono" CssClass="app-input" />
                        </div>

                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtClave" CssClass="app-form-label" Text="Clave" />
                            <asp:TextBox runat="server" ID="txtClave" CssClass="app-input" TextMode="Password" />
                        </div>

                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtConfirmarClave" CssClass="app-form-label" Text="Confirmar clave" />
                            <asp:TextBox runat="server" ID="txtConfirmarClave" CssClass="app-input" TextMode="Password" />
                        </div>
                    </div>
                </section>

                <section class="app-card mb-4">
                    <h2 class="app-card-title mb-4">Direccion principal
                    </h2>

                    <div class="row g-3">
                        <div class="col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtCalle" CssClass="app-form-label" Text="Calle" />
                            <asp:TextBox runat="server" ID="txtCalle" CssClass="app-input" />
                        </div>

                        <div class="col-md-3">
                            <asp:Label runat="server" AssociatedControlID="txtAltura" CssClass="app-form-label" Text="Numero" />
                            <asp:TextBox runat="server" ID="txtAltura" CssClass="app-input" TextMode="Number" />
                        </div>

                        <div class="col-md-3">
                            <asp:Label runat="server" AssociatedControlID="txtLocalidad" CssClass="app-form-label" Text="Localidad" />
                            <asp:TextBox runat="server" ID="txtLocalidad" CssClass="app-input" />
                        </div>

                        <div class="col-md-4">
                            <asp:Label runat="server" AssociatedControlID="txtProvincia" CssClass="app-form-label" Text="Provincia" />
                            <asp:TextBox runat="server" ID="txtProvincia" CssClass="app-input" />
                        </div>

                        <div class="col-md-3">
                            <asp:Label runat="server" AssociatedControlID="txtCp" CssClass="app-form-label" Text="Codigo postal" />
                            <asp:TextBox runat="server" ID="txtCp" CssClass="app-input" />
                        </div>

                        <div class="col-md-5">
                            <asp:Label runat="server" AssociatedControlID="txtObservaciones" CssClass="app-form-label" Text="Observaciones" />
                            <asp:TextBox runat="server" ID="txtObservaciones" CssClass="app-input" />
                        </div>
                    </div>
                </section>

                <section class="app-card">
                    <asp:Label runat="server" ID="lblError" CssClass="app-alert app-alert-danger d-block mb-3" Visible="false" />

                    <div class="d-grid gap-2">
                        <asp:LinkButton
                            runat="server"
                            ID="btnRegistrarse"
                            CssClass="app-btn-primary text-center"
                            Style="display: block; width: 100%; box-sizing: border-box;"
                            Text="Registrarse"
                            OnClick="btnRegistrarse_Click" />

                        <a runat="server" ID="lnkLogin" href="~/Login.aspx" class="app-btn-secondary text-center" style="display: block; width: 100%; box-sizing: border-box;">Ya tengo cuenta
                        </a>
                    </div>
                </section>

            </div>
        </div>

    </main>

</asp:Content>
