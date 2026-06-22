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
            AutenticacionSesion.CerrarSesion(Session);
            Response.Redirect("~/Default.aspx", false);
        }

        private void ConfigurarNavegacion()
        {
            Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);
            bool hayUsuario = usuario != null;
            bool esInvitado = AutenticacionSesion.EsInvitado(Session);

            liIngresar.Visible = !hayUsuario;
            liRegistrarse.Visible = !hayUsuario;
            liMiPerfil.Visible = hayUsuario && !esInvitado;
            liMisCompras.Visible = hayUsuario && !esInvitado;
            liAdmin.Visible = AutenticacionSesion.TieneRol(usuario, AutenticacionSesion.RolAdmin) || AutenticacionSesion.TieneRol(usuario, AutenticacionSesion.RolVendedor);
            liCerrarSesion.Visible = hayUsuario;
        }
    }
}
