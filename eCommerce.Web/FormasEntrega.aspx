<%@ Page Title="Formas de entrega" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormasEntrega.aspx.cs" Inherits="eCommerce.Web.FormasEntrega" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloFormasEntrega">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <h1 id="tituloFormasEntrega" class="app-title mb-2 fs-2">Formas de entrega
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Configura las opciones de envio, retiro y entrega disponibles para los clientes.
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
                    <asp:Label runat="server" AssociatedControlID="txtFiltrodescripcion" CssClass="app-form-label" Text="Buscar por descripcion" />
                    <asp:TextBox
                        runat="server"
                        ID="txtFiltrodescripcion"
                        CssClass="app-input"
                        Placeholder="Descripcion de la forma de entrega"
                        AutoPostBack="true"
                        OnTextChanged="txtFiltrodescripcion_TextChanged" />
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
                <h2 class="app-card-title mb-0">Formas de entrega registradas
                </h2>
            </div>

            <div class="table-responsive">
                <asp:GridView
                    runat="server"
                    ID="dgvFormasEntrega"
                    DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    CssClass="app-table"
                    GridLines="None"
                    BorderStyle="None"
                    EmptyDataText="No hay formas de entrega para mostrar."
                    OnRowCommand="dgvFormasEntrega_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="Id" />
                        <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />

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
                    <asp:Label runat="server" ID="lblTituloFormulario" Text="Agregar forma de entrega" />
                </h2>
            </div>

            <asp:Label runat="server" ID="lblError" CssClass="d-block mb-3" Style="color: var(--color-danger);" />

            <div class="row g-3 align-items-end">
                <div class="col-md-8">
                    <asp:Label runat="server" AssociatedControlID="txtNombreFormaEntrega" CssClass="app-form-label" Text="Descripcion" />
                    <asp:TextBox runat="server" ID="txtNombreFormaEntrega" CssClass="app-input" Placeholder="Descripcion de la forma de entrega" />
                </div>

                <div class="col-md-4 d-flex flex-wrap gap-2">
                    <asp:Button
                        ID="btnAgregarFormaEntrega"
                        runat="server"
                        Text="Agregar forma de entrega"
                        CssClass="app-btn-primary"
                        OnClick="btnAgregarFormaEntrega_Click" />

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
