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
        public double precio { set; get; }
        public int existencia { set; get; }
        public string fecha_creacion { set; get; }
        public string fecha_actualizacion { set; get; }
    }
}
