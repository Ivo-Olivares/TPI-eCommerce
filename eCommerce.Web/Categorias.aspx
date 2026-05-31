<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Categorias.aspx.cs" Inherits="eCommerce.Web.Categorias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col">
            <asp:GridView runat="server" ID="dgvCategorias" AutoGenerateColumns="false" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="Id" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />       
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>
