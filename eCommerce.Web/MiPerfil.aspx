<%@ Page Title="Mi perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="eCommerce.Web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloMiPerfil">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <h1 id="tituloMiPerfil" class="app-title mb-2 fs-2">Mi perfil
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Consulta y actualiza tus datos personales y direcciones.
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
                    DataKeyNames="Id"
                    EmptyDataText="No tenes direcciones cargadas."
                    OnRowCommand="dgvDirecciones_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                        <asp:BoundField HeaderText="Calle" DataField="Calle" />
                        <asp:BoundField HeaderText="Numero" DataField="Altura" />
                        <asp:BoundField HeaderText="Localidad" DataField="Localidad" />
                        <asp:BoundField HeaderText="Provincia" DataField="Provincia" />
                        <asp:BoundField HeaderText="Codigo postal" DataField="Cp" />

                        <asp:TemplateField HeaderText="Accion">
                            <ItemTemplate>
                                <div class="d-flex flex-wrap gap-2">
                                    <asp:LinkButton
                                        ID="btnEditarDireccion"
                                        runat="server"
                                        CommandName="EditarDireccion"
                                        CommandArgument='<%# Eval("Id") %>'
                                        Text="Editar"
                                        CssClass="app-btn-link" />

                                    <asp:LinkButton
                                        ID="btnEliminarDireccion"
                                        runat="server"
                                        CommandName="EliminarDireccion"
                                        CommandArgument='<%# Eval("Id") %>'
                                        Text="Eliminar"
                                        CssClass="app-btn-link" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </section>

        <section class="app-card mb-4">
            <div class="mb-4">
                <h2 class="app-card-title mb-0">
                    <asp:Label runat="server" ID="lblTituloDireccion" Text="Agregar direccion" />
                </h2>
            </div>

            <asp:HiddenField runat="server" ID="hdfIdDireccion" />

            <div class="row g-3">
                <div class="col-md-4">
                    <asp:Label runat="server" AssociatedControlID="txtDescripcionDireccion" CssClass="app-form-label" Text="Descripcion" />
                    <asp:TextBox runat="server" ID="txtDescripcionDireccion" CssClass="app-input" Placeholder="Principal, trabajo, etc." />
                </div>

                <div class="col-md-5">
                    <asp:Label runat="server" AssociatedControlID="txtCalleDireccion" CssClass="app-form-label" Text="Calle" />
                    <asp:TextBox runat="server" ID="txtCalleDireccion" CssClass="app-input" />
                </div>

                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="txtAlturaDireccion" CssClass="app-form-label" Text="Numero" />
                    <asp:TextBox runat="server" ID="txtAlturaDireccion" CssClass="app-input" TextMode="Number" />
                </div>

                <div class="col-md-4">
                    <asp:Label runat="server" AssociatedControlID="txtLocalidadDireccion" CssClass="app-form-label" Text="Localidad" />
                    <asp:TextBox runat="server" ID="txtLocalidadDireccion" CssClass="app-input" />
                </div>

                <div class="col-md-4">
                    <asp:Label runat="server" AssociatedControlID="txtProvinciaDireccion" CssClass="app-form-label" Text="Provincia" />
                    <asp:TextBox runat="server" ID="txtProvinciaDireccion" CssClass="app-input" />
                </div>

                <div class="col-md-4">
                    <asp:Label runat="server" AssociatedControlID="txtCpDireccion" CssClass="app-form-label" Text="Codigo postal" />
                    <asp:TextBox runat="server" ID="txtCpDireccion" CssClass="app-input" />
                </div>

                <div class="col-md-12">
                    <asp:Label runat="server" AssociatedControlID="txtObservacionesDireccion" CssClass="app-form-label" Text="Observaciones" />
                    <asp:TextBox runat="server" ID="txtObservacionesDireccion" CssClass="app-input" TextMode="MultiLine" Rows="3" />
                </div>

                <div class="col-md-12 d-flex flex-wrap gap-2">
                    <asp:Button
                        runat="server"
                        ID="btnGuardarDireccion"
                        CssClass="app-btn-primary"
                        Text="Agregar direccion"
                        OnClick="btnGuardarDireccion_Click" />

                    <asp:Button
                        runat="server"
                        ID="btnCancelarDireccion"
                        CssClass="app-btn-secondary"
                        Text="Cancelar"
                        Visible="false"
                        OnClick="btnCancelarDireccion_Click" />
                </div>
            </div>
        </section>

    </main>

</asp:Content>
