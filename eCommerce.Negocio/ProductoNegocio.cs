using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            ProductoDatos datos = new ProductoDatos();
            return datos.ListarProductos();
        }

        public void AgregarProducto(Producto producto)
        {
            ValidarProducto(producto);

            ProductoDatos datos = new ProductoDatos();
            datos.AgregarProducto(producto);
        }

        public void ModificarProducto(Producto producto)
        {
            ValidarProducto(producto);

            ProductoDatos datos = new ProductoDatos();
            datos.ModificarProducto(producto);
        }

        public void DesactivarProducto(Producto producto)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.DesactivarProducto(producto);
        }

        public void ActivarProducto(Producto producto)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.ActivarProducto(producto);
        }

        private void ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Sku))
                throw new Exception("El sku del producto no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new Exception("El nombre del producto no puede estar vacío.");

            if (producto.Categoria.Id <= 0)
                throw new Exception("Debe seleccionar una categoría.");

            if (producto.Marca.Id <= 0)
                throw new Exception("Debe seleccionar una marca.");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a cero.");

            if (producto.Stock < 0)
                throw new Exception("El stock no puede ser negativo.");
        }
    }
}
