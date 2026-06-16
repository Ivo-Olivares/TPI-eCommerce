using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ProductoNegocio negocio = new ProductoNegocio();
                dgvProductos.DataSource = negocio.Listar();
                dgvProductos.DataBind();
            }
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            ViewState["IdProducto"] = null;
            txtSku.Text = "";
            txtNombreProducto.Text = "";
            txtDescripcion.Text = "";
            txtPrecio.Text = "";
            txtStock.Text = "";
            lblError.Text = "";
            btnAgregarProducto.Text = "Agregar Producto";
            btnCancelar.Visible = false;
        }

        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
        }
    }
}
