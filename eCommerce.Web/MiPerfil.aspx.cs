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
    public partial class MiPerfil : System.Web.UI.Page
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
                CargarDirecciones(usuario.Id);
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

        private void CargarDirecciones(int idUsuario)
        {
            DireccionNegocio negocio = new DireccionNegocio();
            List<Direccion> direcciones = negocio.Listar(idUsuario);

            dgvDirecciones.DataSource = direcciones;
            dgvDirecciones.DataBind();
        }

        protected void btnGuardarPerfil_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuarioSesion = AutenticacionSesion.ObtenerUsuario(Session);

                if (usuarioSesion == null || AutenticacionSesion.EsInvitado(Session))
                {
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }

                Usuario usuarioActualizado = new Usuario();
                usuarioActualizado.Id = usuarioSesion.Id;
                usuarioActualizado.Nombre = txtNombre.Text;
                usuarioActualizado.Apellido = txtApellido.Text;
                usuarioActualizado.Telefono = txtTelefono.Text;

                UsuarioNegocio negocio = new UsuarioNegocio();
                negocio.ActualizarDatosBasicos(usuarioActualizado);

                usuarioSesion.Nombre = usuarioActualizado.Nombre;
                usuarioSesion.Apellido = usuarioActualizado.Apellido;
                usuarioSesion.Telefono = usuarioActualizado.Telefono;
                AutenticacionSesion.IniciarSesion(Session, usuarioSesion);

                CargarDatosPersonales(usuarioSesion);
                MostrarMensaje("Los datos del perfil se actualizaron correctamente.", "alert-success");
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert-danger");
            }
        }

        private void MostrarMensaje(string mensaje, string cssClass)
        {
            lblMensaje.Text = mensaje;

            if (cssClass == "alert-success")
                lblMensaje.CssClass = "app-alert app-alert-success d-block mb-4";
            else
                lblMensaje.CssClass = "app-alert app-alert-danger d-block mb-4";

            lblMensaje.Visible = true;
        }
    }
}