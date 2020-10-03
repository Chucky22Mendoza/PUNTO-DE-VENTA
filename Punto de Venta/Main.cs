﻿using Data.Model;
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
        private LogicVenta venta;
        private LogicProductos productos;
        private LogicInventario inventario;
        private LogicCorte corte;

        public static string code = "";

        public Main() {

            InitializeComponent();

            //Ventas

            var listLabelSale = new List<Label>();

            listLabelSale.Add(lblReceived);
            listLabelSale.Add(lblCash);
            listLabelSale.Add(lblTotal);
            listLabelSale.Add(lblTotal2);
            listLabelSale.Add(lblItems);

            var listButtonSale = new List<MetroButton>();

            listButtonSale.Add(btnAddProductSale);
            listButtonSale.Add(btnCharge);
            listButtonSale.Add(btnDeleteProduct);

            venta = new LogicVenta(tableSaleProducts, txtProductCode, listLabelSale, listButtonSale);

            //Productos

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

            productos = new LogicProductos(tableProducts, listInput, listCombo, listNumeric);

            productos.index();

            //Inventario

            var listLabel = new List<Label>();

            listLabel.Add(lblCodigoProducto);
            listLabel.Add(lblTituloDescripcion);
            listLabel.Add(lblDescripcion);
            listLabel.Add(lblTituloCantidadActual);
            listLabel.Add(lblCantidadActual);
            listLabel.Add(lblTituloCantidad);

            inventario = new LogicInventario(txtCodigoProducto, numInventario, listLabel);

            //Corte
            var listLabelCorte = new List<Label>();

            listLabelCorte.Add(lblVentasTotales);
            listLabelCorte.Add(lblGananciaDia);
            listLabelCorte.Add(lblEntradaCambio);
            listLabelCorte.Add(lblDineroInicial);
            listLabelCorte.Add(lblTotalEfectivo);
            listLabelCorte.Add(lblVentasEfectivo);
            listLabelCorte.Add(lblEntradas);
            listLabelCorte.Add(lblSalidas);
            listLabelCorte.Add(lblTotalDineroCaja);
            tblDepartamento.BackgroundColor = Color.WhiteSmoke;

            corte = new LogicCorte(listLabelCorte, tblDepartamento, btnImprimirCorte, btnCorte);

            corte.index();
        }

        /**
         * Funciones para ventas
         */

        private void btnAddProduct_Click(object sender, EventArgs e)
        {
            code = txtProductCode.Text;
            AddProduct addProduct = new AddProduct();
            addProduct.Show();
            //venta.addProduct();
        }

        private void btnDeleteProduct_Click(object sender, EventArgs e)
        {
            venta.removeProduct();
        }


        /**
         * Funciones de productos
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
         * Eventos de Inventario  
         */

        private void txtCodigoProducto_TextChanged(object sender, EventArgs e) {
            if (!txtCodigoProducto.Equals("")) {
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

        /**
         * Eventos de Corte
         */
        private void btnCorte_Click(object sender, EventArgs e) {
            corte.setDataGUI();
        }

        private void btnImprimirCorte_Click(object sender, EventArgs e) {
            corte.imprimirCorte();
        }

        public static implicit operator string(Main v) {
            throw new NotImplementedException();
        }

        
    }
}
