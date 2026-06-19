<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Marcas.aspx.cs" Inherits="eCommerce.Web.Marcas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


   
    <div class="row">
        <div class="col">
            <asp:GridView runat="server" ID="dgvMarcas" AutoGenerateColumns="false" CssClass="table table-bordered"
                OnSelectedIndexChanged="dgvMarcas_SelectedIndexChanged">
                <Columns>
                    <asp:BoundField HeaderText="Id" DataField="Id" />
                    <asp:BoundField HeaderText="Nombre" DataField="Nombre" />     
                    
                    <asp:CommandField
                        ShowSelectButton="true"
                        selectText="Seleccionar" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
     <asp:CheckBox ID="chkMostrarInactivas"
     runat="server"
     text ="Mostrar marcas Inactivas" 
     autopostback="true"
      OnCheckedChanged="chkMostrarInactivas_CheckedChanged"/>
     <br />
     <br />

    
    <asp:label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
   
    <br />
    
    <asp:TextBox runat="server" ID="txtNombreMarca" CssClass="form-control" Placeholder="Nombre de la marca" />
    
    <asp:HiddenField runat="server" ID="hfIdMarca" />
    
    <br />


    <asp:Button ID="btnAgregarMarca" runat="server" Text="Agregar Marca" CssClass="btn btn-primary" OnClick="btnAgregarMarca_Click" />
    <asp:Button ID="btnModificarMarca" runat="server" Text="Modificar Marca" CssClass="btn btn-warning" OnClick="btnModificarMarca_Click" />
    <asp:Button ID="btnDesactivarMarca" runat="server" Text="Desactivar Marca" CssClass="btn btn-danger" OnClick="btnDesactivarMarca_Click" />
    <asp:Button ID="btnActicarMarca" runat="server" Text="Activar Marca" CssClass="btn btn-primary" OnClick="btnActicarMarca_Click"/>
</asp:Content>