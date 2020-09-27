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

namespace Punto_de_Venta {
    public partial class Main : Form {
        private LogicInventario inventario;
        private LogicCorte corte;

        public Main() {
            InitializeComponent();

            var listLabelInventario = new List<Label>();

            listLabelInventario.Add(lblCodigoProducto);
            listLabelInventario.Add(lblTituloDescripcion);
            listLabelInventario.Add(lblDescripcion);
            listLabelInventario.Add(lblTituloCantidadActual);
            listLabelInventario.Add(lblCantidadActual);
            listLabelInventario.Add(lblTituloCantidad);

            inventario = new LogicInventario(txtCodigoProducto, numInventario, listLabelInventario);

            var listLabelCorte = new List<Label>();

            listLabelCorte.Add(lblVentasTotales);
            listLabelCorte.Add(lblGananciaDia);
            listLabelCorte.Add(lblEntradaDinero);
            listLabelCorte.Add(lblDineroInicial);
            listLabelCorte.Add(lblTotalEfectivo);
            listLabelCorte.Add(lblVentasEfectivo);
            listLabelCorte.Add(lblEntradas);
            listLabelCorte.Add(lblTotalDineroCaja);
            listLabelCorte.Add(lblEfectivo);
            listLabelCorte.Add(lblTotalContado);

            corte = new LogicCorte(listLabelCorte, tblDepartamento);
        }

        private void txtCodigoProducto_TextChanged(object sender, EventArgs e) {
            if(!txtCodigoProducto.Equals("")) {
                inventario.buscarProducto(txtCodigoProducto.Text);
            }
        }

        private void btnAgregarInventario_Click(object sender, EventArgs e) {
            inventario.registrar();
        }

        private void btnReporteInventario_Click(object sender, EventArgs e) {
            inventario.reporteInventario();
        }

        private void btnProductuosBajosInv_Click(object sender, EventArgs e) {
            inventario.reporteBajoInventario();
        }

        private void btnReporteMovimientos_Click(object sender, EventArgs e) {
            inventario.reporteMovimientos();
        }

        private void btnCorte_Click(object sender, EventArgs e) {
            corte.setDataGUI();
        }

        private void btnImprimirCorte_Click(object sender, EventArgs e) {

        }
    }
}
