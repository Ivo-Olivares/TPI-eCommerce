using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class ImagenDatos
    {
        public List<Imagen> ListarPorProducto(int idProducto)
        {
            List<Imagen> lista = new List<Imagen>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdImagen,Nombre, UrlImagen FROM IMAGENES WHERE IdProducto = @IdProducto");
                datos.setearParametros("@IdProducto", idProducto);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Imagen aux = new Imagen();
                    aux.Id = (int)datos.Lector["IdImagen"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.UrlImagen = (string)datos.Lector["UrlImagen"];
                    lista.Add(aux);

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

        public void GuardarImagenPrincipal(int idProducto, string urlImagen)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("IF EXISTS (SELECT 1 FROM IMAGENES WHERE IdProducto = @IdProducto) UPDATE IMAGENES SET UrlImagen = @UrlImagen WHERE IdProducto = @IdProducto ELSE INSERT INTO IMAGENES (IdProducto, Nombre, UrlImagen) VALUES (@IdProducto, 'Imagen principal', @UrlImagen)");
                datos.setearParametros("@IdProducto", idProducto);
                datos.setearParametros("@UrlImagen", urlImagen);
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


    }
}
