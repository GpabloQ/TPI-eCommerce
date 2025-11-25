using Dominio;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class DomicilioNegocio
    
    {
        public long Agregar(Domicilio dom)
        {
            AccesoDatos datos = new AccesoDatos();
            datos.setearConsulta(
                "INSERT INTO DOMICILIOS (Calle, Ciudad, Provincia, Numero, Piso, Departamento, CodigoPostal, Estado) " +
                "OUTPUT INSERTED.IdDomicilio " +
                "VALUES (@Calle, @Ciudad, @Provincia, @Numero, @Piso, @Departamento, @CodigoPostal, 1)");

            datos.setearParametro("@Calle", dom.Calle);
            datos.setearParametro("@Ciudad", dom.Ciudad);
            datos.setearParametro("@Provincia", dom.Provincia);
            datos.setearParametro("@Numero", dom.Numero);
            datos.setearParametro("@Piso", dom.Piso);
            datos.setearParametro("@Departamento", dom.Departamento);
            datos.setearParametro("@CodigoPostal", dom.CodigoPostal);

            
            return Convert.ToInt64(datos.ObtenerIdGenerado());
        }

        public Domicilio BuscarPorId(long id)
        {
            AccesoDatos datos = new AccesoDatos();
            Domicilio aux = new Domicilio();

            try
            {
                datos.setearConsulta("SELECT * FROM DOMICILIOS WHERE IdDomicilio = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarLectura();

                if (datos.Lector.Read())
                {
                    aux.IdDomicilio = id;
                    aux.Calle = datos.Lector["Calle"].ToString();
                    aux.Numero = datos.Lector["Numero"].ToString();
                    aux.Piso = datos.Lector["Piso"].ToString();
                    aux.Departamento = datos.Lector["Departamento"].ToString();
                    aux.Ciudad = datos.Lector["Ciudad"].ToString();
                    aux.Provincia = datos.Lector["Provincia"].ToString();
                    aux.CodigoPostal = datos.Lector["CodigoPostal"].ToString();
                }

                return aux;
            }
            finally { datos.cerrarConexion(); }
        }

        public void Eliminar(long id)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("DELETE FROM DOMICILIOS WHERE IdDomicilio = @id");
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

    }
}