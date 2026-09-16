using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nk_Colletion_New
{
    public class Producto1
    {
        public int IdProducto { get; set; }
        public int IdTipoProducto { get; set; }
        public int IdCategoria { get; set; }
        public int IdMarca { get; set; }
        public string Talla { get; set; }
        public string Color { get; set; }
        public DateTime Fecha { get; set; }
        public string Codigo { get; set; }
        public int StockMinimo { get; set; }
        public decimal PrecioVenta { get; set; }
        public bool Estado { get; set; }
    }
}
