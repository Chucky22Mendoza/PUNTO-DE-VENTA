using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class TipoVenta {

        [PrimaryKey, Identity]
        public int idTipo { set; get; }
        public string tipo { set; get; }

    }
}
