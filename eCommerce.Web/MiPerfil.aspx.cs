using System;
using System.Web.UI;
using eCommerce.Dominio;

namespace eCommerce.Web
{
    public partial class MiPerfil : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

            if (usuario == null || AutenticacionSesion.EsInvitado(Session))
            {
                Response.Redirect("~/Login.aspx", false);
                return;
            }

            if (!IsPostBack)
            {
                CargarDatosPersonales(usuario);
            }
        }

        private void CargarDatosPersonales(Usuario usuario)
        {
            txtNombre.Text = usuario.Nombre;
            txtApellido.Text = usuario.Apellido;
            txtEmail.Text = usuario.Email;
            txtTelefono.Text = usuario.Telefono;
            txtDni.Text = usuario.Dni;
            txtFechaNacimiento.Text = usuario.FechaNacimiento.ToString("dd/MM/yyyy");
        }
    }
}
