using Dominio;
using System;
using System.Collections;
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


        public List<Articulo> Listar()
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
                A.Descripcion,
                A.Precio,
                A.Estado,
                M.IdMarca,
                M.Nombre AS Marca,
                C.IdCategoria,
                C.Nombre AS Categoria,
                (SELECT TOP 1 I.UrlImagen 
                 FROM IMAGENES I 
                 WHERE I.IdArticulo = A.IdArticulo) AS UrlImagen
            FROM ARTICULOS A
            INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca
            INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var art = new Articulo
                    {
                        IdArticulo = Convert.ToInt64(datos.Lector["IdArticulo"]),
                        Codigo = datos.Lector["Codigo"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : datos.Lector["Descripcion"].ToString(),
                        Precio = datos.Lector["Precio"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["Precio"]),
                        Estado = datos.Lector["Estado"] is DBNull ? true : Convert.ToBoolean(datos.Lector["Estado"]),
                        Marca = new Marca
                        {
                            IdMarca = Convert.ToInt64(datos.Lector["IdMarca"]),
                            Nombre = datos.Lector["Marca"].ToString()
                        },
                        Categoria = new Categoria
                        {
                            IdCategoria = (int)Convert.ToInt64(datos.Lector["IdCategoria"]),
                            Nombre = datos.Lector["Categoria"].ToString()
                        },
                        UrlImagen = datos.Lector["UrlImagen"] is DBNull ? null : datos.Lector["UrlImagen"].ToString()
                    };

                    lista.Add(art);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los artículos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<Articulo> Listar2()
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
                M.IdMarca,
                M.Nombre AS Marca,
                C.IdCategoria,
                C.Nombre AS Categoria,
                A.Descripcion,
                A.Precio,
                A.Estado,
                I.UrlImagen
            FROM ARTICULOS A
            INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca
            INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
            LEFT JOIN IMAGENES I ON I.IdArticulo = A.IdArticulo
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    long idArticulo = Convert.ToInt64(datos.Lector["IdArticulo"]);
                    Articulo articuloExistente = lista.FirstOrDefault(a => a.IdArticulo == idArticulo);

                    if (articuloExistente == null)
                    {
                        var articulo = new Articulo
                        {
                            IdArticulo = idArticulo,
                            Codigo = datos.Lector["Codigo"].ToString(),
                            Nombre = datos.Lector["Nombre"].ToString(),
                            Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : datos.Lector["Descripcion"].ToString(),
                            Precio = datos.Lector["Precio"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["Precio"]),
                            Estado = datos.Lector["Estado"] is DBNull ? true : Convert.ToBoolean(datos.Lector["Estado"]),
                            Marca = new Marca
                            {
                                IdMarca = Convert.ToInt64(datos.Lector["IdMarca"]),
                                Nombre = datos.Lector["Marca"].ToString()
                            },
                            Categoria = new Categoria
                            {
                                IdCategoria = (int)Convert.ToInt64(value: datos.Lector["IdCategoria"]),
                                
                                Nombre = datos.Lector["Categoria"].ToString()
                            },
                            ListaUrls = new List<string>()
                        };

                        if (!(datos.Lector["UrlImagen"] is DBNull))
                            articulo.ListaUrls.Add(datos.Lector["UrlImagen"].ToString());

                        lista.Add(articulo);
                    }
                    else
                    {
                        // Si ya existe el artículo, agregar imagen adicional
                        if (!(datos.Lector["UrlImagen"] is DBNull))
                            articuloExistente.ListaUrls.Add(datos.Lector["UrlImagen"].ToString());
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los artículos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public List<Articulo> ListarUnaSolaImagen()
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
                A.Descripcion,
                A.Precio,
                A.Estado,
                M.IdMarca,
                M.Nombre AS Marca,
                C.IdCategoria,
                C.Nombre AS Categoria,
                (SELECT TOP 1 I.UrlImagen 
                 FROM IMAGENES I 
                 WHERE I.IdArticulo = A.IdArticulo) AS UrlImagen
            FROM ARTICULOS A
            INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca
            INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo art = new Articulo
                    {
                        IdArticulo = Convert.ToInt64(datos.Lector["IdArticulo"]),
                        Codigo = datos.Lector["Codigo"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : datos.Lector["Descripcion"].ToString(),
                        Precio = datos.Lector["Precio"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["Precio"]),
                        Estado = datos.Lector["Estado"] is DBNull ? true : Convert.ToBoolean(datos.Lector["Estado"]),
                        Marca = new Marca
                        {
                            IdMarca = Convert.ToInt64(datos.Lector["IdMarca"]),
                            Nombre = datos.Lector["Marca"].ToString()
                        },
                        Categoria = new Categoria
                        {
                            IdCategoria = (int)Convert.ToInt64(datos.Lector["IdCategoria"]),
                            Nombre = datos.Lector["Categoria"].ToString()
                        },
                        UrlImagen = datos.Lector["UrlImagen"] is DBNull ? null : datos.Lector["UrlImagen"].ToString()
                    };

                    lista.Add(art);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar artículos con una sola imagen: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public void AgregarArticulo(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(@"
            INSERT INTO ARTICULOS 
                (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, Precio, Cantidad, Estado)
            VALUES 
                (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @Precio, @Cantidad, 1);

            DECLARE @IdArticulo INT = SCOPE_IDENTITY();

            IF (@UrlImagen IS NOT NULL AND @UrlImagen <> '')
            BEGIN
                INSERT INTO IMAGENES (UrlImagen, IdArticulo)
                VALUES (@UrlImagen, @IdArticulo);
            END
        ");

                datos.setearParametro("@Codigo", nuevo.Codigo);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion", nuevo.Descripcion ?? "");
                datos.setearParametro("@IdMarca", nuevo.Marca.IdMarca);
                datos.setearParametro("@IdCategoria", nuevo.Categoria.IdCategoria);
                datos.setearParametro("@Precio", nuevo.Precio);
                datos.setearParametro("@Cantidad", nuevo.Cantidad > 0 ? nuevo.Cantidad : 0);
                datos.setearParametro("@UrlImagen", string.IsNullOrWhiteSpace(nuevo.UrlImagen) ? null : nuevo.UrlImagen);

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar el artículo: " + ex.Message);
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


                datos.setearParametro("@cod", articulo.Codigo);
                datos.setearParametro("@nom", articulo.Nombre);
                datos.setearParametro("@idmarca", articulo.Marca.IdMarca);
                datos.setearParametro("@idcategoria", articulo.Categoria.IdCategoria);
                datos.setearParametro("@desc", articulo.Descripcion);
                datos.setearParametro("@precio", articulo.Precio);
                datos.setearParametro("@Id", articulo.IdArticulo);

                datos.ejecutarAccion();
                
                if (!string.IsNullOrEmpty(articulo.UrlImagen))
                {
                    datos = new AccesoDatos();

                    datos.setearConsulta(@"IF EXISTS (SELECT 1 FROM IMAGENES WHERE IdArticulo = @Id)
                    UPDATE IMAGENES SET ImagenUrl = @imagen WHERE IdArticulo = @Id
                    ELSE
                    INSERT INTO IMAGENES (ImagenUrl, IdArticulo) VALUES (@imagen, @Id) ");

                    datos.setearParametro("@imagen", articulo.UrlImagen);
                    datos.setearParametro("@Id", articulo.IdArticulo);

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

        public void ModificarArticulo(Articulo articulo, string urlVieja, string urlNueva)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                // Actualiza los datos principales del artículo
                datos.setearConsulta(@"
            UPDATE ARTICULOS 
            SET Codigo = @Codigo,
                Nombre = @Nombre,
                Descripcion = @Descripcion,
                Precio = @Precio,
                Cantidad = @Cantidad,
                IdMarca = @IdMarca,
                IdCategoria = @IdCategoria,
                Estado = @Estado
            WHERE IdArticulo = @IdArticulo
        ");

                datos.setearParametro("@Codigo", articulo.Codigo);
                datos.setearParametro("@Nombre", articulo.Nombre);
                datos.setearParametro("@Descripcion", articulo.Descripcion ?? "");
                datos.setearParametro("@Precio", articulo.Precio);
                datos.setearParametro("@Cantidad", articulo.Cantidad);
                datos.setearParametro("@IdMarca", articulo.Marca.IdMarca);
                datos.setearParametro("@IdCategoria", articulo.Categoria.IdCategoria);
                datos.setearParametro("@Estado", articulo.Estado);
                datos.setearParametro("@IdArticulo", articulo.IdArticulo);

                datos.ejecutarAccion();
                datos.cerrarConexion(); 
                datos = null;           

                //Si hay una imagen vieja y una nueva → la actualiza
                if (!string.IsNullOrEmpty(urlVieja) && !string.IsNullOrEmpty(urlNueva))
                {
                    datos = new AccesoDatos();
                    datos.setearConsulta(@"
                UPDATE IMAGENES 
                SET UrlImagen = @NuevaUrl 
                WHERE IdArticulo = @IdArticulo AND UrlImagen = @UrlVieja
            ");

                    datos.setearParametro("@NuevaUrl", urlNueva);
                    datos.setearParametro("@UrlVieja", urlVieja);
                    datos.setearParametro("@IdArticulo", articulo.IdArticulo);
                    datos.ejecutarAccion();
                }
                // Si no existía imagen vieja pero hay una nueva → inserta
                else if (string.IsNullOrEmpty(urlVieja) && !string.IsNullOrEmpty(urlNueva))
                {
                    datos = new AccesoDatos();
                    datos.setearConsulta(@"
                INSERT INTO IMAGENES (UrlImagen, IdArticulo)
                VALUES (@NuevaUrl, @IdArticulo)
            ");
                    datos.setearParametro("@NuevaUrl", urlNueva);
                    datos.setearParametro("@IdArticulo", articulo.IdArticulo);
                    datos.ejecutarAccion();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el artículo: " + ex.Message);
            }
            finally
            {
                if (datos != null)
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

        public List<Articulo> FiltrarPorPrecio(decimal precioMax)
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
                A.Descripcion,
                A.Precio,
                A.Estado,
                M.IdMarca,
                M.Nombre AS Marca,
                C.IdCategoria,
                C.Nombre AS Categoria,
                (SELECT TOP 1 I.UrlImagen 
                 FROM IMAGENES I 
                 WHERE I.IdArticulo = A.IdArticulo) AS UrlImagen
            FROM ARTICULOS A
            INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca
            INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
            WHERE A.Precio <= @PrecioMax AND A.Estado = 1
            ORDER BY A.Precio ASC
        ");

                datos.setearParametro("@PrecioMax", precioMax);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var art = new Articulo
                    {
                        IdArticulo = Convert.ToInt64(datos.Lector["IdArticulo"]),
                        Codigo = datos.Lector["Codigo"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : datos.Lector["Descripcion"].ToString(),
                        Precio = datos.Lector["Precio"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["Precio"]),
                        Estado = datos.Lector["Estado"] is DBNull ? true : Convert.ToBoolean(datos.Lector["Estado"]),
                        Marca = new Marca
                        {
                            IdMarca = Convert.ToInt64(datos.Lector["IdMarca"]),
                            Nombre = datos.Lector["Marca"].ToString()
                        },
                        Categoria = new Categoria
                        {
                            IdCategoria = (int)Convert.ToInt64(datos.Lector["IdCategoria"]),
                            Nombre = datos.Lector["Categoria"].ToString()
                        },
                        UrlImagen = datos.Lector["UrlImagen"] is DBNull ? null : datos.Lector["UrlImagen"].ToString()
                    };

                    lista.Add(art);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al filtrar artículos por precio: " + ex.Message);
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
            SELECT 
                A.IdArticulo,
                A.Codigo,
                A.Nombre,
                A.Descripcion,
                A.Precio,
                A.Estado,
                M.IdMarca,
                M.Nombre AS Marca,
                C.IdCategoria,
                C.Nombre AS Categoria,
                (SELECT TOP 1 I.UrlImagen 
                 FROM IMAGENES I 
                 WHERE I.IdArticulo = A.IdArticulo) AS UrlImagen
            FROM ARTICULOS A
            INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca
            INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
            WHERE A.Nombre LIKE '%' + @nombre + '%'
              AND A.Estado = 1
            ORDER BY A.Nombre ASC
        ");

                datos.setearParametro("@nombre", nombre);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    var art = new Articulo
                    {
                        IdArticulo = Convert.ToInt64(datos.Lector["IdArticulo"]),
                        Codigo = datos.Lector["Codigo"].ToString(),
                        Nombre = datos.Lector["Nombre"].ToString(),
                        Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : datos.Lector["Descripcion"].ToString(),
                        Precio = datos.Lector["Precio"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["Precio"]),
                        Estado = datos.Lector["Estado"] is DBNull ? true : Convert.ToBoolean(datos.Lector["Estado"]),
                        Marca = new Marca
                        {
                            IdMarca = Convert.ToInt64(datos.Lector["IdMarca"]),
                            Nombre = datos.Lector["Marca"].ToString()
                        },
                        Categoria = new Categoria
                        {
                            IdCategoria = (int)Convert.ToInt64(datos.Lector["IdCategoria"]),
                            Nombre = datos.Lector["Categoria"].ToString()
                        },
                        UrlImagen = datos.Lector["UrlImagen"] is DBNull ? null : datos.Lector["UrlImagen"].ToString()
                    };

                    lista.Add(art);
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar productos: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> ListarConImagenes()
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
                A.Descripcion,
                A.Precio,
                A.Estado,
                M.IdMarca,
                M.Nombre AS Marca,
                C.IdCategoria,
                C.Nombre AS Categoria,
                I.UrlImagen
            FROM ARTICULOS A
            INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca
            INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
            LEFT JOIN IMAGENES I ON I.IdArticulo = A.IdArticulo
            ORDER BY A.IdArticulo
        ");

                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    long idArticulo = Convert.ToInt64(datos.Lector["IdArticulo"]);
                    Articulo articuloExistente = lista.FirstOrDefault(a => a.IdArticulo == idArticulo);


                    if (articuloExistente == null)
                    {
                        var articulo = new Articulo
                        {
                            IdArticulo = idArticulo,
                            Codigo = datos.Lector["Codigo"].ToString(),
                            Nombre = datos.Lector["Nombre"].ToString(),
                            Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : datos.Lector["Descripcion"].ToString(),
                            Precio = datos.Lector["Precio"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["Precio"]),
                            Estado = datos.Lector["Estado"] is DBNull ? true : Convert.ToBoolean(datos.Lector["Estado"]),
                            Marca = new Marca
                            {
                                IdMarca = Convert.ToInt64(datos.Lector["IdMarca"]),
                                Nombre = datos.Lector["Marca"].ToString()
                            },
                            Categoria = new Categoria
                            {
                                IdCategoria = (int)Convert.ToInt64(datos.Lector["IdCategoria"]),
                                Nombre = datos.Lector["Categoria"].ToString()
                            },
                            ListaUrls = new List<string>()
                        };

                        // Si hay una imagen, la agregamos
                        if (!(datos.Lector["UrlImagen"] is DBNull))
                            articulo.ListaUrls.Add(datos.Lector["UrlImagen"].ToString());

                        lista.Add(articulo);
                    }
                    else
                    {
                        // Si ya existe el artículo, agregamos las imágenes adicionales
                        if (!(datos.Lector["UrlImagen"] is DBNull))
                            articuloExistente.ListaUrls.Add(datos.Lector["UrlImagen"].ToString());
                    }
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los artículos con múltiples imágenes: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }



        public Articulo ListarPorIDArticulo(int idarticulo)
        {
            
            AccesoDatos datos = new AccesoDatos();
            Articulo art = null;

            try
            {
                datos.setearConsulta(@"
             SELECT 
                  A.IdArticulo,
                  A.Codigo,
                  A.Cantidad,
                  A.Nombre,
                  A.Descripcion,
                  A.Precio,
                  A.Estado,
                  M.IdMarca,
                  M.Nombre AS Marca,
                  C.IdCategoria,
                  C.Nombre AS Categoria,
                  I.UrlImagen
              FROM ARTICULOS A
              INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca
              INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
              LEFT JOIN IMAGENES I ON I.IdArticulo = A.IdArticulo
              where A.IdArticulo = @idarticulo
        ");
                datos.setearParametro("@idarticulo", idarticulo);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    if (art == null)
                    {
                        art = new Articulo
                        {
                            IdArticulo = Convert.ToInt64(datos.Lector["IdArticulo"]),
                            Cantidad = Convert.ToInt32(datos.Lector["Cantidad"]),
                            Codigo = datos.Lector["Codigo"].ToString(),
                            Nombre = datos.Lector["Nombre"].ToString(),
                            Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : datos.Lector["Descripcion"].ToString(),
                            Precio = datos.Lector["Precio"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["Precio"]),
                            Estado = datos.Lector["Estado"] is DBNull ? true : Convert.ToBoolean(datos.Lector["Estado"]),
                            Marca = new Marca
                            {
                                IdMarca = Convert.ToInt64(datos.Lector["IdMarca"]),
                                Nombre = datos.Lector["Marca"].ToString()
                            },
                            Categoria = new Categoria
                            {
                                IdCategoria = (int)Convert.ToInt64(datos.Lector["IdCategoria"]),
                                Nombre = datos.Lector["Categoria"].ToString()
                            },
                            ListaUrls = new List<string>()
                        };
                    }
                         if (!(datos.Lector["UrlImagen"] is DBNull))
                art.ListaUrls.Add(datos.Lector["UrlImagen"].ToString());
                }

                return art;
               
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar el artículo: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


        public List<Articulo> ListarArticulosPorCategoria(int idcategoria)
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
                A.Descripcion,
                A.Precio,
                A.Cantidad,
                A.Estado,
                M.IdMarca,
                M.Nombre AS Marca,
                C.IdCategoria,
                C.Nombre AS Categoria,
                I.UrlImagen
            FROM ARTICULOS A
            INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca
            INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria
            LEFT JOIN IMAGENES I ON I.IdArticulo = A.IdArticulo
            WHERE A.IdCategoria = @idcategoria
        ");
                datos.setearParametro("@idCategoria", idcategoria);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    // Buscar si ya existe el artículo en la lista (por si tiene varias imágenes)
                    Articulo art = lista.FirstOrDefault(a => a.IdArticulo == Convert.ToInt32(datos.Lector["IdArticulo"]));

                    if (art == null)
                    {
                        art = new Articulo
                        {
                            IdArticulo = Convert.ToInt64(datos.Lector["IdArticulo"]),
                            Cantidad = Convert.ToInt32(datos.Lector["Cantidad"]),
                            Codigo = datos.Lector["Codigo"].ToString(),
                            Nombre = datos.Lector["Nombre"].ToString(),
                            Descripcion = datos.Lector["Descripcion"] is DBNull ? "" : datos.Lector["Descripcion"].ToString(),
                            Precio = datos.Lector["Precio"] is DBNull ? 0 : Convert.ToDecimal(datos.Lector["Precio"]),
                            Estado = datos.Lector["Estado"] is DBNull ? true : Convert.ToBoolean(datos.Lector["Estado"]),
                            Marca = new Marca
                            {
                                IdMarca = Convert.ToInt32(datos.Lector["IdMarca"]),
                                Nombre = datos.Lector["Marca"].ToString()
                            },
                            Categoria = new Categoria
                            {
                                IdCategoria = Convert.ToInt32(datos.Lector["IdCategoria"]),
                                Nombre = datos.Lector["Categoria"].ToString()
                            },
                            ListaUrls = new List<string>()
                        };

                        lista.Add(art);
                    }

                    // Agregar imagen si existe
                    if (!(datos.Lector["UrlImagen"] is DBNull))
                        art.ListaUrls.Add(datos.Lector["UrlImagen"].ToString());
                }

                return lista;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar artículos por categoría: " + ex.Message);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }


    }
}
