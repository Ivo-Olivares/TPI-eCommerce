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
                <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-select" />
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="ddlMarca" CssClass="form-label" Text="Marca" />
                <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-select" />
            </div>
            <div class="col-md-2">
                <asp:Button runat="server" ID="btnBuscar" CssClass="btn btn-primary w-100" Text="Buscar" OnClick="btnBuscar_Click" />
            </div>
        </div>

        <asp:Label runat="server" ID="lblMensaje" CssClass="alert alert-info d-block" Visible="false" />

        <div class="row g-3">
            <asp:Repeater runat="server" ID="rptProductos" OnItemCommand="rptProductos_ItemCommand">
                <ItemTemplate>
                    <div class="col-md-4">
                        <div class="border rounded h-100 p-3">
                            <div class="bg-light border rounded mb-3 d-flex align-items-center justify-content-center" style="height: 160px;">
                                <span class="text-muted">Imagen producto</span>
                            </div>
                            <h2 class="h5"><%# Eval("Nombre") %></h2>
                            <p class="text-muted mb-1"><%# Eval("Marca.Nombre") %> | <%# Eval("Categoria.Nombre") %></p>
                            <p class="text-muted"><%# Eval("Descripcion") %></p>
                            <p class="fw-bold mb-1"><%# Eval("Precio", "{0:C}") %></p>
                            <p class="text-muted small mb-2">Stock disponible: <%# Eval("Stock") %></p>
                            <div class="d-flex gap-2">
                                <a href='<%# "DetalleProducto.aspx?id=" + Eval("Id") %>' class="btn btn-outline-primary btn-sm">Ver detalle</a>
                                <asp:LinkButton runat="server" CssClass="btn btn-primary btn-sm" Text="Agregar" CommandName="Agregar" CommandArgument='<%# Eval("Id") %>' />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </main>
</asp:Content>
