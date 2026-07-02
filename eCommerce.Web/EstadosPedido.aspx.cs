using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class EstadosPedido : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AutorizacionPagina.RequerirAdmin(Session, Response))
                return;

            if (!IsPostBack)
            {
                EstadoPedidoNegocio negocio = new EstadoPedidoNegocio();
                dgvEstadosPedido.DataSource = negocio.Listar();
                dgvEstadosPedido.DataBind();
            }
        }

        protected void btnAgregarEstadoPedido_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IdEstadoPedido"] != null)
                {
                    EstadoPedido estadoPedido = new EstadoPedido();

                    estadoPedido.Id = (int)ViewState["IdEstadoPedido"];
                    estadoPedido.Descripcion = txtNombreEstadosPedido.Text.Trim();

                    EstadoPedidoNegocio negocio = new EstadoPedidoNegocio();

                    negocio.ModificarEstadoPedido(estadoPedido);
                    dgvEstadosPedido.DataSource = negocio.Listar();
                    dgvEstadosPedido.DataBind();

                    ViewState["IdEstadoPedido"] = null;
                    txtNombreEstadosPedido.Text = "";
                    lblError.Text = "";
                    lblTituloFormulario.Text = "Agregar estado de pedido";
                    btnAgregarEstadoPedido.Text = "Agregar estado de pedido";
                    btnCancelar.Visible = false;
                }
                else
                {
                    EstadoPedido estadoPedido = new EstadoPedido();
                    estadoPedido.Descripcion = txtNombreEstadosPedido.Text.Trim();

                    EstadoPedidoNegocio negocio = new EstadoPedidoNegocio();
                    negocio.AgregarEstadoPedido(estadoPedido);
                    dgvEstadosPedido.DataSource = negocio.Listar();
                    dgvEstadosPedido.DataBind();

                    txtNombreEstadosPedido.Text = "";
                    lblError.Text = "";
                    lblTituloFormulario.Text = "Agregar estado de pedido";
                    btnAgregarEstadoPedido.Text = "Agregar estado de pedido";
                    btnCancelar.Visible = false;
                }
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ViewState["IdEstadoPedido"] = null;
            txtNombreEstadosPedido.Text = "";
            lblError.Text = "";
            lblTituloFormulario.Text = "Agregar estado de pedido";
            btnAgregarEstadoPedido.Text = "Agregar estado de pedido";
            btnCancelar.Visible = false;
        }

        protected void dgvEstadosPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            EstadoPedidoNegocio negocio = new EstadoPedidoNegocio();

            if (e.CommandName == "Editar")
            {
                EstadoPedido estadoPedido = negocio.Listar().Find(x => x.Id == id);

                txtNombreEstadosPedido.Text = estadoPedido.Descripcion;
                ViewState["IdEstadoPedido"] = estadoPedido.Id;

                lblTituloFormulario.Text = "Modificar estado de pedido";
                btnAgregarEstadoPedido.Text = "Modificar estado de pedido";
                btnCancelar.Visible = true;
            }

            if (e.CommandName == "Desactivar")
            {
                EstadoPedido estadoPedido = new EstadoPedido();
                estadoPedido.Id = id;

                negocio.DesactivarEstadoPedido(estadoPedido);

                dgvEstadosPedido.DataSource = negocio.Listar();
                dgvEstadosPedido.DataBind();

                ViewState["IdEstadoPedido"] = null;
                txtNombreEstadosPedido.Text = "";
                lblError.Text = "";
                lblTituloFormulario.Text = "Agregar estado de pedido";
                btnAgregarEstadoPedido.Text = "Agregar estado de pedido";
                btnCancelar.Visible = false;
            }

            if (e.CommandName == "Activar")
            {
                EstadoPedido estadoPedido = new EstadoPedido();
                estadoPedido.Id = id;

                negocio.ActivarEstadoPedido(estadoPedido);

                dgvEstadosPedido.DataSource = negocio.Listar();
                dgvEstadosPedido.DataBind();

                ViewState["IdEstadoPedido"] = null;
                txtNombreEstadosPedido.Text = "";
                lblError.Text = "";
                lblTituloFormulario.Text = "Agregar estado de pedido";
                btnAgregarEstadoPedido.Text = "Agregar estado de pedido";
                btnCancelar.Visible = false;
            }
        }
    }
}