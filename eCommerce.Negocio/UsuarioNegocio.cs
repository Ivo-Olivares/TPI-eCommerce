using eCommerce.Datos;
using eCommerce.Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;

namespace eCommerce.Negocio
{
    public class UsuarioNegocio
    {
        private const string RolCliente = "Cliente";
        private const string RolInvitado = "Invitado";
        private const int IteracionesHash = 10000;
        private const int TamanioSalt = 16;
        private const int TamanioHash = 32;

        public Usuario RegistrarCliente(Usuario usuario, Direccion direccion, string confirmarClave)
        {
            ValidarRegistro(usuario, direccion, confirmarClave);

            UsuarioDatos datos = new UsuarioDatos();

            if (datos.ExisteEmail(usuario.Email))
                throw new Exception("Ya existe un usuario registrado con ese email.");

            usuario.Rol = RolCliente;
            usuario.Activo = true;
            usuario.Clave = HashearClave(usuario.Clave);

            int idUsuario = datos.AgregarUsuario(usuario);
            datos.AsignarRol(idUsuario, RolCliente);
            datos.AgregarDireccion(idUsuario, direccion);

            return datos.BuscarPorEmail(usuario.Email);
        }

        public Usuario Login(string email, string clave)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new Exception("Debe ingresar el email.");

            if (string.IsNullOrWhiteSpace(clave))
                throw new Exception("Debe ingresar la clave.");

            UsuarioDatos datos = new UsuarioDatos();
            Usuario usuario = datos.BuscarPorEmail(email.Trim().ToLower());

            if (usuario == null || !usuario.Activo || !ValidarClave(clave, usuario.Clave))
                throw new Exception("Email o clave incorrectos.");

            return usuario;
        }

        public Usuario CrearInvitado()
        {
            Usuario usuario = new Usuario();
            usuario.Id = 0;
            usuario.Nombre = "Invitado";
            usuario.Apellido = "";
            usuario.Email = "";
            usuario.Rol = RolInvitado;
            usuario.Activo = true;
            usuario.Roles = new List<Rol> { new Rol { Id = 0, Nombre = RolInvitado } };
            usuario.ListaDirecciones = new List<Direccion>();

            return usuario;
        }

        public bool TieneRol(Usuario usuario, string nombreRol)
        {
            if (usuario == null || usuario.Roles == null)
                return false;

            return usuario.Roles.Any(x => string.Equals(x.Nombre, nombreRol, StringComparison.InvariantCultureIgnoreCase));
        }

        private void ValidarRegistro(Usuario usuario, Direccion direccion, string confirmarClave)
        {
            if (usuario == null)
                throw new Exception("Debe completar los datos del usuario.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new Exception("El nombre no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(usuario.Apellido))
                throw new Exception("El apellido no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(usuario.Dni))
                throw new Exception("El DNI no puede estar vacío.");

            if (usuario.FechaNacimiento == DateTime.MinValue)
                throw new Exception("Debe ingresar la fecha de nacimiento.");

            if (usuario.FechaNacimiento.Date >= DateTime.Today)
                throw new Exception("La fecha de nacimiento no es válida.");

            if (string.IsNullOrWhiteSpace(usuario.Email))
                throw new Exception("El email no puede estar vacío.");

            if (!usuario.Email.Contains("@") || !usuario.Email.Contains("."))
                throw new Exception("El email no tiene un formato válido.");

            if (string.IsNullOrWhiteSpace(usuario.Telefono))
                throw new Exception("El telefono no puede estar vacío.");

            if (string.IsNullOrWhiteSpace(usuario.Clave))
                throw new Exception("La clave no puede estar vacía.");

            if (usuario.Clave.Length < 6)
                throw new Exception("La clave debe tener al menos 6 caracteres.");

            if (usuario.Clave != confirmarClave)
                throw new Exception("Las claves ingresadas no coinciden.");

            ValidarDireccion(direccion);

            usuario.Nombre = usuario.Nombre.Trim();
            usuario.Apellido = usuario.Apellido.Trim();
            usuario.Dni = usuario.Dni.Trim();
            usuario.Email = usuario.Email.Trim().ToLower();
            usuario.Telefono = usuario.Telefono.Trim();
        }

        private void ValidarDireccion(Direccion direccion)
        {
            if (direccion == null)
                throw new Exception("Debe completar la dirección principal.");

            if (string.IsNullOrWhiteSpace(direccion.Calle))
                throw new Exception("La calle no puede estar vacía.");

            if (direccion.Altura <= 0)
                throw new Exception("El número de calle debe ser mayor a cero.");

            if (string.IsNullOrWhiteSpace(direccion.Localidad))
                throw new Exception("La localidad no puede estar vacía.");

            if (string.IsNullOrWhiteSpace(direccion.Provincia))
                throw new Exception("La provincia no puede estar vacía.");

            if (direccion.Cp <= 0)
                throw new Exception("El código postal debe ser mayor a cero.");

            direccion.Descripcion = "Principal";
            direccion.Calle = direccion.Calle.Trim();
            direccion.Localidad = direccion.Localidad.Trim();
            direccion.Provincia = direccion.Provincia.Trim();
            direccion.Observaciones = direccion.Observaciones == null ? "" : direccion.Observaciones.Trim();
        }

        private string HashearClave(string clave)
        {
            byte[] salt = new byte[TamanioSalt];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(clave, salt, IteracionesHash))
            {
                byte[] hash = pbkdf2.GetBytes(TamanioHash);
                return string.Format("PBKDF2${0}${1}${2}", IteracionesHash, Convert.ToBase64String(salt), Convert.ToBase64String(hash));
            }
        }

        private bool ValidarClave(string clave, string claveGuardada)
        {
            if (string.IsNullOrWhiteSpace(claveGuardada) || !claveGuardada.StartsWith("PBKDF2$"))
                return false;

            string[] partes = claveGuardada.Split('$');

            if (partes.Length != 4)
                return false;

            int iteraciones = int.Parse(partes[1]);
            byte[] salt = Convert.FromBase64String(partes[2]);
            byte[] hashGuardado = Convert.FromBase64String(partes[3]);

            using (Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(clave, salt, iteraciones))
            {
                byte[] hashIngresado = pbkdf2.GetBytes(hashGuardado.Length);
                return CompararBytes(hashGuardado, hashIngresado);
            }
        }

        private bool CompararBytes(byte[] primero, byte[] segundo)
        {
            if (primero.Length != segundo.Length)
                return false;

            int diferencias = 0;

            for (int i = 0; i < primero.Length; i++)
            {
                diferencias |= primero[i] ^ segundo[i];
            }

            return diferencias == 0;
        }
    }
}
