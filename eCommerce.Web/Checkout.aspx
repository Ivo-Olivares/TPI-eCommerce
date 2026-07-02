<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="eCommerce.Web.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloCheckout">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <span class="app-badge app-badge-primary mb-2">Checkout</span>

                    <h1 id="tituloCheckout" class="app-title mb-2 fs-2">Finalizar compra</h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Completá los datos de entrega y pago para confirmar tu pedido.
                    </p>
                </div>

                <div class="col-md-4 text-md-end text-start">
                    <a runat="server" href="~/Carrito.aspx" class="app-btn-secondary">&larr; Volver al carrito
                    </a>
                </div>
            </div>
        </section>

        <asp:Label
            ID="lblError"
            runat="server"
            CssClass="app-alert app-alert-danger d-block mb-4"
            Visible="false" />

        <div class="row g-4 align-items-start">

            <div class="col-lg-5">
                <section class="app-card">
                    <span class="app-badge app-badge-primary mb-3">Datos del pedido</span>

                    <h2 class="app-card-title mb-3">Entrega y pago
                    </h2>

                    <div class="app-form-group">
                        <asp:Label
                            runat="server"
                            AssociatedControlID="ddlEntrega"
                            CssClass="app-form-label"
                            Text="Tipo de entrega" />

                        <asp:DropDownList
                            ID="ddlEntrega"
                            runat="server"
                            CssClass="app-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlEntrega_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <asp:Panel
                        ID="pnlDireccion"
                        runat="server"
                        CssClass="app-form-group">
                        <asp:Label
                            runat="server"
                            AssociatedControlID="ddlDireccion"
                            CssClass="app-form-label"
                            Text="Dirección" />

                        <asp:DropDownList
                            ID="ddlDireccion"
                            runat="server"
                            CssClass="app-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlDireccion_SelectedIndexChanged">
                        </asp:DropDownList>

                        <asp:LinkButton
                            ID="btnMostrarNuevaDireccion"
                            runat="server"
                            CssClass="app-btn-secondary text-center mt-3"
                            Style="display: block; width: 100%; box-sizing: border-box;"
                            Text="Agregar nueva dirección"
                            OnClick="btnMostrarNuevaDireccion_Click" />

                        <asp:Panel
                            ID="pnlNuevaDireccion"
                            runat="server"
                            Visible="false"
                            CssClass="mt-3">
                            <div class="row g-3">
                                <div class="col-md-12">
                                    <asp:Label
                                        runat="server"
                                        AssociatedControlID="txtNuevaDireccionDescripcion"
                                        CssClass="app-form-label"
                                        Text="Descripción" />
                                    <asp:TextBox
                                        ID="txtNuevaDireccionDescripcion"
                                        runat="server"
                                        CssClass="app-input"
                                        Placeholder="Casa, trabajo, etc." />
                                </div>

                                <div class="col-md-8">
                                    <asp:Label
                                        runat="server"
                                        AssociatedControlID="txtNuevaDireccionCalle"
                                        CssClass="app-form-label"
                                        Text="Calle" />
                                    <asp:TextBox
                                        ID="txtNuevaDireccionCalle"
                                        runat="server"
                                        CssClass="app-input" />
                                </div>

                                <div class="col-md-4">
                                    <asp:Label
                                        runat="server"
                                        AssociatedControlID="txtNuevaDireccionAltura"
                                        CssClass="app-form-label"
                                        Text="Número" />
                                    <asp:TextBox
                                        ID="txtNuevaDireccionAltura"
                                        runat="server"
                                        CssClass="app-input"
                                        TextMode="Number" />
                                </div>

                                <div class="col-md-6">
                                    <asp:Label
                                        runat="server"
                                        AssociatedControlID="txtNuevaDireccionLocalidad"
                                        CssClass="app-form-label"
                                        Text="Localidad" />
                                    <asp:TextBox
                                        ID="txtNuevaDireccionLocalidad"
                                        runat="server"
                                        CssClass="app-input" />
                                </div>

                                <div class="col-md-6">
                                    <asp:Label
                                        runat="server"
                                        AssociatedControlID="txtNuevaDireccionProvincia"
                                        CssClass="app-form-label"
                                        Text="Provincia" />
                                    <asp:TextBox
                                        ID="txtNuevaDireccionProvincia"
                                        runat="server"
                                        CssClass="app-input" />
                                </div>

                                <div class="col-md-12">
                                    <asp:Label
                                        runat="server"
                                        AssociatedControlID="txtNuevaDireccionCp"
                                        CssClass="app-form-label"
                                        Text="Código postal" />
                                    <asp:TextBox
                                        ID="txtNuevaDireccionCp"
                                        runat="server"
                                        CssClass="app-input" />
                                </div>

                                <div class="col-md-12">
                                    <asp:Label
                                        runat="server"
                                        AssociatedControlID="txtNuevaDireccionObservaciones"
                                        CssClass="app-form-label"
                                        Text="Observaciones" />
                                    <asp:TextBox
                                        ID="txtNuevaDireccionObservaciones"
                                        runat="server"
                                        CssClass="app-input"
                                        TextMode="MultiLine"
                                        Rows="3" />
                                </div>

                                <div class="col-md-12 d-flex flex-wrap gap-2">
                                    <asp:Button
                                        ID="btnGuardarNuevaDireccion"
                                        runat="server"
                                        CssClass="app-btn-primary"
                                        Text="Guardar dirección"
                                        OnClick="btnGuardarNuevaDireccion_Click" />

                                    <asp:Button
                                        ID="btnCancelarNuevaDireccion"
                                        runat="server"
                                        CssClass="app-btn-secondary"
                                        Text="Cancelar"
                                        OnClick="btnCancelarNuevaDireccion_Click" />
                                </div>
                            </div>
                        </asp:Panel>
                    </asp:Panel>

                    <div class="app-form-group mb-0">
                        <asp:Label
                            runat="server"
                            AssociatedControlID="ddlFormaPago"
                            CssClass="app-form-label"
                            Text="Forma de pago" />

                        <asp:DropDownList
                            ID="ddlFormaPago"
                            runat="server"
                            CssClass="app-select"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlFormaPago_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </section>
            </div>

            <div class="col-lg-7">
                <section class="app-card p-0">
                    <div class="p-4 border-bottom">
                        <span class="app-badge app-badge-primary mb-3">Resumen</span>

                        <h2 class="app-card-title mb-1">Resumen del pedido
                        </h2>

                        <p class="app-text-muted mb-0">
                            Revisá los productos antes de confirmar la compra.
                        </p>
                    </div>

                    <div class="table-responsive">
                        <asp:GridView
                            ID="dgvResumen"
                            runat="server"
                            AutoGenerateColumns="False"
                            CssClass="app-table"
                            GridLines="None"
                            BorderStyle="None">
                            <Columns>
                                <asp:TemplateField HeaderText="Producto">
                                    <ItemTemplate>
                                        <strong style="color: var(--color-slate-900);">
                                            <%# Eval("Producto.Nombre") %>
                                        </strong>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" />

                                <asp:BoundField
                                    DataField="PrecioUnitario"
                                    HeaderText="Precio unitario"
                                    DataFormatString="{0:$ #,##0.00}" />

                                <asp:BoundField
                                    DataField="Subtotal"
                                    HeaderText="Subtotal"
                                    DataFormatString="{0:$ #,##0.00}" />
                            </Columns>
                        </asp:GridView>
                    </div>

                    <div class="p-4 border-top">
                        <div class="row g-3 mb-4">
                            <div class="col-md-4">
                                <span class="app-text-muted d-block">Forma de entrega</span>
                                <strong style="color: var(--color-slate-900);">
                                    <asp:Label ID="lblResumenEntrega" runat="server" Text="Pendiente" />
                                </strong>
                            </div>

                            <div class="col-md-4">
                                <span class="app-text-muted d-block">Dirección</span>
                                <strong style="color: var(--color-slate-900);">
                                    <asp:Label ID="lblResumenDireccion" runat="server" Text="Pendiente" />
                                </strong>
                            </div>

                            <div class="col-md-4">
                                <span class="app-text-muted d-block">Forma de pago</span>
                                <strong style="color: var(--color-slate-900);">
                                    <asp:Label ID="lblResumenPago" runat="server" Text="Pendiente" />
                                </strong>
                            </div>
                        </div>

                        <div class="d-flex justify-content-between align-items-center mb-4">
                            <span class="app-card-title mb-0">Total</span>

                            <strong style="color: var(--color-slate-900); font-size: 1.25rem;">
                                <asp:Label ID="lblTotal" runat="server" Text="$ 0,00" />
                            </strong>
                        </div>

                        <div class="d-flex flex-column gap-2">
                            <asp:LinkButton
                                ID="btnConfirmar"
                                runat="server"
                                Text="Confirmar compra"
                                CssClass="app-btn-primary text-center"
                                Style="display: block; width: 100%; box-sizing: border-box;"
                                OnClick="btnConfirmar_Click" />

                            <a runat="server" href="~/Catalogo.aspx" class="app-btn-secondary text-center" style="display: block; width: 100%; box-sizing: border-box; padding: 0.75rem 1.5rem;">Seguir comprando</a>
                        </div>
                    </div>
                </section>
            </div>

        </div>

    </main>

</asp:Content>
