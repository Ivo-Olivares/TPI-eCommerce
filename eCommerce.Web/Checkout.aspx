<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master"
    AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="eCommerce.Web.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <h2>Checkout</h2>
        <p>Completa los datos para finalizar la compra.</p>

        <hr />

        <h4>Datos de entrega</h4>

        <div class="mb-3">
            <label>Tipo de entrega</label>
            <asp:DropDownList ID="ddlEntrega" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>

        <div class="mb-3">
            <label>Dirección</label>
            <asp:DropDownList ID="ddlDireccion" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>

        <div class="mb-3">
            <label>Forma de pago</label>
            <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-control">
            </asp:DropDownList>
        </div>

        <hr />

        <h4>Resumen del pedido</h4>

        <div class="card p-3 mb-3">
            <asp:GridView ID="dgvResumen" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField HeaderText="Producto" DataField="Producto" />
                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                    <asp:BoundField HeaderText="Precio unitario" DataField="PrecioUnitario" DataFormatString="{0:C}" />
                    <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>

            <div class="text-end mt-3">
                <strong>Total:</strong>
                <asp:Label ID="lblTotal" runat="server" Text="$ 0,00" />
            </div>
        </div>

        <asp:Label ID="lblError" runat="server" CssClass="text-danger d-block mb-3" Visible="false" />

        <asp:Button ID="btnConfirmar" runat="server" Text="Confirmar compra" CssClass="btn btn-primary" OnClick="btnConfirmar_Click" />
    </div>

</asp:Content>
