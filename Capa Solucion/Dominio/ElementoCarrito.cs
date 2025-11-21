using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class ElementoCarrito
    {
        public long IdElemento { get; set; }
        public long IdCarrito { get; set; }
        public long IdArticulo { get; set; }
        public long Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
