using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class PedidosDatos
    {
        public List<Pedido> ListarPorUsuario(int idUsuario)
        {
           List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT P.IdPedido, P.FechaCreacion, P.FechaEntrega, P.Total, FP.Descripcion FormaPago, FE.Descripcion FormaEntrega, EP.Descripcion EstadoPedido FROM PEDIDOS P INNER JOIN FORMASPAGO FP ON P.IdFormaPago = FP.IdFormaPago INNER JOIN FORMASENTREGA FE ON P.IdFormaEntrega = FE.IdFormaEntrega INNER JOIN ESTADOSPEDIDO EP ON P.IdEstadoPedido = EP.IdEstadoPedido WHERE P.IdUsuario = @IdUsuario ORDER BY P.FechaCreacion DESC");
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.ejecutarLectura();


                while(datos.Lector.Read())
                {
                    Pedido pedido = new Pedido();

                    pedido.Id = (int)datos.Lector["IDpedido"];
                    pedido.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    pedido.FechaEntrega = datos.Lector["FechaEntrega"] is DBNull ? (DateTime?)null : (DateTime)datos.Lector["FechaEntrega"];
                    pedido.Total = (decimal)datos.Lector["Total"];


                    pedido.FormaPago.Descripcion = (string)datos.Lector["FormaPago"];
                    pedido.FormaEntrega.Descripcion = (string)datos.Lector["FormaEntrega"];
                    pedido.EstadoPedido.Descripcion = (string)datos.Lector["EstadoPedido"];

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
