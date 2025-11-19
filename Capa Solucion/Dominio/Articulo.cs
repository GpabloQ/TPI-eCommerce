    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    namespace Dominio
    {
        public class Articulo
        {
            [DisplayName("ID ARTÍCULO")]
            public long IdArticulo { get; set; }

            [DisplayName("CÓDIGO")]
            public string Codigo { get; set; }

            [DisplayName("NOMBRE")]
            public string Nombre { get; set; }

            [DisplayName("DESCRIPCIÓN")]
            public string Descripcion { get; set; }

            [DisplayName("PRECIO")]
            public decimal Precio { get; set; }

            [DisplayName("CANTIDAD EN STOCK")]
            public int Cantidad { get; set; }  

            [DisplayName("ESTADO")]
            public bool Estado { get; set; }

            [DisplayName("MARCA")]
            public Marca Marca { get; set; }

            [DisplayName("CATEGORÍA")]
            public Categoria Categoria { get; set; }

            [DisplayName("URL IMAGEN PRINCIPAL")]
            public string UrlImagen { get; set; }

            [DisplayName("IMÁGENES ASOCIADAS")]
            public List<string> ListaUrls { get; set; }

            public Articulo()
            {
                ListaUrls = new List<string>();
                Marca = new Marca();
                Categoria = new Categoria();
                Estado = true;       
                Cantidad = 0;        
            }
        }
    }




