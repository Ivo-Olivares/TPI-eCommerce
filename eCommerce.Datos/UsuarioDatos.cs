using eCommerce.Dominio;
using System;
using System.Collections.Generic;

namespace eCommerce.Datos
{
    public class UsuarioDatos
    {
        public Usuario BuscarPorEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdUsuario, Nombre, Apellido, Dni, FechaNacimiento, Email, Telefono, Clave, Activo FROM USUARIOS WHERE Email = @Email");
                datos.setearParametros("@Email", email);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    Usuario usuario = MapearUsuario(datos);
                    usuario.Roles = ListarRoles(usuario.Id);
                    return usuario;
                }

                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool ExisteEmail(string email)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdUsuario FROM USUARIOS WHERE Email = @Email");
                datos.setearParametros("@Email", email);
                datos.ejecutarLectura();
                return datos.Lector.Read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public int AgregarUsuario(Usuario usuario)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO USUARIOS (Nombre, Apellido, Dni, FechaNacimiento, Email, Telefono, Clave, Activo) OUTPUT INSERTED.IdUsuario VALUES (@Nombre, @Apellido, @Dni, @FechaNacimiento, @Email, @Telefono, @Clave, 1)");
                datos.setearParametros("@Nombre", usuario.Nombre);
                datos.setearParametros("@Apellido", usuario.Apellido);
                datos.setearParametros("@Dni", usuario.Dni);
                datos.setearParametros("@FechaNacimiento", usuario.FechaNacimiento);
                datos.setearParametros("@Email", usuario.Email);
                datos.setearParametros("@Telefono", usuario.Telefono);
                datos.setearParametros("@Clave", usuario.Clave);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                    return (int)datos.Lector["IdUsuario"];

                throw new Exception("No se pudo registrar el usuario.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void AgregarDireccion(int idUsuario, Direccion direccion)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO DIRECCIONES (IdUsuario, Descripcion, Calle, Altura, Localidad, Provincia, Cp, Observaciones) VALUES (@IdUsuario, @Descripcion, @Calle, @Altura, @Localidad, @Provincia, @Cp, @Observaciones)");
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.setearParametros("@Descripcion", direccion.Descripcion);
                datos.setearParametros("@Calle", direccion.Calle);
                datos.setearParametros("@Altura", direccion.Altura);
                datos.setearParametros("@Localidad", direccion.Localidad);
                datos.setearParametros("@Provincia", direccion.Provincia);
                datos.setearParametros("@Cp", direccion.Cp);
                datos.setearParametros("@Observaciones", direccion.Observaciones);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AsignarRol(int idUsuario, string nombreRol)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO USUARIOS_ROLES (IdUsuario, IdRol) SELECT @IdUsuario, IdRol FROM ROLES WHERE Nombre = @NombreRol AND NOT EXISTS (SELECT 1 FROM USUARIOS_ROLES WHERE IdUsuario = @IdUsuario AND IdRol = ROLES.IdRol)");
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.setearParametros("@NombreRol", nombreRol);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Rol> ListarRoles(int idUsuario)
        {
            List<Rol> roles = new List<Rol>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT R.IdRol, R.Nombre FROM ROLES R INNER JOIN USUARIOS_ROLES UR ON R.IdRol = UR.IdRol WHERE UR.IdUsuario = @IdUsuario");
                datos.setearParametros("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Rol rol = new Rol();
                    rol.Id = (int)datos.Lector["IdRol"];
                    rol.Nombre = (string)datos.Lector["Nombre"];
                    roles.Add(rol);
                }

                return roles;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        private Usuario MapearUsuario(AccesoDatos datos)
        {
            Usuario usuario = new Usuario();
            usuario.Id = (int)datos.Lector["IdUsuario"];
            usuario.Nombre = (string)datos.Lector["Nombre"];
            usuario.Apellido = (string)datos.Lector["Apellido"];
            usuario.Dni = (string)datos.Lector["Dni"];
            usuario.FechaNacimiento = (DateTime)datos.Lector["FechaNacimiento"];
            usuario.Email = (string)datos.Lector["Email"];
            usuario.Telefono = (string)datos.Lector["Telefono"];
            usuario.Clave = (string)datos.Lector["Clave"];
            usuario.Activo = (bool)datos.Lector["Activo"];
            usuario.ListaDirecciones = new List<Direccion>();
            usuario.Roles = new List<Rol>();

            return usuario;
        }
    }
}
