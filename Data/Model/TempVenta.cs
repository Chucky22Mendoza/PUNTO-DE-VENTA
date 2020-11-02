using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class TempVenta {
        [PrimaryKey, Identity, DisplayName("id")]
        public int idTV { set; get; }

        public string codigo { set; get; }

        public double cantidad { set; get; }

        public double total { set; get; }
    }
}
