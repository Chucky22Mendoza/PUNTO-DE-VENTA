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

        double total = 0;
        double cash = 0;

        public Cobrar()
        {

            InitializeComponent();
        }

        private void Cobrar_Load(object sender, EventArgs e)
        {
            total = venta.getTotalSale();

            btnOK.Enabled = false;

            lblTotalSale.Text = "$ " + total.ToString();

        }

        private void txtEfectivo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
        (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }

        private void txtEfectivo_TextChanged(object sender, EventArgs e)
        {
            if (txtEfectivo.Text != "")
            {
                double comingCash = Convert.ToDouble(txtEfectivo.Text);

                if (comingCash <= total)
                {
                    lblCash.Text = "$0.00";
                }
                else
                {
                    cash = comingCash - total;

                    lblCash.Text = "$ " + cash.ToString();
                }

                if (comingCash >= total)
                {
                    btnOK.Enabled = true;
                }
                else
                {
                    btnOK.Enabled = false;
                }
            }
            else {
                lblCash.Text = "$0.00";
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            venta.completeSale();
            this.Close();

        }

        private void btnCancelSale_Click(object sender, EventArgs e)
        {
            venta.cancelSale();
            this.Close();
        }
    }
}
