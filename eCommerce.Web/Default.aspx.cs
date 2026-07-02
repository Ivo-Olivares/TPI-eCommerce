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

        protected bool TieneImagen(object imagen)
        {
            List<Imagen> lista = imagen as List<Imagen>;
            return lista != null && lista.Count > 0 && !string.IsNullOrWhiteSpace(lista[0].UrlImagen);
        }

        protected string ObtenerImagen(object imagen)
        {
            List<Imagen> lista = imagen as List<Imagen>;

            if (lista == null || lista.Count == 0)
                return "";

            return lista[0].UrlImagen;
        }
    }
}
