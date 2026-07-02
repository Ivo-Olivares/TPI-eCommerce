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
                CargarResumenPedido();
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


                if (string.IsNullOrWhiteSpace(ddlDireccion.SelectedValue))
                    throw new Exception("Debe seleccionar una dirección.");

                int idDireccion = int.Parse(ddlDireccion.SelectedValue);
                DireccionNegocio direccionNegocio = new DireccionNegocio();

                if (!direccionNegocio.PerteneceAlUsuario(idDireccion, usuario.Id))
                    throw new Exception("La dirección seleccionada no pertenece al usuario logueado.");

                if (string.IsNullOrWhiteSpace(ddlEntrega.SelectedValue))
                    throw new Exception("Debe seleccionar una forma de entrega.");

                if (string.IsNullOrWhiteSpace(ddlFormaPago.SelectedValue))
                    throw new Exception("Debe seleccionar una forma de pago.");

                decimal total = CalcularTotal(carrito);

                Pedido pedido = CrearPedidoDesdeCheckout(usuario, total);

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
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
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

        private Pedido CrearPedidoDesdeCheckout(Usuario usuario, decimal total)
        {
            Pedido pedido = new Pedido();

            pedido.Usuario.Id = usuario.Id;
            pedido.Direccion.Id = int.Parse(ddlDireccion.SelectedValue);
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

        private void RedirigirALogin()
        {
            string returnUrl = HttpUtility.UrlEncode("~/Checkout.aspx");
            Response.Redirect("~/Login.aspx?ReturnUrl=" + returnUrl, false);
        }
    }
}
