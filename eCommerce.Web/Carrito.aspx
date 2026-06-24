<%@ Page Title="Carrito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="eCommerce.Web.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <div>
                <h1 class="h3 mb-1">Carrito de compras</h1>
                <p class="text-muted mb-0">Pantalla prevista para revisar items, cantidades, subtotales y total.</p>
            </div>
            <a runat="server" href="~/Catalogo" class="btn btn-outline-secondary">Seguir comprando</a>
        </div>

        <asp:GridView runat="server" ID="dgvCarrito" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
            <Columns>
                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="PrecioUnitario" HeaderText="Precio unitario" DataFormatString="{0:C}" />
                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>

        <asp:Label runat="server" ID="lblMensaje" CssClass="alert alert-info d-block" Visible="false" />

        <asp:Panel runat="server" ID="pnlResumen" CssClass="row justify-content-end">
            <div class="col-md-4">
                <div class="border rounded p-3">
                    <div class="d-flex justify-content-between">
                        <span>Subtotal</span>
                        <strong>
                            <asp:Label runat="server" ID="lblSubtotal" />
                        </strong>
                    </div>
                    <div class="d-flex justify-content-between">
                        <span>Envio</span>
                        <span>A definir</span>
                    </div>
                    <hr />
                    <div class="d-flex justify-content-between fs-5">
                        <span>Total</span>
                        <strong>
                            <asp:Label runat="server" ID="lblTotal" />
                        </strong>
                    </div>
                    <div class="d-grid gap-2 mt-3">
                        <a runat="server" href="~/Checkout" class="btn btn-primary">Continuar al checkout</a>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </main>
</asp:Content>
