<%@ Page Title="Panel de administracion" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Admin.aspx.cs" Inherits="eCommerce.Web.Admin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <main class="app-container app-section">

        <section class="app-hero mb-4" aria-labelledby="tituloAdmin">
            <div class="row align-items-center g-3">
                <div class="col-md-8">
                    <span class="app-badge app-badge-primary mb-2">Administracion</span>

                    <h1 id="tituloAdmin" class="app-title mb-2 fs-2">
                        Panel de administracion
                    </h1>

                    <p class="app-subtitle mb-0 fs-6">
                        Gestiona las secciones principales del eCommerce.
                    </p>
                </div>

                <div class="col-md-4 text-md-end text-start">
                    <a runat="server" href="~/Catalogo.aspx" class="app-btn-secondary">
                        Ver catalogo →
                    </a>
                </div>
            </div>
        </section>

        <section class="row g-4">

            <div runat="server" id="pnlProductos" class="col-sm-6 col-lg-4">
                <a href="Productos.aspx" class="text-decoration-none">
                    <article class="app-card app-card-interactive h-100">
                        <h2 class="app-card-title mb-2 d-flex align-items-center gap-2">
                            <span>📦</span>
                            <span>Productos</span>
                        </h2>

                        <p class="app-text-muted mb-3">
                            Gestiona el catalogo, precios, stock y estado de los productos.
                        </p>

                        <span class="app-btn-link">
                            Gestionar →
                        </span>
                    </article>
                </a>
            </div>

            <div runat="server" id="pnlPedidos" class="col-sm-6 col-lg-4">
                <a href="Pedidos.aspx" class="text-decoration-none">
                    <article class="app-card app-card-interactive h-100">
                        <h2 class="app-card-title mb-2 d-flex align-items-center gap-2">
                            <span>🧾</span>
                            <span>Pedidos</span>
                        </h2>

                        <p class="app-text-muted mb-3">
                            Revisa pedidos, seguimiento y estado de cada compra.
                        </p>

                        <span class="app-btn-link">
                            Gestionar →
                        </span>
                    </article>
                </a>
            </div>

            <div runat="server" id="pnlCategorias" class="col-sm-6 col-lg-4">
                <a href="Categorias.aspx" class="text-decoration-none">
                    <article class="app-card app-card-interactive h-100">
                        <h2 class="app-card-title mb-2 d-flex align-items-center gap-2">
                            <span>🏷️</span>
                            <span>Categorias</span>
                        </h2>

                        <p class="app-text-muted mb-3">
                            Administra las categorias disponibles para los productos.
                        </p>

                        <span class="app-btn-link">
                            Gestionar →
                        </span>
                    </article>
                </a>
            </div>

            <div runat="server" id="pnlMarcas" class="col-sm-6 col-lg-4">
                <a href="Marcas.aspx" class="text-decoration-none">
                    <article class="app-card app-card-interactive h-100">
                        <h2 class="app-card-title mb-2 d-flex align-items-center gap-2">
                            <span>🏭</span>
                            <span>Marcas</span>
                        </h2>

                        <p class="app-text-muted mb-3">
                            Administra las marcas disponibles para los productos.
                        </p>

                        <span class="app-btn-link">
                            Gestionar →
                        </span>
                    </article>
                </a>
            </div>

            <div runat="server" id="pnlFormasPago" class="col-sm-6 col-lg-4">
                <a href="FormasPago.aspx" class="text-decoration-none">
                    <article class="app-card app-card-interactive h-100">
                        <h2 class="app-card-title mb-2 d-flex align-items-center gap-2">
                            <span>💳</span>
                            <span>Formas de pago</span>
                        </h2>

                        <p class="app-text-muted mb-3">
                            Configura los medios de pago habilitados para clientes.
                        </p>

                        <span class="app-btn-link">
                            Gestionar →
                        </span>
                    </article>
                </a>
            </div>

            <div runat="server" id="pnlFormasEntrega" class="col-sm-6 col-lg-4">
                <a href="FormasEntrega.aspx" class="text-decoration-none">
                    <article class="app-card app-card-interactive h-100">
                        <h2 class="app-card-title mb-2 d-flex align-items-center gap-2">
                            <span>🚚</span>
                            <span>Formas de entrega</span>
                        </h2>

                        <p class="app-text-muted mb-3">
                            Configura envio, retiro y acuerdos de entrega.
                        </p>

                        <span class="app-btn-link">
                            Gestionar →
                        </span>
                    </article>
                </a>
            </div>

            <div runat="server" id="pnlEstadosPedido" class="col-sm-6 col-lg-4">
                <a href="EstadosPedido.aspx" class="text-decoration-none">
                    <article class="app-card app-card-interactive h-100">
                        <h2 class="app-card-title mb-2 d-flex align-items-center gap-2">
                            <span>✅</span>
                            <span>Estados de pedido</span>
                        </h2>

                        <p class="app-text-muted mb-3">
                            Gestiona los estados disponibles para el seguimiento de pedidos.
                        </p>

                        <span class="app-btn-link">
                            Gestionar →
                        </span>
                    </article>
                </a>
            </div>

        </section>

    </main>

</asp:Content>