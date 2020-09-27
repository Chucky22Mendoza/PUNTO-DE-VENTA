using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model {
    public class Producto {
        [PrimaryKey, Identity, DisplayName("id")]
        public int id_producto { set; get; }

        [DisplayName("Nombre")]
        public string nombre { set; get; }

        [DisplayName("Descripción")]
        public string descripcion { set; get; }

        [DisplayName("Tipo venta")]
        public int tipo_venta { set; get; }

        [DisplayName("Código de barras")]
        public string codigo_barras { set; get; }

        [DisplayName("Código")]
        public string codigo { set; get; }

        [DisplayName("Precio venta")]
        public double precio { set; get; }

        [DisplayName("Precio costo")]
        public double precio_costo { set; get; }

        [DisplayName("Precio mayoreo")]
        public double precio_mayoreo { set; get; }

        [DisplayName("Existencia")]
        public double existencia { set; get; }

        [DisplayName("Min. inventario")]
        public double min_inventario { set; get; }

        [DisplayName("Departamento")]
        public int departamento { set; get; }

        [DisplayName("Fecha de creación")]
        public DateTime fecha_creacion { set; get; }

        [DisplayName("Fecha de actualización")]
        public DateTime fecha_actualizacion { set; get; }
    }
}
