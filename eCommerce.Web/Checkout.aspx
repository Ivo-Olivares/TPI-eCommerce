<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" 
AutoEventWireup="true" CodeBehind="Checkout.aspx.cs" Inherits="eCommerce.Web.Checkout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="container mt-4">
        <h2>Checkout</h2>
        <p>Completa los datos para finalizar la compra.</p>
        <hr />
        <h4> Datos de entrega </h4>

        <div class="mb-3">
            <label>Tipo de entrega</label>
            <asp:DropDownlist ID="ddlEntrega" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccionar" Value="" />
                <asp:ListItem Text="Retiro en sucursal" Value="Retiro" />
                <asp:ListItem Text="Envio a domicilio" Value="Envio" />   
            </asp:DropDownlist>
        </div>

        <div class="mb-3">
            <label>Direccion</label>
            <asp:textbox ID="txtDireccion" runat="server" Cssclass="form-control" /> </div>

        <div class="mb-3">
            <label> Forma de pago </label>
            <asp:DropDownList ID="ddlFormaPago" runat="server" CssClass="form-control">
                <asp:ListItem Text="Seleccionar" Value="" />
                <asp:ListItem Text="Efectivo" Value="Efectivo" />
                <asp:ListItem Text="Tranferencia" Value="Tranferencia" />
                <asp:ListItem Text="Tarjeta" Value="Tarjeta" />
            </asp:DropDownList>
        </div>

        <hr />
        <h4> Resumen del pedido </h4>
            <div class="card p-3 mb-3">
                <p><strong>Productos:</strong> Pendiente de implementacion</p>
                <p><strong>Total</strong>Pendiente de implementacion</p>
            </div>

    <asp:Button ID="btnConfirmar" runat="server" text="Confirmar compra" CssClass="btn btn-primary" />

    </div>

</asp:Content>