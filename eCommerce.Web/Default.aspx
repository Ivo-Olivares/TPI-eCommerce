<%@ Page Title="Inicio" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="eCommerce.Web._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloInicio">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <span class="app-badge app-badge-primary mb-2">Bienvenido</span>

                    <h1 id="tituloInicio" class="app-title mb-2 fs-2">UTN eCommerce
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Encontrá productos de distintas categorías y realizá tus compras de forma simple y organizada.
                    </p>
                </div>

                <div class="col-md-4 text-md-end text-start">
                    <a runat="server" href="~/Catalogo.aspx" class="app-btn-primary">Ver catálogo →
                    </a>
                </div>
            </div>
        </section>

        <section class="mb-5">
            <div class="d-flex justify-content-between align-items-center mb-3">
                <h2 class="app-title fs-3 mb-0">Productos</h2>
                <a runat="server" href="~/Catalogo.aspx" class="app-btn-link">Ver todos →</a>
            </div>

            <div class="row g-4">
                <asp:Repeater ID="rptProductos" runat="server">
                    <ItemTemplate>
                        <div class="col-sm-6 col-lg-3">
                            <article class="app-card app-card-interactive h-100">
                                <div class="app-icon-box app-icon-box-primary w-100 mb-3" style="height: 140px;">
                                    <span>🛍️</span>
                                </div>

                                <span class="app-badge app-badge-primary mb-2">
                                    <%# Eval("Categoria.Nombre") %>
                                </span>

                                <h3 class="app-card-title fs-6">
                                    <%# Eval("Nombre") %>
                                </h3>

                                <p class="app-text-muted mb-3">
                                    Producto disponible para agregar al carrito.
                                </p>

                                <div class="d-flex justify-content-between align-items-center">
                                    <strong><%# Eval("Precio", "{0:C}") %></strong>
                                    <a href='<%# "DetalleProducto.aspx?id=" + Eval("Id") %>' class="app-btn-secondary py-1 px-3">Ver detalle
                                    </a>
                                </div>
                            </article>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

        <section class="mb-4">
            <h2 class="app-title fs-3 mb-3">Categorías</h2>

            <div class="row g-4">
                <asp:Repeater ID="rptCategorias" runat="server">
                    <ItemTemplate>
                        <div class="col-6 col-lg-3">
                            <a href='<%# "Catalogo.aspx?idCategoria=" + Eval("Id") %>' class="text-decoration-none">
                                <div class="app-card app-card-interactive text-center py-4">
                                    <div class="app-icon-box app-icon-box-primary mb-3">
                                        <span>🛍️</span>
                                    </div>

                                    <h3 class="app-card-title fs-6 mb-0">
                                        <%# Eval("Nombre") %>
                                    </h3>
                                </div>
                            </a>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </section>

    </main>

</asp:Content>
