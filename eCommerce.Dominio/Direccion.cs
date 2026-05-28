using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Dominio
{
    public class Direccion
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Calle { get; set; }
        public int Altura { get; set; }
        public string Localidad { get; set; }
        public string Provincia { get; set; }
        public int Cp { get; set; }
        public string Observaciones { get; set; }
    }
}
