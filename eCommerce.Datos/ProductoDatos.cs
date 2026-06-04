using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Datos
{
    public class ProductoDatos
    {
        public List<Producto> ListarProductos()
        {
            List<Producto> lista = new List<Producto>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Select P.IdProducto,P.Sku,P.Nombre As Producto,M.Nombre As Marca,C.Nombre As Categoria,P.Precio,P.Stock, P.Activo from PRODUCTOS P Inner Join MARCAS M On P.IdMarca = M.IdMarca Inner Join CATEGORIAS C On P.IdCategoria = C.IdCategoria;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = (int)datos.Lector["IdProducto"];
                    producto.Sku = (string)datos.Lector["Sku"];
                    producto.Nombre = (string)datos.Lector["Producto"];
                    producto.Marca.Nombre = (string)datos.Lector["Marca"];
                    producto.Categoria.Nombre = (string)datos.Lector["Categoria"];
                    producto.Precio = (decimal)datos.Lector["Precio"];
                    producto.Stock = (int)datos.Lector["Stock"];
                    producto.Activo = (bool)datos.Lector["Activo"];


                    lista.Add(producto);
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
