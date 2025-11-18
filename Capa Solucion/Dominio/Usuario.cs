using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Usuario
    {
            public long IdUsuario { get; set; }
            public string Nombre { get; set; }
            public string Apellido { get; set; }
            public string Mail { get; set; }
            public string Contrasenia { get; set; }
            public string Telefono { get; set; }
            public string DNI { get; set; }
            public int TipoUsuario { get; set; }
            public bool Estado { get; set; }
            public DateTime FechaNacimiento { get; set; }


            //Estas propiedades son para el metodo listar con join
            public string NombreCompleto { get; set; }
            public string TipoUsuarioNombre { get; set; }
    }
}
