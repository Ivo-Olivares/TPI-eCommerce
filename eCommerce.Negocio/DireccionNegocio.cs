using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class DireccionNegocio
    {
        public List<Direccion> Listar(int idUsuario)
        {
            DireccionDatos datos = new DireccionDatos();
            return datos.ListarPorUsuario(idUsuario);
        }

        public void AgregarDireccion(Direccion direccion, int idUsuario)
        {
            ValidarDireccion(direccion);

            DireccionDatos datos = new DireccionDatos();
            datos.AgregarDireccion(direccion, idUsuario);
        }

        private void ValidarDireccion(Direccion direccion)
        {
            if (string.IsNullOrWhiteSpace(direccion.Calle))
                throw new Exception("La calle no puede estar vacía.");

            if (direccion.Altura <= 0)
                throw new Exception("La altura debe ser un número válido.");

            if (string.IsNullOrWhiteSpace(direccion.Localidad))
                throw new Exception("La localidad no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(direccion.Provincia))
                throw new Exception("La provincia no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(direccion.Cp))
                throw new Exception("El código postal no puede estar vacío.");

            direccion.Calle = direccion.Calle.Trim();
            direccion.Localidad = direccion.Localidad.Trim();
            direccion.Provincia = direccion.Provincia.Trim();
        }
    }
}
