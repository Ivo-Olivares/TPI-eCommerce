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

        public List<Pedido> ListarTodos()
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"SELECT P.IdPedido, P.IdUsuario, U.Nombre, U.Apellido, U.Email, P.FechaCreacion, P.FechaEntrega, P.Total,
                    FP.IdFormaPago, FP.Descripcion FormaPago,
                    FE.IdFormaEntrega, FE.Descripcion FormaEntrega,
                    EP.IdEstadoPedido, EP.Descripcion EstadoPedido
                    FROM PEDIDOS P
                    INNER JOIN USUARIOS U ON P.IdUsuario = U.IdUsuario
                    INNER JOIN FORMASPAGO FP ON P.IdFormaPago = FP.IdFormaPago
                    INNER JOIN FORMASENTREGA FE ON P.IdFormaEntrega = FE.IdFormaEntrega
                    INNER JOIN ESTADOSPEDIDO EP ON P.IdEstadoPedido = EP.IdEstadoPedido
                    ORDER BY P.FechaCreacion DESC");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido();

                    pedido.Id = (int)datos.Lector["IdPedido"];
                    pedido.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    pedido.FechaEntrega = datos.Lector["FechaEntrega"] is DBNull ? (DateTime?)null : (DateTime)datos.Lector["FechaEntrega"];
                    pedido.Total = (decimal)datos.Lector["Total"];

                    pedido.Usuario = new Usuario();
                    pedido.Usuario.Id = (int)datos.Lector["IdUsuario"];
                    pedido.Usuario.Nombre = datos.Lector["Nombre"] is DBNull ? "" : (string)datos.Lector["Nombre"];
                    pedido.Usuario.Apellido = datos.Lector["Apellido"] is DBNull ? "" : (string)datos.Lector["Apellido"];
                    pedido.Usuario.Email = datos.Lector["Email"] is DBNull ? "" : (string)datos.Lector["Email"];

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

        public int ActualizarEstado(int idPedido, int idEstadoPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE PEDIDOS SET IdEstadoPedido = @IdEstadoPedido WHERE IdPedido = @IdPedido");
                datos.setearParametros("@IdEstadoPedido", idEstadoPedido);
                datos.setearParametros("@IdPedido", idPedido);
                return datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int AgregarPedido(Pedido pedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO PEDIDOS
            (IdUsuario, IdFormaPago, IdFormaEntrega, IdEstadoPedido, IdDireccion, FechaCreacion, FechaEntrega, Total)
            OUTPUT INSERTED.IdPedido
            VALUES
            (@IdUsuario, @IdFormaPago, @IdFormaEntrega, @IdEstadoPedido, @IdDireccion, @FechaCreacion, @FechaEntrega, @Total)");

                datos.setearParametros("@IdUsuario", pedido.Usuario.Id);
                datos.setearParametros("@IdFormaPago", pedido.FormaPago.Id);
                datos.setearParametros("@IdFormaEntrega", pedido.FormaEntrega.Id);
                datos.setearParametros("@IdEstadoPedido", pedido.EstadoPedido.Id);
                datos.setearParametros("@IdDireccion", pedido.Direccion.Id);
                datos.setearParametros("@FechaCreacion", pedido.FechaCreacion);
                datos.setearParametros("@FechaEntrega", DBNull.Value);
                datos.setearParametros("@Total", pedido.Total);

                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["IdPedido"];
                }

                throw new Exception("No se pudo obtener el Id del pedido generado.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int ConfirmarCompra(Pedido pedido, List<DetallePedido> detalles)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.iniciarTransaccion();

                int idPedido = InsertarPedido(datos, pedido);

                foreach (DetallePedido detalle in detalles)
                {
                    InsertarDetalle(datos, idPedido, detalle);
                    DescontarStock(datos, detalle);
                }

                datos.confirmarTransaccion();
                return idPedido;
            }
            catch (Exception ex)
            {
                datos.cancelarTransaccion();
                throw ex;
            }
        }

        private int InsertarPedido(AccesoDatos datos, Pedido pedido)
        {
            datos.setearConsulta(@"
                INSERT INTO PEDIDOS
                (IdUsuario, IdFormaPago, IdFormaEntrega, IdEstadoPedido, IdDireccion, FechaCreacion, FechaEntrega, Total)
                OUTPUT INSERTED.IdPedido
                VALUES
                (@IdUsuario, @IdFormaPago, @IdFormaEntrega, @IdEstadoPedido, @IdDireccion, @FechaCreacion, @FechaEntrega, @Total)");

            datos.setearParametros("@IdUsuario", pedido.Usuario.Id);
            datos.setearParametros("@IdFormaPago", pedido.FormaPago.Id);
            datos.setearParametros("@IdFormaEntrega", pedido.FormaEntrega.Id);
            datos.setearParametros("@IdEstadoPedido", pedido.EstadoPedido.Id);
            datos.setearParametros("@IdDireccion", pedido.Direccion.Id);
            datos.setearParametros("@FechaCreacion", pedido.FechaCreacion);
            datos.setearParametros("@FechaEntrega", DBNull.Value);
            datos.setearParametros("@Total", pedido.Total);

            object resultado = datos.ejecutarEscalar();

            if (resultado == null || resultado is DBNull)
                throw new Exception("No se pudo obtener el Id del pedido generado.");

            return Convert.ToInt32(resultado);
        }

        private void InsertarDetalle(AccesoDatos datos, int idPedido, DetallePedido detalle)
        {
            datos.setearConsulta("INSERT INTO DETALLESPEDIDO (IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@IdPedido, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)");
            datos.setearParametros("@IdPedido", idPedido);
            datos.setearParametros("@IdProducto", detalle.Producto.Id);
            datos.setearParametros("@Cantidad", detalle.Cantidad);
            datos.setearParametros("@PrecioUnitario", detalle.PrecioUnitario);
            datos.setearParametros("@Subtotal", detalle.Subtotal);
            datos.ejecutarAccion();
        }

        private void DescontarStock(AccesoDatos datos, DetallePedido detalle)
        {
            datos.setearConsulta("UPDATE PRODUCTOS SET Stock = Stock - @Cantidad WHERE IdProducto = @IdProducto AND Activo = 1 AND Stock >= @Cantidad");
            datos.setearParametros("@IdProducto", detalle.Producto.Id);
            datos.setearParametros("@Cantidad", detalle.Cantidad);

            int filasAfectadas = datos.ejecutarAccion();

            if (filasAfectadas != 1)
                throw new Exception("No hay stock suficiente para el producto: " + detalle.Producto.Nombre);
        }
    }
}
