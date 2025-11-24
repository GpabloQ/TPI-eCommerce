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
        public string UrlImagen { get; set; }
            public string Nombre { get; set; }
            public string Descripcion { get; set; }
            public string Marca { get; set; }
            public string Categoria { get; set; }

        public List<string> Imagenes { get; set; } = new List<string>();

    }
}
