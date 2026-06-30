<%@ Page Title="Mi perfil" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="MiPerfil.aspx.cs" Inherits="eCommerce.Web.MiPerfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <main class="py-4">
        <h1 class="h3 mb-3">Mi perfil</h1>
        <asp:Label runat="server" ID="lblMensaje" Visible="false" CssClass="alert d-block" />

        <h2 class="h5 mb-3">Datos personales</h2>
        <div class="row g-3">
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtNombre" CssClass="form-label" Text="Nombre" />
                <asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtApellido" CssClass="form-label" Text="Apellido" />
                <asp:TextBox runat="server" ID="txtApellido" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtEmail" CssClass="form-label" Text="Email" />
                <asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" TextMode="Email" ReadOnly="true" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtTelefono" CssClass="form-label" Text="Telefono" />
                <asp:TextBox runat="server" ID="txtTelefono" CssClass="form-control" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtDni" CssClass="form-label" Text="DNI" />
                <asp:TextBox runat="server" ID="txtDni" CssClass="form-control" ReadOnly="true" />
            </div>
            <div class="col-md-6">
                <asp:Label runat="server" AssociatedControlID="txtFechaNacimiento" CssClass="form-label" Text="Fecha de nacimiento" />
                <asp:TextBox runat="server" ID="txtFechaNacimiento" CssClass="form-control" ReadOnly="true" />
            </div>
        </div>

        <div class="d-flex justify-content-between align-items-center mt-4 mb-2">
            <h2 class="h5 mb-0">Direcciones</h2>
        </div>
        <asp:GridView runat="server" ID="dgvDirecciones" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" EmptyDataText="No tenes direcciones cargadas.">
            <Columns>
                <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />
                <asp:BoundField HeaderText="Calle" DataField="Calle" />
                <asp:BoundField HeaderText="Numero" DataField="Altura" />
                <asp:BoundField HeaderText="Localidad" DataField="Localidad" />
                <asp:BoundField HeaderText="Codigo postal" DataField="Cp" />
            </Columns>
        </asp:GridView>

        <div class="mt-3">
            <asp:Button runat="server" ID="btnGuardarPerfil" CssClass="btn btn-primary" Text="Guardar cambios" OnClick="btnGuardarPerfil_Click" />
        </div>
    </main>
</asp:Content>
