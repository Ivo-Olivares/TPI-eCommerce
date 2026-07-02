<%@ Page Title="Detalle de producto" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="DetalleProducto.aspx.cs" Inherits="eCommerce.Web.DetalleProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <a runat="server" href="~/Catalogo.aspx" class="app-btn-link mb-4 d-inline-flex">
            ← Volver al catálogo
        </a>

        <asp:Panel runat="server" ID="pnlMensaje" CssClass="app-empty-state mb-4" Visible="false">
            <asp:Label runat="server" ID="lblMensaje" />
        </asp:Panel>

        <asp:Panel runat="server" ID="pnlDetalle" CssClass="app-card" Visible="false">
            <div class="row g-4 align-items-stretch">

                <div class="col-md-5">
                    <div class="app-icon-box app-icon-box-primary w-100 h-100" style="min-height: 300px;">
                        <span style="font-size: 3rem;">🛍️</span>
                    </div>
                </div>

                <div class="col-md-7 d-flex flex-column">
                    <div>
                        <h1 class="app-title fs-2 mb-1">
                            <asp:Label runat="server" ID="lblNombre" />
                        </h1>

                        <p class="app-text-muted mb-3">
                            <asp:Label runat="server" ID="lblMeta" />
                        </p>

                        <p class="app-text mb-4">
                            <asp:Label runat="server" ID="lblDescripcion" />
                        </p>

                        <div class="d-flex flex-column gap-2 mb-4">
                            <div class="d-flex align-items-center gap-3">
                                <span class="app-form-label mb-0" style="min-width: 70px;">Precio</span>
                                <strong style="color: var(--color-slate-900); font-size: 1.25rem;">
                                    <asp:Label runat="server" ID="lblPrecio" />
                                </strong>
                            </div>

                            <div class="d-flex align-items-center gap-3">
                                <span class="app-form-label mb-0" style="min-width: 70px;">Stock</span>
                                <span class="app-text-muted">
                                    <asp:Label runat="server" ID="lblStock" />
                                    unidades
                                </span>
                            </div>

                            <div class="d-flex align-items-center gap-3">
                                <span class="app-form-label mb-0" style="min-width: 70px;">Estado</span>
                                <asp:Label runat="server" ID="lblEstado" CssClass="app-badge app-badge-primary" />
                            </div>
                        </div>

                        <asp:Label runat="server" ID="lblError" CssClass="app-alert app-alert-danger d-block mb-3" Visible="false" />
                        <asp:Label runat="server" ID="lblExito" CssClass="app-alert app-alert-success d-block mb-3" Visible="false" />
                    </div>

                    <div class="row g-3 align-items-end mt-auto">
                        <div class="col-sm-3">
                            <asp:Label runat="server" AssociatedControlID="txtCantidad" CssClass="app-form-label" Text="Cantidad" />
                            <asp:TextBox runat="server" ID="txtCantidad" CssClass="app-input" TextMode="Number" Text="1" />
                        </div>

                        <div class="col-sm-9 d-flex flex-wrap gap-2">
                            <asp:Button
                                runat="server"
                                ID="btnAgregarCarrito"
                                CssClass="app-btn-primary"
                                Text="Agregar al carrito"
                                OnClick="btnAgregarCarrito_Click" />

                            <a runat="server" href="~/Carrito.aspx" class="app-btn-secondary">
                                Ir al carrito
                            </a>
                        </div>
                    </div>
                </div>

            </div>
        </asp:Panel>

    </main>

</asp:Content>