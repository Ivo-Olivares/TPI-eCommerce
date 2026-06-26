using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class PedidoDatos
    {
        public List<Pedido> ListarPorUsuario(int idUsuario)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.IdPedido, P.FechaCreacion, P.FechaEntrega, P.Total, FP.IdFormaPago, FP.Descripcion FormaPago, FE.IdFormaEntrega, FE.Descripcion FormaEntrega, EP.IdEstadoPedido, EP.Descripcion EstadoPedido FROM PEDIDOS P INNER JOIN FORMASPAGO FP ON P.IdFormaPago = FP.IdFormaPago INNER JOIN FORMASENTREGA FE ON P.IdFormaEntrega = FE.IdFormaEntrega INNER JOIN ESTADOSPEDIDO EP ON P.IdEstadoPedido = EP.IdEstadoPedido WHERE P.IdUsuario = @IdUsuario ORDER BY P.FechaCreacion DESC");
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido();

                    pedido.Id = (int)datos.Lector["IdPedido"];
                    pedido.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    pedido.FechaEntrega = datos.Lector["FechaEntrega"] is DBNull ? (DateTime?)null : (DateTime)datos.Lector["FechaEntrega"];
                    pedido.Total = (decimal)datos.Lector["Total"];


                    pedido.FormaPago = new FormaPago();
                    pedido.FormaPago.Id = (int)datos.Lector["IdFormaPago"];
                    pedido.FormaPago.Descripcion = datos.Lector["FormaPago"] is DBNull ? "" : (string)datos.Lector["FormaPago"];

                    pedido.FormaEntrega = new FormaEntrega();
                    pedido.FormaEntrega.Id = (int)datos.Lector["IdFormaEntrega"];
                    pedido.FormaEntrega.Descripcion = datos.Lector["FormaEntrega"] is DBNull ? "" : (string)datos.Lector["FormaEntrega"];

                    pedido.EstadoPedido = new EstadoPedido();
                    pedido.EstadoPedido.Id = (int)datos.Lector["IdEstadoPedido"];
                    pedido.EstadoPedido.Descripcion = datos.Lector["EstadoPedido"] is DBNull ? "" : (string)datos.Lector["EstadoPedido"];

                    lista.Add(pedido);
                }

            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }


            return lista;

        }
    }
}
