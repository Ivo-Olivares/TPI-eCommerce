<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="eCommerce.Web.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloPedidos">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <h1 id="tituloPedidos" class="app-title mb-2 fs-2">Pedidos
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Revisa las compras realizadas y actualiza el estado de cada pedido.
                    </p>
                </div>

                <div class="col-md-4 text-md-end text-start">
                    <a runat="server" href="~/Admin.aspx" class="app-btn-secondary">&larr; Volver al panel
                    </a>
                </div>
            </div>
        </section>

        <asp:Label runat="server" ID="lblMensaje" Visible="false" CssClass="app-alert d-block mb-4" />

        <section class="app-card mb-4">
            <div class="row g-3 align-items-end">
                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="txtCliente" CssClass="app-form-label" Text="Cliente" />
                    <asp:TextBox runat="server" ID="txtCliente" CssClass="app-input" Placeholder="Nombre, apellido o email" />
                </div>

                <div class="col-md-3">
                    <asp:Label runat="server" AssociatedControlID="ddlEstado" CssClass="app-form-label" Text="Estado" />
                    <asp:DropDownList runat="server" ID="ddlEstado" CssClass="app-select" />
                </div>

                <div class="col-md-2">
                    <asp:Label runat="server" AssociatedControlID="txtFechaDesde" CssClass="app-form-label" Text="Desde" />
                    <asp:TextBox runat="server" ID="txtFechaDesde" CssClass="app-input" TextMode="Date" />
                </div>

                <div class="col-md-2">
                    <asp:Label runat="server" AssociatedControlID="txtFechaHasta" CssClass="app-form-label" Text="Hasta" />
                    <asp:TextBox runat="server" ID="txtFechaHasta" CssClass="app-input" TextMode="Date" />
                </div>

                <div class="col-md-2">
                    <asp:Button runat="server" ID="btnFiltrar" CssClass="app-btn-primary w-100" Text="Filtrar" OnClick="btnFiltrar_Click" />
                </div>
            </div>
        </section>

        <section class="app-card p-0 mb-4">
            <div class="p-4 border-bottom">
                <h2 class="app-card-title mb-0">Pedidos registrados
                </h2>
            </div>

            <div class="table-responsive">
                <asp:GridView
                    runat="server"
                    ID="dgvPedidos"
                    AutoGenerateColumns="false"
                    CssClass="app-table"
                    GridLines="None"
                    BorderStyle="None"
                    DataKeyNames="Id"
                    EmptyDataText="No hay pedidos para mostrar.">
                    <Columns>
                        <asp:BoundField HeaderText="Pedido" DataField="Id" />
                        <asp:BoundField HeaderText="Fecha" DataField="FechaCreacion" DataFormatString="{0:dd/MM/yyyy}" />
                        <asp:BoundField HeaderText="Cliente" DataField="Usuario.Email" />
                        <asp:BoundField HeaderText="Estado" DataField="EstadoPedido.Descripcion" />
                        <asp:BoundField HeaderText="Forma de pago" DataField="FormaPago.Descripcion" />
                        <asp:BoundField HeaderText="Forma de entrega" DataField="FormaEntrega.Descripcion" />
                        <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:$ #,##0.00}" />

                        <asp:TemplateField HeaderText="Detalle">
                            <ItemTemplate>
                                <asp:HyperLink
                                    runat="server"
                                    ID="lnkVerDetalle"
                                    NavigateUrl='<%# "Pedidos.aspx?id=" + Eval("Id") %>'
                                    Text="Ver detalle"
                                    CssClass="app-btn-link" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </section>

        <asp:Panel runat="server" ID="pnlDetalle" Visible="false" CssClass="app-card p-0 mb-4">
            <div class="p-4 border-bottom">
                <div class="row g-3 align-items-start">
                    <div class="col-lg-5">
                        <h2 class="app-card-title mb-1">Detalle del pedido
                        </h2>

                        <asp:Label runat="server" ID="lblPedidoSeleccionado" CssClass="app-text-muted" />
                    </div>

                    <div class="col-lg-7">
                        <div class="row g-3">
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="ddlEstadoCambio" CssClass="app-form-label" Text="Cambiar estado" />

                                <div class="d-flex flex-column flex-sm-row gap-2">
                                    <asp:DropDownList runat="server" ID="ddlEstadoCambio" CssClass="app-select" />
                                    <asp:Button
                                        runat="server"
                                        ID="btnCambiarEstado"
                                        CssClass="app-btn-primary py-1 px-3"
                                        Text="Actualizar estado"
                                        OnClick="btnCambiarEstado_Click" />
                                </div>
                            </div>

                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtObservacionesInternas" CssClass="app-form-label" Text="Observaciones internas" />
                                <asp:TextBox
                                    runat="server"
                                    ID="txtObservacionesInternas"
                                    CssClass="app-input mb-2"
                                    TextMode="MultiLine"
                                    Rows="3"
                                    MaxLength="500" />
                                <asp:Button
                                    runat="server"
                                    ID="btnGuardarObservaciones"
                                    CssClass="app-btn-secondary py-1 px-3 w-100"
                                    Text="Guardar observaciones"
                                    OnClick="btnGuardarObservaciones_Click" />
                            </div>
                        </div>
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
                    BorderStyle="None"
                    EmptyDataText="El pedido no tiene productos cargados.">
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
