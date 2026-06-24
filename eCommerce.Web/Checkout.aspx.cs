using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class Checkout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCombos();
                CargarResumen();
            }
        }

        private void CargarCombos()
        {
            CargarFormasEntrega();
            CargarFormasPago();
        }

        private void CargarFormasEntrega()
        {
            FormaEntregaNegocio negocio = new FormaEntregaNegocio();
            List<FormaEntrega> entregas = negocio.Listar().Where(x => x.Activo).ToList();

            ddlFormaEntrega.DataSource = entregas;
            ddlFormaEntrega.DataTextField = "Descripcion";
            ddlFormaEntrega.DataValueField = "Id";
            ddlFormaEntrega.DataBind();
            ddlFormaEntrega.Items.Insert(0, new ListItem("Seleccionar", ""));
        }

        private void CargarFormasPago()
        {
            FormaPagoNegocio negocio = new FormaPagoNegocio();
            List<FormaPago> formasPago = negocio.Listar().Where(x => x.Activo).ToList();

            ddlFormaPago.DataSource = formasPago;
            ddlFormaPago.DataTextField = "Descripcion";
            ddlFormaPago.DataValueField = "Id";
            ddlFormaPago.DataBind();
            ddlFormaPago.Items.Insert(0, new ListItem("Seleccionar", ""));
        }

        private void CargarResumen()
        {
            List<ItemCarrito> carrito = CarritoSesion.Obtener(Session);

            dgvResumen.DataSource = carrito;
            dgvResumen.DataBind();

            bool tieneItems = carrito.Count > 0;
            pnlCheckout.Visible = tieneItems;
            lblMensaje.Visible = !tieneItems;
            lblMensaje.Text = tieneItems ? "" : "El carrito esta vacio.";
            lblTotal.Text = CarritoSesion.CalcularTotal(Session).ToString("C");
            btnConfirmar.Enabled = tieneItems;
        }
    }
}
