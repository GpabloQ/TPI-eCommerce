 using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {

        public List<Marca> listar(string id = "")
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();
            SqlCommand comando = new SqlCommand();
            try
            {
                string consulta = "SELECT IdMarca, Nombre FROM MARCAS WHERE Estado = 1 ";
                if (!string.IsNullOrEmpty(id))
                {
                    consulta += "AND IdMarca = @id";
                }

                datos.setearConsulta(consulta);

                if (!string.IsNullOrEmpty(id))
                {
                    datos.setearParametro("@id", id);
                }
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca aux = new Marca();
                    aux.IdMarca = (long)datos.Lector["IdMarca"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    lista.Add(aux);

                }

                return lista;
            }
            catch (Exception )
            {
                throw ;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Marca> ListarConConteo()
        {
            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT m.IdMarca, m.Nombre, COUNT(a.IdArticulo) AS CantidadProductos " +
                                    "FROM MARCAS m LEFT JOIN ARTICULOS a ON a.IdMarca = m.IdMarca " +
                                    "GROUP BY m.IdMarca, m.Nombre ORDER BY m.Nombre");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Marca m = new Marca();
                    m.IdMarca = Convert.ToInt32(datos.Lector["IdMarca"]);   // ← FIX
                    m.Nombre = datos.Lector["Nombre"].ToString();
                    m.Cantidad = Convert.ToInt32(datos.Lector["CantidadProductos"]);  // ← FIX

                    lista.Add(m);
                }

                return lista;
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

        public bool ExisteNombre(string nombre)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM MARCAS WHERE Nombre = @nombre AND Estado = 1");
                datos.setearParametro("@nombre", nombre);

                datos.ejecutarLectura();
                datos.Lector.Read();

                int count = (int)datos.Lector[0];
                return count > 0;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool TieneArticulosRelacionados(long idMarca)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE IdMarca = @id");
                datos.setearParametro("@id", idMarca);

                datos.ejecutarLectura();
                datos.Lector.Read();

                int cantidad = (int)datos.Lector[0];
                return cantidad > 0;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public void agregar(Marca nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO MARCAS (Nombre, Estado) VALUES (@nombre, @estado)");
                datos.setearParametro("@nombre", nueva.Nombre);
                datos.setearParametro("@estado", nueva.Estado);
                
                datos.ejecutarAccion();
            }
            catch (Exception )
            {

                throw ;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificar(Marca modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Nombre = @nombre, Estado = @estado WHERE IdMarca = @idmarca");
                datos.setearParametro("@idmarca", modificar.IdMarca);
                datos.setearParametro("@nombre", modificar.Nombre);
                datos.setearParametro("@estado", modificar.Estado);

                datos.ejecutarAccion();
            }
            catch (Exception )
            {

                throw ;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(long id)
        {
            if (TieneArticulosRelacionados(id))
                throw new Exception("No se pudo realizar la eliminación porque tiene artículos relacionados.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Estado = 0 WHERE IdMarca = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public bool eliminacionFisica(long id)
        {
            if (ExisteMarcaEnArticulos(id))
            {
                return false;
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM MARCAS WHERE IdMarca = @idmarca");
                datos.setearParametro("@idmarca", id);
                datos.ejecutarAccion();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }


        public void eliminacionLogica(Marca modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE MARCAS SET Estado = @estado WHERE IdMarca = @idmarca");
                datos.setearParametro("@idmarca", modificar.IdMarca);
                datos.setearParametro("@nombre", modificar.Nombre);
                datos.setearParametro("@estado", modificar.Estado);

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


        public bool ExisteMarcaEnArticulos(long idMarca)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE IdMarca = @idMarca");
                datos.setearParametro("@idMarca", idMarca);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    int cantidad = (int)datos.Lector[0];
                    return cantidad > 0; // True si hay artículos con esa marca
                }

                return false;
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

    }
}
