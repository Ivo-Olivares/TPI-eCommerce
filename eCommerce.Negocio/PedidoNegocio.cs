using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;

namespace eCommerce.Negocio
{
    public class PedidoNegocio
    {
        public int ConfirmarPedido(Usuario usuario, Direccion direccion, int idFormaPago, int idFormaEntrega, List<ItemCarrito> items)
        {
            ValidarPedido(usuario, direccion, idFormaPago, idFormaEntrega, items);

            PedidoDatos datos = new PedidoDatos();
            return datos.ConfirmarPedido(usuario, direccion, idFormaPago, idFormaEntrega, items);
        }

        private void ValidarPedido(Usuario usuario, Direccion direccion, int idFormaPago, int idFormaEntrega, List<ItemCarrito> items)
        {
            if (usuario == null || usuario.Id <= 0)
                throw new Exception("Debe iniciar sesion para confirmar la compra.");

            if (items == null || items.Count == 0)
                throw new Exception("El carrito esta vacio.");

            if (idFormaPago <= 0)
                throw new Exception("Debe seleccionar una forma de pago.");

            if (idFormaEntrega <= 0)
                throw new Exception("Debe seleccionar una forma de entrega.");

            ValidarDireccion(direccion);

            foreach (ItemCarrito item in items)
            {
                if (item.Cantidad <= 0)
                    throw new Exception("Las cantidades del carrito deben ser mayores a cero.");

                if (item.Producto == null || item.IdProducto <= 0)
                    throw new Exception("Hay un producto invalido en el carrito.");
            }
        }

        private void ValidarDireccion(Direccion direccion)
        {
            if (direccion == null)
                throw new Exception("Debe completar la direccion de entrega.");

            if (string.IsNullOrWhiteSpace(direccion.Calle))
                throw new Exception("Debe ingresar la calle.");

            if (direccion.Altura <= 0)
                throw new Exception("Debe ingresar una altura valida.");

            if (string.IsNullOrWhiteSpace(direccion.Localidad))
                throw new Exception("Debe ingresar la localidad.");

            if (string.IsNullOrWhiteSpace(direccion.Provincia))
                throw new Exception("Debe ingresar la provincia.");

            if (direccion.Cp <= 0)
                throw new Exception("Debe ingresar un codigo postal valido.");

            direccion.Calle = direccion.Calle.Trim();
            direccion.Localidad = direccion.Localidad.Trim();
            direccion.Provincia = direccion.Provincia.Trim();

            if (!string.IsNullOrWhiteSpace(direccion.Observaciones))
                direccion.Observaciones = direccion.Observaciones.Trim();
        }
    }
}
