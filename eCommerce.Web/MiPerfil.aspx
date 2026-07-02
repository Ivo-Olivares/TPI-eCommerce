<%@ Page Title="Mi perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="eCommerce.Web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloMiPerfil">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <h1 id="tituloMiPerfil" class="app-title mb-2 fs-2">Mi perfil
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Consulta y actualiza tus datos personales.
                    </p>
                </div>

                <div class="col-md-4 text-md-end text-start">
                    <a runat="server" href="~/MisCompras.aspx" class="app-btn-secondary">Mis compras →
                    </a>
                </div>
            </div>
        </section>

        <asp:Label runat="server" ID="lblMensaje" Visible="false" CssClass="app-alert d-block mb-4" />

        <section class="app-card mb-4">
            <div class="mb-4">
                <h2 class="app-card-title mb-0">Datos personales
                </h2>
            </div>

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
                    <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="app-form-label" Text="Email" />
                    <asp:TextBox runat="server" ID="txtEmail" CssClass="app-input" TextMode="Email" ReadOnly="true" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtTelefono" CssClass="app-form-label" Text="Telefono" />
                    <asp:TextBox runat="server" ID="txtTelefono" CssClass="app-input" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtDni" CssClass="app-form-label" Text="DNI" />
                    <asp:TextBox runat="server" ID="txtDni" CssClass="app-input" ReadOnly="true" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtFechaNacimiento" CssClass="app-form-label" Text="Fecha de nacimiento" />
                    <asp:TextBox runat="server" ID="txtFechaNacimiento" CssClass="app-input" ReadOnly="true" />
                </div>

                <div class="col-md-12">
                    <asp:Button
                        runat="server"
                        ID="btnGuardarPerfil"
                        CssClass="app-btn-primary"
                        Text="Guardar cambios"
                        OnClick="btnGuardarPerfil_Click" />
                </div>
            </div>
        </section>

        <section class="app-card p-0 mb-4">
            <div class="p-4 border-bottom">
                <h2 class="app-card-title mb-0">Direcciones
                </h2>
            </div>

            <div class="table-responsive">
                <asp:GridView
                    runat="server"
                    ID="dgvDirecciones"
                    AutoGenerateColumns="false"
                    CssClass="app-table"
                    GridLines="None"
                    BorderStyle="None"
                    EmptyDataText="No tenes direcciones cargadas.">
                    <Columns>
                        <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                        <asp:BoundField HeaderText="Calle" DataField="Calle" />
                        <asp:BoundField HeaderText="Numero" DataField="Altura" />
                        <asp:BoundField HeaderText="Localidad" DataField="Localidad" />
                        <asp:BoundField HeaderText="Codigo postal" DataField="Cp" />
                    </Columns>
                </asp:GridView>
            </div>
        </section>

    </main>

</asp:Content>
