using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using eCommerce.Dominio;
using eCommerce.Negocio;

namespace eCommerce.Web
{
    public partial class Catalogo : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCategorias();
                CargarMarcas();
                AplicarCategoriaDesdeUrl();
                CargarCatalogo();
            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            CargarCatalogo();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            ddlCategoria.SelectedValue = "";
            ddlMarca.SelectedValue = "";
            ddlOrden.SelectedValue = "nombre";

            CargarCatalogo();
        }

        protected string FormatearPrecio(object valor)
        {
            decimal precio = (decimal)valor;
            return precio.ToString("C", CultureInfo.GetCultureInfo("es-AR"));
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            ddlCategoria.DataSource = negocio.Listar().Where(x => x.Activo).OrderBy(x => x.Nombre).ToList();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();

            ddlMarca.DataSource = negocio.ListarMarcas().Where(x => x.Activo).OrderBy(x => x.Nombre).ToList();
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void AplicarCategoriaDesdeUrl()
        {
            string idCategoriaUrl = Request.QueryString["idCategoria"];

            if (string.IsNullOrWhiteSpace(idCategoriaUrl))
                return;

            ListItem item = ddlCategoria.Items.FindByValue(idCategoriaUrl);

            if (item != null)
            {
                ddlCategoria.ClearSelection();
                item.Selected = true;
            }
        }
        private void CargarCatalogo()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            List<Producto> productos = negocio.ListarActivos().Where(x => x.Stock > 0).ToList();

            productos = AplicarFiltros(productos);
            productos = AplicarOrden(productos);

            rptProductos.DataSource = productos;
            rptProductos.DataBind();

            pnlSinResultados.Visible = productos.Count == 0;
        }

        private List<Producto> AplicarFiltros(List<Producto> productos)
        {
            string busqueda = txtBuscar.Text.Trim();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                productos = productos
                    .Where(x => Contiene(x.Nombre, busqueda) || Contiene(x.Descripcion, busqueda) || Contiene(x.Sku, busqueda))
                    .ToList();
            }

            if (!string.IsNullOrWhiteSpace(ddlCategoria.SelectedValue))
            {
                int idCategoria = int.Parse(ddlCategoria.SelectedValue);
                productos = productos.Where(x => x.Categoria.Id == idCategoria).ToList();
            }

            if (!string.IsNullOrWhiteSpace(ddlMarca.SelectedValue))
            {
                int idMarca = int.Parse(ddlMarca.SelectedValue);
                productos = productos.Where(x => x.Marca.Id == idMarca).ToList();
            }

            return productos;
        }

        private List<Producto> AplicarOrden(List<Producto> productos)
        {
            switch (ddlOrden.SelectedValue)
            {
                case "precio-asc":
                    return productos.OrderBy(x => x.Precio).ThenBy(x => x.Nombre).ToList();
                case "precio-desc":
                    return productos.OrderByDescending(x => x.Precio).ThenBy(x => x.Nombre).ToList();
                case "stock-desc":
                    return productos.OrderByDescending(x => x.Stock).ThenBy(x => x.Nombre).ToList();
                default:
                    return productos.OrderBy(x => x.Nombre).ToList();
            }
        }

        private bool Contiene(string texto, string busqueda)
        {
            if (string.IsNullOrWhiteSpace(texto))
                return false;

            return texto.IndexOf(busqueda, StringComparison.InvariantCultureIgnoreCase) >= 0;
        }

        protected void rptProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "AgregarCarrito")
            {
                int idProducto = int.Parse(e.CommandArgument.ToString());

                ProductoNegocio productoNegocio = new ProductoNegocio();
                Producto producto = productoNegocio.Listar()
                    .Find(x => x.Id == idProducto);

                if (producto == null)
                    return;

                CarritoSesion.AgregarProducto(Session, producto, 1);

                Response.Redirect("~/Carrito.aspx");
            }
        }

    }
}