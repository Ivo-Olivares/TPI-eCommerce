using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Dominio
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }
        public Usuario Usuario { get; set; }
        public FormaPago FormaPago { get; set; }
        public FormaEntrega FormaEntrega { get; set; }
        public EstadoPedido EstadoPedido { get; set; }
        public List<DetallePedido> ListaDetalles { get; set; }
    }
}
