using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using eCommerce.Dominio;

namespace eCommerce.Web
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ConfigurarNavegacion();
        }

        protected void btnCerrarSesion_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Default.aspx", false);
        }

        private void ConfigurarNavegacion()
        {
            Usuario usuario = Session["Usuario"] as Usuario;
            bool hayUsuario = usuario != null;
            bool esInvitado = Session["EsInvitado"] is bool && (bool)Session["EsInvitado"];

            liIngresar.Visible = !hayUsuario;
            liRegistrarse.Visible = !hayUsuario;
            liMiPerfil.Visible = hayUsuario && !esInvitado;
            liMisCompras.Visible = hayUsuario && !esInvitado;
            liAdmin.Visible = TieneRol(usuario, "Admin") || TieneRol(usuario, "Vendedor");
            liCerrarSesion.Visible = hayUsuario;
        }

        private bool TieneRol(Usuario usuario, string nombreRol)
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
