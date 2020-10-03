using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model {
    public class Venta {
        [PrimaryKey, Identity]
        public int id_venta { set; get; }
        public double total { set; get; }
        public int estado { set; get; }
        public int total_articulos { set; get; }
        public DateTime fecha_registro { set; get; }
    }
}
