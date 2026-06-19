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
            ValidarMarca(marca);

            MarcaDatos datos = new MarcaDatos();

            if (datos.ExisteMarca(marca.Nombre))
                throw new Exception("Ya existe una marca con ese nombre.");

            datos.AgregarMarca(marca);
        }

        public void ModificarMarca(Marca marca)
        {
            ValidarMarca(marca);

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


        private void ValidarMarca(Marca marca)
        {
            if(string.IsNullOrWhiteSpace(marca.Nombre))
                throw new Exception("El nombre de la marca no puede estar vacío.");

            marca.Nombre = marca.Nombre.Trim();

            if (EsSoloNumeros(marca.Nombre))
                throw new Exception("El nombre de la marca no puede contener solamente numeros.");

            Marca marcaExistente = ListarTodasLasMarcas().Find(x => string.Equals(x.Nombre, marca.Nombre, StringComparison.InvariantCultureIgnoreCase) && x.Id != marca.Id);

            if (marcaExistente != null)
                throw new Exception("Ya existe una marca con ese nombre.");
        }

        private List<Marca> ListarTodasLasMarcas()
        {
            return ListarMarcas().Concat(ListarInactivas()).ToList();
        }

        private bool EsSoloNumeros(string texto)
        {
            string textoSinEspacios = new string(texto.Where(x => !char.IsWhiteSpace(x)).ToArray());
            return textoSinEspacios.All(char.IsDigit);
        }












    }
}
