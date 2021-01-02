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
    public partial class InOut : Form
    {
        private LogicVenta venta;

        public InOut()
        {
            InitializeComponent();

            venta = new LogicVenta();
        }

        private void btnRegistrarInOut_Click(object sender, EventArgs e)
        {

            if (txtCantidadInOut.TextLength > 0)
            {
                int tipo = 0;

                if (rbOut.Checked)
                {
                    tipo = 1;
                }

                venta.registerInOut(tipo, txtCantidadInOut.Text);
            
                this.Close();
            }

        }

        private void txtCantidadInOut_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void label1_Click(object sender, EventArgs e) {

        }

        private void txtCantidadInOut_TextChanged(object sender, EventArgs e) {

        }
    }
}
