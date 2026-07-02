using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Dominio
{
    public class Pedido
    {
        public Pedido()
        {
            Usuario = new Usuario();
            FormaPago = new FormaPago();
            FormaEntrega = new FormaEntrega();
            EstadoPedido = new EstadoPedido();
            Direccion = new Direccion();
            ListaDetalles = new List<DetallePedido>();
        }
        public int Id { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public decimal Total { get; set; }
        public Usuario Usuario { get; set; }
        public FormaPago FormaPago { get; set; }
        public FormaEntrega FormaEntrega { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
        public Direccion Direccion { get; set; }
        public string ObservacionesInternas { get; set; }
        public List<DetallePedido> ListaDetalles { get; set; }
    }
}
