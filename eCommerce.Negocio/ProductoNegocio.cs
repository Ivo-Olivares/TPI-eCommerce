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

        public List<Producto> ListarActivos()
        {
            return Listar().Where(x => x.Activo).ToList();
        }

        public Producto ObtenerPorId(int id)
        {
            ProductoDatos datos = new ProductoDatos();
            return datos.ObtenerPorId(id);
        }

        public Producto ObtenerActivoPorId(int id)
        {
            Producto producto = ObtenerPorId(id);

            if (producto == null || !producto.Activo)
                return null;

            return producto;
        }

        public int AgregarProducto(Producto producto)
        {
            ValidarProducto(producto);

            ProductoDatos datos = new ProductoDatos();

            return datos.AgregarProducto(producto);
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

        public void DescontarStock(int idproducto, int cantidad)
        {

            if (idproducto <= 0)
                throw new Exception("el producto no es valido");

            if (cantidad <= 0)
                throw new Exception("la cantidad debe ser mayor a cero");


            ProductoDatos datos = new ProductoDatos();
            bool puedoDescontar = datos.DescontarStock(idproducto, cantidad);

            if(!puedoDescontar)
            {
                throw new Exception("no hay stock suficiente para completar la compra.");

            }



        }

        public void DescontarStockPedido(List<DetallePedido> detalles)
        {

            if (detalles == null || detalles.Count == 0)
            {   
                throw new Exception("no hay productos para descontar stock");
            }
            foreach (DetallePedido detalle in detalles)
            {
                if (detalle.Producto == null || detalle.Producto.Id <= 0)
                    throw new Exception("Uno de los productos no es valido");

                    DescontarStock(detalle.Producto.Id, detalle.Cantidad);
            }
        }

        public void ValidarStockDisponible(List<DetallePedido> detalles)
        {
            if (detalles == null || detalles.Count == 0)
                throw new Exception("No hay productos para validar stock");

            foreach (DetallePedido detalle in detalles)
            {
                if (detalle.Producto == null || detalle.Producto.Id <= 0)
                    throw new Exception("Uno de los productos no es válido.");

                if (detalle.Cantidad <= 0)
                    throw new Exception("La cantidad debe ser mayor a cero.");

                Producto producto = ObtenerActivoPorId(detalle.Producto.Id);

                if (producto == null)
                    throw new Exception("Uno de los productos ya no está disponible.");

                if (detalle.Cantidad > producto.Stock)
                    throw new Exception("No hay stock suficiente para el producto: " + producto.Nombre);
            }


        }



        private void ValidarProducto(Producto producto)
        {
            if (string.IsNullOrWhiteSpace(producto.Sku))
                throw new Exception("El sku del producto no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new Exception("El nombre del producto no puede estar vacío.");

            producto.Sku = producto.Sku.Trim();
            producto.Nombre = producto.Nombre.Trim();

            if (EsSoloNumeros(producto.Nombre))
                throw new Exception("El nombre del producto no puede contener solamente numeros.");

            if (producto.Categoria.Id <= 0)
                throw new Exception("Debe seleccionar una categoría.");

            if (producto.Marca.Id <= 0)
                throw new Exception("Debe seleccionar una marca.");

            if (producto.Precio <= 0)
                throw new Exception("El precio debe ser mayor a cero.");

            if (producto.Stock < 0)
                throw new Exception("El stock no puede ser negativo.");

            Producto productoConMismoSku = Listar().Find(x => string.Equals(x.Sku, producto.Sku, StringComparison.InvariantCultureIgnoreCase) && x.Id != producto.Id);

            if (productoConMismoSku != null)
                throw new Exception("Ya existe un producto con ese sku.");

            Producto productoConMismoNombre = Listar().Find(x => string.Equals(x.Nombre, producto.Nombre, StringComparison.InvariantCultureIgnoreCase) && x.Id != producto.Id);

            if (productoConMismoNombre != null)
                throw new Exception("Ya existe un producto con ese nombre.");
        }

        private bool EsSoloNumeros(string texto)
        {
            string textoSinEspacios = new string(texto.Where(x => !char.IsWhiteSpace(x)).ToArray());
            return textoSinEspacios.All(char.IsDigit);
        }
    }
}
