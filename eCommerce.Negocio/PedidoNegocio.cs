using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace eCommerce.Negocio
{
    public class PedidoNegocio
    {
        public List<Pedido> ListarPorUsuario(int idUsuario)
        {
            PedidoDatos datos = new PedidoDatos();
            return datos.ListarPorUsuario(idUsuario);
        }

        public List<Pedido> ListarTodos()
        {
            PedidoDatos datos = new PedidoDatos();
            return datos.ListarTodos();
        }

        public void ActualizarEstado(int idPedido, int idEstadoPedido)
        {
            if (idPedido <= 0)
                throw new Exception("Debe seleccionar un pedido valido.");

            if (idEstadoPedido <= 0)
                throw new Exception("Debe seleccionar un estado valido.");

            EstadoPedidoNegocio estadoNegocio = new EstadoPedidoNegocio();
            EstadoPedido estado = estadoNegocio.Listar().Find(x => x.Id == idEstadoPedido && x.Activo);

            if (estado == null)
                throw new Exception("Debe seleccionar un estado activo.");

            PedidoDatos datos = new PedidoDatos();
            int filasAfectadas = datos.ActualizarEstado(idPedido, idEstadoPedido);

            if (filasAfectadas != 1)
                throw new Exception("No se encontro el pedido seleccionado.");
        }

        public void ActualizarObservacionesInternas(int idPedido, string observaciones)
        {
            if (idPedido <= 0)
                throw new Exception("Debe seleccionar un pedido valido.");

            observaciones = (observaciones ?? "").Trim();

            if (observaciones.Length > 500)
                throw new Exception("Las observaciones internas no pueden superar los 500 caracteres.");

            PedidoDatos datos = new PedidoDatos();
            int filasAfectadas = datos.ActualizarObservacionesInternas(idPedido, observaciones);

            if (filasAfectadas != 1)
                throw new Exception("No se encontro el pedido seleccionado.");
        }

        public int ConfirmarCompra(Pedido pedido, List<DetallePedido> detalles)
        {
            ValidarPedido(pedido);
            ValidarDetalles(detalles);

            pedido.Total = detalles.Sum(x => x.Subtotal);

            PedidoDatos datos = new PedidoDatos();
            return datos.ConfirmarCompra(pedido, detalles);
        }

        private void ValidarPedido(Pedido pedido)
        {
            if (pedido == null)
                throw new Exception("No se pudo generar el pedido.");

            if (pedido.Usuario == null || pedido.Usuario.Id <= 0)
                throw new Exception("El pedido debe tener un usuario valido.");

            if (pedido.Direccion == null || pedido.Direccion.Id <= 0)
                throw new Exception("Debe seleccionar una direccion.");

            if (pedido.FormaPago == null || pedido.FormaPago.Id <= 0)
                throw new Exception("Debe seleccionar una forma de pago.");

            if (pedido.FormaEntrega == null || pedido.FormaEntrega.Id <= 0)
                throw new Exception("Debe seleccionar una forma de entrega.");

            if (pedido.EstadoPedido == null || pedido.EstadoPedido.Id <= 0)
                throw new Exception("El pedido debe tener un estado inicial.");

            if (pedido.Total < 0)
                throw new Exception("El total del pedido no puede ser negativo.");
        }

        private void ValidarDetalles(List<DetallePedido> detalles)
        {
            if (detalles == null || detalles.Count == 0)
                throw new Exception("El pedido debe tener al menos un producto.");

            foreach (DetallePedido detalle in detalles)
            {
                if (detalle.Producto == null || detalle.Producto.Id <= 0)
                    throw new Exception("Uno de los productos no es valido.");

                if (detalle.Cantidad <= 0)
                    throw new Exception("La cantidad debe ser mayor a cero.");

                if (detalle.PrecioUnitario <= 0)
                    throw new Exception("El precio unitario debe ser mayor a cero.");

                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;
            }
        }
    }
}
