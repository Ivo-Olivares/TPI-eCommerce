<%@ Page Title="Mis compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="eCommerce.Web.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <h1 class="h3 mb-3">Mis compras</h1>

        <div class="row g-3 align-items-end mb-3">
            <div class="col-md-3">
                <asp:Label runat="server" AssociatedControlID="ddlEstado" CssClass="form-label" Text="Estado" />
                <asp:DropDownList runat="server" ID="ddlEstado" CssClass="form-select">
                    <asp:ListItem Text="Todos" Value="" />
                    <asp:ListItem Text="Pendiente" Value="Pendiente" />
                    <asp:ListItem Text="Pagado" Value="Pagado" />
                    <asp:ListItem Text="Enviado" Value="Enviado" />
                    <asp:ListItem Text="Entregado" Value="Entregado" />
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
                <asp:Button runat="server" ID="btnFiltrar" CssClass="btn btn-outline-primary w-100" Text="Filtrar" OnClick="btnFiltrar_Click" />
            </div>
        </div>
  

        <asp:GridView runat="server" ID="dgvCompras" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
            <Columns>
                <asp:BoundField HeaderText="Pedido" DataField="Id" />
                <asp:BoundField HeaderText="Fecha" DataField="FechaCreacion" DataFormatString="{0:dd/mm/yyyy}" />
                <asp:BoundField HeaderText="Estado" datafield ="EstadoPedido.Descripcion" />
                <asp:BoundField HeaderText="Forma de pago" datafield ="FormaPago.Descripcion" />
                <asp:BoundField HeaderText="Forma de entrega" datafield ="FormaEntrega.Descripcion"/>
                <asp:BoundField HeaderText="Total" datafield ="Total" DataFormatString="{0:c}"/>
            </Columns>
        </asp:GridView>
    </main>
</asp:Content>
