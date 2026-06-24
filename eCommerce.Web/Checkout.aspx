<%@ Page Title="Checkout" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="eCommerce.Web.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <div class="d-flex justify-content-between align-items-center mb-3">
            <div>
                <h1 class="h3 mb-1">Checkout</h1>
                <p class="text-muted mb-0">Completa los datos para finalizar la compra.</p>
            </div>
            <a runat="server" href="~/Carrito" class="btn btn-outline-secondary">Volver al carrito</a>
        </div>

        <asp:Label runat="server" ID="lblMensaje" CssClass="alert alert-info d-block" Visible="false" />

        <asp:Panel runat="server" ID="pnlCheckout">
            <div class="row g-4">
                <div class="col-lg-7">
                    <div class="border rounded p-3 mb-3">
                        <h2 class="h5 mb-3">Datos de entrega</h2>

                        <div class="row g-3">
                            <div class="col-md-8">
                                <asp:Label runat="server" AssociatedControlID="txtCalle" CssClass="form-label" Text="Calle" />
                                <asp:TextBox runat="server" ID="txtCalle" CssClass="form-control" />
                            </div>
                            <div class="col-md-4">
                                <asp:Label runat="server" AssociatedControlID="txtAltura" CssClass="form-label" Text="Altura" />
                                <asp:TextBox runat="server" ID="txtAltura" CssClass="form-control" TextMode="Number" />
                            </div>
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtLocalidad" CssClass="form-label" Text="Localidad" />
                                <asp:TextBox runat="server" ID="txtLocalidad" CssClass="form-control" />
                            </div>
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtProvincia" CssClass="form-label" Text="Provincia" />
                                <asp:TextBox runat="server" ID="txtProvincia" CssClass="form-control" />
                            </div>
                            <div class="col-md-4">
                                <asp:Label runat="server" AssociatedControlID="txtCp" CssClass="form-label" Text="Codigo postal" />
                                <asp:TextBox runat="server" ID="txtCp" CssClass="form-control" TextMode="Number" />
                            </div>
                            <div class="col-md-8">
                                <asp:Label runat="server" AssociatedControlID="txtObservaciones" CssClass="form-label" Text="Observaciones" />
                                <asp:TextBox runat="server" ID="txtObservaciones" CssClass="form-control" />
                            </div>
                        </div>
                    </div>

                    <div class="border rounded p-3">
                        <h2 class="h5 mb-3">Pago y entrega</h2>

                        <div class="row g-3">
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="ddlFormaEntrega" CssClass="form-label" Text="Forma de entrega" />
                                <asp:DropDownList runat="server" ID="ddlFormaEntrega" CssClass="form-select" />
                            </div>
                            <div class="col-md-6">
                                <asp:Label runat="server" AssociatedControlID="ddlFormaPago" CssClass="form-label" Text="Forma de pago" />
                                <asp:DropDownList runat="server" ID="ddlFormaPago" CssClass="form-select" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-5">
                    <div class="border rounded p-3">
                        <h2 class="h5 mb-3">Resumen del pedido</h2>

                        <asp:GridView runat="server" ID="dgvResumen" AutoGenerateColumns="false" CssClass="table table-sm">
                            <Columns>
                                <asp:BoundField DataField="NombreProducto" HeaderText="Producto" />
                                <asp:BoundField DataField="Cantidad" HeaderText="Cant." />
                                <asp:BoundField DataField="Subtotal" HeaderText="Subtotal" DataFormatString="{0:C}" />
                            </Columns>
                        </asp:GridView>

                        <div class="d-flex justify-content-between fs-5">
                            <span>Total</span>
                            <strong>
                                <asp:Label runat="server" ID="lblTotal" />
                            </strong>
                        </div>

                        <div class="d-grid mt-3">
                            <button type="submit" name="accionCheckout" value="confirmar" class="btn btn-primary">Confirmar compra</button>
                        </div>
                    </div>
                </div>
            </div>
        </asp:Panel>
    </main>
</asp:Content>
