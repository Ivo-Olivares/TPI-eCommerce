using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class FormaPagoDatos
    {
        public List<FormaPago> ListarFormasPago()
        {
            List<FormaPago> lista = new List<FormaPago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdFormaPago, Descripcion FROM FORMASPAGO");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaPago formaPago = new FormaPago();
                    formaPago.Id = (int)datos.Lector["IdFormaPago"];
                    formaPago.Descripcion = (string)datos.Lector["Descripcion"];

                    lista.Add(formaPago);
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
