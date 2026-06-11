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
            if (string.IsNullOrWhiteSpace(categoria.Nombre))
                throw new Exception("El nombre de la Categoria no puede estar vacío.");

            CategoriaDatos datos = new CategoriaDatos();
            datos.AgregarCategoria(categoria);
        }
        public void ModificarCategoria(Categoria categoria)
        {
            CategoriaDatos datos = new CategoriaDatos();
            datos.ModificarCategoria(categoria);
        }

        public void DesactivarCategoria(Categoria categoria)
        {
            CategoriaDatos datos = new CategoriaDatos();
            datos.DesactivarCategoria(categoria);
        }

    }
}
