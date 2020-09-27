using Data.Model;
using Logic;
using MetroFramework.Controls;
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
        private LogicProductos productos;

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

            var listLabel = new List<Label>();

            listLabel.Add(lblCodigoProducto);
            listLabel.Add(lblTituloDescripcion);
            listLabel.Add(lblDescripcion);
            listLabel.Add(lblTituloCantidadActual);
            listLabel.Add(lblCantidadActual);
            listLabel.Add(lblTituloCantidad);

            var listInput = new List<MetroTextBox>();

            listInput.Add(txtCodeBars);
            listInput.Add(txtName);
            listInput.Add(txtCode);
            listInput.Add(txtDescription);

            var listCombo = new List<MetroComboBox>();

            listCombo.Add(cbTypeSale);
            listCombo.Add(cbDepartment);

            var listNumeric = new List<NumericUpDown>();

            listNumeric.Add(numCost);
            listNumeric.Add(numSale);
            listNumeric.Add(numWholeSale);
            listNumeric.Add(numQuantityCurrent);
            listNumeric.Add(numQuantityMinimum);

            inventario = new LogicInventario(txtCodigoProducto, numInventario, listLabel);
            productos = new LogicProductos(tableProducts, listInput, listCombo, listNumeric);

            productos.index();
        }

        /**
         * Funciones de producto
         */

        private void btnStore_Click(object sender, EventArgs e)
        {
            productos.store();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            productos.update();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            productos.clean();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            productos.edit();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            productos.delete();
        }

        /**
         * Funciones de Inventario
         */

        private void txtCodigoProducto_TextChanged(object sender, EventArgs e)
        {
            if (!txtCodigoProducto.Equals(""))
            {
                inventario.buscarProducto(txtCodigoProducto.Text);
            }
        }

        private void btnAgregarInventario_Click(object sender, EventArgs e)
        {
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

        private void lblVentasEfectivo_Click(object sender, EventArgs e) {

        }
    }
}
