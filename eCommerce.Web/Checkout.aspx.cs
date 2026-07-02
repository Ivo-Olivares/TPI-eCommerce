using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

                if (usuario == null || AutenticacionSesion.EsInvitado(Session))
                {
                    RedirigirALogin();
                    return;
                }

                List<DetallePedido> carrito = CarritoSesion.Obtener(Session);
                if (carrito.Count == 0)
                {
                    Response.Redirect("~/Carrito.aspx", false);
                    return;
                }



                if (usuario.Id <= 0)
                {
                    throw new Exception("El usuario logueado no tiene un Id válido.");
                }

                CargarDirecciones(usuario.Id);
                CargarFormasEntrega();
                CargarFormasPago();
                ActualizarVisibilidadDireccion();
                CargarResumenPedido();
                ActualizarResumenCheckout();
            }
        }

        private void CargarDirecciones(int idUsuario)
        {
            DireccionNegocio negocio = new DireccionNegocio();

            ddlDireccion.DataSource = negocio.Listar(idUsuario);
            ddlDireccion.DataTextField = "Descripcion";
            ddlDireccion.DataValueField = "Id";
            ddlDireccion.DataBind();

            ddlDireccion.Items.Insert(0, new ListItem("Seleccionar dirección", ""));
        }

        private void CargarFormasEntrega()
        {
            FormaEntregaNegocio negocio = new FormaEntregaNegocio();

            ddlEntrega.DataSource = negocio.Listar().Where(x => x.Activo).ToList();
            ddlEntrega.DataTextField = "Descripcion";
            ddlEntrega.DataValueField = "Id";
            ddlEntrega.DataBind();

            ddlEntrega.Items.Insert(0, new ListItem("Seleccionar forma de entrega", ""));
        }

        private void CargarFormasPago()
        {
            FormaPagoNegocio negocio = new FormaPagoNegocio();

            ddlFormaPago.DataSource = negocio.Listar().Where(x => x.Activo).ToList();
            ddlFormaPago.DataTextField = "Descripcion";
            ddlFormaPago.DataValueField = "Id";
            ddlFormaPago.DataBind();

            ddlFormaPago.Items.Insert(0, new ListItem("Seleccionar forma de pago", ""));
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

                if (usuario == null || AutenticacionSesion.EsInvitado(Session))
                {
                    RedirigirALogin();
                    return;
                }

                List<DetallePedido> carrito = CarritoSesion.Obtener(Session);

                if (carrito.Count == 0)
                {
                    throw new Exception("el carrito esta vacio");
                }

                if (string.IsNullOrWhiteSpace(ddlEntrega.SelectedValue))
                    throw new Exception("Debe seleccionar una forma de entrega.");

                bool requiereDireccion = EntregaSeleccionadaRequiereDireccion();
                int? idDireccion = null;

                if (requiereDireccion)
                {
                    if (string.IsNullOrWhiteSpace(ddlDireccion.SelectedValue))
                        throw new Exception("Debe seleccionar una dirección.");

                    idDireccion = int.Parse(ddlDireccion.SelectedValue);
                    DireccionNegocio direccionNegocio = new DireccionNegocio();

                    if (!direccionNegocio.PerteneceAlUsuario(idDireccion.Value, usuario.Id))
                        throw new Exception("La dirección seleccionada no pertenece al usuario logueado.");
                }

                if (string.IsNullOrWhiteSpace(ddlFormaPago.SelectedValue))
                    throw new Exception("Debe seleccionar una forma de pago.");

                decimal total = CalcularTotal(carrito);

                Pedido pedido = CrearPedidoDesdeCheckout(usuario, total, idDireccion);

                PedidoNegocio pedidoNegocio = new PedidoNegocio();
                pedidoNegocio.ConfirmarCompra(pedido, carrito);

                Session.Remove(CarritoSesion.ClaveCarrito);

                lblError.CssClass = "text-success d-block mb-3";
                lblError.Text = "Compra realizada correctamente.";
                lblError.Visible = true;

                btnConfirmar.Enabled = false;

            }
            catch (Exception ex)
            {
                lblError.CssClass = "app-alert app-alert-danger d-block mb-4";
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        protected void ddlEntrega_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
            ActualizarVisibilidadDireccion();
            ActualizarResumenCheckout();
        }

        protected void ddlDireccion_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
            ActualizarResumenCheckout();
        }

        protected void ddlFormaPago_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblError.Visible = false;
            ActualizarResumenCheckout();
        }

        protected void btnMostrarNuevaDireccion_Click(object sender, EventArgs e)
        {
            lblError.Visible = false;
            pnlNuevaDireccion.Visible = true;
            btnMostrarNuevaDireccion.Visible = false;
        }

        protected void btnGuardarNuevaDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;

                Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

                if (usuario == null || AutenticacionSesion.EsInvitado(Session))
                {
                    RedirigirALogin();
                    return;
                }

                if (!EntregaSeleccionadaRequiereDireccion())
                    throw new Exception("Debe seleccionar una forma de entrega con envio a domicilio.");

                Direccion direccion = CrearDireccionDesdeFormulario();
                DireccionNegocio negocio = new DireccionNegocio();
                int idDireccion = negocio.AgregarDireccion(direccion, usuario.Id);

                CargarDirecciones(usuario.Id);
                SeleccionarDireccion(idDireccion);
                OcultarFormularioNuevaDireccion();
                ActualizarResumenCheckout();

                MostrarMensaje("La dirección se agregó correctamente.", "text-success d-block mb-3");
            }
            catch (Exception ex)
            {
                lblError.CssClass = "app-alert app-alert-danger d-block mb-4";
                lblError.Text = ex.Message;
                lblError.Visible = true;
                pnlNuevaDireccion.Visible = true;
                btnMostrarNuevaDireccion.Visible = false;
            }
        }

        protected void btnCancelarNuevaDireccion_Click(object sender, EventArgs e)
        {
            OcultarFormularioNuevaDireccion();
        }

        private void CargarResumenPedido()
        {
            List<DetallePedido> carrito = CarritoSesion.Obtener(Session);

            if (carrito == null || carrito.Count == 0)
            {
                dgvResumen.DataSource = null;
                dgvResumen.DataBind();

                lblTotal.Text = "$ 0,00";
                return;
            }

            dgvResumen.DataSource = carrito;
            dgvResumen.DataBind();

            decimal total = 0;

            foreach (DetallePedido item in carrito)
            {
                total += item.Subtotal;
            }

            lblTotal.Text = total.ToString("$ #,##0.00");
        }

        private void ActualizarResumenCheckout()
        {
            lblResumenEntrega.Text = ObtenerTextoSeleccionado(ddlEntrega, "Pendiente");
            lblResumenPago.Text = ObtenerTextoSeleccionado(ddlFormaPago, "Pendiente");
            lblResumenDireccion.Text = ObtenerTextoResumenDireccion();
        }

        private string ObtenerTextoSeleccionado(DropDownList lista, string textoPendiente)
        {
            if (lista == null || string.IsNullOrWhiteSpace(lista.SelectedValue) || lista.SelectedItem == null)
                return textoPendiente;

            return lista.SelectedItem.Text;
        }

        private string ObtenerTextoResumenDireccion()
        {
            if (string.IsNullOrWhiteSpace(ddlEntrega.SelectedValue))
                return "Pendiente";

            if (!EntregaSeleccionadaRequiereDireccion())
                return "No corresponde";

            if (string.IsNullOrWhiteSpace(ddlDireccion.SelectedValue))
                return "Pendiente";

            Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);
            if (usuario == null || usuario.Id <= 0)
                return "Pendiente";

            DireccionNegocio negocio = new DireccionNegocio();
            Direccion direccion = negocio.Listar(usuario.Id)
                .Find(x => x.Id.ToString() == ddlDireccion.SelectedValue);

            if (direccion == null)
                return ddlDireccion.SelectedItem.Text;

            return FormatearDireccion(direccion);
        }

        private string FormatearDireccion(Direccion direccion)
        {
            string descripcion = string.IsNullOrWhiteSpace(direccion.Descripcion)
                ? ""
                : direccion.Descripcion + ": ";

            return descripcion + direccion.Calle + " " + direccion.Altura + ", " + direccion.Localidad + ", " + direccion.Provincia + " (" + direccion.Cp + ")";
        }

        private Pedido CrearPedidoDesdeCheckout(Usuario usuario, decimal total, int? idDireccion)
        {
            Pedido pedido = new Pedido();

            pedido.Usuario.Id = usuario.Id;
            pedido.Direccion = idDireccion.HasValue ? new Direccion { Id = idDireccion.Value } : null;
            pedido.FormaEntrega.Id = int.Parse(ddlEntrega.SelectedValue);
            pedido.FormaPago.Id = int.Parse(ddlFormaPago.SelectedValue);
            pedido.FechaCreacion = DateTime.Now;
            pedido.FechaEntrega = null;
            pedido.Total = total;

            EstadoPedidoNegocio estadoNegocio = new EstadoPedidoNegocio();
            pedido.EstadoPedido = estadoNegocio.ObtenerEstadoInicial();

            return pedido;
        }

        private decimal CalcularTotal(List<DetallePedido> carrito)
        {
            decimal total = 0;

            foreach (DetallePedido item in carrito)
            {
                total += item.Subtotal;
            }

            return total;
        }

        private bool EntregaSeleccionadaRequiereDireccion()
        {
            if (string.IsNullOrWhiteSpace(ddlEntrega.SelectedValue))
                return false;

            FormaEntregaNegocio negocio = new FormaEntregaNegocio();
            return negocio.RequiereDireccion(int.Parse(ddlEntrega.SelectedValue));
        }

        private void ActualizarVisibilidadDireccion()
        {
            bool requiereDireccion = EntregaSeleccionadaRequiereDireccion();

            pnlDireccion.Visible = requiereDireccion;
            ddlDireccion.Enabled = requiereDireccion;
            btnMostrarNuevaDireccion.Visible = requiereDireccion && !pnlNuevaDireccion.Visible;

            if (!requiereDireccion)
            {
                LimpiarDireccionSeleccionada();
                OcultarFormularioNuevaDireccion();
            }
        }

        private void LimpiarDireccionSeleccionada()
        {
            if (ddlDireccion.Items.Count > 0)
                ddlDireccion.SelectedIndex = 0;
        }

        private void OcultarFormularioNuevaDireccion()
        {
            LimpiarFormularioNuevaDireccion();
            pnlNuevaDireccion.Visible = false;
            btnMostrarNuevaDireccion.Visible = EntregaSeleccionadaRequiereDireccion();
        }

        private void LimpiarFormularioNuevaDireccion()
        {
            txtNuevaDireccionDescripcion.Text = "";
            txtNuevaDireccionCalle.Text = "";
            txtNuevaDireccionAltura.Text = "";
            txtNuevaDireccionLocalidad.Text = "";
            txtNuevaDireccionProvincia.Text = "";
            txtNuevaDireccionCp.Text = "";
            txtNuevaDireccionObservaciones.Text = "";
        }

        private Direccion CrearDireccionDesdeFormulario()
        {
            Direccion direccion = new Direccion();

            direccion.Descripcion = string.IsNullOrWhiteSpace(txtNuevaDireccionDescripcion.Text)
                ? "Direccion de envio"
                : txtNuevaDireccionDescripcion.Text.Trim();
            direccion.Calle = txtNuevaDireccionCalle.Text;
            direccion.Localidad = txtNuevaDireccionLocalidad.Text;
            direccion.Provincia = txtNuevaDireccionProvincia.Text;
            direccion.Cp = txtNuevaDireccionCp.Text;
            direccion.Observaciones = txtNuevaDireccionObservaciones.Text;

            if (!int.TryParse(txtNuevaDireccionAltura.Text, out int altura))
                throw new Exception("La altura debe ser un numero valido.");

            direccion.Altura = altura;

            return direccion;
        }

        private void SeleccionarDireccion(int idDireccion)
        {
            ListItem item = ddlDireccion.Items.FindByValue(idDireccion.ToString());

            if (item == null)
                throw new Exception("La direccion se guardo, pero no se pudo seleccionar.");

            ddlDireccion.SelectedValue = item.Value;
        }

        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblError.CssClass = cssClass;
            lblError.Text = mensaje;
            lblError.Visible = true;
        }

        private void RedirigirALogin()
        {
            string returnUrl = HttpUtility.UrlEncode("~/Checkout.aspx");
            Response.Redirect("~/Login.aspx?ReturnUrl=" + returnUrl, false);
        }
    }
}
