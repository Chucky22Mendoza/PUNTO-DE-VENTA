using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Departamentos {

        [PrimaryKey, Identity]
        public int idDepartamento { set; get; }
        public string nombre { set; get; }

    }
}
