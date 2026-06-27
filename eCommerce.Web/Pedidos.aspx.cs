using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class Pedidos : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AutorizacionPagina.RequerirGestionPedidos(Session, Response))
                return;

            if (!IsPostBack)
            {
                CargarEstados();
            }

            if (ProcesarCambioEstadoPorUrl())
                return;

            CargarPedidos();

            int idPedido;
            if (int.TryParse(Request.QueryString["id"], out idPedido))
                CargarDetallePedido(idPedido);

            if (Request.QueryString["actualizado"] == "1")
                MostrarMensaje("El estado del pedido se actualizo correctamente.", "alert-success");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            OcultarMensaje();
            pnlDetalle.Visible = false;
            CargarPedidos();
        }

        private bool ProcesarCambioEstadoPorUrl()
        {
            try
            {
                int idPedido;
                if (!int.TryParse(Request.QueryString["id"], out idPedido))
                    return false;

                int idEstadoPedido;
                if (!int.TryParse(Request.QueryString["estado"], out idEstadoPedido))
                    return false;

                PedidoNegocio negocio = new PedidoNegocio();
                negocio.ActualizarEstado(idPedido, idEstadoPedido);

                Response.Redirect("~/Pedidos.aspx?id=" + idPedido + "&actualizado=1", false);
                Context.ApplicationInstance.CompleteRequest();
                return true;
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert-danger");
                return false;
            }
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

            rptEstadosCambio.DataSource = estados;
            rptEstadosCambio.DataBind();
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

            DetallePedidoNegocio detalleNegocio = new DetallePedidoNegocio();
            List<DetallePedido> detalle = detalleNegocio.ListarPorPedido(idPedido);

            dgvDetalle.DataSource = detalle;
            dgvDetalle.DataBind();

            pnlDetalle.Visible = true;
        }

        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = "alert d-block " + cssClass;
            lblMensaje.Visible = true;
        }

        private void OcultarMensaje()
        {
            lblMensaje.Text = "";
            lblMensaje.Visible = false;
        }
    }
}
