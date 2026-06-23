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
            ValidarFormaPago(formaPago);

            FormaPagoDatos datos = new FormaPagoDatos();
            datos.AgregarFormaPago(formaPago);
        }
        public void ModificarFormaPago(FormaPago formaPago)
        {
            ValidarFormaPago(formaPago);

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

        private void ValidarFormaPago(FormaPago formaPago)
        {
            if (string.IsNullOrWhiteSpace(formaPago.Descripcion))
                throw new Exception("El nombre de la forma de pago no puede estar vacío.");

            formaPago.Descripcion = formaPago.Descripcion.Trim();

            if (EsSoloNumeros(formaPago.Descripcion))
                throw new Exception("El nombre de la forma de pago no puede contener solamente numeros.");

            FormaPago formaPagoExistente = Listar().Find(x => string.Equals(x.Descripcion, formaPago.Descripcion, StringComparison.InvariantCultureIgnoreCase) && x.Id != formaPago.Id);

            if (formaPagoExistente != null)
                throw new Exception("Ya existe una forma de pago con ese nombre.");
        }

        private bool EsSoloNumeros(string texto)
        {
            string textoSinEspacios = new string(texto.Where(x => !char.IsWhiteSpace(x)).ToArray());
            return textoSinEspacios.All(char.IsDigit);
        }



        public List<FormaPago> filtrarFormaPago(string filtroDescripcion,string estado)
        {
            FormaPagoDatos datos = new FormaPagoDatos();
            return datos.FiltrarFormasPagos ( filtroDescripcion , estado);

        }
    }
}
