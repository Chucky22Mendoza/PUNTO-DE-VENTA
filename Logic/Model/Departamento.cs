using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic {
    public class Departamento {
        [DisplayName("Departamento")]
        public string nombre { set; get; }
        public double ventas { set; get; }
        [DisplayName("Ventas")]
        public string ventasStr { set; get; }
    }
}
