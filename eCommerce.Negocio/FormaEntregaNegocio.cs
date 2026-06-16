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
            if (string.IsNullOrWhiteSpace(formaEntrega.Descripcion))
                throw new Exception("El nombre de la forma de entrega no puede estar vacío.");

            FormaEntregaDatos datos = new FormaEntregaDatos();
            datos.AgregarFormaEntrega(formaEntrega);
        }
        public void ModificarFormaEntrega(FormaEntrega formaEntrega)
        {
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
    }
}
