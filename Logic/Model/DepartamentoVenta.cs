using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic {
    public class DepartamentoVenta {
        public int idDepartamento { set; get; }
        [DisplayName("Departamento")]
        public string nombre { set; get; }
        public double total_departamento { set; get; }
        [DisplayName("Ventas")]
        public string ventasStr { set; get; }
    }
}
