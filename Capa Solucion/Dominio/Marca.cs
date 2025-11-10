using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Dominio
{
    public class Marca
    {
        //[DisplayName("ID")]
        public long IdMarca { get; set; }
        [DisplayName("MARCA")]
        public string Nombre { get; set; }

        public bool? Estado { get; set; }
        public object Id { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
