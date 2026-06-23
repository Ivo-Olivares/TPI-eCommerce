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
                datos.setearConsulta("SELECT IdFormaEntrega, Descripcion, Activo FROM FORMASENTREGA");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaEntrega formaEntrega = new FormaEntrega();
                    formaEntrega.Id = (int)datos.Lector["IdFormaEntrega"];
                    formaEntrega.Descripcion = (string)datos.Lector["Descripcion"];
                    formaEntrega.Activo = (bool)datos.Lector["Activo"];

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

        public void AgregarFormaEntrega(FormaEntrega formaEntrega)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into FORMASENTREGA (Descripcion) values (@Descripcion)");
                datos.setearParametros("@Descripcion", formaEntrega.Descripcion);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ModificarFormaEntrega(FormaEntrega formaEntrega)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update FORMASENTREGA set Descripcion = @Descripcion where IdFormaEntrega = @Id");
                datos.setearParametros("@Descripcion", formaEntrega.Descripcion);
                datos.setearParametros("@Id", formaEntrega.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }



        }

        public void DesactivarFormaEntrega(FormaEntrega formaEntrega)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update FORMASENTREGA Set Activo = 0 WHERE IdFormaEntrega = @Id");
                datos.setearParametros("@Id", formaEntrega.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void ActivarFormaEntrega(FormaEntrega formaEntrega)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update FORMASENTREGA Set Activo = 1 WHERE IdFormaEntrega = @Id");
                datos.setearParametros("@Id", formaEntrega.Id);
                datos.ejecutarAccion();

            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public List<FormaEntrega> FiltrarEntregas(string filtroDescripcion, string estado)
        {
            List<FormaEntrega> lista = new List<FormaEntrega>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                string consulta = "SELECT IdFormaEntrega, Descripcion , Activo FROM FORMASENTREGA WHERE Descripcion LIKE @filtro";
                if (estado == "Activos")
                {
                    consulta += " AND Activo = 1";
                }
                else if (estado == "Inactivos")
                {
                    consulta += " AND Activo = 0";
                }

                datos.setearConsulta(consulta);
                datos.setearParametros("@filtro", "%" + filtroDescripcion.Trim() + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    FormaEntrega entrega = new FormaEntrega();

                    entrega.Id = (int)datos.Lector["IdformaEntrega"];
                    entrega.Descripcion = (string)datos.Lector["Descripcion"];
                    entrega.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(entrega);
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
