using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Domicilio
    {
        public long IdDomicilio { get; set; }
        public string Calle { get; set; }
        public string Ciudad { get; set; }
        public string Provincia { get; set; }
        public String Numero { get; set; }
        public String Piso { get; set; }
        public string Departamento { get; set; }
        public String CodigoPostal { get; set; }
        public bool Estado { get; set; }
    }
}
