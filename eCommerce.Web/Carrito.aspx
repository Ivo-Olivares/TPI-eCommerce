<%@ Page Title="Carrito" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Carrito.aspx.cs" Inherits="eCommerce.Web.Carrito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloCarrito">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <span class="app-badge app-badge-primary mb-2">Carrito</span>

                    <h1 id="tituloCarrito" class="app-title mb-2 fs-2">
                        Carrito de compras
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Revisá los productos antes de continuar al checkout.
                    </p>
                </div>

                <div class="col-md-4 text-md-end text-start">
                    <a runat="server" href="~/Catalogo.aspx" class="app-btn-secondary">
                        Seguir comprando →
                    </a>
                </div>
            </div>
        </section>

        <asp:Label runat="server" ID="lblError" CssClass="app-alert app-alert-danger d-block mb-4" Visible="false" />
        <asp:Label runat="server" ID="lblExito" CssClass="app-alert app-alert-success d-block mb-4" Visible="false" />

        <asp:Panel runat="server" ID="pnlVacio" CssClass="app-empty-state mb-4" Visible="false">
            <div class="app-empty-icon">🛒</div>

            <h2 class="app-card-title mb-2">
                El carrito está vacío
            </h2>

            <p class="app-text-muted mb-3">
                Agregá productos desde el catálogo para continuar con tu compra.
            </p>

            <a runat="server" href="~/Catalogo.aspx" class="app-btn-primary">
                Ver catálogo
            </a>
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlCarrito" Visible="false" DefaultButton="btnActualizar">

            <div class="row g-4 align-items-start">

                <div class="col-lg-8">
                    <section class="app-card p-0">
                        <div class="table-responsive">
                            <table class="app-table">
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

                                                    <strong style="color: var(--color-slate-900);">
                                                        <%#: Eval("Producto.Nombre") %>
                                                    </strong>

                                                    <span class="app-text-muted d-block">
                                                        <%#: Eval("Producto.Marca.Nombre") %> | <%#: Eval("Producto.Categoria.Nombre") %>
                                                    </span>
                                                </td>

                                                <td>
                                                    <%# FormatearPrecio(Eval("PrecioUnitario")) %>
                                                </td>

                                                <td>
                                                    <asp:TextBox
                                                        runat="server"
                                                        ID="txtCantidadItem"
                                                        CssClass="app-input"
                                                        TextMode="Number"
                                                        Text='<%# Eval("Cantidad") %>' />
                                                </td>

                                                <td>
                                                    <strong style="color: var(--color-slate-900);">
                                                        <%# FormatearPrecio(Eval("Subtotal")) %>
                                                    </strong>
                                                </td>

                                                <td>
                                                    <asp:Button
                                                        runat="server"
                                                        ID="btnQuitar"
                                                        CssClass="app-btn-secondary py-1 px-3"
                                                        Text="Quitar"
                                                        CommandName="Quitar"
                                                        CommandArgument='<%# Eval("Producto.Id") %>' />
                                                </td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </section>
                </div>

                <div class="col-lg-4">
                    <section class="app-card">
                        <span class="app-badge app-badge-primary mb-3">Resumen</span>

                        <h2 class="app-card-title mb-3">
                            Total de la compra
                        </h2>

                        <div class="d-flex justify-content-between mb-2">
                            <span class="app-text-muted">Subtotal</span>
                            <strong style="color: var(--color-slate-900);">
                                <asp:Label runat="server" ID="lblSubtotal" />
                            </strong>
                        </div>

                        <div class="d-flex justify-content-between mb-3">
                            <span class="app-text-muted">Envío</span>
                            <span class="app-text-muted">A definir</span>
                        </div>

                        <hr />

                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <span class="app-card-title mb-0">Total</span>
                            <strong style="color: var(--color-slate-900); font-size: 1.25rem;">
                                <asp:Label runat="server" ID="lblTotal" />
                            </strong>
                        </div>

                        <div class="d-grid gap-2">
                            <asp:Button
                                runat="server"
                                ID="btnCheckout"
                                CssClass="app-btn-primary w-100"
                                Text="Continuar al checkout"
                                OnClick="btnCheckout_Click" />

                            <asp:Button
                                runat="server"
                                ID="btnActualizar"
                                CssClass="app-btn-secondary w-100"
                                Text="Actualizar cantidades"
                                OnClick="btnActualizar_Click" />
                        </div>
                    </section>
                </div>

            </div>

        </asp:Panel>

    </main>

</asp:Content>