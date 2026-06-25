using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eCommerce.Datos;
using eCommerce.Dominio;

namespace eCommerce.Web
{
    public partial class Marcas : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MarcaNegocio negocio = new MarcaNegocio();
                dgvMarcas.DataSource = negocio.ListarMarcas();
                dgvMarcas.DataBind();
            }
        }

        protected void btnAgregarMarca_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IdMarca"] != null)
                {
                    Marca marca = new Marca();

                    marca.Id = (int)ViewState["IdMarca"];
                    marca.Nombre = txtNombreMarca.Text.Trim();

                    MarcaNegocio negocio = new MarcaNegocio();

                    negocio.ModificarMarca(marca);
                    dgvMarcas.DataSource = negocio.ListarMarcas();
                    dgvMarcas.DataBind();

                    ViewState["IdMarca"] = null;
                    txtNombreMarca.Text = "";
                    lblError.Text = "";

                    btnAgregarMarca.Text = "Agregar Marca";
                }
                else
                {
                    Marca marca = new Marca();
                    marca.Nombre = txtNombreMarca.Text.Trim();

                    MarcaNegocio negocio = new MarcaNegocio();
                    negocio.AgregarMArca(marca);
                    dgvMarcas.DataSource = negocio.ListarMarcas();
                    dgvMarcas.DataBind();

                    txtNombreMarca.Text = "";
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
            ViewState["IdMarca"] = null;
            txtNombreMarca.Text = "";
            lblError.Text = "";
            btnAgregarMarca.Text = "Agregar Marca";
            btnCancelar.Visible = false;
        }

        protected void dgvMarcas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            MarcaNegocio negocio = new MarcaNegocio();

            if (e.CommandName == "Editar")
            {
                Marca marca = negocio.ListarMarcas().Find(x => x.Id == id);

                txtNombreMarca.Text = marca.Nombre;

                ViewState["IdMarca"] = marca.Id;

                btnAgregarMarca.Text = "Modificar Marca";
                btnCancelar.Visible = true;
            }

            if (e.CommandName == "Desactivar")
            {
                Marca marca = new Marca();
                marca.Id = id;

                negocio.DesactivarMarca(marca);

                dgvMarcas.DataSource = negocio.ListarMarcas();
                dgvMarcas.DataBind();

                ViewState["IdMarca"] = null;
                txtNombreMarca.Text = "";
                btnAgregarMarca.Text = "Agregar Marca";
                btnCancelar.Visible = false;
            }

            if (e.CommandName == "Activar")
            {
                Marca marca = new Marca();
                marca.Id = id;

                negocio.ActivarMarca(marca);

                dgvMarcas.DataSource = negocio.ListarMarcas();
                dgvMarcas.DataBind();

                ViewState["IdMarca"] = null;
                txtNombreMarca.Text = "";
                btnAgregarMarca.Text = "Agregar Marca";
                btnCancelar.Visible = false;
            }

        }


        private void AplicarFiltro()
        {
            MarcaNegocio marca = new MarcaNegocio();

            dgvMarcas.DataSource = marca.FiltrarMarcas(txtFiltroNombre.Text, ddlFiltroEstado.SelectedValue);
            dgvMarcas.DataBind();

        }


        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }

        protected void txtFiltroNombre_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();
        }


    }
}
