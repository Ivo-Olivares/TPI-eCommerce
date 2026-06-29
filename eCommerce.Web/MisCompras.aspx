<%@ Page Title="Mis compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="eCommerce.Web.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <h1 class="h3 mb-3">Mis compras</h1>

        <div class="row g-3 align-items-end mb-3">
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="ddlEstado" CssClass="form-label" Text="Estado" />
                <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-select">
                </asp:DropDownList>
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="txtFechaDesde" CssClass="form-label" Text="Desde" />
                <asp:TextBox runat="server" ID="txtFechaDesde" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="txtFechaHasta" CssClass="form-label" Text="Hasta" />
                <asp:TextBox runat="server" ID="txtFechaHasta" CssClass="form-control" TextMode="Date" />
            </div>
            <div class="col-md-3">
                <asp:Button runat="server" ID="btnFiltrar" CssClass="btn btn-outline-primary w-100" Text="Filtrar" />
            </div>
        </div>
  

        <asp:GridView runat="server" ID="dgvCompras" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" DataKeyNames="Id" OnSelectedIndexChanged="dgvCompras_SelectedIndexChanged">
            <Columns>
                <asp:BoundField HeaderText="Pedido" DataField="Id" />
                <asp:BoundField HeaderText="Fecha" DataField="FechaCreacion" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField HeaderText="Estado" datafield ="EstadoPedido.Descripcion" />
                <asp:BoundField HeaderText="Forma de pago" datafield ="FormaPago.Descripcion" />
                <asp:BoundField HeaderText="Forma de entrega" datafield ="FormaEntrega.Descripcion"/>
                <asp:BoundField HeaderText="Total" datafield ="Total" DataFormatString="{0:c}"/>
                <asp:CommandField HeaderText="Detalle" ShowSelectButton="true" SelectText="Ver detalle" />
            </Columns>
        </asp:GridView>

        <asp:Panel runat="server" ID="pnlDetalle" Visible="false" CssClass="mt-4">
           <h2 class="h5 mb-3">Detalle del pedido</h2>
            <asp:GridView runat="server" ID="dgvDetalle" AutoGenerateColumns="false" CssClass="table table-bordered table-striped"> <Columns>
            <asp:BoundField HeaderText="Producto" DataField="Producto.Nombre" />
            <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
            <asp:BoundField HeaderText="Precio unitario" DataField="PrecioUnitario" DataFormatString="{0:C}" />
            <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:C}" />
        </Columns>
    </asp:GridView>
</asp:Panel>
    </main>
</asp:Content>
