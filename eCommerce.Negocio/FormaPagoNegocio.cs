using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class FormaPagoNegocio
    {
        public List<FormaPago> Listar()
        {
            FormaPagoDatos datos = new FormaPagoDatos();
            return datos.ListarFormasPago();
        }

        public void AgregarFormaPago(FormaPago formaPago)
        {
            if (string.IsNullOrWhiteSpace(formaPago.Descripcion))
                throw new Exception("El nombre de la forma de pago no puede estar vacío.");

            FormaPagoDatos datos = new FormaPagoDatos();
            datos.AgregarFormaPago(formaPago);
        }
        public void ModificarFormaPago(FormaPago formaPago)
        {
            FormaPagoDatos datos = new FormaPagoDatos();
            datos.ModificarFormaPago(formaPago);
        }

        public void DesactivarFormaPago(FormaPago formaPago)
        {
            FormaPagoDatos datos = new FormaPagoDatos();
            datos.DesactivarFormaPago(formaPago);
        }

        public void ActivarFormaPago(FormaPago formaPago)
        {
            FormaPagoDatos datos = new FormaPagoDatos();
            datos.ActivarFormaPago(formaPago);
        }
    }
}
