using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class FormaEntregaNegocio
    {
        public List<FormaEntrega> Listar()
        {
            FormaEntregaDatos datos = new FormaEntregaDatos();
            return datos.ListarFormasEntrega();
        }

        public void AgregarFormaEntrega(FormaEntrega formaEntrega)
        {
            ValidarFormaEntrega(formaEntrega);

            FormaEntregaDatos datos = new FormaEntregaDatos();
            datos.AgregarFormaEntrega(formaEntrega);
        }
        public void ModificarFormaEntrega(FormaEntrega formaEntrega)
        {
            ValidarFormaEntrega(formaEntrega);

            FormaEntregaDatos datos = new FormaEntregaDatos();
            datos.ModificarFormaEntrega(formaEntrega);
        }

        public void DesactivarFormaEntrega(FormaEntrega formaEntrega)
        {
            FormaEntregaDatos datos = new FormaEntregaDatos();
            datos.DesactivarFormaEntrega(formaEntrega);
        }

        public void ActivarFormaEntrega(FormaEntrega formaEntrega)
        {
            FormaEntregaDatos datos = new FormaEntregaDatos();
            datos.ActivarFormaEntrega(formaEntrega);
        }

        private void ValidarFormaEntrega(FormaEntrega formaEntrega)
        {
            if (string.IsNullOrWhiteSpace(formaEntrega.Descripcion))
                throw new Exception("El nombre de la forma de entrega no puede estar vacío.");

            formaEntrega.Descripcion = formaEntrega.Descripcion.Trim();

            if (EsSoloNumeros(formaEntrega.Descripcion))
                throw new Exception("El nombre de la forma de entrega no puede contener solamente numeros.");

            FormaEntrega formaEntregaExistente = Listar().Find(x => string.Equals(x.Descripcion, formaEntrega.Descripcion, StringComparison.InvariantCultureIgnoreCase) && x.Id != formaEntrega.Id);

            if (formaEntregaExistente != null)
                throw new Exception("Ya existe una forma de entrega con ese nombre.");
        }

        private bool EsSoloNumeros(string texto)
        {
            string textoSinEspacios = new string(texto.Where(x => !char.IsWhiteSpace(x)).ToArray());
            return textoSinEspacios.All(char.IsDigit);
        }


        public List<FormaEntrega> filtrarentrega (string filtroDescripcion, string estado)
        {
            FormaEntregaDatos datos = new FormaEntregaDatos();
            return datos.FiltrarEntregas(filtroDescripcion, estado);
        }
    }
}
