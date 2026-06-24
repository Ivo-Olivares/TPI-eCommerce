using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using eCommerce.Dominio;

namespace eCommerce.Web
{
    public partial class Carrito : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                CargarCarrito();
        }

        private void CargarCarrito()
        {
            List<ItemCarrito> carrito = CarritoSesion.Obtener(Session);

            dgvCarrito.DataSource = carrito;
            dgvCarrito.DataBind();

            bool tieneItems = carrito.Count > 0;
            pnlResumen.Visible = tieneItems;
            lblMensaje.Visible = !tieneItems;
            lblMensaje.Text = tieneItems ? "" : "El carrito esta vacio.";

            decimal total = CarritoSesion.CalcularTotal(Session);
            lblSubtotal.Text = total.ToString("C");
            lblTotal.Text = total.ToString("C");
        }

        protected void dgvCarrito_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "ActualizarCantidad" && e.CommandName != "Quitar")
                return;

            try
            {
                int indice = Convert.ToInt32(e.CommandArgument);
                int idProducto = (int)dgvCarrito.DataKeys[indice].Value;

                if (e.CommandName == "Quitar")
                {
                    CarritoSesion.Quitar(Session, idProducto);
                    CargarCarrito();
                    MostrarMensaje("Producto quitado del carrito.", "alert alert-success d-block");
                }
                else
                {
                    GridViewRow fila = dgvCarrito.Rows[indice];
                    TextBox txtCantidad = fila.FindControl("txtCantidad") as TextBox;
                    int cantidad;

                    if (txtCantidad == null || !int.TryParse(txtCantidad.Text, out cantidad))
                        throw new Exception("Debe ingresar una cantidad valida.");

                    CarritoSesion.ActualizarCantidad(Session, idProducto, cantidad);
                    CargarCarrito();
                    MostrarMensaje("Cantidad actualizada.", "alert alert-success d-block");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert alert-danger d-block");
            }
        }

        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = cssClass;
            lblMensaje.Visible = true;
        }
    }
}
