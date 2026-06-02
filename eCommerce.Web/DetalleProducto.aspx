<%@ Page Title="Detalle de producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="eCommerce.Web.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <a runat="server" href="~/Catalogo" class="btn btn-link px-0 mb-3">Volver al catalogo</a>

        <div class="row g-4">
            <div class="col-md-5">
                <div class="bg-light border rounded d-flex align-items-center justify-content-center" style="height: 360px;">
                    <span class="text-muted">Imagen representativa</span>
                </div>
            </div>
            <div class="col-md-7">
                <h1 class="h3">Producto destacado</h1>
                <p class="text-muted mb-2">Marca A | Tecnologia</p>
                <p class="lead">Descripcion completa del producto seleccionado para que el cliente pueda revisar sus caracteristicas antes de agregarlo al carrito.</p>
                <dl class="row">
                    <dt class="col-sm-3">Precio</dt>
                    <dd class="col-sm-9 fw-bold">$ 25.000,00</dd>
                    <dt class="col-sm-3">Stock</dt>
                    <dd class="col-sm-9">Disponible</dd>
                    <dt class="col-sm-3">Estado</dt>
                    <dd class="col-sm-9">Activo</dd>
                </dl>

                <div class="row g-3 align-items-end">
                    <div class="col-sm-4">
                        <asp:Label runat="server" AssociatedControlID="txtCantidad" CssClass="form-label" Text="Cantidad" />
                        <asp:TextBox runat="server" ID="txtCantidad" CssClass="form-control" TextMode="Number" Text="1" />
                    </div>
                    <div class="col-sm-8">
                        <asp:Button runat="server" ID="btnAgregarCarrito" CssClass="btn btn-primary" Text="Agregar al carrito" />
                        <a runat="server" href="~/Carrito" class="btn btn-outline-secondary ms-2">Ir al carrito</a>
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
