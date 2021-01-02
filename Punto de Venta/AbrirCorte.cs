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

namespace Punto_de_Venta {
    public partial class AbrirCorte : Form {
        private LogicCorte corte;
        public AbrirCorte() {
            InitializeComponent();

            corte = new LogicCorte();
        }

        private void btnRegistrarCaja_Click(object sender, EventArgs e) {
            if (txtCantidadCaja.TextLength > 0) {
                corte.registrarInicioCorte(txtCantidadCaja.Text);
                this.Close();
            }
        }

        private void txtCantidadCaja_KeyPress(object sender, KeyPressEventArgs e) {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.')) {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1)) {
                e.Handled = true;
            }
        }
    }
}
