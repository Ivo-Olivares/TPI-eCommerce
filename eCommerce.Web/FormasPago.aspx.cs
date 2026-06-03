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
            FormaPagoNegocio negocio = new FormaPagoNegocio();
            dgvFormasPago.DataSource = negocio.Listar();
            dgvFormasPago.DataBind();
        }
    }
}