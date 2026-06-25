using System;
using System.Web.UI;
using eCommerce.Dominio;
using eCommerce.Negocio;

namespace eCommerce.Web
{
    public partial class Registro : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void btnRegistrarse_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = CrearUsuarioDesdeFormulario();
                Direccion direccion = CrearDireccionDesdeFormulario();

                UsuarioNegocio negocio = new UsuarioNegocio();
                Usuario usuarioRegistrado = negocio.RegistrarCliente(usuario, direccion, txtConfirmarClave.Text);

                AutenticacionSesion.IniciarSesion(Session, usuarioRegistrado);

                Response.Redirect("~/Default.aspx", false);
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private Usuario CrearUsuarioDesdeFormulario()
        {
            Usuario usuario = new Usuario();
            usuario.Nombre = txtNombre.Text;
            usuario.Apellido = txtApellido.Text;
            usuario.Dni = txtDni.Text;
            usuario.Email = txtEmail.Text;
            usuario.Telefono = txtTelefono.Text;
            usuario.Clave = txtClave.Text;

            DateTime fechaNacimiento;
            if (DateTime.TryParse(txtFechaNacimiento.Text, out fechaNacimiento))
                usuario.FechaNacimiento = fechaNacimiento;

            return usuario;
        }

        private Direccion CrearDireccionDesdeFormulario()
        {
            Direccion direccion = new Direccion();
            direccion.Calle = txtCalle.Text;
            direccion.Localidad = txtLocalidad.Text;
            direccion.Provincia = txtProvincia.Text;
            direccion.Observaciones = txtObservaciones.Text;
            direccion.Cp = txtCp.Text.Trim();

            int altura;
            if (int.TryParse(txtAltura.Text, out altura))
            {
                direccion.Altura = altura;
            }

            return direccion;
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text = mensaje;
            lblError.Visible = true;
        }
    }
}
