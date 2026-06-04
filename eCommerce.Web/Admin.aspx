<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="eCommerce.Web.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h2 class="mb-4">Panel de Administración</h2>

    <div class="row mb-3">
        <div class="col-md-6">
            <a href="Categorias.aspx" class="btn btn-primary w-100">Categorias</a>
        </div>

        <div class="col-md-6">
            <a href="Marcas.aspx" class="btn btn-primary w-100">Marcas</a>
        </div>
    </div>

    <div class="row mb-3">
        <div class="col-md-6">
            <a href="FormasPago.aspx" class="btn btn-primary w-100">Formas de Pago</a>
        </div>

        <div class="col-md-6">
            <a href="FormasEntrega.aspx" class="btn btn-primary w-100">Formas de Entrega</a>
        </div>
    </div>

</asp:Content>