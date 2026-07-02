<%@ Page Title="Marcas" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="eCommerce.Web.Marcas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloMarcas">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <h1 id="tituloMarcas" class="app-title mb-2 fs-2">Marcas
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Administra las marcas disponibles para los productos.
                    </p>
                </div>

                <div class="col-md-4 text-md-end text-start">
                    <a runat="server" href="~/Admin.aspx" class="app-btn-secondary">&larr; Volver al panel
                    </a>
                </div>
            </div>
        </section>

        <section class="app-card mb-4">
            <div class="row g-3 align-items-end">
                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="txtFiltroNombre" CssClass="app-form-label" Text="Buscar por nombre" />
                    <asp:TextBox
                        runat="server"
                        ID="txtFiltroNombre"
                        CssClass="app-input"
                        Placeholder="Nombre de la marca"
                        AutoPostBack="true"
                        OnTextChanged="txtFiltroNombre_TextChanged" />
                </div>

                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="ddlFiltroEstado" CssClass="app-form-label" Text="Estado" />
                    <asp:DropDownList
                        runat="server"
                        ID="ddlFiltroEstado"
                        CssClass="app-select"
                        AutoPostBack="true"
                        OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">
                        <asp:ListItem Text="Todos" Value="Todos" />
                        <asp:ListItem Text="Activos" Value="Activos" />
                        <asp:ListItem Text="Inactivos" Value="Inactivos" />
                    </asp:DropDownList>
                </div>
            </div>
        </section>

        <section class="app-card p-0 mb-4">
            <div class="p-4 border-bottom">
                <h2 class="app-card-title mb-0">Marcas registradas
                </h2>
            </div>

            <div class="table-responsive">
                <asp:GridView
                    runat="server"
                    ID="dgvMarcas"
                    DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    CssClass="app-table"
                    GridLines="None"
                    BorderStyle="None"
                    EmptyDataText="No hay marcas para mostrar."
                    OnRowCommand="dgvMarcas_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="Id" />
                        <asp:BoundField HeaderText="Nombre" DataField="Nombre" />

                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <span class='<%# (bool)Eval("Activo") ? "app-badge app-badge-success" : "app-badge app-badge-danger" %>'>
                                    <%# (bool)Eval("Activo") ? "Si" : "No" %>
                                </span>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Accion">
                            <ItemTemplate>
                                <div class="d-flex flex-wrap gap-2">
                                    <asp:LinkButton
                                        ID="btnEditar"
                                        runat="server"
                                        CommandName="Editar"
                                        CommandArgument='<%# Eval("Id") %>'
                                        Text="Editar"
                                        CssClass="app-btn-link" />

                                    <asp:LinkButton
                                        ID="btnDesactivar"
                                        runat="server"
                                        Text='<%# (bool)Eval("Activo") ? "Desactivar" : "Activar" %>'
                                        CommandName='<%# (bool)Eval("Activo") ? "Desactivar" : "Activar" %>'
                                        CommandArgument='<%# Eval("Id") %>'
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
                    <asp:Label runat="server" ID="lblTituloFormulario" Text="Agregar marca" />
                </h2>
            </div>

            <asp:Label runat="server" ID="lblError" CssClass="d-block mb-3" Style="color: var(--color-danger);" />

            <div class="row g-3 align-items-end">
                <div class="col-md-8">
                    <asp:Label runat="server" AssociatedControlID="txtNombreMarca" CssClass="app-form-label" Text="Nombre" />
                    <asp:TextBox runat="server" ID="txtNombreMarca" CssClass="app-input" Placeholder="Nombre de la marca" />
                </div>

                <div class="col-md-4 d-flex flex-wrap gap-2">
                    <asp:Button
                        ID="btnAgregarMarca"
                        runat="server"
                        Text="Agregar marca"
                        CssClass="app-btn-primary"
                        OnClick="btnAgregarMarca_Click" />

                    <asp:Button
                        ID="btnCancelar"
                        runat="server"
                        Text="Cancelar"
                        Visible="false"
                        OnClick="btnCancelar_Click"
                        CssClass="app-btn-secondary" />
                </div>
            </div>
        </section>

    </main>

</asp:Content>
