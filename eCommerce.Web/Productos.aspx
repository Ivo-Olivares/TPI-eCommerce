<%@ Page Title="Productos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="eCommerce.Web.Productos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloProductos">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <h1 id="tituloProductos" class="app-title mb-2 fs-2">Productos
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Gestiona el catálogo, precios, stock y estado de los productos.
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
                <div class="col-md-4">
                    <asp:Label runat="server" AssociatedControlID="txtFiltroProducto" CssClass="app-form-label" Text="Buscar por nombre, SKU, marca o categoría" />
                    <asp:TextBox runat="server" ID="txtFiltroProducto" CssClass="app-input" />
                </div>

                <div class="col-md-2">
                    <asp:Label runat="server" AssociatedControlID="ddlFiltroEstado" CssClass="app-form-label" Text="Estado" />
                    <asp:DropDownList runat="server" ID="ddlFiltroEstado" CssClass="app-select">
                        <asp:ListItem Text="Todos" Value="" />
                        <asp:ListItem Text="Activos" Value="1" />
                        <asp:ListItem Text="Inactivos" Value="0" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <asp:Button runat="server" ID="btnFiltrar" Text="Filtrar" CssClass="app-btn-primary w-100" Style="width: 100%; max-width: none;" OnClick="btnFiltrar_Click" />
                </div>

                <div class="col-md-3">
                    <asp:Button runat="server" ID="btnLimpiarFiltro" Text="Limpiar" CssClass="app-btn-secondary w-100" Style="width: 100%; max-width: none;" OnClick="btnLimpiarFiltro_Click" />
                </div>
            </div>
        </section>

        <section class="app-card p-0 mb-4">
            <div class="p-4 border-bottom">
                <h2 class="app-card-title mb-0">Productos registrados
                </h2>
            </div>

            <div class="table-responsive">
                <asp:GridView
                    runat="server"
                    ID="dgvProductos"
                    DataKeyNames="Id"
                    AutoGenerateColumns="false"
                    CssClass="app-table"
                    GridLines="None"
                    BorderStyle="None"
                    EmptyDataText="No hay productos para mostrar."
                    OnRowCommand="dgvProductos_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="Id" />
                        <asp:BoundField HeaderText="Sku" DataField="Sku" />
                        <asp:BoundField HeaderText="Producto" DataField="Nombre" />
                        <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                        <asp:BoundField HeaderText="Marca" DataField="Marca" />
                        <asp:BoundField HeaderText="Categoria" DataField="Categoria" />
                        <asp:BoundField HeaderText="Precio" DataField="Precio" DataFormatString="{0:$ #,##0.00}" />
                        <asp:BoundField HeaderText="Stock" DataField="Stock" />

                        <asp:TemplateField HeaderText="Activo">
                            <ItemTemplate>
                                <span class='<%# (bool)Eval("Activo") ? "app-badge app-badge-success" : "app-badge app-badge-danger" %>'>
                                    <%# (bool)Eval("Activo") ? "Sí" : "No" %>
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
            <div class="d-flex flex-wrap justify-content-between align-items-center gap-3 mb-4">
                <div>
                    <h2 class="app-card-title mb-0">
                        <asp:Label runat="server" ID="lblTituloFormulario" Text="Agregar producto" />
                    </h2>
                </div>
            </div>

            <asp:Label runat="server" ID="lblError" CssClass="d-block mb-3" Style="color: var(--color-danger);" />

            <div class="row g-3">
                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="txtSku" CssClass="app-form-label" Text="Sku" />
                    <asp:TextBox runat="server" ID="txtSku" CssClass="app-input" Placeholder="Sku" />
                </div>

                <div class="col-md-5">
                    <asp:Label runat="server" AssociatedControlID="txtNombreProducto" CssClass="app-form-label" Text="Nombre" />
                    <asp:TextBox runat="server" ID="txtNombreProducto" CssClass="app-input" Placeholder="Nombre del producto" />
                </div>

                <div class="col-md-2">
                    <asp:Label runat="server" AssociatedControlID="txtPrecio" CssClass="app-form-label" Text="Precio" />
                    <asp:TextBox runat="server" ID="txtPrecio" CssClass="app-input" Placeholder="Precio" />
                </div>

                <div class="col-md-2">
                    <asp:Label runat="server" AssociatedControlID="txtStock" CssClass="app-form-label" Text="Stock" />
                    <asp:TextBox runat="server" ID="txtStock" CssClass="app-input" Placeholder="Stock" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="ddlCategoria" CssClass="app-form-label" Text="Categoria" />
                    <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="app-select" />
                </div>

                <div class="col-md-6">
                    <asp:Label runat="server" AssociatedControlID="ddlMarca" CssClass="app-form-label" Text="Marca" />
                    <asp:DropDownList runat="server" ID="ddlMarca" CssClass="app-select" />
                </div>

                <div class="col-md-12">
                    <asp:Label runat="server" AssociatedControlID="txtUrlImagen" CssClass="app-form-label" Text="URL de imagen" />
                    <asp:TextBox runat="server" ID="txtUrlImagen" CssClass="app-input" Placeholder="URL de imagen" />
                </div>

                <div class="col-md-12">
                    <asp:Label runat="server" AssociatedControlID="txtDescripcion" CssClass="app-form-label" Text="Descripcion" />
                    <asp:TextBox runat="server" ID="txtDescripcion" CssClass="app-input" Placeholder="Descripcion" TextMode="MultiLine" Rows="3" />
                </div>

                <div class="col-md-12 d-flex flex-wrap gap-2">
                    <asp:Button
                        ID="btnAgregarProducto"
                        runat="server"
                        Text="Agregar Producto"
                        CssClass="app-btn-primary"
                        OnClick="btnAgregarProducto_Click" />

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
