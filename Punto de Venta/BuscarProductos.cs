using Data;
using Data.Model;
using Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta
{
    public partial class BuscarProductos : Form
    {

        private LogicProductos productos;
        private LogicVenta ventas = new LogicVenta();

        List<Producto> producto = new List<Producto>();

        public BuscarProductos()
        {
            InitializeComponent();

            productos = new LogicProductos(tableSearchProducts);
        }

        private void txtProductName_TextChanged(object sender, EventArgs e)
        {

            if (txtProductName.Text != "")
            {

                productos.searchProduct(txtProductName.Text);

            }

        }

        private void btnSelectProduct_Click(object sender, EventArgs e)
        {

            if (tableSearchProducts.SelectedRows.Count != 0)
            {

                DataGridViewRow data = this.tableSearchProducts.SelectedRows[0];

                Main.productoCodigo = (string)data.Cells["codigo"].Value;

                this.Close();

            }

        }
    }
}
