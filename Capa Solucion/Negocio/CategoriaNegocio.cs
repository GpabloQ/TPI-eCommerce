using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {


        public List<Categoria> listar(string id = "")
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();
            SqlCommand comando = new SqlCommand();
            try
            {
                string consulta = "SELECT IdCategoria, Nombre FROM CATEGORIAS WHERE Estado = 1 ";
                if (!string.IsNullOrEmpty(id))
                {
                    consulta += "AND IdCategoria = @id";
                }

                datos.setearConsulta(consulta);

                if (!string.IsNullOrEmpty(id))
                {
                    datos.setearParametro("@id", id);
                }
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    aux.IdCategoria = (int)(long)datos.Lector["IdCategoria"];
                    aux.Nombre = (string)datos.Lector["Nombre"];

                    lista.Add(aux);

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
                datos.setearConsulta("SELECT COUNT(*) FROM CATEGORIAS WHERE Nombre = @nombre AND Estado = 1");
                datos.setearParametro("@nombre", nombre);

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

        public void agregar(Categoria nueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("INSERT INTO CATEGORIAS (Nombre, Estado) VALUES (@nombre, @estado)");
                datos.setearParametro("@nombre", nueva.Nombre);
                datos.setearParametro("@estado", nueva.Estado);

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

        public bool TieneArticulosRelacionados(long idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE IdCategoria = @id");
                datos.setearParametro("@id", idCategoria);

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


        public void modificar(Categoria modificar)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("UPDATE CATEGORIAS SET Nombre = @nombre, Estado = @estado WHERE IdCategoria = @idcategoria");
                datos.setearParametro("@idcategoria", modificar.IdCategoria);
                datos.setearParametro("@nombre", modificar.Nombre);
                datos.setearParametro("@estado", modificar.Estado);

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

        public void Eliminar(long id)
        {
            if (TieneArticulosRelacionados(id))
                throw new Exception("No se pudo realizar la eliminación porque tiene artículos relacionados.");

            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("UPDATE CATEGORIAS SET Estado = 0 WHERE IdCategoria = @id");
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

        public bool eliminar(int id)
        {
            if (ExisteCategoriaEnArticulos(id))
            {
                return false;
            }

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM CATEGORIAS WHERE IdCategoria = @idCategoria");
                datos.setearParametro("@idCategoria", id);

                datos.ejecutarAccion();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void eliminacionLogica(Categoria modificar)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE CATEGORIA SET Estado = @estado WHERE IdCategoria = @idcategoria");
                datos.setearParametro("@idcategoria", modificar.IdCategoria);
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


        public bool ExisteCategoriaEnArticulos(int idCategoria)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT COUNT(*) FROM ARTICULOS WHERE IdCategoria = @idCategoria");
                datos.setearParametro("@idCategoria", idCategoria);
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
