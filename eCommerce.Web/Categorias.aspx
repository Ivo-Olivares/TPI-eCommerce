<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="eCommerce.Web.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


      <div class="row mb-3">
     <div class="col-md-4">

         <asp:TextBox 
             runat="server" ID="txtFiltroNombre"
             cssClass="form-control" 
             placeholder="filtrar por nombre"
             AutoPostBack ="true" 
             OntextChanged="txtFiltroNombre_TextChanged"/>
     </div>
       

      <div class="col-md-3">
          <asp:DropDownList 
              runat="server"
              ID="ddlFiltroEstado"
              CssClass="form-control"
              AutoPostBack="true" OnSelectedIndexChanged="ddlFiltroEstado_SelectedIndexChanged">

          <asp:ListItem Text="Todos" Value="Todos" />
          <asp:ListItem Text="Activos" Value="Activos" />
          <asp:ListItem Text="Inactivos" Value="Inactivos" />
      </asp:DropDownList>
      </div>
      
     
</div>
    <div class="row">
        <div class="col">
            <asp:GridView runat="server" ID="dgvCategorias" DataKeyNames="Id" AutoGenerateColumns="false" CssClass="table table-bordered" OnRowCommand="dgvCategorias_RowCommand">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="Id" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
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
                            |
                            <asp:LinkButton
                                ID="btnEliminar"
                                runat="server"
                                CommandName="Eliminar"
                                CommandArgument='<%# Eval("Id") %>'
                                Text="Eliminar"
                                OnClientClick="return confirm('Esta accion no se puede deshacer. ¿Desea eliminar la categoria?');" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>

    <br />

    <asp:TextBox runat="server" ID="txtNombreCategoria" CssClass="form-control" Placeholder="Nombre de la categoria" />

    <br />

    <asp:Button ID="btnAgregarCategoria" runat="server" Text="Agregar Categoria" CssClass="btn btn-primary" OnClick="btnAgregarCategoria_Click" />
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="false" OnClick="btnCancelar_Click" CssClass="btn btn-secondary" />

</asp:Content>
