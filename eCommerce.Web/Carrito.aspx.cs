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
    public partial class Carrito : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCarrito();
            }
        }

        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            if (ActualizarCantidadesCarrito())
            {
                lblExito.Text = "Cantidades actualizadas.";
                lblExito.Visible = true;
                CargarCarrito();
            }
        }

        private bool ActualizarCantidadesCarrito()
        {
            try
            {
                OcultarMensajes();
                List<Tuple<Producto, int>> actualizaciones = new List<Tuple<Producto, int>>();

                foreach (RepeaterItem item in rptCarrito.Items)
                {
                    HiddenField hdfIdProducto = item.FindControl("hdfIdProducto") as HiddenField;
                    TextBox txtCantidadItem = item.FindControl("txtCantidadItem") as TextBox;

                    if (hdfIdProducto == null || txtCantidadItem == null)
                        continue;

                    if (!int.TryParse(hdfIdProducto.Value, out int idProducto))
                        throw new Exception("No se pudo identificar uno de los productos del carrito.");

                    if (!int.TryParse(txtCantidadItem.Text.Trim(), out int cantidad))
                        throw new Exception("Debe ingresar cantidades validas.");

                    Producto producto = ObtenerProductoDisponible(idProducto);
                    actualizaciones.Add(Tuple.Create(producto, cantidad));
                }

                foreach (Tuple<Producto, int> actualizacion in actualizaciones)
                {
                    CarritoSesion.ActualizarProducto(Session, actualizacion.Item1, actualizacion.Item2);
                }

                return true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
                return false;
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (ActualizarCantidadesCarrito())
            {
                Response.Redirect("~/Checkout.aspx");
            }
        }

        protected void rptCarrito_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName != "Quitar")
                return;

            if (int.TryParse(e.CommandArgument.ToString(), out int idProducto))
            {
                OcultarMensajes();
                CarritoSesion.QuitarProducto(Session, idProducto);
                CargarCarrito();
            }
        }

        protected string FormatearPrecio(object valor)
        {
            decimal precio = (decimal)valor;
            return precio.ToString("C", CultureInfo.GetCultureInfo("es-AR"));
        }

        private void CargarCarrito()
        {
            List<DetallePedido> carrito = CarritoSesion.Obtener(Session);
            bool carritoVacio = carrito.Count == 0;

            pnlVacio.Visible = carritoVacio;
            pnlCarrito.Visible = !carritoVacio;

            if (carritoVacio)
            {
                rptCarrito.DataSource = null;
                rptCarrito.DataBind();
                return;
            }

            rptCarrito.DataSource = carrito;
            rptCarrito.DataBind();

            decimal total = CarritoSesion.Total(Session);
            lblSubtotal.Text = total.ToString("C", CultureInfo.GetCultureInfo("es-AR"));
            lblTotal.Text = total.ToString("C", CultureInfo.GetCultureInfo("es-AR"));
        }

        private Producto ObtenerProductoDisponible(int idProducto)
        {
            ProductoNegocio negocio = new ProductoNegocio();
            Producto producto = negocio.ObtenerActivoPorId(idProducto);

            if (producto == null || producto.Stock <= 0)
                throw new Exception("Uno de los productos del carrito ya no está disponible.");

            return producto;
        }

        private void OcultarMensajes()
        {
            lblError.Visible = false;
            lblExito.Visible = false;
        }
    }
}