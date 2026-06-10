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
    public partial class CategoriaForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                chkActivo.Checked = true;
            }
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            Categoria categoria = new Categoria();
            categoria.Nombre = txtNombre.Text;
            categoria.Activo = chkActivo.Checked;

            CategoriaNegocio negocio = new CategoriaNegocio();
            negocio.AgregarCategoria(categoria);

            Response.Redirect("Categorias.aspx");
        }
    }
}