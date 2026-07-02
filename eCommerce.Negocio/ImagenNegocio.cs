using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Negocio
{
    public class ImagenNegocio
    {
        public List<Imagen> ListarPorProducto(int idProducto)
        {
            ImagenDatos datos  = new ImagenDatos(); 
            return datos.ListarPorProducto(idProducto);
        }

        public void GuardarImagenPrincipal(int idProducto, string urlImagen)
        {
            if (idProducto <= 0)
                throw new Exception("El producto no es válido.");

            if (string.IsNullOrWhiteSpace(urlImagen))
                return;

            urlImagen = urlImagen.Trim();

            if (!Uri.TryCreate(urlImagen, UriKind.Absolute, out Uri urlValida) ||
                (urlValida.Scheme != Uri.UriSchemeHttp && urlValida.Scheme != Uri.UriSchemeHttps))
            {
                throw new Exception("La URL de imagen debe comenzar con http:// o https://.");
            }

            ImagenDatos datos = new ImagenDatos();
            datos.GuardarImagenPrincipal(idProducto, urlImagen);
        }


    }
}
