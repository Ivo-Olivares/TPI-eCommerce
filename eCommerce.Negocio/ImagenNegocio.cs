using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> ListarPorProducto(int idProducto)
        {
            ImagenDatos datos  = new ImagenDatos(); 
            return datos.ListarPorProducto(idProducto);
        }


    }
}
