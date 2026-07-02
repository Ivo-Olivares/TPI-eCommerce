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

        protected void btnGuardarDireccion_Click(object sender, EventArgs e)
        {
            try
            {
                Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

                if (usuario == null || AutenticacionSesion.EsInvitado(Session))
                {
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }

                Direccion direccion = new Direccion();
                direccion.Descripcion = txtDescripcionDireccion.Text;
                direccion.Calle = txtCalleDireccion.Text;
                direccion.Localidad = txtLocalidadDireccion.Text;
                direccion.Provincia = txtProvinciaDireccion.Text;
                direccion.Cp = txtCpDireccion.Text;
                direccion.Observaciones = txtObservacionesDireccion.Text;

                if (!int.TryParse(txtAlturaDireccion.Text, out int altura))
                    throw new Exception("La altura debe ser un numero valido.");

                direccion.Altura = altura;

                DireccionNegocio negocio = new DireccionNegocio();

                if (!string.IsNullOrWhiteSpace(hdfIdDireccion.Value))
                {
                    direccion.Id = int.Parse(hdfIdDireccion.Value);
                    negocio.ModificarDireccion(direccion, usuario.Id);
                    MostrarMensaje("La direccion se modifico correctamente.", "alert-success");
                }
                else
                {
                    negocio.AgregarDireccion(direccion, usuario.Id);
                    MostrarMensaje("La direccion se agrego correctamente.", "alert-success");
                }

                LimpiarFormularioDireccion();
                CargarDirecciones(usuario.Id);
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert-danger");
            }
        }

        protected void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            LimpiarFormularioDireccion();
        }

        protected void dgvDirecciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                Usuario usuario = AutenticacionSesion.ObtenerUsuario(Session);

                if (usuario == null || AutenticacionSesion.EsInvitado(Session))
                {
                    Response.Redirect("~/Login.aspx", false);
                    return;
                }

                int idDireccion = int.Parse(e.CommandArgument.ToString());
                DireccionNegocio negocio = new DireccionNegocio();

                if (e.CommandName == "EditarDireccion")
                {
                    Direccion direccion = negocio.Listar(usuario.Id).Find(x => x.Id == idDireccion);

                    if (direccion == null)
                        throw new Exception("No se encontro la direccion seleccionada.");

                    hdfIdDireccion.Value = direccion.Id.ToString();
                    txtDescripcionDireccion.Text = direccion.Descripcion;
                    txtCalleDireccion.Text = direccion.Calle;
                    txtAlturaDireccion.Text = direccion.Altura.ToString();
                    txtLocalidadDireccion.Text = direccion.Localidad;
                    txtProvinciaDireccion.Text = direccion.Provincia;
                    txtCpDireccion.Text = direccion.Cp;
                    txtObservacionesDireccion.Text = direccion.Observaciones;

                    lblTituloDireccion.Text = "Modificar direccion";
                    btnGuardarDireccion.Text = "Modificar direccion";
                    btnCancelarDireccion.Visible = true;
                }

                if (e.CommandName == "EliminarDireccion")
                {
                    negocio.DesactivarDireccion(idDireccion, usuario.Id);
                    LimpiarFormularioDireccion();
                    CargarDirecciones(usuario.Id);
                    MostrarMensaje("La direccion se elimino correctamente.", "alert-success");
                }
            }
            catch (Exception ex)
            {
                MostrarMensaje(ex.Message, "alert-danger");
            }
        }

        private void LimpiarFormularioDireccion()
        {
            hdfIdDireccion.Value = "";
            txtDescripcionDireccion.Text = "";
            txtCalleDireccion.Text = "";
            txtAlturaDireccion.Text = "";
            txtLocalidadDireccion.Text = "";
            txtProvinciaDireccion.Text = "";
            txtCpDireccion.Text = "";
            txtObservacionesDireccion.Text = "";
            lblTituloDireccion.Text = "Agregar direccion";
            btnGuardarDireccion.Text = "Agregar direccion";
            btnCancelarDireccion.Visible = false;
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