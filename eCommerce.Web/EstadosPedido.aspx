<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EstadosPedido.aspx.cs" Inherits="eCommerce.Web.EstadosPedido" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col">
            <asp:GridView runat="server" ID="dgvEstadosPedido" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCommand="dgvEstadosPedido_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="Id" />
                    <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                    <asp:TemplateField HeaderText="Activo">
                        <ItemTemplate>
                            <%# (bool)Eval("Activo") ? "Sí" : "No" %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Acción">
                        <ItemTemplate>

                            <asp:LinkButton
                                ID="btnEditar"
                                runat="server"
                                CommandName="Editar"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="Editar" />
                            |
                            <asp:LinkButton
                                ID="btnDesactivar"
                                runat="server"
                                Text='<%# (bool)Eval("Activo") ? "Desactivar" : "Activar" %>'
                                CommandName='<%# (bool)Eval("Activo") ? "Desactivar" : "Activar" %>'
                                CommandArgument='<%# Eval("Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>

    <br />

    <asp:TextBox runat="server" ID="txtNombreEstadosPedido" CssClass="form-control" Placeholder="Estado de Pedido" />

    <br />

    <asp:Button ID="btnAgregarEstadoPedido" runat="server" Text="Agregar Estado de Pedido" CssClass="btn btn-primary" OnClick="btnAgregarEstadoPedido_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />

</asp:Content>
