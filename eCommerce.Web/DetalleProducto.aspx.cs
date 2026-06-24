using System;
using System.Web.UI;
using eCommerce.Dominio;
using eCommerce.Negocio;

namespace eCommerce.Web
{
    public partial class DetalleProducto : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                CargarProducto();
                ProcesarAgregarDesdeQueryString();
            }
        }

        private void CargarProducto()
        {
            try
            {
                int idProducto;
                if (!int.TryParse(Request.QueryString["id"], out idProducto))
                {
                    MostrarError("No se encontro el producto solicitado.");
                    return;
                }

                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.BuscarPorId(idProducto);

                if (producto == null || !producto.Activo || producto.Stock <= 0 || !producto.Categoria.Activo || !producto.Marca.Activo)
                {
                    MostrarError("El producto no esta disponible.");
                    return;
                }

                ViewState["IdProducto"] = producto.Id;
                lblNombre.Text = producto.Nombre;
                lblMarcaCategoria.Text = producto.Marca.Nombre + " | " + producto.Categoria.Nombre;
                lblDescripcion.Text = producto.Descripcion;
                lblPrecio.Text = producto.Precio.ToString("C");
                lblStock.Text = producto.Stock.ToString();
                lblEstado.Text = producto.Activo ? "Activo" : "Inactivo";
                txtCantidad.Text = "1";
                pnlProducto.Visible = true;
                lblError.Visible = false;
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private void ProcesarAgregarDesdeQueryString()
        {
            if (Request.QueryString["agregar"] != "1" || ViewState["IdProducto"] == null)
                return;

            try
            {
                int idProducto = (int)ViewState["IdProducto"];
                int cantidad;

                if (!int.TryParse(Request.QueryString["cantidad"], out cantidad))
                    throw new Exception("Debe ingresar una cantidad valida.");

                ProductoNegocio negocio = new ProductoNegocio();
                Producto producto = negocio.BuscarPorId(idProducto);

                CarritoSesion.Agregar(Session, producto, cantidad);
                lblError.Text = "Producto agregado al carrito.";
                lblError.CssClass = "alert alert-success d-block";
                lblError.Visible = true;
                pnlProducto.Visible = true;
            }
            catch (Exception ex)
            {
                lblError.Text = ex.Message;
                lblError.CssClass = "alert alert-danger d-block";
                lblError.Visible = true;
                pnlProducto.Visible = true;
            }
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.CssClass = "alert alert-warning d-block";
            lblError.Visible = true;
            pnlProducto.Visible = false;
        }
    }
}
