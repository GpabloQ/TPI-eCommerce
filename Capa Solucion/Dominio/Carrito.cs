using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Carrito
    {
        public long IdCarrito { get; set; }
        public long IdUsuario { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string Estado { get; set; }

        public string EstadoCarrito { get; set; }

        // NECESARIO PARA MOSTRAR LOS ITEMS DEL PEDIDO
        public List<ElementoCarrito> Items { get; set; } = new List<ElementoCarrito>();
    }
}

        
       
       
