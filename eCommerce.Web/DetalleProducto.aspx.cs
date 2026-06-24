using System;
using System.Web.UI;
using eCommerce.Dominio;
using eCommerce.Negocio;

namespace eCommerce.Web
{
    public partial class DetalleProducto : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarProducto();
        }

        private void CargarProducto()
        {
            try
            {
                int idProducto;
                if (!int.TryParse(Request.QueryString["id"], out idProducto))
                {
                    MostrarError("No se encontro el producto solicitado.");
                    return;
                }

                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.BuscarPorId(idProducto);

                if (producto == null || !producto.Activo || producto.Stock <= 0 || !producto.Categoria.Activo || !producto.Marca.Activo)
                {
                    MostrarError("El producto no esta disponible.");
                    return;
                }

                ViewState["IdProducto"] = producto.Id;
                lblNombre.Text = producto.Nombre;
                lblMarcaCategoria.Text = producto.Marca.Nombre + " | " + producto.Categoria.Nombre;
                lblDescripcion.Text = producto.Descripcion;
                lblPrecio.Text = producto.Precio.ToString("C");
                lblStock.Text = producto.Stock.ToString();
                lblEstado.Text = producto.Activo ? "Activo" : "Inactivo";
                txtCantidad.Text = "1";
                pnlProducto.Visible = true;
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
            pnlProducto.Visible = false;
        }
    }
}
