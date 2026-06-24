<%@ Page Title="Mis compras" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MisCompras.aspx.cs" Inherits="eCommerce.Web.MisCompras" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <h1 class="h3 mb-3">Mis compras</h1>

        <asp:Label runat="server" ID="lblMensaje" CssClass="alert alert-info d-block" Visible="false" />

        <asp:Panel runat="server" ID="pnlCompras">
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
                <button type="submit" name="accionCompras" value="filtrar" class="btn btn-outline-primary w-100">Filtrar</button>
            </div>
        </div>

        <asp:GridView runat="server" ID="dgvCompras" AutoGenerateColumns="false" CssClass="table table-bordered table-striped">
            <Columns>
                <asp:BoundField DataField="Id" HeaderText="Pedido" />
                <asp:BoundField DataField="FechaCreacion" HeaderText="Fecha" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
                <asp:TemplateField HeaderText="Estado">
                    <ItemTemplate>
                        <%# Eval("EstadoPedido.Descripcion") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Forma de pago">
                    <ItemTemplate>
                        <%# Eval("FormaPago.Descripcion") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Forma de entrega">
                    <ItemTemplate>
                        <%# Eval("FormaEntrega.Descripcion") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Total" HeaderText="Total" DataFormatString="{0:C}" />
            </Columns>
        </asp:GridView>
        </asp:Panel>
    </main>
</asp:Content>
