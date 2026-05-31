using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            CategoriaDatos datos = new CategoriaDatos();
            return datos.ListarCategorias();
        }
    }
}
