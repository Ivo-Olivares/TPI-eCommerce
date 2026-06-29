using eCommerce.Dominio;
using eCommerce.Negocio;
using eCommerce.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class MisCompras : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string estadoSeleccionado = IsPostBack ? Request.Form[ddlEstado.UniqueID] : null;
            CargarEstados(estadoSeleccionado);
            CargarCompras();
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

        private void CargarCompras() {
            {
                Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

                if (usuario == null || AutenticacionSesion.EsInvitado(Session))
                {
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }

                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> compras = negocio.ListarPorUsuario(usuario.Id);


                if(int.TryParse(ddlEstado.SelectedValue, out int idEstado))
                {
                    compras = compras.Where(x => x.EstadoPedido.Id == idEstado).ToList();
                }

                if (!string.IsNullOrWhiteSpace(txtFechaDesde.Text))
                {
                    DateTime fechaDesde = DateTime.Parse(txtFechaDesde.Text);
                    compras = compras
                        .Where(x => x.FechaCreacion.Date >= fechaDesde.Date)
                        .ToList();
                }

                if (!string.IsNullOrWhiteSpace(txtFechaHasta.Text))
                {
                    DateTime fechaHasta = DateTime.Parse(txtFechaHasta.Text);
                    compras = compras
                        .Where(x => x.FechaCreacion.Date <= fechaHasta.Date)
                        .ToList();
                }

                dgvCompras.DataSource = compras;
                dgvCompras.DataBind();
            }
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
