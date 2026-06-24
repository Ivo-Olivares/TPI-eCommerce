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
                CargarResumen(false);
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

        protected void btnConfirmarCompra_Click(object sender, EventArgs e)
        {
            ConfirmarCompra();
        }

        private void CargarResumen(bool preservarMensaje)
        {
            List<ItemCarrito> carrito = CarritoSesion.Obtener(Session);

            dgvResumen.DataSource = carrito;
            dgvResumen.DataBind();

            bool tieneItems = carrito.Count > 0;
            pnlCheckout.Visible = tieneItems;

            if (!preservarMensaje)
            {
                lblMensaje.Visible = !tieneItems;
                lblMensaje.Text = tieneItems ? "" : "El carrito esta vacio.";
            }

            lblTotal.Text = CarritoSesion.CalcularTotal(Session).ToString("C");
        }

        private void ConfirmarCompra()
        {
            try
            {
                Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

                if (usuario == null || AutenticacionSesion.EsInvitado(Session))
                    throw new Exception("Debe iniciar sesion o registrarse para confirmar la compra.");

                int idFormaPago;
                int idFormaEntrega;

                int.TryParse(Request.Form[ddlFormaPago.UniqueID], out idFormaPago);
                int.TryParse(Request.Form[ddlFormaEntrega.UniqueID], out idFormaEntrega);

                PedidoNegocio negocio = new PedidoNegocio();
                int idPedido = negocio.ConfirmarPedido(usuario, CrearDireccionDesdeFormulario(), idFormaPago, idFormaEntrega, CarritoSesion.Obtener(Session));

                CarritoSesion.Vaciar(Session);
                Response.Redirect("~/MisCompras.aspx?pedido=" + idPedido, false);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert alert-danger d-block");
                CargarResumen(true);
            }
        }

        private Direccion CrearDireccionDesdeFormulario()
        {
            Direccion direccion = new Direccion();
            direccion.Calle = txtCalle.Text;
            direccion.Localidad = txtLocalidad.Text;
            direccion.Provincia = txtProvincia.Text;
            direccion.Observaciones = txtObservaciones.Text;

            int altura;
            if (int.TryParse(txtAltura.Text, out altura))
                direccion.Altura = altura;

            int cp;
            if (int.TryParse(txtCp.Text, out cp))
                direccion.Cp = cp;

            return direccion;
        }

        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = cssClass;
            lblMensaje.Visible = true;
        }
    }
}
