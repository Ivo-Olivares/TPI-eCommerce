using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.SessionState;

namespace eCommerce.Web
{
    public static class CarritoSesion
    {
        public const string ClaveCarrito = "Carrito";

        public static List<DetallePedido> Obtener(HttpSessionState session)
        {
            List<DetallePedido> carrito = session[ClaveCarrito] as List<DetallePedido>;

            if (carrito == null)
            {
                carrito = new List<DetallePedido>();
                session[ClaveCarrito] = carrito;
            }

            return carrito;
        }

        public static void AgregarProducto(HttpSessionState session, Producto producto, int cantidad)
        {
            ValidarProducto(producto);
            ValidarCantidad(cantidad);

            List<DetallePedido> carrito = Obtener(session);
            DetallePedido item = carrito.Find(x => x.Producto != null && x.Producto.Id == producto.Id);
            int cantidadTotal = cantidad;

            if (item != null)
                cantidadTotal += item.Cantidad;

            ValidarStock(producto, cantidadTotal);

            if (item == null)
            {
                item = new DetallePedido();
                item.Producto = producto;
                item.Cantidad = cantidad;
                item.PrecioUnitario = producto.Precio;
                RecalcularSubtotal(item);

                carrito.Add(item);
            }
            else
            {
                item.Producto = producto;
                item.Cantidad = cantidadTotal;
                item.PrecioUnitario = producto.Precio;
                RecalcularSubtotal(item);
            }
        }

        public static void ActualizarProducto(HttpSessionState session, Producto producto, int cantidad)
        {
            ValidarProducto(producto);
            ValidarCantidad(cantidad);
            ValidarStock(producto, cantidad);

            List<DetallePedido> carrito = Obtener(session);
            DetallePedido item = carrito.Find(x => x.Producto != null && x.Producto.Id == producto.Id);

            if (item == null)
                throw new Exception("El producto no existe en el carrito.");

            item.Producto = producto;
            item.Cantidad = cantidad;
            item.PrecioUnitario = producto.Precio;
            RecalcularSubtotal(item);
        }

        public static void QuitarProducto(HttpSessionState session, int idProducto)
        {
            List<DetallePedido> carrito = Obtener(session);
            DetallePedido item = carrito.Find(x => x.Producto != null && x.Producto.Id == idProducto);

            if (item != null)
                carrito.Remove(item);
        }

        public static int CantidadTotal(HttpSessionState session)
        {
            return Obtener(session).Sum(x => x.Cantidad);
        }

        public static decimal Total(HttpSessionState session)
        {
            return Obtener(session).Sum(x => x.Subtotal);
        }

        private static void RecalcularSubtotal(DetallePedido item)
        {
            item.Subtotal = item.Cantidad * item.PrecioUnitario;
        }

        private static void ValidarProducto(Producto producto)
        {
            if (producto == null || producto.Id <= 0 || !producto.Activo)
                throw new Exception("El producto seleccionado no está disponible.");

            if (producto.Stock <= 0)
                throw new Exception("El producto seleccionado no tiene stock disponible.");
        }

        private static void ValidarCantidad(int cantidad)
        {
            if (cantidad <= 0)
                throw new Exception("La cantidad debe ser mayor a cero.");
        }

        private static void ValidarStock(Producto producto, int cantidad)
        {
            if (cantidad > producto.Stock)
                throw new Exception("La cantidad solicitada supera el stock disponible.");
        }
    }
}
