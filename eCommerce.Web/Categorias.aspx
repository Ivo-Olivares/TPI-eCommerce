<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="eCommerce.Web.Categorias" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col">
            <asp:GridView runat="server" ID="dgvCategorias" AutoGenerateColumns="false" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="Id" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
                    <asp:BoundField HeaderText="Activo" DataField="Activo" />
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>

    <br />

    <asp:TextBox runat="server" ID="txtNombreCategoria" CssClass="form-control" Placeholder="Nombre de la categoria" />

    <br />


    <asp:Button ID="btnAgregarCategoria" runat="server" Text="Agregar Categoria" CssClass="btn btn-primary" OnClick="btnAgregarCategoria_Click" />

</asp:Content>
