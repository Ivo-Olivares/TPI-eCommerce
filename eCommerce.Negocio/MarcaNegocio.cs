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
        public List<Marca> ListarMarcas()
        {
            MarcaDatos datos = new MarcaDatos();
            return datos.ListarMarcas();
        }

        public void AgregarMArca(Marca marca)
        {
            if(string.IsNullOrWhiteSpace(marca.Nombre))
                throw new Exception("El nombre de la marca no puede estar vacío.");

            MarcaDatos datos = new MarcaDatos();

            if (datos.ExisteMarca(marca.Nombre))
                throw new Exception("Ya existe una marca con ese nombre.");

            datos.AgregarMarca(marca);
        }

        public void ModificarMarca(Marca marca)
        {
            MarcaDatos datos = new MarcaDatos();
            datos.ModificarMarca(marca);
        }
        

        public void DesactivarMarca(Marca marca)
        {
            MarcaDatos datos = new MarcaDatos();
            datos.desactivarMarca(marca);
        }

        public void ActivarMarca(Marca marca)
        {
            MarcaDatos datos = new MarcaDatos();
            datos.ActivarMarca(marca);
        }


       











    }
}
