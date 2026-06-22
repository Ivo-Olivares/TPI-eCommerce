using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class EstadoPedidoDatos
    {
        public List<EstadoPedido> ListarEstadosPedido()
        {
            List<EstadoPedido> lista = new List<EstadoPedido>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdEstadoPedido, Descripcion, Activo FROM ESTADOSPEDIDO");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    EstadoPedido estadoPedido = new EstadoPedido();
                    estadoPedido.Id = (int)datos.Lector["IdEstadoPedido"];
                    estadoPedido.Descripcion = (string)datos.Lector["Descripcion"];
                    estadoPedido.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(estadoPedido);
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

        public void AgregarEstadoPedido(EstadoPedido estadoPedido)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ESTADOSPEDIDO (Descripcion) values (@Descripcion)");
                datos.setearParametros("@Descripcion", estadoPedido.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarEstadoPedido(EstadoPedido estadoPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update ESTADOSPEDIDO set Descripcion = @Descripcion where IdEstadoPedido = @Id");
                datos.setearParametros("@Descripcion", estadoPedido.Descripcion);
                datos.setearParametros("@Id", estadoPedido.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public void DesactivarEstadoPedido(EstadoPedido estadoPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update ESTADOSPEDIDO Set Activo = 0 WHERE IdEstadoPedido = @Id");
                datos.setearParametros("@Id", estadoPedido.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void ActivarEstadoPedido(EstadoPedido estadoPedido)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update ESTADOSPEDIDO Set Activo = 1 WHERE IdEstadoPedido = @Id");
                datos.setearParametros("@Id", estadoPedido.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
