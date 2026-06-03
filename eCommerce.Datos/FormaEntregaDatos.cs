using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class FormaEntregaDatos
    {
        public List<FormaEntrega> ListarFormasEntrega()
        {
            List<FormaEntrega> lista = new List<FormaEntrega>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdFormaEntrega, Descripcion FROM FORMASENTREGA");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaEntrega formaEntrega = new FormaEntrega();
                    formaEntrega.Id = (int)datos.Lector["IdFormaEntrega"];
                    formaEntrega.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(formaEntrega);
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
    }
}
