<%@ Page Title="Carrito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="eCommerce.Web.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <div>
                <h1 class="h3 mb-1">Carrito de compras</h1>
                <p class="text-muted mb-0">Revisa los productos antes de continuar al checkout.</p>
            </div>
            <a runat="server" href="~/Catalogo" class="btn btn-outline-secondary">Seguir comprando</a>
        </div>

        <asp:Label runat="server" ID="lblError" CssClass="alert alert-danger d-block" Visible="false" />
        <asp:Label runat="server" ID="lblExito" CssClass="alert alert-success d-block" Visible="false" />

        <asp:Panel runat="server" ID="pnlVacio" CssClass="alert alert-info" Visible="false">
            El carrito esta vacio.
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlCarrito" Visible="false" DefaultButton="btnActualizar">
            <div class="table-responsive">
                <table class="table table-bordered table-striped align-middle">
                    <thead>
                        <tr>
                            <th>Producto</th>
                            <th>Precio unitario</th>
                            <th style="width: 150px;">Cantidad</th>
                            <th>Subtotal</th>
                            <th style="width: 120px;">Acciones</th>
                        </tr>
                    </thead>
                    <tbody>
                        <asp:Repeater runat="server" ID="rptCarrito" OnItemCommand="rptCarrito_ItemCommand">
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:HiddenField runat="server" ID="hdfIdProducto" Value='<%# Eval("Producto.Id") %>' />
                                        <strong><%#: Eval("Producto.Nombre") %></strong>
                                        <span class="text-muted d-block small"><%#: Eval("Producto.Marca.Nombre") %> | <%#: Eval("Producto.Categoria.Nombre") %></span>
                                    </td>
                                    <td><%# FormatearPrecio(Eval("PrecioUnitario")) %></td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCantidadItem" CssClass="form-control" TextMode="Number" Text='<%# Eval("Cantidad") %>' />
                                    </td>
                                    <td><%# FormatearPrecio(Eval("Subtotal")) %></td>
                                    <td>
                                        <asp:Button runat="server" ID="btnQuitar" CssClass="btn btn-outline-danger btn-sm" Text="Quitar" CommandName="Quitar" CommandArgument='<%# Eval("Producto.Id") %>' />
                                    </td>
                                </tr>
                            </ItemTemplate>
                        </asp:Repeater>
                    </tbody>
                </table>
            </div>

            <div class="row justify-content-end">
                <div class="col-md-4">
                    <div class="border rounded p-3">
                        <div class="d-flex justify-content-between">
                            <span>Subtotal</span>
                            <strong>
                                <asp:Label runat="server" ID="lblSubtotal" /></strong>
                        </div>
                        <div class="d-flex justify-content-between">
                            <span>Envio</span>
                            <span>A definir</span>
                        </div>
                        <hr />
                        <div class="d-flex justify-content-between fs-5">
                            <span>Total</span>
                            <strong>
                                <asp:Label runat="server" ID="lblTotal" /></strong>
                        </div>
                        <div class="d-grid gap-2 mt-3">
                            <asp:Button runat="server" ID="btnCheckout" CssClass="btn btn-primary" Text="Continuar al checkout" OnClick="btnCheckout_Click" />
                            <asp:Button runat="server" ID="btnActualizar" CssClass="btn btn-outline-secondary" Text="Actualizar cantidades" OnClick="btnActualizar_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </main>
</asp:Content>
