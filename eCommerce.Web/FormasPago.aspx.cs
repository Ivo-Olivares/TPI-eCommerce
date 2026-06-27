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
    public partial class FormasPago : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AutorizacionPagina.RequerirAdmin(Session, Response))
                return;

            if (!IsPostBack)
            {
                FormaPagoNegocio negocio = new FormaPagoNegocio();
                dgvFormasPago.DataSource = negocio.Listar();
                dgvFormasPago.DataBind();
            }
        }

        protected void btnAgregarFormaPago_Click(object sender, EventArgs e)
        {
            try
            {
                if (ViewState["IdFormaPago"] != null)
                {
                    FormaPago formaPago = new FormaPago();

                    formaPago.Id = (int)ViewState["IdFormaPago"];
                    formaPago.Descripcion = txtNombreFormaPago.Text.Trim();

                    FormaPagoNegocio negocio = new FormaPagoNegocio();

                    negocio.ModificarFormaPago(formaPago);
                    AplicarFiltro();

                    ViewState["IdFormaPago"] = null;
                    txtNombreFormaPago.Text = "";
                    lblError.Text = "";

                    btnAgregarFormaPago.Text = "Agregar Forma de pago";
                }
                else
                {
                    FormaPago formaPago = new FormaPago();
                    formaPago.Descripcion = txtNombreFormaPago.Text.Trim();

                    FormaPagoNegocio negocio = new FormaPagoNegocio();
                    negocio.AgregarFormaPago(formaPago);
                    AplicarFiltro();

                    txtNombreFormaPago.Text = "";
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
            ViewState["IdFormaPago"] = null;
            txtNombreFormaPago.Text = "";
            lblError.Text = "";
            btnAgregarFormaPago.Text = "Agregar Forma de pago";
            btnCancelar.Visible = false;
        }

        protected void dgvFormasPago_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            FormaPagoNegocio negocio = new FormaPagoNegocio();

            if (e.CommandName == "Editar")
            {
                FormaPago formaPago = negocio.Listar().Find(x => x.Id == id);

                txtNombreFormaPago.Text = formaPago.Descripcion;

                ViewState["IdFormaPago"] = formaPago.Id;

                btnAgregarFormaPago.Text = "Modificar Forma de pago";
                btnCancelar.Visible = true;
            }

            if (e.CommandName == "Desactivar")
            {
                FormaPago formaPago = new FormaPago();
                formaPago.Id = id;

                negocio.DesactivarFormaPago(formaPago);

                AplicarFiltro();

                ViewState["IdFormaPago"] = null;
                txtNombreFormaPago.Text = "";
                btnAgregarFormaPago.Text = "Agregar Forma de pago";
                btnCancelar.Visible = false;
            }

            if (e.CommandName == "Activar")
            {
                FormaPago formaPago = new FormaPago();
                formaPago.Id = id;

                negocio.ActivarFormaPago(formaPago);

                AplicarFiltro();

                ViewState["IdFormaPago"] = null;
                txtNombreFormaPago.Text = "";
                btnAgregarFormaPago.Text = "Agregar Forma de pago";
                btnCancelar.Visible = false;
            }
        }






        private void AplicarFiltro()
        {
            FormaPagoNegocio negocio = new FormaPagoNegocio();

            dgvFormasPago.DataSource = negocio.filtrarFormaPago(txtFiltroDescripcion.Text, ddlFiltroEstado.SelectedValue);
            dgvFormasPago.DataBind();

        }

        
        protected void txtFiltroDescripcion_TextChanged(object sender, EventArgs e)
        {
            AplicarFiltro();

        }

        protected void ddlFiltroEstado_SelectedIndexChanged(object sender, EventArgs e)
        {
            AplicarFiltro();

        }
    
    
    
    }

}
