using System;
using System.Web.UI;
using eCommerce.Dominio;
using eCommerce.Negocio;

namespace eCommerce.Web
{
    public partial class Login : Page
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

                Session["Usuario"] = usuario;
                Session["EsInvitado"] = false;

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

            Session["Usuario"] = negocio.CrearInvitado();
            Session["EsInvitado"] = true;

            Response.Redirect("~/Default.aspx", false);
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}
