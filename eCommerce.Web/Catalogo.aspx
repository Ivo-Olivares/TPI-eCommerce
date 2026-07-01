<%@ Page Title="Catalogo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="eCommerce.Web.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4 catalogo-page">
        <div class="d-flex flex-wrap justify-content-between align-items-center gap-3 mb-3">
            <div>
                <h1 class="h3 mb-1">Catalogo de productos</h1>
                <p class="text-muted mb-0">Productos disponibles para comprar.</p>
            </div>
            <a runat="server" href="~/Carrito" class="btn btn-outline-primary">Ver carrito</a>
        </div>

        <div class="row g-3 align-items-end mb-4">
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="txtBuscar" CssClass="form-label" Text="Buscar producto" />
                <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control" />
            </div>
            <div class="col-md-2">
                <asp:Label runat="server" AssociatedControlID="ddlCategoria" CssClass="form-label" Text="Categoria" />
                <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-select" />
            </div>
            <div class="col-md-2">
                <asp:Label runat="server" AssociatedControlID="ddlMarca" CssClass="form-label" Text="Marca" />
                <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-select" />
            </div>
            <div class="col-md-2">
                <asp:Label runat="server" AssociatedControlID="ddlOrden" CssClass="form-label" Text="Ordenar" />
                <asp:DropDownList runat="server" ID="ddlOrden" CssClass="form-select">
                    <asp:ListItem Text="Nombre A-Z" Value="nombre" />
                    <asp:ListItem Text="Menor precio" Value="precio-asc" />
                    <asp:ListItem Text="Mayor precio" Value="precio-desc" />
                    <asp:ListItem Text="Mas stock" Value="stock-desc" />
                </asp:DropDownList>
            </div>
            <div class="col-md-3 d-flex gap-2">
                <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-primary flex-fill" Text="Buscar" OnClick="btnBuscar_Click" />
                <asp:Button runat="server" ID="btnLimpiar" CssClass="btn btn-outline-secondary" Text="Limpiar" OnClick="btnLimpiar_Click" />
            </div>
        </div>

        <asp:Panel runat="server" ID="pnlSinResultados" CssClass="alert alert-info" Visible="false">
            No hay productos disponibles con esos filtros.
        </asp:Panel>

        <asp:Repeater runat="server" ID="rptProductos" OnItemCommand="rptProductos_ItemCommand">
            <HeaderTemplate>
                <div class="row g-3">
            </HeaderTemplate>
            <ItemTemplate>
                <div class="col-md-4">
                    <article class="border rounded h-100 p-3 d-flex flex-column">

                        <div class="bg-light border rounded mb-3 d-flex align-items-center justify-content-center catalogo-imagen">
                            <asp:Image
                                runat="server"
                                ImageUrl='<%# ObtenerImagen(Eval("ListaImagenes")) %>'
                                CssClass="img-fluid catalogo-img"
                                AlternateText='<%# Eval("Nombre") %>'
                                Visible='<%# TieneImagen(Eval("ListaImagenes")) %>' />

                            <asp:Label
                                runat="server"
                                CssClass="text-muted"
                                Text="Sin imagen"
                                Visible='<%# !TieneImagen(Eval("ListaImagenes")) %>' />
                        </div>
                       
             
                        
                        <h2 class="h5 mb-1"><%#: Eval("Nombre") %></h2>
                        <p class="text-muted mb-2"><%#: Eval("Marca.Nombre") %> | <%#: Eval("Categoria.Nombre") %></p>
                        <p class="mb-2 catalogo-descripcion"><%#: Eval("Descripcion") %></p>
                        <div class="mt-auto">
                            <p class="fw-bold mb-1"><%# FormatearPrecio(Eval("Precio")) %></p>
                            <p class="text-muted mb-3">Stock disponible: <%# Eval("Stock") %></p>
                            <div class="d-flex gap-2">
                                <div class="d-flex gap-2">
                                    <a href='<%# ResolveUrl("~/DetalleProducto.aspx?id=" + Eval("Id")) %>' class="btn btn-outline-primary btn-sm">Ver detalle
                                    </a>

                                    <asp:Button
                                        runat="server"
                                        Text="Agregar al carrito"
                                        CssClass="btn btn-primary btn-sm"
                                        CommandName="AgregarCarrito"
                                        CommandArgument='<%# Eval("Id") %>' />
                                </div>
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
