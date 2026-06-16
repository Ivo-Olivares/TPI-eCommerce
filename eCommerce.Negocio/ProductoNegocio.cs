using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class ProductoNegocio
    {
        public List<Producto> Listar()
        {
            ProductoDatos datos = new ProductoDatos();
            return datos.ListarProductos();
        }

        public void AgregarProducto(Producto producto)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.AgregarProducto(producto);
        }

        public void ModificarProducto(Producto producto)
        {
            ProductoDatos datos = new ProductoDatos();
            datos.ModificarProducto(producto);
        }
    }
}
