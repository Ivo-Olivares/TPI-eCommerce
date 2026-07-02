using eCommerce.Dominio;
using eCommerce.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace eCommerce.Web
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnIngresar_Click(object sender, EventArgs e)
        {
            try
            {
                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuario = negocio.Login(txtEmail.Text, txtClave.Text);

                AutenticacionSesion.IniciarSesion(Session, usuario);

                Response.Redirect("~/Default.aspx", false);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        protected void btnInvitado_Click(object sender, EventArgs e)
        {
            UsuarioNegocio negocio = new UsuarioNegocio();

            AutenticacionSesion.IniciarSesionInvitado(Session, negocio.CrearInvitado());

            Response.Redirect("~/Default.aspx", false);
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}