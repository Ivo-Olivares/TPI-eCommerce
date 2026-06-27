using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class EstadoPedidoNegocio
    {
        public List<EstadoPedido> Listar()
        {
            EstadoPedidoDatos datos = new EstadoPedidoDatos();
            return datos.ListarEstadosPedido();
        }

        public void AgregarEstadoPedido(EstadoPedido estadoPedido)
        {
            ValidarEstadoPedido(estadoPedido);

            EstadoPedidoDatos datos = new EstadoPedidoDatos();
            datos.AgregarEstadoPedido(estadoPedido);
        }
        public void ModificarEstadoPedido(EstadoPedido estadoPedido)
        {
            ValidarEstadoPedido(estadoPedido);

            EstadoPedidoDatos datos = new EstadoPedidoDatos();
            datos.ModificarEstadoPedido(estadoPedido);
        }

        public void DesactivarEstadoPedido(EstadoPedido estadoPedido)
        {
            EstadoPedidoDatos datos = new EstadoPedidoDatos();
            datos.DesactivarEstadoPedido(estadoPedido);
        }

        public void ActivarEstadoPedido(EstadoPedido estadoPedido)
        {
            EstadoPedidoDatos datos = new EstadoPedidoDatos();
            datos.ActivarEstadoPedido(estadoPedido);
        }

        private void ValidarEstadoPedido(EstadoPedido estadoPedido)
        {
            if (string.IsNullOrWhiteSpace(estadoPedido.Descripcion))
                throw new Exception("El nombre del estado de pedido no puede estar vacío.");

            estadoPedido.Descripcion = estadoPedido.Descripcion.Trim();

            if (EsSoloNumeros(estadoPedido.Descripcion))
                throw new Exception("El nombre del estado de pedido no puede contener solamente numeros.");

            EstadoPedido estadoPedidoExistente = Listar().Find(x => string.Equals(x.Descripcion, estadoPedido.Descripcion, StringComparison.InvariantCultureIgnoreCase) && x.Id != estadoPedido.Id);

            if (estadoPedidoExistente != null)
                throw new Exception("Ya existe un estado de pedido con ese nombre.");
        }

        private bool EsSoloNumeros(string texto)
        {
            string textoSinEspacios = new string(texto.Where(x => !char.IsWhiteSpace(x)).ToArray());
            return textoSinEspacios.All(char.IsDigit);
        }

        public EstadoPedido ObtenerEstadoInicial()
        {
            List<EstadoPedido> lista = Listar();

            foreach (EstadoPedido estado in lista)
            {
                if (estado.Activo && string.Equals(estado.Descripcion, "Pendiente", StringComparison.InvariantCultureIgnoreCase))
                    return estado;
            }

            foreach (EstadoPedido estado in lista)
            {
                if (estado.Activo && string.Equals(estado.Descripcion, "Pendiente de pago", StringComparison.InvariantCultureIgnoreCase))
                    return estado;
            }

            throw new Exception("No se encontro un estado inicial activo para el pedido.");
        }
    }
}
