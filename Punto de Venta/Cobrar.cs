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
    public partial class Cobrar : Form
    {
        private LogicVenta venta = new LogicVenta();

        public Cobrar()
        {

            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {

        }

        private void Cobrar_Load(object sender, EventArgs e)
        {

            lblTotalSale.Text = "$ " + venta.getTotalSale().ToString();
        }
    }
}
