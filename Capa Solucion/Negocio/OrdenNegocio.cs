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
                    "FROM CARRITO WHERE IdUsuario = @IdUsuario AND EstadoCarrito = 'Finalizado'");

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


        public List<ElementoCarrito> ListarElementosPorCarrito(long idCarrito)
        {
            List<ElementoCarrito> lista = new List<ElementoCarrito>();
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta(
                    "SELECT IdElemento, IdCarrito, IdArticulo, Cantidad, PrecioUnitario " +
                    "FROM ELEMENTOCARRITO WHERE IdCarrito = @IdCarrito");

                datos.setearParametro("@IdCarrito", idCarrito);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    ElementoCarrito elemento = new ElementoCarrito
                    {
                        IdElemento = (long)datos.Lector["IdElemento"],
                        IdCarrito = (long)datos.Lector["IdCarrito"],
                        IdArticulo = (long)datos.Lector["IdArticulo"],
                        Cantidad = (long)datos.Lector["Cantidad"],
                        PrecioUnitario = (decimal)datos.Lector["PrecioUnitario"]
                    };

                    lista.Add(elemento);
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

    }
}
