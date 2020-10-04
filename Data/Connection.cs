using Data.Model;
using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Connection : DataConnection {
        public Connection() : base("DB1") { }
        public ITable<Producto> _Producto { get { return GetTable<Producto>(); } }
        public ITable<Venta> _Venta { get { return GetTable<Venta>(); } }
        public ITable<Venta_Producto> _VentaProducto { get { return GetTable<Venta_Producto>(); } }
        public ITable<Departamentos> _Departamentos { get { return GetTable<Departamentos>(); } }
        public ITable<TipoVenta> _TipoVenta { get { return GetTable<TipoVenta>(); } }
        public ITable<Corte_Caja> _CorteCaja { get { return GetTable<Corte_Caja>(); } }
    } 
}
