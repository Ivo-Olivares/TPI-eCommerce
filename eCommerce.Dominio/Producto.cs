using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Dominio
{
    public class Producto
    {
        public int Id { get; set; }
        public string Sku { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Stock { get; set; }
        public bool Activo { get; set; }
        public Categoria Categoria { get; set; }
        public Marca Marca { get; set; }
        public List<Imagen> ListaImagenes { get; set; }
    }
}
