using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class Admin : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AutorizacionPagina.RequerirGestionPedidos(Session, Response))
                return;

            ConfigurarPanelPorRol();
        }

        private void ConfigurarPanelPorRol()
        {
            bool esAdmin = AutenticacionSesion.PuedeAdministrarSistema(Session);

            pnlCategorias.Visible = esAdmin;
            pnlMarcas.Visible = esAdmin;
            pnlFormasPago.Visible = esAdmin;
            pnlFormasEntrega.Visible = esAdmin;
            pnlEstadosPedido.Visible = esAdmin;

            pnlProductos.Visible = AutenticacionSesion.PuedeGestionarProductos(Session);
            pnlPedidos.Visible = AutenticacionSesion.PuedeGestionarPedidos(Session);
        }
    }
}