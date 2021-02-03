using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Departamentos {

        [PrimaryKey, Identity, DisplayName("ID")]
        public int idDepartamento { set; get; }

        [DisplayName("Nombre")]
        public string nombre { set; get; }

    }
}
