using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class DetalleProducto : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarDetalle();
            }
        }

        protected void btnAgregarCarrito_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                lblExito.Visible = false;

                Producto producto = ObtenerProductoDesdeRequest();

                if (producto == null || producto.Stock <= 0)
                    throw new Exception("El producto seleccionado no está disponible.");

                if (!int.TryParse(txtCantidad.Text.Trim(), out int cantidad))
                    throw new Exception("Debe ingresar una cantidad válida.");

                CarritoSesion.AgregarProducto(Session, producto, cantidad);

                lblExito.Text = "Producto agregado al carrito.";
                lblExito.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void CargarDetalle()
        {
            lblError.Visible = false;
            lblExito.Visible = false;

            Producto producto = ObtenerProductoDesdeRequest();

            if (producto == null)
            {
                MostrarMensaje("No se encontró el producto solicitado.");
                return;
            }

            if (producto.Stock <= 0)
            {
                MostrarMensaje("El producto solicitado no tiene stock disponible.");
                return;
            }

            pnlMensaje.Visible = false;
            pnlDetalle.Visible = true;

            lblNombre.Text = producto.Nombre;
            lblMeta.Text = producto.Marca.Nombre + " | " + producto.Categoria.Nombre;
            lblDescripcion.Text = producto.Descripcion;
            lblPrecio.Text = producto.Precio.ToString("C", CultureInfo.GetCultureInfo("es-AR"));
            lblStock.Text = producto.Stock.ToString();
            lblEstado.Text = "Activo";

            txtCantidad.Text = "1";
            txtCantidad.Attributes["min"] = "1";
            txtCantidad.Attributes["max"] = producto.Stock.ToString();
        }

        private Producto ObtenerProductoDesdeRequest()
        {
            if (!int.TryParse(Request.QueryString["id"], out int idProducto))
                return null;

            ProductoNegocio negocio = new ProductoNegocio();
            return negocio.ObtenerActivoPorId(idProducto);
        }

        private void MostrarMensaje(string mensaje)
        {
            pnlDetalle.Visible = false;
            pnlMensaje.Visible = true;
            lblMensaje.Text = mensaje;
        }
    }
}