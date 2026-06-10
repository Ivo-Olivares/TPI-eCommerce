using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class MarcaNegocio
    {
        public List<Marca> Listar()
        {
            MarcaDatos datos = new MarcaDatos();
            return datos.ListarMarcas();
        }

        public void AgregarMArca(Marca marca)
        {
            MarcaDatos datos = new MarcaDatos();
            datos.AgregarMarca(marca);
        }
        public void ModificarMarca(Marca marca)
        {
            MarcaDatos datos = new MarcaDatos();
            datos.ModificarMarca(marca);
        }
        public void EliminarMarca(int id)
        {
            MarcaDatos datos = new MarcaDatos();
            datos.EliminarMarca(id);
        }












        }
}
