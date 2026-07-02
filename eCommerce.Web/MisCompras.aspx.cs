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
    public partial class MisCompras : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            string estadoSeleccionado = IsPostBack ? Request.Form[ddlEstado.UniqueID] : null;
            CargarEstados(estadoSeleccionado);

            if (!IsPostBack)
            {
                CargarCompras();
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {
                lblError.Visible = false;
                CargarCompras();
                pnlDetalle.Visible = false;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.Visible = true;
            }
        }

        private void CargarEstados(string estadoSeleccionado)
        {
            EstadoPedidoNegocio negocio = new EstadoPedidoNegocio();
            List<EstadoPedido> estados = negocio.Listar()
                .Where(x => x.Activo)
                .OrderBy(x => x.Id)
                .ToList();

            ddlEstado.DataSource = estados;
            ddlEstado.DataTextField = "Descripcion";
            ddlEstado.DataValueField = "Id";
            ddlEstado.DataBind();
            ddlEstado.Items.Insert(0, new ListItem("Todos", ""));

            if (!string.IsNullOrWhiteSpace(estadoSeleccionado) && ddlEstado.Items.FindByValue(estadoSeleccionado) != null)
            {
                ddlEstado.SelectedValue = estadoSeleccionado;
            }
        }

        private void CargarCompras()
        {
            Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

            if (usuario == null || AutenticacionSesion.EsInvitado(Session))
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }

            PedidoNegocio negocio = new PedidoNegocio();
            List<Pedido> compras = negocio.ListarPorUsuario(usuario.Id);

            if (int.TryParse(ddlEstado.SelectedValue, out int idEstado))
            {
                compras = compras.Where(x => x.EstadoPedido.Id == idEstado).ToList();
            }

            DateTime fechaDesde;
            DateTime fechaHasta;

            bool tieneFechaDesde = DateTime.TryParse(txtFechaDesde.Text, out fechaDesde);
            bool tieneFechaHasta = DateTime.TryParse(txtFechaHasta.Text, out fechaHasta);

            if (tieneFechaDesde && tieneFechaHasta && fechaDesde.Date > fechaHasta.Date)
            {
                throw new Exception("La fecha desde no puede ser mayor que la fecha hasta.");
            }

            if (tieneFechaDesde)
            {
                compras = compras
                    .Where(x => x.FechaCreacion.Date >= fechaDesde.Date)
                    .ToList();
            }

            if (tieneFechaHasta)
            {
                compras = compras
                    .Where(x => x.FechaCreacion.Date <= fechaHasta.Date)
                    .ToList();
            }

            dgvCompras.DataSource = compras;
            dgvCompras.DataBind();
        }

        protected void dgvCompras_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idPedido = (int)dgvCompras.SelectedDataKey.Value;

            Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);
            PedidoNegocio pedidoNegocio = new PedidoNegocio();
            Pedido pedido = pedidoNegocio.ListarPorUsuario(usuario.Id).Find(x => x.Id == idPedido);

            if (pedido == null)
            {
                lblError.Text = "No se encontró el pedido seleccionado.";
                lblError.Visible = true;
                pnlDetalle.Visible = false;
                return;
            }

            CargarResumenPedido(pedido);

            DetallePedidoNegocio negocio = new DetallePedidoNegocio();
            List<DetallePedido> detalle = negocio.ListarPorPedido(idPedido);

            dgvDetalle.DataSource = detalle;
            dgvDetalle.DataBind();

            pnlDetalle.Visible = true;
        }

        private void CargarResumenPedido(Pedido pedido)
        {
            lblPedidoSeleccionado.Text = pedido.Id.ToString();
            lblFechaPedido.Text = pedido.FechaCreacion.ToString("dd/MM/yyyy HH:mm");
            lblEstadoPedido.Text = pedido.EstadoPedido.Descripcion;
            lblTotalPedido.Text = pedido.Total.ToString("C");
            lblFormaPago.Text = pedido.FormaPago.Descripcion;
            lblFormaEntrega.Text = pedido.FormaEntrega.Descripcion;
            lblFechaEntrega.Text = pedido.FechaEntrega.HasValue ? pedido.FechaEntrega.Value.ToString("dd/MM/yyyy") : "Pendiente";
            lblDireccionPedido.Text = FormatearDireccion(pedido.Direccion);
        }

        private string FormatearDireccion(Direccion direccion)
        {
            if (direccion == null || direccion.Id <= 0)
                return "Sin dirección registrada.";

            string direccionTexto = direccion.Calle + " " + direccion.Altura + ", " + direccion.Localidad + ", " + direccion.Provincia + " (" + direccion.Cp + ")";

            if (!string.IsNullOrWhiteSpace(direccion.Observaciones))
                direccionTexto += " - " + direccion.Observaciones;

            return direccionTexto;
        }
    }
}
