using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class CategoriaNegocio
    {
        public List<Categoria> listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdCategoria, Nombre FROM CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();
                    object valor = datos.Lector["IdCategoria"];

                    if (valor != DBNull.Value)
                        aux.IdCategoria= Convert.ToInt32(valor);
                    else
                        aux.IdCategoria = 0;

                    aux.Nombre = datos.Lector["Nombre"].ToString();

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




        public List<Categoria> Listar()
        {
            List<Categoria> lista = new List<Categoria>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT IdCategoria, Nombre, Estado FROM CATEGORIAS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Categoria aux = new Categoria();

                    aux.IdCategoria = datos.Lector["IdCategoria"] != DBNull.Value
                        ? Convert.ToInt32(datos.Lector["IdCategoria"])
                        : 0;

                    aux.Nombre = datos.Lector["Nombre"] != DBNull.Value
                        ? datos.Lector["Nombre"].ToString()
                        : "";

                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar categorías: " + ex.Message);
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

                datos.setearConsulta("UPDATE CATEGORIAS SET Nombre = @Nombre WHERE Id = @Id");
                datos.setearParametro("@Nombre", modificar.Nombre);
                datos.setearParametro("@Id", modificar.IdCategoria);
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
                datos.setearParametro("@idCategoria",id);

                datos.ejecutarAccion();
                return true;
            }
            catch (Exception)
            {

                throw;
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
