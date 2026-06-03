using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class FormaPagoNegocio
    {
        public List<FormaPago> Listar()
        {
            FormaPagoDatos datos = new FormaPagoDatos();
            return datos.ListarFormasPago();
        }
    }
}
