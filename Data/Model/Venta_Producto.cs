using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model {
    public class Venta_Producto {
        public int id_venta_producto { get; set; }
        public int id_venta { get; set; }
        public int id_producto { get; set; }
        public int cantidad { get; set; }
        public double total { get; set; }
        public DateTime fecha_registro { set; get; }
    }
}
