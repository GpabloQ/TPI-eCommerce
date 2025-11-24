using Dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class OrdenNegocio
    {

        // Crear una orden (carrito finalizado)
        public void CrearOrden(long idUsuario, List<ElementoCarrito> items)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                // 1. Insertar el carrito
                datos.setearConsulta(
                    "INSERT INTO CARRITO (IdUsuario, FechaCreacion, EstadoCarrito, Estado) " +
                    "OUTPUT INSERTED.IdCarrito VALUES (@IdUsuario, @FechaCreacion, @EstadoCarrito, @Estado)");

                datos.setearParametro("@IdUsuario", idUsuario);
                datos.setearParametro("@FechaCreacion", DateTime.Now);
                datos.setearParametro("@EstadoCarrito", "Finalizado");
                datos.setearParametro("@Estado", "Activo");

                datos.ejecutarLectura();
                long idCarrito = 0;
                if (datos.Lector.Read())
                    idCarrito = (long)datos.Lector[0];
                datos.cerrarConexion();

                // 2. Insertar los elementos y descontar stock
                foreach (var item in items)
                {
                    // Insertar elemento
                    datos.setearConsulta(
                        "INSERT INTO ELEMENTOCARRITO (IdCarrito, IdArticulo, Cantidad, PrecioUnitario) " +
                        "VALUES (@IdCarrito, @IdArticulo, @Cantidad, @PrecioUnitario)");

                    datos.setearParametro("@IdCarrito", idCarrito);
                    datos.setearParametro("@IdArticulo", item.IdArticulo);
                    datos.setearParametro("@Cantidad", item.Cantidad);
                    datos.setearParametro("@PrecioUnitario", item.PrecioUnitario);

                    datos.ejecutarAccion();
                    datos.cerrarConexion();

                    // Descontar stock del artículo
                    datos.setearConsulta(
                        "UPDATE ARTICULOS SET Cantidad = Cantidad - @Cantidad WHERE IdArticulo = @IdArticulo");

                    datos.setearParametro("@Cantidad", item.Cantidad);
                    datos.setearParametro("@IdArticulo", item.IdArticulo);

                    datos.ejecutarAccion();
                    datos.cerrarConexion();
                }
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


        // Listar órdenes finalizadas por usuario
        public List<Carrito> ListarOrdenesPorUsuario(long idUsuario)
        {
            List<Carrito> lista = new List<Carrito>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta(
                    "SELECT IdCarrito, IdUsuario, FechaCreacion, EstadoCarrito, Estado " +
                    "FROM CARRITO " +
                    "WHERE IdUsuario = @IdUsuario " +
                    "AND EstadoCarrito COLLATE Latin1_General_CI_AI = 'Finalizado'");

                datos.setearParametro("@IdUsuario", idUsuario);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Carrito carrito = new Carrito
                    {
                        IdCarrito = (long)datos.Lector["IdCarrito"],
                        IdUsuario = (long)datos.Lector["IdUsuario"],
                        FechaCreacion = (DateTime)datos.Lector["FechaCreacion"],
                        EstadoCarrito = datos.Lector["EstadoCarrito"].ToString(),
                        Estado = datos.Lector["Estado"].ToString()
                    };

                    lista.Add(carrito);
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

        public Carrito BuscarCarritoPorId(long idCarrito)
        {
            Carrito carrito = null;
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT IdCarrito, IdUsuario, FechaCreacion, EstadoCarrito, Estado " +
                    "FROM CARRITO WHERE IdCarrito = @idCarrito");

                datos.setearParametro("@idCarrito", idCarrito);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    carrito = new Carrito
                    {
                        IdCarrito = (long)datos.Lector["IdCarrito"],
                        IdUsuario = (long)datos.Lector["IdUsuario"],
                        FechaCreacion = (DateTime)datos.Lector["FechaCreacion"],
                        EstadoCarrito = datos.Lector["EstadoCarrito"].ToString(),
                        Estado = datos.Lector["Estado"].ToString()
                    };
                }

                return carrito;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<ElementoCarrito> ListarElementosPorCarrito(long idCarrito)
        {
            List<ElementoCarrito> lista = new List<ElementoCarrito>();
            AccesoDatos datos = new AccesoDatos();
            AccesoDatos datosImg = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT EC.IdArticulo, EC.Cantidad, EC.PrecioUnitario, " +
                    "A.Nombre AS NombreArticulo, A.Descripcion AS DescripcionArticulo, " +
                    "M.Nombre AS Marca, C.Nombre AS Categoria " +
                    "FROM ELEMENTOCARRITO EC " +
                    "INNER JOIN ARTICULOS A ON A.IdArticulo = EC.IdArticulo " +
                    "INNER JOIN MARCAS M ON M.IdMarca = A.IdMarca " +
                    "INNER JOIN CATEGORIAS C ON C.IdCategoria = A.IdCategoria " +
                    "WHERE EC.IdCarrito = @IdCarrito");

                datos.setearParametro("@IdCarrito", idCarrito);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ElementoCarrito item = new ElementoCarrito
                    {
                        IdArticulo = (long)datos.Lector["IdArticulo"],
                        Cantidad = (long)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"],

                        Nombre = datos.Lector["NombreArticulo"].ToString(),
                        Descripcion = datos.Lector["DescripcionArticulo"].ToString(),
                        Marca = datos.Lector["Marca"].ToString(),
                        Categoria = datos.Lector["Categoria"].ToString(),
                    };

                    // --- SEGUNDA QUERY: TODAS LAS IMAGENES DEL ARTICULO ---
                    datosImg.setearConsulta("SELECT UrlImagen FROM IMAGENES WHERE IdArticulo = @id");
                    datosImg.setearParametro("@id", item.IdArticulo);
                    datosImg.ejecutarLectura();

                    while (datosImg.Lector.Read())
                    {
                        item.Imagenes.Add(datosImg.Lector["UrlImagen"].ToString());
                    }

                    // Si no tiene imágenes → imagen por defecto
                    if (item.Imagenes.Count == 0)
                        item.Imagenes.Add("/Images/no-image.png");

                    datosImg.cerrarConexion();

                    lista.Add(item);
                }

                return lista;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }





    }
}
