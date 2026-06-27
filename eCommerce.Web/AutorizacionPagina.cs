using System.Web;
using System.Web.SessionState;

namespace eCommerce.Web
{
    public static class AutorizacionPagina
    {
        public static bool RequerirAdmin(HttpSessionState session, HttpResponse response)
        {
            return RequerirAcceso(session, response, AutenticacionSesion.PuedeAdministrarSistema(session));
        }

        public static bool RequerirGestionProductos(HttpSessionState session, HttpResponse response)
        {
            return RequerirAcceso(session, response, AutenticacionSesion.PuedeGestionarProductos(session));
        }

        public static bool RequerirGestionPedidos(HttpSessionState session, HttpResponse response)
        {
            return RequerirAcceso(session, response, AutenticacionSesion.PuedeGestionarPedidos(session));
        }

        private static bool RequerirAcceso(HttpSessionState session, HttpResponse response, bool tienePermiso)
        {
            if (tienePermiso)
                return true;

            if (!AutenticacionSesion.EsUsuarioAutenticado(session))
            {
                response.Redirect("~/Login.aspx", false);
                return false;
            }

            response.Redirect("~/Default.aspx", false);
            return false;
        }
    }
}
