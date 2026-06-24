using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using eCommerce.Dominio;
using eCommerce.Negocio;

namespace eCommerce.Web
{
    public partial class MisCompras : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

            if (usuario == null || AutenticacionSesion.EsInvitado(Session))
            {
                MostrarMensaje("Debe iniciar sesion para ver sus compras.", "alert alert-warning d-block");
                pnlCompras.Visible = false;
                return;
            }

            CargarEstados();
            SeleccionarEstadoDesdeForm();
            CargarCompras();

            if (!IsPostBack)
                MostrarPedidoConfirmado();
        }

        private void CargarEstados()
        {
            EstadoPedidoNegocio negocio = new EstadoPedidoNegocio();
            List<EstadoPedido> estados = negocio.Listar().Where(x => x.Activo).ToList();

            ddlEstado.DataSource = estados;
            ddlEstado.DataTextField = "Descripcion";
            ddlEstado.DataValueField = "Id";
            ddlEstado.DataBind();
            ddlEstado.Items.Insert(0, new ListItem("Todos", ""));
        }

        private void CargarCompras()
        {
            try
            {
                Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);
                PedidoNegocio negocio = new PedidoNegocio();
                List<Pedido> compras = negocio.ListarPorUsuario(usuario.Id, ObtenerEstadoSeleccionado(), ObtenerFecha(txtFechaDesde.Text), ObtenerFecha(txtFechaHasta.Text));

                dgvCompras.DataSource = compras;
                dgvCompras.DataBind();

                if (compras.Count == 0)
                    MostrarMensaje("No se encontraron compras para los filtros seleccionados.", "alert alert-info d-block");
                else
                    lblMensaje.Visible = false;
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert alert-danger d-block");
            }
        }

        private int? ObtenerEstadoSeleccionado()
        {
            int idEstado;
            if (int.TryParse(ddlEstado.SelectedValue, out idEstado) && idEstado > 0)
                return idEstado;

            return null;
        }

        private void SeleccionarEstadoDesdeForm()
        {
            string estado = Request.Form[ddlEstado.UniqueID];

            if (!string.IsNullOrWhiteSpace(estado) && ddlEstado.Items.FindByValue(estado) != null)
                ddlEstado.SelectedValue = estado;
        }

        private DateTime? ObtenerFecha(string valor)
        {
            DateTime fecha;
            if (DateTime.TryParse(valor, out fecha))
                return fecha;

            return null;
        }

        private void MostrarPedidoConfirmado()
        {
            if (!string.IsNullOrWhiteSpace(Request.QueryString["pedido"]))
                MostrarMensaje("Compra confirmada correctamente. Pedido #" + Request.QueryString["pedido"], "alert alert-success d-block");
        }

        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = cssClass;
            lblMensaje.Visible = true;
        }
    }
}
