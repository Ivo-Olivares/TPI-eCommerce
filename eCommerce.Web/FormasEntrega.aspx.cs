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
                    dgvFormasEntrega.DataSource = negocio.Listar();
                    dgvFormasEntrega.DataBind();

                    ViewState["IdFormaEntrega"] = null;
                    txtNombreFormaEntrega.Text = "";
                    lblError.Text = "";

                    btnAgregarFormaEntrega.Text = "Agregar Forma de entrega";
                }
                else
                {
                    FormaEntrega formaEntrega = new FormaEntrega();
                    formaEntrega.Descripcion = txtNombreFormaEntrega.Text.Trim();

                    FormaEntregaNegocio negocio = new FormaEntregaNegocio();
                    negocio.AgregarFormaEntrega(formaEntrega);
                    dgvFormasEntrega.DataSource = negocio.Listar();
                    dgvFormasEntrega.DataBind();

                    txtNombreFormaEntrega.Text = "";
                    lblError.Text = "";
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
            btnAgregarFormaEntrega.Text = "Agregar Forma de entrega";
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

                btnAgregarFormaEntrega.Text = "Modificar Forma de entrega";
                btnCancelar.Visible = true;
            }

            if (e.CommandName == "Desactivar")
            {
                FormaEntrega formaEntrega = new FormaEntrega();
                formaEntrega.Id = id;

                negocio.DesactivarFormaEntrega(formaEntrega);

                dgvFormasEntrega.DataSource = negocio.Listar();
                dgvFormasEntrega.DataBind();

                ViewState["IdFormaEntrega"] = null;
                txtNombreFormaEntrega.Text = "";
                btnAgregarFormaEntrega.Text = "Agregar Forma de entrega";
                btnCancelar.Visible = false;
            }

            if (e.CommandName == "Activar")
            {
                FormaEntrega formaEntrega = new FormaEntrega();
                formaEntrega.Id = id;

                negocio.ActivarFormaEntrega(formaEntrega);

                dgvFormasEntrega.DataSource = negocio.Listar();
                dgvFormasEntrega.DataBind();

                ViewState["IdFormaEntrega"] = null;
                txtNombreFormaEntrega.Text = "";
                btnAgregarFormaEntrega.Text = "Agregar Forma de entrega";
                btnCancelar.Visible = false;
            }
        }

        private void FiltrarArticulo ()
        {
            FormaEntregaNegocio negocio = new FormaEntregaNegocio();

            dgvFormasEntrega.DataSource= negocio.filtrarentrega(txtFiltrodescripcion.Text, ddlFiltroEstado.SelectedValue);

            dgvFormasEntrega.DataBind();



        }

        protected void txtFiltrodescripcion_TextChanged(object sender, EventArgs e)
        {


            FiltrarArticulo();
        }

        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarArticulo();
        }
    }
}
