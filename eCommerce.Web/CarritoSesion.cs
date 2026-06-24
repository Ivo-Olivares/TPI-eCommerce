using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;

namespace eCommerce.Web
{
    public static class CarritoSesion
    {
        private const string ClaveCarrito = "Carrito";

        public static List<ItemCarrito> Obtener(HttpSessionState session)
        {
            List<ItemCarrito> carrito = session[ClaveCarrito] as List<ItemCarrito>;

            if (carrito == null)
            {
                carrito = new List<ItemCarrito>();
                session[ClaveCarrito] = carrito;
            }

            return carrito;
        }

        public static void Agregar(HttpSessionState session, Producto producto, int cantidad)
        {
            ValidarProducto(producto);

            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            List<ItemCarrito> carrito = Obtener(session);
            ItemCarrito item = carrito.Find(x => x.IdProducto == producto.Id);
            int cantidadActual = item != null ? item.Cantidad : 0;

            if (cantidadActual + cantidad > producto.Stock)
                throw new Exception("No hay stock suficiente para agregar esa cantidad.");

            if (item == null)
            {
                carrito.Add(new ItemCarrito
                {
                    Producto = producto,
                    Cantidad = cantidad,
                    PrecioUnitario = producto.Precio
                });
            }
            else
            {
                item.Cantidad += cantidad;
                item.Producto = producto;
                item.PrecioUnitario = producto.Precio;
            }
        }

        public static decimal CalcularTotal(HttpSessionState session)
        {
            return Obtener(session).Sum(x => x.Subtotal);
        }

        public static void ActualizarCantidad(HttpSessionState session, int idProducto, int cantidad)
        {
            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");

            List<ItemCarrito> carrito = Obtener(session);
            ItemCarrito item = carrito.Find(x => x.IdProducto == idProducto);

            if (item == null)
                throw new Exception("El producto no esta en el carrito.");

            if (cantidad > item.StockDisponible)
                throw new Exception("No hay stock suficiente para esa cantidad.");

            item.Cantidad = cantidad;
        }

        public static void Quitar(HttpSessionState session, int idProducto)
        {
            List<ItemCarrito> carrito = Obtener(session);
            ItemCarrito item = carrito.Find(x => x.IdProducto == idProducto);

            if (item != null)
                carrito.Remove(item);
        }

        public static void Vaciar(HttpSessionState session)
        {
            session[ClaveCarrito] = new List<ItemCarrito>();
        }

        private static void ValidarProducto(Producto producto)
        {
            if (producto == null || !producto.Activo || producto.Stock <= 0)
                throw new Exception("El producto no esta disponible.");
        }
    }
}
