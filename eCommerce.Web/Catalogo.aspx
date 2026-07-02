<%@ Page Title="Catalogo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="eCommerce.Web.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloCatalogo">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <span class="app-badge app-badge-primary mb-2">Catálogo</span>

                    <h1 id="tituloCatalogo" class="app-title mb-2 fs-2">Productos disponibles
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Encontrá lo que buscás y agregalo al carrito.
                    </p>
                </div>

                <div class="col-md-4 text-md-end text-start">
                    <a runat="server" href="~/Carrito.aspx" class="app-btn-secondary">Ver carrito →
                    </a>
                </div>
            </div>
        </section>

        <section class="app-card mb-4">
            <div class="row g-3 align-items-end">
                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="txtBuscar" CssClass="app-form-label" Text="Buscar producto" />
                    <asp:TextBox runat="server" ID="txtBuscar" CssClass="app-input" />
                </div>

                <div class="col-md-2">
                    <asp:Label runat="server" AssociatedControlID="ddlCategoria" CssClass="app-form-label" Text="Categoría" />
                    <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="app-select" />
                </div>

                <div class="col-md-2">
                    <asp:Label runat="server" AssociatedControlID="ddlMarca" CssClass="app-form-label" Text="Marca" />
                    <asp:DropDownList runat="server" ID="ddlMarca" CssClass="app-select" />
                </div>

                <div class="col-md-2">
                    <asp:Label runat="server" AssociatedControlID="ddlOrden" CssClass="app-form-label" Text="Ordenar" />
                    <asp:DropDownList runat="server" ID="ddlOrden" CssClass="app-select">
                        <asp:ListItem Text="Nombre A-Z" Value="nombre" />
                        <asp:ListItem Text="Menor precio" Value="precio-asc" />
                        <asp:ListItem Text="Mayor precio" Value="precio-desc" />
                        <asp:ListItem Text="Más stock" Value="stock-desc" />
                    </asp:DropDownList>
                </div>

                <div class="col-md-3 d-flex gap-2">
                    <asp:Button runat="server" ID="btnBuscar" CssClass="app-btn-primary flex-fill" Text="Buscar" OnClick="btnBuscar_Click" />
                    <asp:Button runat="server" ID="btnLimpiar" CssClass="app-btn-secondary" Text="Limpiar" OnClick="btnLimpiar_Click" />
                </div>
            </div>
        </section>

        <asp:Panel runat="server" ID="pnlSinResultados" CssClass="app-empty-state mb-4" Visible="false">
            No hay productos disponibles con esos filtros.
        </asp:Panel>

        <asp:Repeater runat="server" ID="rptProductos" OnItemCommand="rptProductos_ItemCommand">
            <HeaderTemplate>
                <div class="row g-4">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-sm-6 col-lg-3">
                    <article class="app-card app-card-interactive h-100 d-flex flex-column">

                     <div class="app-icon-box app-icon-box-primary w-100 mb-3" style="height: 140px; overflow: hidden;">
                        <asp:Image
                            runat="server"
                            ImageUrl='<%# ObtenerImagen(Eval("ListaImagenes")) %>'
                            AlternateText='<%# Eval("Nombre") %>'
                            Visible='<%# TieneImagen(Eval("ListaImagenes")) %>'
                            Style="width: 100%; height: 100%; object-fit: cover; display: block;" />

                        <asp:Label
                            runat="server"
                            CssClass="app-text-muted"
                            Text="Sin imagen"
                            Visible='<%# !TieneImagen(Eval("ListaImagenes")) %>' />
                    </div>

                        <span class="app-badge app-badge-primary mb-2" style="width: fit-content;">
                            <%#: Eval("Categoria.Nombre") %>
            </span>

                        <h2 class="app-card-title fs-6 mb-1">
                            <%#: Eval("Nombre") %>
            </h2>

                        <p class="app-text-muted mb-2">
                            <%#: Eval("Marca.Nombre") %>
                        </p>

                        <p class="app-text-muted mb-3" style="font-size: 0.85rem; line-height: 1.5;">
                            <%#: Eval("Descripcion") %>
                        </p>

                        <div class="mt-auto">
                            <p class="fw-bold mb-1" style="color: var(--color-slate-900);">
                                <%# FormatearPrecio(Eval("Precio")) %>
                            </p>

                            <p class="app-text-muted mb-3" style="font-size: 0.8rem;">
                                Stock disponible: <%# Eval("Stock") %>
                            </p>

                            <div class="d-grid gap-2">
                                <a href='<%# ResolveUrl("~/DetalleProducto.aspx?id=" + Eval("Id")) %>' class="app-btn-secondary py-1 px-3 text-center">Ver detalle
                    </a>

                                <asp:Button
                                    runat="server"
                                    Text="Agregar al carrito"
                                    CssClass="app-btn-primary py-1 px-3 w-100"
                                    CommandName="AgregarCarrito"
                                    CommandArgument='<%# Eval("Id") %>' />
                            </div>
                        </div>
                    </article>
                </div>
            </ItemTemplate>

            <FooterTemplate>
                </div>
            </FooterTemplate>
        </asp:Repeater>

    </main>

</asp:Content>
