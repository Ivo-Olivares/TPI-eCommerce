<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormasPago.aspx.cs" Inherits="eCommerce.Web.FormasPago" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


   <div class="row mb-3">
       <div class="col-md-4">

              <asp:TextBox runat="server" 
                ID="txtFiltroDescripcion"
                cssclass="form-control"
                placeholder ="filtrar por descripcion"
                AutopostBack="true"
                OnTextchanged ="txtFiltroDescripcion_TextChanged" />
           </div>

       
        <div class="col-md-3">
              <asp:DropDownList
                runat="server"
                ID="ddlFiltroEstado"
                CssClass="form-control"
                AutoPostBack="true"
                OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">
                <asp:ListItem Text="Todos" Value="Todos" />
                <asp:ListItem Text="Activos" Value="Activos" />
                <asp:ListItem Text="Inactivos" Value="Inactivos" />
            </asp:DropDownList>
           </div>

         </div>

        <div class="row">
        <div class="col">
            <asp:GridView runat="server" ID="dgvFormasPago" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCommand="dgvFormasPago_RowCommand" >
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

    <asp:TextBox runat="server" ID="txtNombreFormaPago" CssClass="form-control" Placeholder="Forma de Pago" />

    <br />

    <asp:Button ID="btnAgregarFormaPago" runat="server" Text="Agregar forma de pago" CssClass="btn btn-primary" OnClick="btnAgregarFormaPago_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />


</asp:Content>