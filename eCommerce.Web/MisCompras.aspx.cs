using eCommerce.Dominio;
using eCommerce.Negocio;
using eCommerce.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;

namespace eCommerce.Web
{
    public partial class MisCompras : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarCompras();
            }


        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarCompras();

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

            dgvCompras.DataSource = detalle;
            dgvDetalle.DataBind();

            pnlDetalle.Visible = true;




        }
    }
}