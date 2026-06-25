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
    public partial class Checkout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

                if (usuario == null || AutenticacionSesion.EsInvitado(Session))
                {
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }

                if (usuario.Id <= 0)
                {
                    throw new Exception("El usuario logueado no tiene un Id válido.");
                }

                CargarDirecciones(usuario.Id);
                CargarFormasEntrega();
                CargarFormasPago();
            }
        }

        private void CargarDirecciones(int idUsuario)
        {
            DireccionNegocio negocio = new DireccionNegocio();

            ddlDireccion.DataSource = negocio.Listar(idUsuario);
            ddlDireccion.DataTextField = "Descripcion";
            ddlDireccion.DataValueField = "Id";
            ddlDireccion.DataBind();

            ddlDireccion.Items.Insert(0, new ListItem("Seleccionar dirección", ""));
        }

        private void CargarFormasEntrega()
        {
            FormaEntregaNegocio negocio = new FormaEntregaNegocio();

            ddlEntrega.DataSource = negocio.Listar();
            ddlEntrega.DataTextField = "Descripcion";
            ddlEntrega.DataValueField = "Id";
            ddlEntrega.DataBind();

            ddlEntrega.Items.Insert(0, new ListItem("Seleccionar forma de entrega", ""));
        }

        private void CargarFormasPago()
        {
            FormaPagoNegocio negocio = new FormaPagoNegocio();

            ddlFormaPago.DataSource = negocio.Listar();
            ddlFormaPago.DataTextField = "Descripcion";
            ddlFormaPago.DataValueField = "Id";
            ddlFormaPago.DataBind();

            ddlFormaPago.Items.Insert(0, new ListItem("Seleccionar forma de pago", ""));
        }
    }
}