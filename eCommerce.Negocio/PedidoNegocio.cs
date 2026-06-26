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
    }
}
