using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class DetallePedidoNegocio
    {

        public List<DetallePedido> ListarPorPedido(int idPedido)
        {
            if (idPedido <= 0)
                throw new Exception("El pedido no es válido.");

            DetallePedidoDatos datos = new DetallePedidoDatos();
            return datos.ListarPorPedido(idPedido);
        }














        public void AgregarDetallePedido(DetallePedido detalle)
        {
            if (detalle == null)
                throw new Exception("El detalle del pedido no puede estar vacío.");

            if (detalle.Pedido == null || detalle.Pedido.Id <= 0)
                throw new Exception("El detalle debe estar asociado a un pedido válido.");

            if (detalle.Producto == null || detalle.Producto.Id <= 0)
                throw new Exception("El detalle debe tener un producto válido.");

            if (detalle.Cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            if (detalle.PrecioUnitario <= 0)
                throw new Exception("El precio unitario debe ser mayor a cero.");

            detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;

            DetallePedidoDatos datos = new DetallePedidoDatos();
            datos.AgregarDetallePedido(detalle);
        }
    }
}
