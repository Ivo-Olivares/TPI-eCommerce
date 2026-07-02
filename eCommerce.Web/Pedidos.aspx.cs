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
    public partial class Pedidos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AutorizacionPagina.RequerirGestionPedidos(Session, Response))
                return;

            if (!IsPostBack)
            {
                CargarEstados();
                CargarPedidos();

                int idPedido;
                if (int.TryParse(Request.QueryString["id"], out idPedido))
                    CargarDetallePedido(idPedido);
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            pnlDetalle.Visible = false;
            CargarPedidos();
        }

        protected void btnCambiarEstado_Click(object sender, EventArgs e)
        {
            try
            {
                if (!AutorizacionPagina.RequerirGestionPedidos(Session, Response))
                    return;

                int idPedido = ObtenerIdPedidoSeleccionado();
                int idEstadoPedido;
                if (!int.TryParse(ddlEstadoCambio.SelectedValue, out idEstadoPedido))
                    throw new Exception("Debe seleccionar un estado valido.");

                PedidoNegocio negocio = new PedidoNegocio();
                negocio.ActualizarEstado(idPedido, idEstadoPedido);

                CargarPedidos();
                CargarDetallePedido(idPedido);
                MostrarMensaje("El estado del pedido se actualizo correctamente.", "alert-success");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert-danger");
            }
        }

        protected void btnGuardarObservaciones_Click(object sender, EventArgs e)
        {
            try
            {
                if (!AutorizacionPagina.RequerirGestionPedidos(Session, Response))
                    return;

                int idPedido = ObtenerIdPedidoSeleccionado();

                PedidoNegocio negocio = new PedidoNegocio();
                negocio.ActualizarObservacionesInternas(idPedido, txtObservacionesInternas.Text);

                CargarPedidos();
                CargarDetallePedido(idPedido);
                MostrarMensaje("Las observaciones internas se guardaron correctamente.", "alert-success");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert-danger");
            }
        }

        private int ObtenerIdPedidoSeleccionado()
        {
            int idPedido;
            if (int.TryParse(Request.QueryString["id"], out idPedido) && idPedido > 0)
                return idPedido;

            if (ViewState["IdPedidoSeleccionado"] != null)
                return (int)ViewState["IdPedidoSeleccionado"];

            throw new Exception("Debe seleccionar un pedido.");
        }

        private void CargarEstados()
        {
            EstadoPedidoNegocio negocio = new EstadoPedidoNegocio();
            List<EstadoPedido> estados = negocio.Listar()
                .Where(x => x.Activo)
                .OrderBy(x => x.Descripcion)
                .ToList();

            ddlEstado.DataSource = estados;
            ddlEstado.DataTextField = "Descripcion";
            ddlEstado.DataValueField = "Id";
            ddlEstado.DataBind();
            ddlEstado.Items.Insert(0, new ListItem("Todos", ""));
        }

        private void CargarPedidos()
        {
            try
            {
                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> pedidos = negocio.ListarTodos();

                if (!string.IsNullOrWhiteSpace(ddlEstado.SelectedValue))
                {
                    int idEstado = int.Parse(ddlEstado.SelectedValue);
                    pedidos = pedidos.Where(x => x.EstadoPedido.Id == idEstado).ToList();
                }

                if (!string.IsNullOrWhiteSpace(txtFechaDesde.Text))
                {
                    DateTime fechaDesde;
                    if (!DateTime.TryParse(txtFechaDesde.Text, out fechaDesde))
                        throw new Exception("La fecha desde no es valida.");

                    pedidos = pedidos.Where(x => x.FechaCreacion.Date >= fechaDesde.Date).ToList();
                }

                if (!string.IsNullOrWhiteSpace(txtFechaHasta.Text))
                {
                    DateTime fechaHasta;
                    if (!DateTime.TryParse(txtFechaHasta.Text, out fechaHasta))
                        throw new Exception("La fecha hasta no es valida.");

                    pedidos = pedidos.Where(x => x.FechaCreacion.Date <= fechaHasta.Date).ToList();
                }

                dgvPedidos.DataSource = pedidos;
                dgvPedidos.DataBind();
            }
            catch (Exception ex)
            {
                dgvPedidos.DataSource = null;
                dgvPedidos.DataBind();
                MostrarMensaje(ex.Message, "alert-danger");
            }
        }

        private void CargarDetallePedido(int idPedido)
        {
            PedidoNegocio pedidoNegocio = new PedidoNegocio();
            Pedido pedido = pedidoNegocio.ListarTodos().Find(x => x.Id == idPedido);

            if (pedido == null)
                throw new Exception("No se encontro el pedido seleccionado.");

            ViewState["IdPedidoSeleccionado"] = idPedido;
            lblPedidoSeleccionado.Text = "Pedido #" + pedido.Id + " - " + pedido.Usuario.Email;
            txtObservacionesInternas.Text = pedido.ObservacionesInternas;
            CargarEstadosCambio();
            ListItem estadoActual = ddlEstadoCambio.Items.FindByValue(pedido.EstadoPedido.Id.ToString());
            if (estadoActual != null)
                ddlEstadoCambio.SelectedValue = estadoActual.Value;

            DetallePedidoNegocio detalleNegocio = new DetallePedidoNegocio();
            List<DetallePedido> detalle = detalleNegocio.ListarPorPedido(idPedido);

            dgvDetalle.DataSource = detalle;
            dgvDetalle.DataBind();

            pnlDetalle.Visible = true;
        }

        private void CargarEstadosCambio()
        {
            EstadoPedidoNegocio negocio = new EstadoPedidoNegocio();
            List<EstadoPedido> estados = negocio.Listar()
                .Where(x => x.Activo)
                .OrderBy(x => x.Descripcion)
                .ToList();

            ddlEstadoCambio.DataSource = estados;
            ddlEstadoCambio.DataTextField = "Descripcion";
            ddlEstadoCambio.DataValueField = "Id";
            ddlEstadoCambio.DataBind();
        }

        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblMensaje.Text = mensaje;

            if (cssClass == "alert-success")
                lblMensaje.CssClass = "app-alert app-alert-success d-block mb-4";
            else
                lblMensaje.CssClass = "app-alert app-alert-danger d-block mb-4";

            lblMensaje.Visible = true;
        }

        private void OcultarMensaje()
        {
            lblMensaje.Text = "";
            lblMensaje.Visible = false;
        }
    }
}
