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

            DetallePedidoNegocio negocio = new DetallePedidoNegocio();
            List<DetallePedido> detalle = negocio.ListarPorPedido(idPedido);

            dgvDetalle.DataSource = detalle;
            dgvDetalle.DataBind();

            pnlDetalle.Visible = true;
        }
    }
}