using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Model
{
    public class ProductoVenta {
        public int id_producto { set; get; }
        [DisplayName("Producto")]
        public string nombre { set; get; }
        [DisplayName("Código")]
        public string codigo { set; get; }
        public double total_producto { set; get; }
        [DisplayName("Ventas")]
        public string ventasStr { set; get; }
    }
}
