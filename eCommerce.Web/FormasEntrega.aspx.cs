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
            if (!IsPostBack)
            {
                FormaEntregaNegocio negocio = new FormaEntregaNegocio();
                dgvFormasEntrega.DataSource = negocio.Listar();
                dgvFormasEntrega.DataBind();
            }
        }
    }
}