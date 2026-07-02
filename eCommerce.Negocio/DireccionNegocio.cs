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

        public bool PerteneceAlUsuario(int idDireccion, int idUsuario)
        {
            if (idDireccion <= 0 || idUsuario <= 0)
                return false;

            DireccionDatos datos = new DireccionDatos();
            return datos.PerteneceAlUsuario(idDireccion, idUsuario);
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

        public void ModificarDireccion(Direccion direccion, int idUsuario)
        {
            if (direccion.Id <= 0)
                throw new Exception("La direccion seleccionada no es valida.");

            if (!PerteneceAlUsuario(direccion.Id, idUsuario))
                throw new Exception("No se puede modificar la direccion seleccionada.");

            ValidarDireccion(direccion);

            DireccionDatos datos = new DireccionDatos();
            datos.ModificarDireccion(direccion, idUsuario);
        }

        public void DesactivarDireccion(int idDireccion, int idUsuario)
        {
            if (idDireccion <= 0)
                throw new Exception("La direccion seleccionada no es valida.");

            if (!PerteneceAlUsuario(idDireccion, idUsuario))
                throw new Exception("No se puede eliminar la direccion seleccionada.");

            DireccionDatos datos = new DireccionDatos();
            datos.DesactivarDireccion(idDireccion, idUsuario);
        }
    }
}
