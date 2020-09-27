using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model {
    public class Producto {
        [PrimaryKey, Identity]
        public int id_producto { set; get; }
        public string nombre { set; get; }
        public string descripcion { set; get; }
        public string codigo_barras { set; get; }
        public string codigo { set; get; }
        public double precio_venta { set; get; }
        public double precio_costo { set; get; }
        public double precio_mayoreo { set; get; }
        public int existencia { set; get; }
        public int min_inventario { set; get; }
        public int tipo_venta { set; get; }
        public DateTime fecha_registro { set; get; }
        public DateTime fecha_actualizacion { set; get; }
    }
}
