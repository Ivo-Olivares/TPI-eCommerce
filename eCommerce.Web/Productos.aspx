<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Productos.aspx.cs" Inherits="eCommerce.Web.Productos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row mb-3">
        <div class="col-md-4">
            <label>Buscar producto</label>
            <asp:TextBox runat="server" ID="txtFiltroProducto" CssClass="form-control" />
        </div>

        <div class="col-md-2">
            <label>Estado</label>
            <asp:DropDownList runat="server" ID="ddlFiltroEstado" CssClass="form-control">
                <asp:ListItem Text="Todos" Value="" />
                <asp:ListItem Text="Activos" Value="1" />
                <asp:ListItem Text="Inactivos" Value="0" />
            </asp:DropDownList>
        </div>

        <div class="col-md-3 d-flex align-items-end">
            <asp:Button runat="server" ID="btnFiltrar" Text="Filtrar" CssClass="btn btn-primary w-100" OnClick="btnFiltrar_Click" />
        </div>

        <div class="col-md-3 d-flex align-items-end">
            <asp:Button runat="server" ID="btnLimpiarFiltro" Text="Limpiar" CssClass="btn btn-outline-secondary w-100" OnClick="btnLimpiarFiltro_Click" />
        </div>
    </div>

    <div class="row">
        <div class="col">
            <div class="table-responsive">
                <asp:GridView runat="server" ID="dgvProductos" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCommand="dgvProductos_RowCommand">
                    <Columns>
                        <asp:BoundField HeaderText="Id" DataField="Id" />
                        <asp:BoundField HeaderText="Sku" DataField="Sku" />
                        <asp:BoundField HeaderText="Producto" DataField="Nombre" />
                        <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                        <asp:BoundField HeaderText="Marca" DataField="Marca" />
                        <asp:BoundField HeaderText="Categoria" DataField="Categoria" />
                        <asp:BoundField HeaderText="Precio" DataField="Precio" />
                        <asp:BoundField HeaderText="Stock" DataField="Stock" />
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
    </div>

    <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>

    <br />

    <asp:TextBox runat="server" ID="txtSku" CssClass="form-control" Placeholder="Sku" />

    <br />

    <asp:TextBox runat="server" ID="txtNombreProducto" CssClass="form-control" Placeholder="Nombre del producto" />

    <br />

    <asp:TextBox runat="server" ID="txtDescripcion" CssClass="form-control" Placeholder="Descripcion" TextMode="MultiLine" Rows="3" />

    <br />

    <asp:DropDownList runat="server" ID="ddlCategoria" CssClass="form-control" />

    <br />

    <asp:DropDownList runat="server" ID="ddlMarca" CssClass="form-control" />

    <br />

    <asp:TextBox runat="server" ID="txtPrecio" CssClass="form-control" Placeholder="Precio" />

    <br />

    <asp:TextBox runat="server" ID="txtStock" CssClass="form-control" Placeholder="Stock" />

    <br />

    <asp:TextBox runat="server" ID="txtUrlImagen" CssClass="form-control" Placeholder="URL de imagen" />

    <br />

    <asp:Button ID="btnAgregarProducto" runat="server" Text="Agregar Producto" CssClass="btn btn-primary" OnClick="btnAgregarProducto_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />

</asp:Content>
