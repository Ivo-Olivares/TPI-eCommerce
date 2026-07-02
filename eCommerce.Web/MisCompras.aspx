<%@ Page Title="Mis compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="eCommerce.Web.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloMisCompras">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <span class="app-badge app-badge-primary mb-2">Mis compras</span>

                    <h1 id="tituloMisCompras" class="app-title mb-2 fs-2">
                        Historial de compras
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Consultá tus pedidos realizados y revisá el detalle de cada compra.
                    </p>
                </div>

                <div class="col-md-4 text-md-end text-start">
                    <a runat="server" href="~/Catalogo.aspx" class="app-btn-secondary">
                        Ver catálogo →
                    </a>
                </div>
            </div>
        </section>

        <section class="app-card mb-4">
            <div class="row g-3 align-items-end">
                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="ddlEstado" CssClass="app-form-label" Text="Estado" />
                    <asp:DropDownList runat="server" ID="ddlEstado" CssClass="app-select">
                    </asp:DropDownList>
                </div>

                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="txtFechaDesde" CssClass="app-form-label" Text="Desde" />
                    <asp:TextBox runat="server" ID="txtFechaDesde" CssClass="app-input" TextMode="Date" />
                </div>

                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="txtFechaHasta" CssClass="app-form-label" Text="Hasta" />
                    <asp:TextBox runat="server" ID="txtFechaHasta" CssClass="app-input" TextMode="Date" />
                </div>

                <div class="col-md-3">
                    <asp:Button runat="server" ID="btnFiltrar" CssClass="app-btn-primary w-100" Text="Filtrar" OnClick="btnFiltrar_Click" />
                </div>
            </div>
        </section>

        <asp:Label runat="server" ID="lblError" CssClass="app-alert app-alert-danger d-block mb-4" Visible="false" />

        <section class="app-card p-0 mb-4">
            <div class="p-4 border-bottom">
                <span class="app-badge app-badge-primary mb-3">Pedidos</span>

                <h2 class="app-card-title mb-1">
                    Compras realizadas
                </h2>

            </div>

            <div class="table-responsive">
                <asp:GridView
                    runat="server"
                    ID="dgvCompras"
                    AutoGenerateColumns="false"
                    CssClass="app-table"
                    GridLines="None"
                    BorderStyle="None"
                    DataKeyNames="Id"
                    OnSelectedIndexChanged="dgvCompras_SelectedIndexChanged"
                    EmptyDataText="No se encontraron compras.">
                    <Columns>
                        <asp:BoundField HeaderText="Pedido" DataField="Id" />
                        <asp:BoundField HeaderText="Fecha" DataField="FechaCreacion" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField HeaderText="Estado" DataField="EstadoPedido.Descripcion" />
                        <asp:BoundField HeaderText="Forma de pago" DataField="FormaPago.Descripcion" />
                        <asp:BoundField HeaderText="Forma de entrega" DataField="FormaEntrega.Descripcion" />
                        <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:$ #,##0.00}" />
                        <asp:CommandField HeaderText="Detalle" ShowSelectButton="true" SelectText="Ver detalle">
                            <ControlStyle CssClass="app-btn-link" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>
        </section>

        <asp:Panel runat="server" ID="pnlDetalle" Visible="false" CssClass="app-card p-0 mb-4">
            <div class="p-4 border-bottom">

                <h2 class="app-card-title mb-1">
                    Detalle del pedido
                </h2>

                <p class="app-text-muted mb-0">
                    Productos incluidos en la compra seleccionada.
                </p>

                <div class="row g-3 mt-3">
                    <div class="col-md-3">
                        <span class="app-text-muted d-block">Pedido</span>
                        <asp:Label runat="server" ID="lblPedidoSeleccionado" CssClass="fw-semibold" />
                    </div>

                    <div class="col-md-3">
                        <span class="app-text-muted d-block">Fecha</span>
                        <asp:Label runat="server" ID="lblFechaPedido" CssClass="fw-semibold" />
                    </div>

                    <div class="col-md-3">
                        <span class="app-text-muted d-block">Estado</span>
                        <asp:Label runat="server" ID="lblEstadoPedido" CssClass="fw-semibold" />
                    </div>

                    <div class="col-md-3">
                        <span class="app-text-muted d-block">Total</span>
                        <asp:Label runat="server" ID="lblTotalPedido" CssClass="fw-semibold" />
                    </div>

                    <div class="col-md-3">
                        <span class="app-text-muted d-block">Forma de pago</span>
                        <asp:Label runat="server" ID="lblFormaPago" CssClass="fw-semibold" />
                    </div>

                    <div class="col-md-3">
                        <span class="app-text-muted d-block">Forma de entrega</span>
                        <asp:Label runat="server" ID="lblFormaEntrega" CssClass="fw-semibold" />
                    </div>

                    <div class="col-md-3">
                        <span class="app-text-muted d-block">Fecha de entrega</span>
                        <asp:Label runat="server" ID="lblFechaEntrega" CssClass="fw-semibold" />
                    </div>

                    <div class="col-md-12">
                        <span class="app-text-muted d-block">Dirección</span>
                        <asp:Label runat="server" ID="lblDireccionPedido" CssClass="fw-semibold" />
                    </div>
                </div>
            </div>

            <div class="table-responsive">
                <asp:GridView
                    runat="server"
                    ID="dgvDetalle"
                    AutoGenerateColumns="false"
                    CssClass="app-table"
                    GridLines="None"
                    BorderStyle="None">
                    <Columns>
                        <asp:BoundField HeaderText="Producto" DataField="Producto.Nombre" />
                        <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                        <asp:BoundField HeaderText="Precio unitario" DataField="PrecioUnitario" DataFormatString="{0:$ #,##0.00}" />
                        <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:$ #,##0.00}" />
                    </Columns>
                </asp:GridView>
            </div>
        </asp:Panel>

    </main>

</asp:Content>
