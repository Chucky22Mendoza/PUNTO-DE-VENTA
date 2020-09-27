using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model {
    public class Corte_Caja {
        public int id_corte_caja { get; set; }
        public double dinero_inicial { get; set; }
        public DateTime fecha_corte_inicio { get; set; }
        public DateTime fecha_corte_fin { get; set; }
        public double ganancia { get; set; }
        public double ventas_totales { get; set; }
    }
}
