using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class MarcaDatos
    {
        public List<Marca> ListarMarcas()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdMarca, Nombre, Activo FROM MARCAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();
                    marca.Id = (int)datos.Lector["IdMarca"];
                    marca.Nombre = (string)datos.Lector["Nombre"];
                    marca.Activo = (bool)datos.Lector["Activo"];

                    lista.Add(marca);
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

        public void AgregarMarca(Marca nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into MARCAS (Nombre) values (@Nombre)");
                datos.setearParametros("@Nombre", nuevo.Nombre);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally { datos.cerrarConexion(); }
        }
        public void ModificarMarca(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update MARCAS set Nombre = @Nombre where IdMarca = @Id");
                datos.setearParametros("@Nombre", marca.Nombre);
                datos.setearParametros("id", marca.Id);
                datos.ejecutarAccion();

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

        public void desactivarMarca(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("update MARCAS set Activo = 0 where IdMarca = @Id");
                datos.setearParametros("Id", marca.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally             {
                datos.cerrarConexion();
            }
        }

        public void ActivarMarca(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConsulta("update MARCAS set Activo = 1 where IdMarca = @Id");
                datos.setearParametros("Id", marca.Id);
                datos.ejecutarAccion();

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

        public void EliminarMarca( int Id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("delete from MARCAS where IdMarca = @Id");
                datos.setearParametros("Id", Id); 
                datos.ejecutarAccion();
                




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








        public bool ExisteMarca(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) Cantidad  FROM MARCAS WHERE nombre = @nombre");
                datos.setearParametros("@nombre", nombre);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["Cantidad"] > 0;

                    return false;

                }

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
            return false;

        }

        public List<Marca> FiltrarMarcas(string filtroNombre, string estado)
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            try
            {

                string consulta = "SELECT IdMarca, Nombre , Activo FROM MARCAS WHERE Nombre LIKE @filtro";
                if (estado == "Activos")
                {
                    consulta += " AND Activo = 1";
                }
                else if (estado == "Inactivos")
                {
                    consulta += " AND Activo = 0";
                }

                datos.setearConsulta(consulta);
                datos.setearParametros("@filtro", "%" + filtroNombre.Trim() + "%");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca marca = new Marca();

                    marca.Id = (int)datos.Lector["IdMarca"];
                    marca.Nombre = (string)datos.Lector["Nombre"];
                    marca.Activo = (bool)datos.Lector["Activo"];
                   
                    lista.Add(marca);
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
