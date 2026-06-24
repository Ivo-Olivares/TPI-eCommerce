using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace eCommerce.Datos
{
    public class PedidoDatos
    {
        public List<Pedido> ListarPorUsuario(int idUsuario, int? idEstado, DateTime? fechaDesde, DateTime? fechaHasta)
        {
            List<Pedido> lista = new List<Pedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "SELECT P.IdPedido, P.FechaCreacion, P.FechaEntrega, P.Total, FP.IdFormaPago, FP.Descripcion AS FormaPago, FE.IdFormaEntrega, FE.Descripcion AS FormaEntrega, EP.IdEstadoPedido, EP.Descripcion AS EstadoPedido FROM PEDIDOS P INNER JOIN FORMASPAGO FP ON P.IdFormaPago = FP.IdFormaPago INNER JOIN FORMASENTREGA FE ON P.IdFormaEntrega = FE.IdFormaEntrega INNER JOIN ESTADOSPEDIDO EP ON P.IdEstadoPedido = EP.IdEstadoPedido WHERE P.IdUsuario = @IdUsuario";

                if (idEstado.HasValue)
                    consulta += " AND P.IdEstadoPedido = @IdEstado";

                if (fechaDesde.HasValue)
                    consulta += " AND P.FechaCreacion >= @FechaDesde";

                if (fechaHasta.HasValue)
                    consulta += " AND P.FechaCreacion < DATEADD(DAY, 1, @FechaHasta)";

                consulta += " ORDER BY P.FechaCreacion DESC";

                datos.setearConsulta(consulta);
                datos.setearParametros("@IdUsuario", idUsuario);

                if (idEstado.HasValue)
                    datos.setearParametros("@IdEstado", idEstado.Value);

                if (fechaDesde.HasValue)
                    datos.setearParametros("@FechaDesde", fechaDesde.Value);

                if (fechaHasta.HasValue)
                    datos.setearParametros("@FechaHasta", fechaHasta.Value);

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Pedido pedido = new Pedido();
                    pedido.Id = (int)datos.Lector["IdPedido"];
                    pedido.FechaCreacion = (DateTime)datos.Lector["FechaCreacion"];
                    pedido.FechaEntrega = datos.Lector["FechaEntrega"] is DBNull ? (DateTime?)null : (DateTime)datos.Lector["FechaEntrega"];
                    pedido.Total = (decimal)datos.Lector["Total"];
                    pedido.FormaPago = new FormaPago { Id = (int)datos.Lector["IdFormaPago"], Descripcion = (string)datos.Lector["FormaPago"] };
                    pedido.FormaEntrega = new FormaEntrega { Id = (int)datos.Lector["IdFormaEntrega"], Descripcion = (string)datos.Lector["FormaEntrega"] };
                    pedido.EstadoPedido = new EstadoPedido { Id = (int)datos.Lector["IdEstadoPedido"], Descripcion = (string)datos.Lector["EstadoPedido"] };

                    lista.Add(pedido);
                }

                return lista;
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

        public int ConfirmarPedido(Usuario usuario, Direccion direccion, int idFormaPago, int idFormaEntrega, List<ItemCarrito> items)
        {
            using (SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString))
            {
                conexion.Open();
                SqlTransaction transaccion = conexion.BeginTransaction();

                try
                {
                    ValidarStock(conexion, transaccion, items);

                    int idDireccion = InsertarDireccion(conexion, transaccion, usuario.Id, direccion);
                    int idEstadoPedido = ObtenerEstadoPendiente(conexion, transaccion);
                    decimal total = items.Sum(x => x.Subtotal);
                    int idPedido = InsertarPedido(conexion, transaccion, usuario.Id, idDireccion, idFormaPago, idFormaEntrega, idEstadoPedido, total);

                    foreach (ItemCarrito item in items)
                    {
                        InsertarDetalle(conexion, transaccion, idPedido, item);
                        DescontarStock(conexion, transaccion, item);
                    }

                    transaccion.Commit();
                    return idPedido;
                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
        }

        private void ValidarStock(SqlConnection conexion, SqlTransaction transaccion, List<ItemCarrito> items)
        {
            foreach (ItemCarrito item in items)
            {
                using (SqlCommand comando = new SqlCommand("SELECT Stock, Activo FROM PRODUCTOS WITH (UPDLOCK, ROWLOCK) WHERE IdProducto = @IdProducto", conexion, transaccion))
                {
                    comando.Parameters.AddWithValue("@IdProducto", item.IdProducto);

                    using (SqlDataReader lector = comando.ExecuteReader())
                    {
                        if (!lector.Read())
                            throw new Exception("Uno de los productos del carrito no existe.");

                        bool activo = (bool)lector["Activo"];
                        int stock = (int)lector["Stock"];

                        if (!activo || stock < item.Cantidad)
                            throw new Exception("No hay stock suficiente para confirmar la compra.");
                    }
                }
            }
        }

        private int InsertarDireccion(SqlConnection conexion, SqlTransaction transaccion, int idUsuario, Direccion direccion)
        {
            using (SqlCommand comando = new SqlCommand("INSERT INTO DIRECCIONES (IdUsuario, Descripcion, Calle, Altura, Localidad, Provincia, Cp, Observaciones) OUTPUT INSERTED.IdDireccion VALUES (@IdUsuario, @Descripcion, @Calle, @Altura, @Localidad, @Provincia, @Cp, @Observaciones)", conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                comando.Parameters.AddWithValue("@Descripcion", "Direccion de checkout");
                comando.Parameters.AddWithValue("@Calle", direccion.Calle);
                comando.Parameters.AddWithValue("@Altura", direccion.Altura);
                comando.Parameters.AddWithValue("@Localidad", direccion.Localidad);
                comando.Parameters.AddWithValue("@Provincia", direccion.Provincia);
                comando.Parameters.AddWithValue("@Cp", direccion.Cp);
                comando.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(direccion.Observaciones) ? (object)DBNull.Value : direccion.Observaciones);

                return (int)comando.ExecuteScalar();
            }
        }

        private int ObtenerEstadoPendiente(SqlConnection conexion, SqlTransaction transaccion)
        {
            using (SqlCommand comando = new SqlCommand("SELECT IdEstadoPedido FROM ESTADOSPEDIDO WHERE Descripcion = @Descripcion AND Activo = 1", conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@Descripcion", "Pendiente");
                object resultado = comando.ExecuteScalar();

                if (resultado == null)
                    throw new Exception("No existe un estado de pedido Pendiente activo.");

                return (int)resultado;
            }
        }

        private int InsertarPedido(SqlConnection conexion, SqlTransaction transaccion, int idUsuario, int idDireccion, int idFormaPago, int idFormaEntrega, int idEstadoPedido, decimal total)
        {
            using (SqlCommand comando = new SqlCommand("INSERT INTO PEDIDOS (IdUsuario, IdDireccion, IdFormaPago, IdFormaEntrega, IdEstadoPedido, FechaCreacion, FechaEntrega, Total) OUTPUT INSERTED.IdPedido VALUES (@IdUsuario, @IdDireccion, @IdFormaPago, @IdFormaEntrega, @IdEstadoPedido, GETDATE(), NULL, @Total)", conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@IdUsuario", idUsuario);
                comando.Parameters.AddWithValue("@IdDireccion", idDireccion);
                comando.Parameters.AddWithValue("@IdFormaPago", idFormaPago);
                comando.Parameters.AddWithValue("@IdFormaEntrega", idFormaEntrega);
                comando.Parameters.AddWithValue("@IdEstadoPedido", idEstadoPedido);
                comando.Parameters.AddWithValue("@Total", total);

                return (int)comando.ExecuteScalar();
            }
        }

        private void InsertarDetalle(SqlConnection conexion, SqlTransaction transaccion, int idPedido, ItemCarrito item)
        {
            using (SqlCommand comando = new SqlCommand("INSERT INTO DETALLESPEDIDO (IdPedido, IdProducto, Cantidad, PrecioUnitario, Subtotal) VALUES (@IdPedido, @IdProducto, @Cantidad, @PrecioUnitario, @Subtotal)", conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@IdPedido", idPedido);
                comando.Parameters.AddWithValue("@IdProducto", item.IdProducto);
                comando.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                comando.Parameters.AddWithValue("@PrecioUnitario", item.PrecioUnitario);
                comando.Parameters.AddWithValue("@Subtotal", item.Subtotal);
                comando.ExecuteNonQuery();
            }
        }

        private void DescontarStock(SqlConnection conexion, SqlTransaction transaccion, ItemCarrito item)
        {
            using (SqlCommand comando = new SqlCommand("UPDATE PRODUCTOS SET Stock = Stock - @Cantidad WHERE IdProducto = @IdProducto AND Stock >= @Cantidad", conexion, transaccion))
            {
                comando.Parameters.AddWithValue("@Cantidad", item.Cantidad);
                comando.Parameters.AddWithValue("@IdProducto", item.IdProducto);

                if (comando.ExecuteNonQuery() == 0)
                    throw new Exception("No se pudo descontar el stock del producto.");
            }
        }
    }
}
