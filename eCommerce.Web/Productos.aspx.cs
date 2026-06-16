using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Linq;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class Productos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProductos();
                CargarCategorias();
                CargarMarcas();
            }
        }

        private void CargarProductos()
        {
            ProductoNegocio negocio = new ProductoNegocio();
            dgvProductos.DataSource = negocio.Listar();
            dgvProductos.DataBind();
        }

        private void CargarCategorias()
        {
            CategoriaNegocio negocio = new CategoriaNegocio();
            ddlCategoria.DataSource = negocio.Listar().Where(x => x.Activo).ToList();
            ddlCategoria.DataTextField = "Nombre";
            ddlCategoria.DataValueField = "Id";
            ddlCategoria.DataBind();
            ddlCategoria.Items.Insert(0, new ListItem("Seleccionar categoria", "0"));
        }

        private void CargarMarcas()
        {
            MarcaNegocio negocio = new MarcaNegocio();
            ddlMarca.DataSource = negocio.ListarMarcas();
            ddlMarca.DataTextField = "Nombre";
            ddlMarca.DataValueField = "Id";
            ddlMarca.DataBind();
            ddlMarca.Items.Insert(0, new ListItem("Seleccionar marca", "0"));
        }

        protected void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            try
            {
                Producto producto = new Producto();
                producto.Sku = txtSku.Text.Trim();
                producto.Nombre = txtNombreProducto.Text.Trim();
                producto.Descripcion = txtDescripcion.Text.Trim();
                producto.Categoria.Id = int.Parse(ddlCategoria.SelectedValue);
                producto.Marca.Id = int.Parse(ddlMarca.SelectedValue);

                if (!decimal.TryParse(txtPrecio.Text.Trim(), out decimal precio))
                    throw new Exception("El precio debe ser un número válido.");

                if (!int.TryParse(txtStock.Text.Trim(), out int stock))
                    throw new Exception("El stock debe ser un número válido.");

                producto.Precio = precio;
                producto.Stock = stock;

                ProductoNegocio negocio = new ProductoNegocio();

                if (ViewState["IdProducto"] != null)
                {
                    producto.Id = (int)ViewState["IdProducto"];
                    negocio.ModificarProducto(producto);
                }
                else
                {
                    negocio.AgregarProducto(producto);
                }

                CargarProductos();

                LimpiarFormulario();
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            LimpiarFormulario();
        }

        private void LimpiarFormulario()
        {
            ViewState["IdProducto"] = null;
            txtSku.Text = "";
            txtNombreProducto.Text = "";
            txtDescripcion.Text = "";
            ddlCategoria.SelectedValue = "0";
            ddlMarca.SelectedValue = "0";
            txtPrecio.Text = "";
            txtStock.Text = "";
            lblError.Text = "";
            btnAgregarProducto.Text = "Agregar Producto";
            btnCancelar.Visible = false;
        }

        protected void dgvProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int id = int.Parse(e.CommandArgument.ToString());

            ProductoNegocio negocio = new ProductoNegocio();

            if (e.CommandName == "Editar")
            {
                Producto producto = negocio.Listar().Find(x => x.Id == id);

                txtSku.Text = producto.Sku;
                txtNombreProducto.Text = producto.Nombre;
                txtDescripcion.Text = producto.Descripcion;
                ddlCategoria.SelectedValue = producto.Categoria.Id.ToString();
                ddlMarca.SelectedValue = producto.Marca.Id.ToString();
                txtPrecio.Text = producto.Precio.ToString();
                txtStock.Text = producto.Stock.ToString();

                ViewState["IdProducto"] = producto.Id;

                btnAgregarProducto.Text = "Modificar Producto";
                btnCancelar.Visible = true;
            }

            if (e.CommandName == "Desactivar")
            {
                Producto producto = new Producto();
                producto.Id = id;

                negocio.DesactivarProducto(producto);

                CargarProductos();
                LimpiarFormulario();
            }

            if (e.CommandName == "Activar")
            {
                Producto producto = new Producto();
                producto.Id = id;

                negocio.ActivarProducto(producto);

                CargarProductos();
                LimpiarFormulario();
            }
        }
    }
}
