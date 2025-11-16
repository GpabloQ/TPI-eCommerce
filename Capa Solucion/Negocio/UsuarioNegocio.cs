using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class UsuarioNegocio
    {
        public void AgregarUsuario(Usuario nuevo)

        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO USUARIOS (Nombre, Apellido, Mail, Contrasenia, Telefono, DNI, TipoUsuario, Estado, FechaNacimiento) " +
                    "VALUES (@nombre, @apellido, @mail, @contrasenia, @telefono, @dni, @tipoUsuario, @estado, @fechaNacimiento)");
                datos.setearParametro("@nombre", nuevo.Nombre);
                datos.setearParametro("@apellido", nuevo.Apellido);
                datos.setearParametro("@mail", nuevo.Mail);
                datos.setearParametro("@contrasenia", nuevo.Contrasenia);
                datos.setearParametro("@telefono", nuevo.Telefono);
                datos.setearParametro("@dni", nuevo.DNI);
                datos.setearParametro("@tipoUsuario", nuevo.TipoUsuario);
                datos.setearParametro("@estado", nuevo.Estado);
                datos.setearParametro("@fechaNacimiento", nuevo.FechaNacimiento);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        //Esto nos sirve para chekear que el usuarios nuevo no se alla registrado con el mismo email
        public bool ExisteMail(string mail)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT Mail FROM USUARIOS WHERE Mail = @mail");
                datos.setearParametro("@mail", mail);
                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        public List<Usuario> listar()
        {
            List<Usuario> lista = new List<Usuario>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                "SELECT U.IdUsuario, U.Nombre, U.Apellido, U.Mail, U.Telefono, U.DNI, " +
                "U.TipoUsuario, U.Estado, U.FechaNacimiento, " +
                "(U.Nombre + ' ' + U.Apellido) AS NombreCompleto, " +
                "T.Tipo AS TipoUsuarioNombre " +
                "FROM USUARIOS U " +
                "INNER JOIN TIPOUSUARIOS T ON T.IdTipoUsuario = U.TipoUsuario");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Usuario aux = new Usuario();

                    // BIGINT → long
                    aux.IdUsuario = Convert.ToInt64(datos.Lector["IdUsuario"]);

                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Apellido = datos.Lector["Apellido"].ToString();
                    aux.Mail = datos.Lector["Mail"].ToString();

                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value
                        ? datos.Lector["Telefono"].ToString()
                        : "";

                    aux.DNI = datos.Lector["DNI"] != DBNull.Value
                        ? datos.Lector["DNI"].ToString()
                        : "";

                    // BIGINT → int (siempre convertir)
                    aux.TipoUsuario = Convert.ToInt32(datos.Lector["TipoUsuario"]);

                    // BIT → bool
                    aux.Estado = datos.Lector["Estado"] != DBNull.Value
                        ? Convert.ToBoolean(datos.Lector["Estado"])
                        : false;

                    aux.FechaNacimiento = datos.Lector["FechaNacimiento"] != DBNull.Value
                        ? Convert.ToDateTime(datos.Lector["FechaNacimiento"])
                        : DateTime.MinValue;

                    // Nuevos campos
                    aux.NombreCompleto = datos.Lector["NombreCompleto"].ToString();
                    aux.TipoUsuarioNombre = datos.Lector["TipoUsuarioNombre"].ToString();

                    aux.Contrasenia = "********"; // No mostrar la real

                    lista.Add(aux);
                }

                return lista;
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

        public void EliminarUsuario(long id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM USUARIOS WHERE IdUsuario = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
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

        public Usuario buscarPorId(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            Usuario aux = new Usuario();

            try
            {
                datos.setearConsulta("SELECT * FROM USUARIOS WHERE IdUsuario = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    // BIGINT → usar Int64
                    aux.IdUsuario = Convert.ToInt64(datos.Lector["IdUsuario"]);

                    aux.Nombre = datos.Lector["Nombre"].ToString();
                    aux.Apellido = datos.Lector["Apellido"].ToString();
                    aux.Mail = datos.Lector["Mail"].ToString();

                    aux.Telefono = datos.Lector["Telefono"] != DBNull.Value
                        ? datos.Lector["Telefono"].ToString()
                        : "";

                    aux.DNI = datos.Lector["DNI"] != DBNull.Value
                        ? datos.Lector["DNI"].ToString()
                        : "";

                    aux.FechaNacimiento = datos.Lector["FechaNacimiento"] != DBNull.Value
                        ? Convert.ToDateTime(datos.Lector["FechaNacimiento"])
                        : DateTime.MinValue;

                    // TipoUsuario también puede ser BIGINT
                    aux.TipoUsuario = Convert.ToInt32(datos.Lector["TipoUsuario"]);

                    // Estado BIT → convertir correctamente
                    aux.Estado = Convert.ToBoolean(datos.Lector["Estado"]);
                }

                return aux;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void ModificarUsuario(Usuario u)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "UPDATE USUARIOS SET Nombre=@n, Apellido=@a, Mail=@m, Telefono=@t, DNI=@dni, " +
                    "FechaNacimiento=@fn, TipoUsuario=@tu WHERE IdUsuario=@id");

                datos.setearParametro("@n", u.Nombre);
                datos.setearParametro("@a", u.Apellido);
                datos.setearParametro("@m", u.Mail);
                datos.setearParametro("@t", u.Telefono);
                datos.setearParametro("@dni", u.DNI);
                datos.setearParametro("@fn", u.FechaNacimiento);
                datos.setearParametro("@tu", u.TipoUsuario);
                datos.setearParametro("@id", u.IdUsuario);

                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}
