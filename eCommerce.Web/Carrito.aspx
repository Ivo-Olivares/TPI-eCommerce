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
                <asp:BoundField HeaderText="Producto" />
                <asp:BoundField HeaderText="Cantidad" />
                <asp:BoundField HeaderText="Precio unitario" />
                <asp:BoundField HeaderText="Subtotal" />
                <asp:ButtonField Text="Quitar" ButtonType="Button" />
            </Columns>
        </asp:GridView>

        <div class="row justify-content-end">
            <div class="col-md-4">
                <div class="border rounded p-3">
                    <div class="d-flex justify-content-between">
                        <span>Subtotal</span>
                        <strong>$ 75.400,00</strong>
                    </div>
                    <div class="d-flex justify-content-between">
                        <span>Envio</span>
                        <span>A definir</span>
                    </div>
                    <hr />
                    <div class="d-flex justify-content-between fs-5">
                        <span>Total</span>
                        <strong>$ 75.400,00</strong>
                    </div>
                    <div class="d-grid gap-2 mt-3">
                        <a runat="server" href="~/Checkout" class="btn btn-primary">Continuar al checkout</a>
                        <asp:Button runat="server" ID="btnActualizar" CssClass="btn btn-outline-secondary" Text="Actualizar cantidades" />
                    </div>
                </div>
            </div>
        </div>
    </main>
</asp:Content>
