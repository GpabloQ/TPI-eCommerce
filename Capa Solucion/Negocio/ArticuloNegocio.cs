using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Negocio
{
    public class ArticuloNegocio
    {
        

        public List<Articulo> listar()
        {

            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT \r\n    A.IdArticulo AS Id,\r\n    A.Codigo,\r\n    A.Nombre,\r\n    M.Nombre AS Marca,\r\n    C.Nombre AS Tipo,\r\n    A.Descripcion,\r\n    A.Precio,\r\n    I.UrlImagen AS Imagen,\r\n    A.IdCategoria,\r\n    A.IdMarca\r\nFROM ARTICULOS A\r\nJOIN MARCAS M ON M.IdMarca = A.IdMarca\r\nJOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria\r\nJOIN IMAGENES I ON I.IdArticulo = A.IdArticulo;");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {

                    Articulo aux = new Articulo();
                    aux.id = (int)datos.Lector["Id"];
                    aux.codigoArticulo = (string)datos.Lector["Codigo"];
                    aux.nombre = (string)datos.Lector["Nombre"];

                    aux.Marca = new Marca();
                    aux.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    aux.Marca.Nombre = (string)datos.Lector["Marca"];
                    aux.tipo = new Categoria();
                    aux.tipo.Id = (int)datos.Lector["IdCategoria"];
                    aux.tipo.Nombre = (string)datos.Lector["Tipo"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.descripcion = (string)datos.Lector["Descripcion"];
                    aux.precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["Imagen"];

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

        public List<Articulo> listar2()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT 
                A.IdArticulo,
                A.Codigo,
                A.Nombre,
                M.Nombre AS Marca,
                C.Nombre AS Tipo,
                A.Descripcion,
                A.Precio,
                I.UrlImagen AS Imagen,
                A.IdCategoria,
                A.IdMarca
            FROM ARTICULOS A
            INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca
            INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
            LEFT JOIN IMAGENES I ON I.IdArticulo = A.IdArticulo");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    int idArticulo = Convert.ToInt32(datos.Lector["IdArticulo"]);

                    // Buscar si ya existe el artículo
                    Articulo aux = lista.FirstOrDefault(a => a.id == idArticulo);

                    if (aux == null)
                    {
                        aux = new Articulo();
                        aux.id = idArticulo;
                        aux.codigoArticulo = datos.Lector["Codigo"].ToString();
                        aux.nombre = datos.Lector["Nombre"].ToString();

                        aux.Marca = new Marca();
                        aux.Marca.IdMarca = Convert.ToInt32(datos.Lector["IdMarca"]);
                        aux.Marca.Nombre = datos.Lector["Marca"].ToString();

                        aux.tipo = new Categoria();
                        aux.tipo.Id = Convert.ToInt32(datos.Lector["IdCategoria"]);
                        aux.tipo.Nombre = datos.Lector["Tipo"].ToString();

                        if (!(datos.Lector["Descripcion"] is DBNull))
                            aux.descripcion = datos.Lector["Descripcion"].ToString();

                        // 🟢 Este es otro punto común de error: conversión de precio
                        if (!(datos.Lector["Precio"] is DBNull))
                            aux.precio = Convert.ToDecimal(datos.Lector["Precio"]);

                        aux.ListaUrls = new List<string>();

                        lista.Add(aux);
                    }

                    // Agregar imagen si existe
                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.ListaUrls.Add(datos.Lector["Imagen"].ToString());
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

        public List<Articulo> listarUnaSolaImagen()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            SELECT A.Id, A.Codigo, A.Nombre, 
                   M.Descripcion AS Marca, 
                   C.Descripcion AS Tipo, 
                   A.Descripcion, 
                   A.Precio, 
                   (SELECT TOP 1 I.ImagenUrl 
                    FROM IMAGENES I 
                    WHERE I.IdArticulo = A.Id) AS Imagen, 
                   A.IdCategoria, 
                   A.IdMarca
            FROM ARTICULOS A
            JOIN MARCAS M ON M.Id = A.IdMarca
            JOIN CATEGORIAS C ON C.Id = A.IdCategoria
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.id = (int)datos.Lector["Id"];
                    aux.codigoArticulo = (string)datos.Lector["Codigo"];
                    aux.nombre = (string)datos.Lector["Nombre"];

                    aux.Marca = new Marca
                    {
                        IdMarca = (int)datos.Lector["IdMarca"],
                        Nombre = (string)datos.Lector["Marca"]
                    };

                    aux.tipo = new Categoria
                    {
                        Id = (int)datos.Lector["IdCategoria"],
                        Nombre = (string)datos.Lector["Tipo"]
                    };

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.descripcion = (string)datos.Lector["Descripcion"];

                    aux.precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["Imagen"];

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

        public void agregarArticulo(Articulo nuevo)
        {

            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("insert into ARTICULOS (Codigo,Nombre,Descripcion,IdMarca,IdCategoria,Precio) values (@Codigo,@Nombre,@Descripcion,@IdMarca,@IdCategoria,@Precio); insert into IMAGENES (ImagenUrl,IdArticulo) values (@imagen,SCOPE_IDENTITY())");
                datos.setearParametro("@Codigo", nuevo.codigoArticulo);
                datos.setearParametro("@Nombre", nuevo.nombre);
                datos.setearParametro("@IdMarca", nuevo.Marca.IdMarca);
                datos.setearParametro("@IdCategoria", nuevo.tipo.Id);
                datos.setearParametro("@Descripcion", nuevo.descripcion);
                datos.setearParametro("@Precio", nuevo.precio);
                datos.setearParametro("@imagen", nuevo.UrlImagen);

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

        public void modificarProducto(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                
                datos.setearConsulta(@"UPDATE ARTICULOS SET Codigo = @cod, Nombre = @nom, IdMarca = @idmarca, IdCategoria = @idcategoria, 
                Descripcion = @desc, Precio = @precio
                WHERE IdArticulo = @Id");


                datos.setearParametro("@cod", articulo.codigoArticulo);
                datos.setearParametro("@nom", articulo.nombre);
                datos.setearParametro("@idmarca", articulo.Marca.IdMarca);
                datos.setearParametro("@idcategoria", articulo.tipo.Id);
                datos.setearParametro("@desc", articulo.descripcion);
                datos.setearParametro("@precio", articulo.precio);
                datos.setearParametro("@Id", articulo.id);

                datos.ejecutarAccion();
                
                if (!string.IsNullOrEmpty(articulo.UrlImagen))
                {
                    datos = new AccesoDatos();

                    datos.setearConsulta(@"IF EXISTS (SELECT 1 FROM IMAGENES WHERE IdArticulo = @Id)
                    UPDATE IMAGENES SET ImagenUrl = @imagen WHERE IdArticulo = @Id
                    ELSE
                    INSERT INTO IMAGENES (ImagenUrl, IdArticulo) VALUES (@imagen, @Id) ");

                    datos.setearParametro("@imagen", articulo.UrlImagen);
                    datos.setearParametro("@Id", articulo.id);

                    datos.ejecutarAccion();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el artículo: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void modificarArticulo(Articulo articulo, string urlVieja, string urlNueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Primero actualizamos los datos del artículo
                datos.setearConsulta(@"
            UPDATE ARTICULOS 
            SET Codigo = @cod, Nombre = @nom, IdMarca = @idmarca, IdCategoria = @idcategoria, 
                Descripcion = @desc, Precio = @precio 
            WHERE IdArticulo = @Id;
            
            -- Actualizamos solo la imagen específica que coincide con la URL vieja
            UPDATE IMAGENES 
            SET ImagenUrl = @nuevaUrl 
            WHERE IdArticulo = @Id AND ImagenUrl = @urlVieja;
        ");                

                datos.setearParametro("@cod", articulo.codigoArticulo);
                datos.setearParametro("@nom", articulo.nombre);
                datos.setearParametro("@idmarca", articulo.Marca.IdMarca);
                datos.setearParametro("@idcategoria", articulo.tipo.Id);
                datos.setearParametro("@desc", articulo.descripcion);
                datos.setearParametro("@precio", articulo.precio);
                datos.setearParametro("@Id", articulo.id);

                datos.setearParametro("@urlVieja", urlVieja);
                datos.setearParametro("@nuevaUrl", urlNueva);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el artículo: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        // BALTA, FRAN:
        // como la Base de Datos tiene relacion de llave foranea en Imagenes con Articulos
        // para borrar un articulo primero se deben borrar las imagenes relacionadas a ese articulo
        // luego borrar el articulo
        // si no tuviera esa relacion de llave foranea podria borrar directamente el articulo
        // y borrar en cascada las imagenes relacionadas pero bueno, lo hice asi para no tocar la base de datos

        public void EliminarArticulo(int idArticulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // 1ro Borrar las imagenes
                datos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarAccion();

                // 2do Borrar el artículo
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE IdArticulo = @IdArticulo");
                datos.setearParametro("@IdArticulo", idArticulo);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public void EliminarImagen(int idArticulo, string urlImagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // Borra la imagen que coincida con el artículo y la URL
                datos.setearConsulta("DELETE FROM IMAGENES WHERE IdArticulo = @idArticulo AND ImagenUrl = @urlImagen");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearParametro("@urlImagen", urlImagen);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {
                throw new Exception("Error al eliminar la imagen.");
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void AgregarImagen(int idArticulo, string urlImagen)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO IMAGENES (IdArticulo, ImagenUrl) VALUES (@idArticulo, @urlImagen)");
                datos.setearParametro("@idArticulo", idArticulo);
                datos.setearParametro("@urlImagen", urlImagen);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar la imagen: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public bool Existe(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("SELECT IdArticulo FROM ARTICULOS WHERE IdArticulo = @id");
                 datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                return datos.Lector.Read();
            }
            catch (Exception)
            {
                datos.cerrarConexion();
                return false;
            }

        }

        public List<Articulo> filtrarPorPrecio(decimal precioMax)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT A.IdArticulo, A.Codigo, A.Nombre, \r\n       M.Nombre AS Marca, \r\n       C.Nombre AS Tipo, \r\n       A.Descripcion, \r\n       A.Precio, \r\n       I.UrlImagen AS Imagen, \r\n       A.IdCategoria, \r\n       A.IdMarca\r\nFROM ARTICULOS A\r\nLEFT JOIN IMAGENES I ON I.IdArticulo = A.IdArticulo\r\nJOIN MARCAS M ON M.IdMarca = A.IdMarca\r\nJOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria\r\nWHERE A.Precio <= @PrecioMax\r\n");


                datos.setearParametro("@PrecioMax", precioMax);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.id = (int)datos.Lector["Id"];
                    aux.codigoArticulo = (string)datos.Lector["Codigo"];
                    aux.nombre = (string)datos.Lector["Nombre"];

                    aux.Marca = new Marca();
                    aux.Marca.IdMarca = (int)datos.Lector["IdMarca"];
                    aux.Marca.Nombre = (string)datos.Lector["Marca"];

                    aux.tipo = new Categoria();
                    aux.tipo.Id = (int)datos.Lector["IdCategoria"];
                    aux.tipo.Nombre = (string)datos.Lector["Tipo"];

                    if (!(datos.Lector["Descripcion"] is DBNull))
                        aux.descripcion = (string)datos.Lector["Descripcion"];

                    aux.precio = (decimal)datos.Lector["Precio"];

                    if (!(datos.Lector["Imagen"] is DBNull))
                        aux.UrlImagen = (string)datos.Lector["Imagen"];

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

        public List<Articulo> BuscarProducto(string nombre)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
                    SELECT A.IdArticulo, A.Codigo, A.Nombre, 
                               M.Nombre AS Marca, 
                               C.Nombre AS Tipo, 
                               A.Descripcion, 
                               A.Precio, 
                               (SELECT TOP 1 I.UrlImagen 
                                FROM IMAGENES I 
                                WHERE I.IdArticulo = A.IdArticulo) AS Imagen, 
                               A.IdCategoria, 
                               A.IdMarca
                        FROM ARTICULOS A
                        JOIN MARCAS M ON M.IdMarca = A.IdMarca
                        JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
                    WHERE A.Nombre LIKE '%' + @nombre + '%'
                    ");

                datos.setearParametro("@nombre", nombre);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo
                    {
                        id = (int)datos.Lector["Id"],
                        codigoArticulo = (string)datos.Lector["Codigo"],
                        nombre = (string)datos.Lector["Nombre"],
                        descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"],
                        precio = (decimal)datos.Lector["Precio"],
                        UrlImagen = datos.Lector["Imagen"] is DBNull ? null : (string)datos.Lector["Imagen"],
                        Marca = new Marca
                        {
                            IdMarca = (int)datos.Lector["IdMarca"],
                            Nombre = (string)datos.Lector["Marca"]
                        },
                        tipo = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Nombre = (string)datos.Lector["Tipo"]
                        }
                    };

                    lista.Add(aux);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public Articulo BuscarPorId(int id)
        {
            Articulo articulo = null;


            AccesoDatos datos = new AccesoDatos();
            {
                datos.setearConsulta(@"
            SELECT A.IdArticulo, A.Codigo, A.Nombre, 
                   M.Nombre AS Marca, 
                   C.Nombre AS Tipo, 
                   A.Descripcion, 
                   A.Precio, 
                   (SELECT TOP 1 I.UrlImagen 
                    FROM IMAGENES I 
                    WHERE I.IdArticulo = A.IdArticulo) AS Imagen, 
                   A.IdCategoria, 
                   A.IdMarca
            FROM ARTICULOS A
            JOIN MARCAS M ON M.IdMarca = A.IdMarca
            JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
            WHERE A.Nombre LIKE '%' + @nombre + '%'

        ");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    articulo = new Articulo
                    {
                        id = (int)datos.Lector["Id"],
                        codigoArticulo = (string)datos.Lector["Codigo"],
                        nombre = (string)datos.Lector["Nombre"],
                        descripcion = datos.Lector["Descripcion"] is DBNull ? "" : (string)datos.Lector["Descripcion"],
                        precio = (decimal)datos.Lector["Precio"],
                        Marca = new Marca
                        {
                            IdMarca = (int)datos.Lector["IdMarca"],
                            Nombre = (string)datos.Lector["Marca"]
                        },
                        tipo = new Categoria
                        {
                            Id = (int)datos.Lector["IdCategoria"],
                            Nombre = (string)datos.Lector["Tipo"]
                        }
                    };
                }
            }

            if (articulo != null)
            {

                AccesoDatos datosImg = new AccesoDatos();
                {
                    datosImg.setearConsulta("SELECT ImagenUrl FROM IMAGENES WHERE IdArticulo = @id");
                    datosImg.setearParametro("@id", id);
                    datosImg.ejecutarLectura();

                    while (datosImg.Lector.Read())
                    {
                        if (!(datosImg.Lector["ImagenUrl"] is DBNull))
                            articulo.ListaUrls.Add((string)datosImg.Lector["ImagenUrl"]);
                    }
                }


                articulo.UrlImagen = articulo.ListaUrls.FirstOrDefault();
            }

            return articulo;
        }

    }
}
