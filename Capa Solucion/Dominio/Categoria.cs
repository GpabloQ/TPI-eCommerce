using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Categoria
    {
        [DisplayName("ID")]
        public int Id { get; set; }
       
        [DisplayName("NOMBRE")]
        public string Nombre { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
