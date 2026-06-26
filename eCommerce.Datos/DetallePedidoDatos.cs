using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class DetallePedidoDatos
    {
        public void AgregarDetallePedido(DetallePedido detalle)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO DETALLESPEDIDO (IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@IdPedido, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)");

                datos.setearParametros("@IdPedido", detalle.Pedido.Id);
                datos.setearParametros("@IdProducto", detalle.Producto.Id);
                datos.setearParametros("@Cantidad", detalle.Cantidad);
                datos.setearParametros("@PrecioUnitario", detalle.PrecioUnitario);
                datos.setearParametros("@Subtotal", detalle.Subtotal);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
