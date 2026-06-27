<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eCommerce.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="py-4">
        <section class="row align-items-center mb-4" aria-labelledby="tituloInicio">
            <div class="col-md-8">
                <h1 id="tituloInicio">Gestion de ventas eCommerce</h1>
                <p class="lead">Aplicacion Web Forms para administrar catalogo, clientes, pedidos y seguimiento de compras.</p>
            </div>
            <div class="col-md-4 text-md-end">
                <a runat="server" href="~/Catalogo" class="btn btn-primary btn-md">Ver Catalogo</a>
            </div>
        </section>

        <div class="row g-3">
            <section class="col-md-4" aria-labelledby="catalogoTitle">
                <div class="border rounded p-3 h-100">
                    <h2 id="catalogoTitle" class="h4">Catalogo</h2>
                    <p>Productos organizados por categorias, con precio, stock y estado activo o inactivo.</p>
                </div>
            </section>
            <section class="col-md-4" aria-labelledby="comprasTitle">
                <div class="border rounded p-3 h-100">
                    <h2 id="comprasTitle" class="h4">Compras</h2>
                    <p>Flujo previsto para carrito, checkout, forma de entrega, forma de pago y confirmacion.</p>
                </div>
            </section>
            <section class="col-md-4" aria-labelledby="gestionTitle">
                <div class="border rounded p-3 h-100">
                    <h2 id="gestionTitle" class="h4">Administracion</h2>
                    <p>Panel previsto para gestion de productos, categorias, formas de pago, entregas y pedidos.</p>
                </div>
            </section>
        </div>
    </main>

</asp:Content>
