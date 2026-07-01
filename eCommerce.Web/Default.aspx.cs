using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                CargarCategorias();
            }
        }

        private void CargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();

            List<Producto> productos = negocio.Listar()
                .Where(x => x.Activo && x.Stock > 0)
                .Take(4)
                .ToList();

            rptProductos.DataSource = productos;
            rptProductos.DataBind();
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();

            List<Categoria> categorias = negocio.Listar()
                .Where(x => x.Activo)
                .Take(4)
                .ToList();

            rptCategorias.DataSource = categorias;
            rptCategorias.DataBind();
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