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
            ImagenDatos imagenDatos = new ImagenDatos();   


            try
            {
                datos.setearConsulta("Select P.IdProducto,P.IdMarca,P.IdCategoria,P.Sku,P.Nombre As Producto,P.Descripcion,M.Nombre As Marca,C.Nombre As Categoria,P.Precio,P.Stock, P.Activo from PRODUCTOS P Inner Join MARCAS M On P.IdMarca = M.IdMarca Inner Join CATEGORIAS C On P.IdCategoria = C.IdCategoria;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = (int)datos.Lector["IdProducto"];
                    producto.Marca.Id = (int)datos.Lector["IdMarca"];
                    producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    producto.Sku = (string)datos.Lector["Sku"];
                    producto.Nombre = (string)datos.Lector["Producto"];
                    producto.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                    producto.Marca.Nombre = (string)datos.Lector["Marca"];
                    producto.Categoria.Nombre = (string)datos.Lector["Categoria"];
                    producto.Precio = (decimal)datos.Lector["Precio"];
                    producto.Stock = (int)datos.Lector["Stock"];
                    producto.Activo = (bool)datos.Lector["Activo"];
                    producto.ListaImagenes = imagenDatos.ListarPorProducto(producto.Id);


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

        public Producto ObtenerPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            ImagenDatos imagenDatos = new ImagenDatos();

            try
            {
                datos.setearConsulta("Select P.IdProducto,P.IdMarca,P.IdCategoria,P.Sku,P.Nombre As Producto,P.Descripcion,M.Nombre As Marca,C.Nombre As Categoria,P.Precio,P.Stock, P.Activo from PRODUCTOS P Inner Join MARCAS M On P.IdMarca = M.IdMarca Inner Join CATEGORIAS C On P.IdCategoria = C.IdCategoria Where P.IdProducto = @IdProducto;");
                datos.setearParametros("@IdProducto", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Producto producto = new Producto();
                    producto.Id = (int)datos.Lector["IdProducto"];
                    producto.Marca.Id = (int)datos.Lector["IdMarca"];
                    producto.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    producto.Sku = (string)datos.Lector["Sku"];
                    producto.Nombre = (string)datos.Lector["Producto"];
                    producto.Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"];
                    producto.Marca.Nombre = (string)datos.Lector["Marca"];
                    producto.Categoria.Nombre = (string)datos.Lector["Categoria"];
                    producto.Precio = (decimal)datos.Lector["Precio"];
                    producto.Stock = (int)datos.Lector["Stock"];
                    producto.Activo = (bool)datos.Lector["Activo"];
                    producto.ListaImagenes = imagenDatos.ListarPorProducto(producto.Id);

                    return producto;
                }

                return null;
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

        public int AgregarProducto(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("insert into PRODUCTOS (IdCategoria, IdMarca, Sku, Nombre, Descripcion, Precio, Stock, Activo) values (@IdCategoria, @IdMarca, @Sku, @Nombre, @Descripcion, @Precio, @Stock, 1); SELECT SCOPE_IDENTITY() AS IdProducto;");
                datos.setearParametros("@IdCategoria", producto.Categoria.Id);
                datos.setearParametros("@IdMarca", producto.Marca.Id);
                datos.setearParametros("@Sku", producto.Sku);
                datos.setearParametros("@Nombre", producto.Nombre);
                datos.setearParametros("@Descripcion", producto.Descripcion);
                datos.setearParametros("@Precio", producto.Precio);
                datos.setearParametros("@Stock", producto.Stock);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return Convert.ToInt32(datos.Lector["IdProducto"]);

                }

                return 0;

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

        public void ModificarProducto(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("update PRODUCTOS set IdCategoria = @IdCategoria, IdMarca = @IdMarca, Sku = @Sku, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, Stock = @Stock where IdProducto = @Id");
                datos.setearParametros("@IdCategoria", producto.Categoria.Id);
                datos.setearParametros("@IdMarca", producto.Marca.Id);
                datos.setearParametros("@Sku", producto.Sku);
                datos.setearParametros("@Nombre", producto.Nombre);
                datos.setearParametros("@Descripcion", producto.Descripcion);
                datos.setearParametros("@Precio", producto.Precio);
                datos.setearParametros("@Stock", producto.Stock);
                datos.setearParametros("@Id", producto.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DesactivarProducto(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update PRODUCTOS Set Activo = 0 WHERE IdProducto = @Id");
                datos.setearParametros("@Id", producto.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void ActivarProducto(Producto producto)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("Update PRODUCTOS Set Activo = 1 WHERE IdProducto = @Id");
                datos.setearParametros("@Id", producto.Id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DescontarStock(int idProducto, int cantidad)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE PRODUCTOS SET Stock = Stock - @Cantidad WHERE IdProducto = @IdProducto AND Activo = 1 AND Stock >= @Cantidad; SELECT @@ROWCOUNT AS FilasAfectadas;");
                datos.setearParametros("@IdProducto", idProducto);
                datos.setearParametros("@Cantidad", cantidad);


                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    return (int)datos.Lector["FilasAfectadas"] == 1;
                }

                return false;


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
