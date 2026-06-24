using System;
using System.Collections.Generic;
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
                CargarFiltros();
                CargarFiltrosDesdeQueryString();
                CargarProductos();
            }
        }

        protected void rptProductos_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Agregar")
                return;

            try
            {
                int idProducto = int.Parse(e.CommandArgument.ToString());
                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.BuscarPorId(idProducto);

                CarritoSesion.Agregar(Session, producto, 1);
                CargarProductos();
                MostrarMensaje("Producto agregado al carrito.", "alert alert-success d-block");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert alert-danger d-block");
            }
        }

        private void CargarFiltros()
        {
            CargarCategorias();
            CargarMarcas();
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            List<Categoria> categorias = negocio.Listar().Where(x => x.Activo).ToList();

            ddlCategoria.DataSource = categorias;
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            List<Marca> marcas = negocio.ListarMarcas().Where(x => x.Activo).ToList();

            ddlMarca.DataSource = marcas;
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Todas", ""));
        }

        private void CargarProductos()
        {
            try
            {
                ProductoNegocio negocio = new ProductoNegocio();
                List<Producto> productos = negocio.Listar()
                    .Where(x => x.Activo && x.Stock > 0 && x.Categoria.Activo && x.Marca.Activo)
                    .ToList();

                productos = AplicarFiltros(productos);

                rptProductos.DataSource = productos;
                rptProductos.DataBind();

                lblMensaje.Visible = productos.Count == 0;
                lblMensaje.Text = productos.Count == 0 ? "No se encontraron productos disponibles." : "";
                if (productos.Count == 0)
                    lblMensaje.CssClass = "alert alert-info d-block";
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert alert-danger d-block");
            }
        }

        private List<Producto> AplicarFiltros(List<Producto> productos)
        {
            string busqueda = txtBuscar.Text.Trim();

            if (!string.IsNullOrWhiteSpace(busqueda))
            {
                productos = productos.Where(x =>
                    x.Nombre.IndexOf(busqueda, StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                    x.Sku.IndexOf(busqueda, StringComparison.InvariantCultureIgnoreCase) >= 0 ||
                    x.Descripcion.IndexOf(busqueda, StringComparison.InvariantCultureIgnoreCase) >= 0).ToList();
            }

            int idCategoria;
            if (int.TryParse(ddlCategoria.SelectedValue, out idCategoria) && idCategoria > 0)
                productos = productos.Where(x => x.Categoria.Id == idCategoria).ToList();

            int idMarca;
            if (int.TryParse(ddlMarca.SelectedValue, out idMarca) && idMarca > 0)
                productos = productos.Where(x => x.Marca.Id == idMarca).ToList();

            return productos;
        }

        private void CargarFiltrosDesdeQueryString()
        {
            txtBuscar.Text = Request.QueryString["buscar"] ?? "";
            SeleccionarValor(ddlCategoria, Request.QueryString["categoria"]);
            SeleccionarValor(ddlMarca, Request.QueryString["marca"]);
        }

        private void SeleccionarValor(DropDownList lista, string valor)
        {
            if (!string.IsNullOrWhiteSpace(valor) && lista.Items.FindByValue(valor) != null)
                lista.SelectedValue = valor;
        }

        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = cssClass;
            lblMensaje.Visible = true;
        }
    }
}
