<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FormasEntrega.aspx.cs" Inherits="eCommerce.Web.FormasEntrega" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="row">
        <div class="col">
            <asp:GridView runat="server" ID="dgvFormasEntrega" AutoGenerateColumns="false" CssClass="table table-bordered">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="Id" />
                    <asp:BoundField HeaderText="Descripcion" DataField="Descripcion" />       
                </Columns>
            </asp:GridView>
        </div>
    </div>

</asp:Content>