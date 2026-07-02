using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class AccesoDatos
    {
        private SqlConnection conexion;
        private SqlCommand comando;
        private SqlDataReader lector;
        private SqlTransaction transaccion;

        public SqlDataReader Lector
        {
            get { return lector; }
        }

        public AccesoDatos()
        {
            conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
            comando.Parameters.Clear();
        }

        public void ejecutarLectura()
        {
            prepararComando();

            try
            {
                abrirConexion();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int ejecutarAccion()
        {
            prepararComando();
            try
            {
                abrirConexion();
                return comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (transaccion == null)
                    conexion.Close();
            }
        }

        public object ejecutarEscalar()
        {
            prepararComando();

            try
            {
                abrirConexion();
                return comando.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (transaccion == null)
                    conexion.Close();
            }
        }

        public void iniciarTransaccion()
        {
            abrirConexion();
            transaccion = conexion.BeginTransaction();
            prepararComando();
        }

        public void confirmarTransaccion()
        {
            if (transaccion != null)
            {
                transaccion.Commit();
                transaccion = null;
            }

            cerrarConexion();
        }

        public void cancelarTransaccion()
        {
            if (transaccion != null)
            {
                transaccion.Rollback();
                transaccion = null;
            }

            cerrarConexion();
        }


        public void setearParametros(string nombre ,object valor)
        {
            comando.Parameters.AddWithValue(nombre, valor);
        }

        public void cerrarConexion()
        {
            if (lector != null)
            {
                lector.Close();
                lector = null;
            }

            if (conexion.State != System.Data.ConnectionState.Closed)
                conexion.Close();
        }

        private void abrirConexion()
        {
            if (conexion.State == System.Data.ConnectionState.Closed)
                conexion.Open();
        }

        private void prepararComando()
        {
            comando.Connection = conexion;
            comando.Transaction = transaccion;
        }
    }
}
