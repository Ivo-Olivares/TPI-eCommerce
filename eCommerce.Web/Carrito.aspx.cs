using System;
using System.Collections.Generic;
using System.Web.UI;
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
    }
}
