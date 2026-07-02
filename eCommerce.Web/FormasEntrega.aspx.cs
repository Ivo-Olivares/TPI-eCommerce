using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class FormasEntrega : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AutorizacionPagina.RequerirAdmin(Session, Response))
                return;

            if (!IsPostBack)
            {
                FormaEntregaNegocio negocio = new FormaEntregaNegocio();
                dgvFormasEntrega.DataSource = negocio.Listar();
                dgvFormasEntrega.DataBind();
            }
        }

        protected void btnAgregarFormaEntrega_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IdFormaEntrega"] != null)
                {
                    FormaEntrega formaEntrega = new FormaEntrega();

                    formaEntrega.Id = (int)ViewState["IdFormaEntrega"];
                    formaEntrega.Descripcion = txtNombreFormaEntrega.Text.Trim();

                    FormaEntregaNegocio negocio = new FormaEntregaNegocio();

                    negocio.ModificarFormaEntrega(formaEntrega);
                    AplicarFiltro();

                    ViewState["IdFormaEntrega"] = null;
                    txtNombreFormaEntrega.Text = "";
                    lblError.Text = "";
                    lblTituloFormulario.Text = "Agregar forma de entrega";
                    btnAgregarFormaEntrega.Text = "Agregar forma de entrega";
                    btnCancelar.Visible = false;
                }
                else
                {
                    FormaEntrega formaEntrega = new FormaEntrega();
                    formaEntrega.Descripcion = txtNombreFormaEntrega.Text.Trim();

                    FormaEntregaNegocio negocio = new FormaEntregaNegocio();
                    negocio.AgregarFormaEntrega(formaEntrega);
                    AplicarFiltro();

                    txtNombreFormaEntrega.Text = "";
                    lblError.Text = "";
                    lblTituloFormulario.Text = "Agregar forma de entrega";
                    btnAgregarFormaEntrega.Text = "Agregar forma de entrega";
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
            ViewState["IdFormaEntrega"] = null;
            txtNombreFormaEntrega.Text = "";
            lblError.Text = "";
            lblTituloFormulario.Text = "Agregar forma de entrega";
            btnAgregarFormaEntrega.Text = "Agregar forma de entrega";
            btnCancelar.Visible = false;
        }

        protected void dgvFormasEntrega_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            FormaEntregaNegocio negocio = new FormaEntregaNegocio();

            if (e.CommandName == "Editar")
            {
                FormaEntrega formaEntrega = negocio.Listar().Find(x => x.Id == id);

                txtNombreFormaEntrega.Text = formaEntrega.Descripcion;
                ViewState["IdFormaEntrega"] = formaEntrega.Id;

                lblTituloFormulario.Text = "Modificar forma de entrega";
                btnAgregarFormaEntrega.Text = "Modificar forma de entrega";
                btnCancelar.Visible = true;
            }

            if (e.CommandName == "Desactivar")
            {
                FormaEntrega formaEntrega = new FormaEntrega();
                formaEntrega.Id = id;

                negocio.DesactivarFormaEntrega(formaEntrega);

                AplicarFiltro();

                ViewState["IdFormaEntrega"] = null;
                txtNombreFormaEntrega.Text = "";
                lblError.Text = "";
                lblTituloFormulario.Text = "Agregar forma de entrega";
                btnAgregarFormaEntrega.Text = "Agregar forma de entrega";
                btnCancelar.Visible = false;
            }

            if (e.CommandName == "Activar")
            {
                FormaEntrega formaEntrega = new FormaEntrega();
                formaEntrega.Id = id;

                negocio.ActivarFormaEntrega(formaEntrega);

                AplicarFiltro();

                ViewState["IdFormaEntrega"] = null;
                txtNombreFormaEntrega.Text = "";
                lblError.Text = "";
                lblTituloFormulario.Text = "Agregar forma de entrega";
                btnAgregarFormaEntrega.Text = "Agregar forma de entrega";
                btnCancelar.Visible = false;
            }
        }

        private void AplicarFiltro()
        {
            FormaEntregaNegocio negocio = new FormaEntregaNegocio();

            dgvFormasEntrega.DataSource = negocio.filtrarentrega(txtFiltrodescripcion.Text, ddlFiltroEstado.SelectedValue);
            dgvFormasEntrega.DataBind();
        }

        protected void txtFiltrodescripcion_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }
    }
}