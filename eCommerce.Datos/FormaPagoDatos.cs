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
                datos.setearConsulta("SELECT IdFormaPago, Descripcion, Activo FROM FORMASPAGO");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaPago formaPago = new FormaPago();
                    formaPago.Id = (int)datos.Lector["IdFormaPago"];
                    formaPago.Descripcion = (string)datos.Lector["Descripcion"];
                    formaPago.Activo = (bool)datos.Lector["Activo"];

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

        public void AgregarFormaPago(FormaPago formaPago)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into FormasPago (Descripcion) values (@Descripcion)");
                datos.setearParametros("@Descripcion", formaPago.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFormaPago(FormaPago formaPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update FORMASPAGO set Descripcion = @Descripcion where IdFormaPago = @Id");
                datos.setearParametros("@Descripcion", formaPago.Descripcion);
                datos.setearParametros("@Id", formaPago.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public void DesactivarFormaPago(FormaPago formaPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update FORMASPAGO Set Activo = 0 WHERE IdFormaPago = @Id");
                datos.setearParametros("@Id", formaPago.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void ActivarFormaPago(FormaPago formaPago)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update FORMASPAGO Set Activo = 1 WHERE IdFormaPago = @Id");
                datos.setearParametros("@Id", formaPago.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }


        public List<FormaPago>FiltrarFormasPagos(string filtroDecripcion, string estado)
        {
            List<FormaPago> lista = new List<FormaPago>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                string consulta = "select IdFormaPago, Descripcion, Activo FROM FORMASPAGO WHERE Descripcion LIKE @filtro";


                if (estado == "Activos")
                {
                    consulta += " AND Activo = 1";
                }
                else if (estado == "Inactivos")

                {
                    consulta += " AND Activo = 0";
                }

                datos.setearConsulta(consulta);
                datos.setearParametros ("@filtro", "%" + filtroDecripcion.Trim() + "%");

                datos.ejecutarLectura();


                while (datos.Lector.Read())
                {
                    FormaPago formaPago = new FormaPago();
                    formaPago.Id = (int)datos.Lector["IdFormaPago"];
                    formaPago.Descripcion = (string)datos.Lector["Descripcion"];
                    formaPago.Activo = (bool)datos.Lector["Activo"];

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
