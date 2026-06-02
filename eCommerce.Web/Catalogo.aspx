<%@ Page Title="Catalogo" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Catalogo.aspx.cs" Inherits="eCommerce.Web.Catalogo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <div>
                <h1 class="h3 mb-1">Catalogo de productos</h1>
                <p class="text-muted mb-0">Pantalla prevista para busqueda y navegacion de productos publicados.</p>
            </div>
            <a runat="server" href="~/Carrito" class="btn btn-outline-primary">Ver carrito</a>
        </div>

        <div class="row g-3 align-items-end mb-4">
            <div class="col-md-4">
                <asp:Label runat="server" AssociatedControlID="txtBuscar" CssClass="form-label" Text="Buscar producto" />
                <asp:TextBox runat="server" ID="txtBuscar" CssClass="form-control" />
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="ddlCategoria" CssClass="form-label" Text="Categoria" />
                <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-select">
                    <asp:ListItem Text="Todas" Value="" />
                    <asp:ListItem Text="Tecnologia" Value="Tecnologia" />
                    <asp:ListItem Text="Hogar" Value="Hogar" />
                    <asp:ListItem Text="Indumentaria" Value="Indumentaria" />
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="ddlMarca" CssClass="form-label" Text="Marca" />
                <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-select">
                    <asp:ListItem Text="Todas" Value="" />
                    <asp:ListItem Text="Marca A" Value="Marca A" />
                    <asp:ListItem Text="Marca B" Value="Marca B" />
                    <asp:ListItem Text="Marca C" Value="Marca C" />
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-primary w-100" Text="Buscar" />
            </div>
        </div>

        <div class="row g-3">
            <div class="col-md-4">
                <div class="border rounded h-100 p-3">
                    <div class="bg-light border rounded mb-3 d-flex align-items-center justify-content-center" style="height: 160px;">
                        <span class="text-muted">Imagen producto</span>
                    </div>
                    <h2 class="h5">Producto destacado</h2>
                    <p class="text-muted">Descripcion breve del producto publicado en el catalogo.</p>
                    <p class="fw-bold mb-2">$ 25.000,00</p>
                    <div class="d-flex gap-2">
                        <a runat="server" href="~/DetalleProducto" class="btn btn-outline-primary btn-sm">Ver detalle</a>
                        <asp:Button runat="server" ID="btnAgregarProducto1" CssClass="btn btn-primary btn-sm" Text="Agregar" />
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="border rounded h-100 p-3">
                    <div class="bg-light border rounded mb-3 d-flex align-items-center justify-content-center" style="height: 160px;">
                        <span class="text-muted">Imagen producto</span>
                    </div>
                    <h2 class="h5">Producto con stock</h2>
                    <p class="text-muted">Ficha resumida para simular el listado publico de productos.</p>
                    <p class="fw-bold mb-2">$ 18.500,00</p>
                    <div class="d-flex gap-2">
                        <a runat="server" href="~/DetalleProducto" class="btn btn-outline-primary btn-sm">Ver detalle</a>
                        <asp:Button runat="server" ID="btnAgregarProducto2" CssClass="btn btn-primary btn-sm" Text="Agregar" />
                    </div>
                </div>
            </div>
            <div class="col-md-4">
                <div class="border rounded h-100 p-3">
                    <div class="bg-light border rounded mb-3 d-flex align-items-center justify-content-center" style="height: 160px;">
                        <span class="text-muted">Imagen producto</span>
                    </div>
                    <h2 class="h5">Producto nuevo</h2>
                    <p class="text-muted">Tarjeta prevista para mostrar precio, marca, categoria y acciones.</p>
                    <p class="fw-bold mb-2">$ 32.900,00</p>
                    <div class="d-flex gap-2">
                        <a runat="server" href="~/DetalleProducto" class="btn btn-outline-primary btn-sm">Ver detalle</a>
                        <asp:Button runat="server" ID="btnAgregarProducto3" CssClass="btn btn-primary btn-sm" Text="Agregar" />
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
