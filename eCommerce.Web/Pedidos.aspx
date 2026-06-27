<%@ Page Title="Pedidos" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Pedidos.aspx.cs" Inherits="eCommerce.Web.Pedidos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <h1 class="h3 mb-3">Administracion de pedidos</h1>

        <asp:Label runat="server" ID="lblMensaje" Visible="false" CssClass="alert d-block" />

        <div class="row g-3 align-items-end mb-3">
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="ddlEstado" CssClass="form-label" Text="Estado" />
                <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-select" />
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
                <asp:Button runat="server" ID="btnFiltrar" CssClass="btn btn-outline-primary w-100" Text="Filtrar" OnClick="btnFiltrar_Click" />
            </div>
        </div>

        <asp:GridView runat="server" ID="dgvPedidos" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" DataKeyNames="Id" OnSelectedIndexChanged="dgvPedidos_SelectedIndexChanged" EmptyDataText="No hay pedidos para mostrar.">
            <Columns>
                <asp:BoundField HeaderText="Pedido" DataField="Id" />
                <asp:BoundField HeaderText="Fecha" DataField="FechaCreacion" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField HeaderText="Cliente" DataField="Usuario.Email" />
                <asp:BoundField HeaderText="Estado" DataField="EstadoPedido.Descripcion" />
                <asp:BoundField HeaderText="Forma de pago" DataField="FormaPago.Descripcion" />
                <asp:BoundField HeaderText="Forma de entrega" DataField="FormaEntrega.Descripcion" />
                <asp:BoundField HeaderText="Total" DataField="Total" DataFormatString="{0:C}" />
                <asp:CommandField HeaderText="Detalle" ShowSelectButton="true" SelectText="Ver detalle" />
            </Columns>
        </asp:GridView>

        <asp:Panel runat="server" ID="pnlDetalle" Visible="false" CssClass="mt-4">
            <div class="d-flex flex-column flex-md-row justify-content-between gap-3 mb-3">
                <div>
                    <h2 class="h5 mb-1">Detalle del pedido</h2>
                    <asp:Label runat="server" ID="lblPedidoSeleccionado" CssClass="text-muted" />
                </div>
                <div class="d-flex gap-2 align-items-end">
                    <div>
                        <asp:Label runat="server" AssociatedControlID="ddlEstadoNuevo" CssClass="form-label" Text="Cambiar estado" />
                        <asp:DropDownList runat="server" ID="ddlEstadoNuevo" CssClass="form-select" />
                    </div>
                    <asp:Button runat="server" ID="btnActualizarEstado" CssClass="btn btn-primary" Text="Actualizar" OnClick="btnActualizarEstado_Click" />
                </div>
            </div>

            <asp:GridView runat="server" ID="dgvDetalle" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" EmptyDataText="El pedido no tiene productos cargados.">
                <Columns>
                    <asp:BoundField HeaderText="Producto" DataField="Producto.Nombre" />
                    <asp:BoundField HeaderText="Cantidad" DataField="Cantidad" />
                    <asp:BoundField HeaderText="Precio unitario" DataField="PrecioUnitario" DataFormatString="{0:C}" />
                    <asp:BoundField HeaderText="Subtotal" DataField="Subtotal" DataFormatString="{0:C}" />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </main>
</asp:Content>
