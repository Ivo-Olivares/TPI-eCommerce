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

        public List<DetallePedido> ListarPorPedido (int idPedido)
        {

            List<DetallePedido> lista = new List<DetallePedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT DP.IdDetallePedido, DP.IdPedido, DP.IdProducto, P.Nombre Producto, DP.Cantidad, DP.PrecioUnitario, DP.Subtotal FROM DETALLESPEDIDO DP INNER JOIN PRODUCTOS P ON DP.IdProducto = P.IdProducto WHERE DP.IdPedido = @IdPedido");
                datos.setearParametros("@IdPedido", idPedido);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    DetallePedido detalle = new DetallePedido();
                    detalle.Id = (int)datos.Lector["IdDetallePedido"];
                    detalle.Pedido = new Pedido();
                    detalle.Pedido.Id = (int)datos.Lector["IdPedido"];
                    detalle.Producto = new Producto();
                    detalle.Producto.Id = (int)datos.Lector["IdProducto"];
                    detalle.Producto.Nombre = (string)datos.Lector["Producto"];
                    detalle.Cantidad = (int)datos.Lector["Cantidad"];
                    detalle.PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"];
                    detalle.Subtotal = (decimal)datos.Lector["Subtotal"];

                    lista.Add(detalle);
                }

                return lista;



            }
            catch (Exception ex)
            {

                throw;
            }
            finally

            {
                datos.cerrarConexion();
            }



        }



    }
}
