using System;
using System.Collections.Generic;
using System.Web.UI;
using eCommerce.Dominio;

namespace eCommerce.Web
{
    public partial class Carrito : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            bool preservarMensaje = ProcesarAcciones();
            CargarCarrito(preservarMensaje);
        }

        private void CargarCarrito(bool preservarMensaje)
        {
            List<ItemCarrito> carrito = CarritoSesion.Obtener(Session);

            dgvCarrito.DataSource = carrito;
            dgvCarrito.DataBind();

            bool tieneItems = carrito.Count > 0;
            pnlResumen.Visible = tieneItems;

            if (!preservarMensaje)
            {
                lblMensaje.Visible = !tieneItems;
                lblMensaje.Text = tieneItems ? "" : "El carrito esta vacio.";
            }

            decimal total = CarritoSesion.CalcularTotal(Session);
            lblSubtotal.Text = total.ToString("C");
            lblTotal.Text = total.ToString("C");
        }

        private bool ProcesarAcciones()
        {
            if (ProcesarQuitar())
                return true;

            if (IsPostBack)
            {
                ProcesarActualizarCantidades();
                return true;
            }

            return false;
        }

        private bool ProcesarQuitar()
        {
            int idProducto;
            if (!int.TryParse(Request.QueryString["quitar"], out idProducto))
                return false;

            CarritoSesion.Quitar(Session, idProducto);
            Response.Redirect("~/Carrito?quitado=1", false);
            Context.ApplicationInstance.CompleteRequest();
            return true;
        }

        private void ProcesarActualizarCantidades()
        {
            try
            {
                List<ItemCarrito> carrito = CarritoSesion.Obtener(Session);

                foreach (ItemCarrito item in carrito)
                {
                    int cantidad;
                    string valor = Request.Form["cantidad_" + item.IdProducto];

                    if (!int.TryParse(valor, out cantidad))
                        throw new Exception("Debe ingresar una cantidad valida.");

                    CarritoSesion.ActualizarCantidad(Session, item.IdProducto, cantidad);
                }

                MostrarMensaje("Cantidades actualizadas.", "alert alert-success d-block");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert alert-danger d-block");
            }
        }

        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblMensaje.Text = mensaje;
            lblMensaje.CssClass = cssClass;
            lblMensaje.Visible = true;
        }
    }
}
