using LinqToDB.Mapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Model
{
    public class Entradas_Salidas
    {

        [PrimaryKey, Identity]
        public int identradas_salidas { set; get; }

        public int tipo { set; get; }

        public double cantidad { set; get; }

        public DateTime date_create { set; get; }

    }
}
