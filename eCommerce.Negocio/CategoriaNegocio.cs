using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> Listar()
        {
            CategoriaDatos datos = new CategoriaDatos();
            return datos.ListarCategorias();
        }

        public void AgregarCategoria(Categoria categoria)
        {
            ValidarCategoria(categoria);

            CategoriaDatos datos = new CategoriaDatos();
            datos.AgregarCategoria(categoria);
        }
        public void ModificarCategoria(Categoria categoria)
        {
            ValidarCategoria(categoria);

            CategoriaDatos datos = new CategoriaDatos();
            datos.ModificarCategoria(categoria);
        }

        public void DesactivarCategoria(Categoria categoria)
        {
            CategoriaDatos datos = new CategoriaDatos();
            datos.DesactivarCategoria(categoria);
        }

        public void ActivarCategoria(Categoria categoria)
        {
            CategoriaDatos datos = new CategoriaDatos();
            datos.ActivarCategoria(categoria);
        }

        private void ValidarCategoria(Categoria categoria)
        {
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new Exception("El nombre de la Categoria no puede estar vacío.");

            categoria.Nombre = categoria.Nombre.Trim();

            if (EsSoloNumeros(categoria.Nombre))
                throw new Exception("El nombre de la Categoria no puede contener solamente numeros.");

            Categoria categoriaExistente = Listar().Find(x => string.Equals(x.Nombre, categoria.Nombre, StringComparison.InvariantCultureIgnoreCase) && x.Id != categoria.Id);

            if (categoriaExistente != null)
                throw new Exception("Ya existe una Categoria con ese nombre.");
        }

        private bool EsSoloNumeros(string texto)
        {
            string textoSinEspacios = new string(texto.Where(x => !char.IsWhiteSpace(x)).ToArray());
            return textoSinEspacios.All(char.IsDigit);
        }


        public List<Categoria> FiltrarCategorias (string filtroNombre , string estado)
        {

            CategoriaDatos datos = new CategoriaDatos();
            return datos.filtrarCategorias(filtroNombre, estado);
        }




    }
}
