<%@ Page Title="Ingresar" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="eCommerce.Web.Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <div class="mb-4 text-center">
            <h1 id="tituloLogin" class="app-title mb-2 fs-2">Ingresar
            </h1>

            <p class="app-subtitle mb-0 fs-6">
                Accede a tu cuenta para continuar.
            </p>
        </div>

        <div class="row justify-content-center">
            <div class="col-lg-5 col-md-7">

                <section class="app-card">
                    <h2 class="app-card-title mb-4">Datos de acceso
                    </h2>

                    <div class="app-form-group">
                        <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="app-form-label" Text="Email" />
                        <asp:TextBox runat="server" ID="txtEmail" CssClass="app-input" TextMode="Email" />
                    </div>

                    <div class="app-form-group">
                        <asp:Label runat="server" AssociatedControlID="txtClave" CssClass="app-form-label" Text="Clave" />
                        <asp:TextBox runat="server" ID="txtClave" CssClass="app-input" TextMode="Password" />
                    </div>

                    <asp:Label runat="server" ID="lblError" CssClass="app-alert app-alert-danger d-block mb-3" Visible="false" />

                    <div class="d-grid gap-2">
                        <asp:LinkButton
                            runat="server"
                            ID="btnIngresar"
                            CssClass="app-btn-primary text-center"
                            Style="display: block; width: 100%; box-sizing: border-box;"
                            Text="Ingresar"
                            OnClick="btnIngresar_Click" />

                        <asp:LinkButton
                            runat="server"
                            ID="btnInvitado"
                            CssClass="app-btn-secondary text-center"
                            Style="display: block; width: 100%; box-sizing: border-box;"
                            Text="Entrar como invitado"
                            OnClick="btnInvitado_Click" />

                        <a runat="server" href="~/Registro.aspx" class="app-btn-secondary text-center" style="display: block; width: 100%; box-sizing: border-box;">Crear cuenta
                        </a>
                    </div>
                </section>

            </div>
        </div>

    </main>

</asp:Content>
