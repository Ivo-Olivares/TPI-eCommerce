using eCommerce.Dominio;
using System;
using System.Web.SessionState;

namespace eCommerce.Web
{
    public static class AutenticacionSesion
    {
        public const string RolAdmin = "Admin";
        public const string RolVendedor = "Vendedor";

        private const string ClaveUsuario = "Usuario";
        private const string ClaveEsInvitado = "EsInvitado";

        public static void IniciarSesion(HttpSessionState session, Usuario usuario)
        {
            session[ClaveUsuario] = usuario;
            session[ClaveEsInvitado] = false;
        }

        public static void IniciarSesionInvitado(HttpSessionState session, Usuario usuario)
        {
            session[ClaveUsuario] = usuario;
            session[ClaveEsInvitado] = true;
        }

        public static void CerrarSesion(HttpSessionState session)
        {
            session.Clear();
        }

        public static Usuario ObtenerUsuario(HttpSessionState session)
        {
            return session[ClaveUsuario] as Usuario;
        }

        public static bool EsInvitado(HttpSessionState session)
        {
            return session[ClaveEsInvitado] is bool && (bool)session[ClaveEsInvitado];
        }

        public static bool EsUsuarioAutenticado(HttpSessionState session)
        {
            return ObtenerUsuario(session) != null && !EsInvitado(session);
        }

        public static bool TieneAlgunRol(HttpSessionState session, params string[] roles)
        {
            Usuario usuario = ObtenerUsuario(session);

            if (usuario == null || roles == null)
                return false;

            foreach (string rol in roles)
            {
                if (TieneRol(usuario, rol))
                    return true;
            }

            return false;
        }

        public static bool PuedeAdministrarSistema(HttpSessionState session)
        {
            return EsUsuarioAutenticado(session) && TieneAlgunRol(session, RolAdmin);
        }

        public static bool PuedeGestionarProductos(HttpSessionState session)
        {
            return EsUsuarioAutenticado(session) && TieneAlgunRol(session, RolAdmin, RolVendedor);
        }

        public static bool PuedeGestionarPedidos(HttpSessionState session)
        {
            return EsUsuarioAutenticado(session) && TieneAlgunRol(session, RolAdmin, RolVendedor);
        }

        public static bool TieneRol(Usuario usuario, string nombreRol)
        {
            if (usuario == null || usuario.Roles == null)
                return false;

            foreach (Rol rol in usuario.Roles)
            {
                if (string.Equals(rol.Nombre, nombreRol, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
