using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class DireccionDatos
    {
        public List<Direccion> ListarPorUsuario(int idUsuario)
        {
            List<Direccion> lista = new List<Direccion>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdDireccion, Descripcion, Calle, Altura, Localidad, Provincia, Cp, Observaciones, Activo FROM DIRECCIONES WHERE IdUsuario = @IdUsuario AND Activo = 1");
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Direccion direccion = new Direccion();
                    direccion.Id = (int)datos.Lector["IdDireccion"];
                    direccion.Descripcion = datos.Lector["Descripcion"] as string;
                    direccion.Calle = (string)datos.Lector["Calle"];
                    direccion.Altura = (int)datos.Lector["Altura"];
                    direccion.Localidad = (string)datos.Lector["Localidad"];
                    direccion.Provincia = (string)datos.Lector["Provincia"];
                    direccion.Cp = (string)datos.Lector["Cp"];
                    direccion.Observaciones = datos.Lector["Observaciones"] as string;
                    direccion.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(direccion);
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

        public int AgregarDireccion(Direccion direccion, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO DIRECCIONES (IdUsuario, Descripcion, Calle, Altura, Localidad, Provincia, Cp, Observaciones, Activo) OUTPUT INSERTED.IdDireccion VALUES (@IdUsuario, @Descripcion, @Calle, @Altura, @Localidad, @Provincia, @Cp, @Observaciones, 1)");
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.setearParametros("@Descripcion", direccion.Descripcion);
                datos.setearParametros("@Calle", direccion.Calle);
                datos.setearParametros("@Altura", direccion.Altura);
                datos.setearParametros("@Localidad", direccion.Localidad);
                datos.setearParametros("@Provincia", direccion.Provincia);
                datos.setearParametros("@Cp", direccion.Cp);
                datos.setearParametros("@Observaciones", direccion.Observaciones);

                object resultado = datos.ejecutarEscalar();

                if (resultado == null || resultado is DBNull)
                    throw new Exception("No se pudo obtener el Id de la direccion generada.");

                return Convert.ToInt32(resultado);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool PerteneceAlUsuario(int idDireccion, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM DIRECCIONES WHERE IdDireccion = @IdDireccion AND IdUsuario = @IdUsuario AND Activo = 1");
                datos.setearParametros("@IdDireccion", idDireccion);
                datos.setearParametros("@IdUsuario", idUsuario);

                return Convert.ToInt32(datos.ejecutarEscalar()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarDireccion(Direccion direccion, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE DIRECCIONES SET Descripcion = @Descripcion, Calle = @Calle, Altura = @Altura, Localidad = @Localidad, Provincia = @Provincia, Cp = @Cp, Observaciones = @Observaciones WHERE IdDireccion = @IdDireccion AND IdUsuario = @IdUsuario AND Activo = 1");
                datos.setearParametros("@IdDireccion", direccion.Id);
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.setearParametros("@Descripcion", direccion.Descripcion);
                datos.setearParametros("@Calle", direccion.Calle);
                datos.setearParametros("@Altura", direccion.Altura);
                datos.setearParametros("@Localidad", direccion.Localidad);
                datos.setearParametros("@Provincia", direccion.Provincia);
                datos.setearParametros("@Cp", direccion.Cp);
                datos.setearParametros("@Observaciones", direccion.Observaciones);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void DesactivarDireccion(int idDireccion, int idUsuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE DIRECCIONES SET Activo = 0 WHERE IdDireccion = @IdDireccion AND IdUsuario = @IdUsuario AND Activo = 1");
                datos.setearParametros("@IdDireccion", idDireccion);
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
