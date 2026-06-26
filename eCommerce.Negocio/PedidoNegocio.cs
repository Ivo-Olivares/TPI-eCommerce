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

        public int AgregarPedido(Pedido pedido)
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

            PedidoDatos datos = new PedidoDatos();
            return datos.AgregarPedido(pedido);
        }
    }
}
