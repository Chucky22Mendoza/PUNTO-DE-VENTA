using Data.Model;
using Logic;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Punto_de_Venta {
    public partial class Main : Form {
        private LogicVenta venta;
        private LogicProductos productos;
        private LogicInventario inventario;
        private LogicCorte corte;
        private LogicDepartamento departamento;

        public static string code = "";

        public Main() {

            InitializeComponent();
            
            btnReporteMovimientos.Visible = false;

            //Ventas

            var labels = new List<Label>();

            labels.Add(lblTotal);
            labels.Add(lblItems);

            //var listButtonSale = new List<MetroButton>();

            //listButtonSale.Add(btnCharge);
            //listButtonSale.Add(btnClean);

            venta = new LogicVenta( listSale,  labels);

            //Productos

            var listInput = new List<MetroTextBox>();

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
            listLabelCorte.Add(lblMensaje);
            tblDepartamento.BackgroundColor = Color.WhiteSmoke;

            corte = new LogicCorte(listLabelCorte, tblDepartamento, tblProductosVentas, btnImprimirCorte, btnCorte, btnAbrirCorte);

            corte.index();

            departamento = new LogicDepartamento(tblDepartamentosTab, txtDepartamentoTab);

            departamento.index();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            Console.WriteLine(e);

            Encabezados();
            venta.removeProduct();
            this.ActiveControl = txtProductCode;
            this.txtProductCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductCode_KeyPress);
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);

        }

        private void Encabezados()
        {
            //Encabezados del litView
            listSale.View = View.Details;
            listSale.Columns.Add("Codigo", 100, HorizontalAlignment.Left);
            listSale.Columns.Add("Producto", 250, HorizontalAlignment.Left);
            listSale.Columns.Add("Cant", 75, HorizontalAlignment.Left);
            listSale.Columns.Add("Prec", 75, HorizontalAlignment.Left);
            //lvVenta.Columns.Add("Iva", 75,HorizontalAlignment.Right);
            listSale.Columns.Add("Total", 100, HorizontalAlignment.Left);
        }

        private void puntoVentaTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (puntoVentaTabs.SelectedTab.Text == "Venta")
            {
                txtProductCode.Focus();
                getProducts.Stop();
            }
            else if (puntoVentaTabs.SelectedTab.Text == "Productos")
            {
                txtCode.Focus();
                productos.index();
                getProducts.Start();


            } else if(puntoVentaTabs.SelectedTab.Text == "Inventario")
            {
                txtCodigoProducto.Focus();
                getProducts.Stop();
            }
        }

        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            bool res = venta.checkDeposito();

            if (res)
            {
                lblMensaje.Text = "";
                try
                {
                    if (!((txtProductCode.Text == "")))
                    {
                        if (e.KeyChar == 13)
                        {
                            if (txtProductCode.Text != "")
                            {
                                SaveTemp_Ventas(txtProductCode.Text, 1);
                                txtProductCode.Text = "";
                                txtProductCode.Focus();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            } else
            {
                MessageBox.Show("Es necesario primero añadir el deposito inicial.", "Alerta del deposito inicial.", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RealizaVenta();

            }
        }

        private void SaveTemp_Ventas(string product, double quantity)
        {
            SystemSounds.Exclamation.Play();
            venta.addProduct(product, quantity);
        }

        private void RealizaVenta()
        {
            if (listSale.Items.Count > 0)
            {
                SystemSounds.Asterisk.Play();
                venta.makeSale();

                Cobrar cobrar = new Cobrar();
                cobrar.ShowDialog();
            }
            else
            {
                this.txtProductCode.Focus();
            }
        }

        /**
         * Funciones para ventas
         */

        private void btnCharge_Click(object sender, EventArgs e)
        {
            RealizaVenta();
            txtProductCode.Focus();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            venta.removeProduct();
            txtProductCode.Focus();
        }

        private void btnInOut_Click(object sender, EventArgs e)
        {
            InOut inOut = new InOut();
            inOut.ShowDialog();
            this.txtProductCode.Focus();
        }

        /**
         * Funciones de productos
         */


        private void txtName_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
        }

        private void txtDescription_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
        }

        private void cbTypeSale_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
        }

        private void numCost_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
        }

        private void numSale_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
        }

        private void numWholeSale_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
        }

        private void numQuantityCurrent_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
        }

        private void numQuantityMinimum_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
        }

        private void cbDepartment_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
        }



        private void btnStore_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
            else
            {
                productos.store();
                txtCode.Focus();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtCode.Text.Length == 0)
            {
                txtCode.Focus();
            }
            else
            {
                productos.update();
                txtCode.Focus();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            productos.clean();
            txtCode.Focus();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            productos.edit();
        }        

        private void btnDelete_Click(object sender, EventArgs e)
        {
            productos.delete();
            txtCode.Focus();
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
            if (txtCodigoProducto.Text.Length == 0)
            {
                txtCodigoProducto.Focus();
            }
            else
            {
                inventario.registrar();
            }
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

        private void numInventario_Click(object sender, EventArgs e)
        {
            if (txtCodigoProducto.Text.Length == 0)
            {
                txtCodigoProducto.Focus();
            }
        }

        private void btnAbrirCorte_Click(object sender, EventArgs e) {
            AbrirCorte abrirCorte = new AbrirCorte();
            abrirCorte.ShowDialog();
            corte.index();
            this.txtProductCode.Focus();
        }

        private void listSale_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                foreach (ListViewItem listViewItem in ((ListView)sender).SelectedItems)
                {
                    ListView.SelectedListViewItemCollection sales = ((ListView)sender).SelectedItems;

                    double precio = 0.0;

                    foreach (ListViewItem sale in sales)
                    {
                        precio += Double.Parse(sale.SubItems[3].Text);
                    }

                    listViewItem.Remove();

                    venta.reduceCount(precio);

                    txtProductCode.Focus();

                }
            }
        }

        /**
         * Eventos de Departamento
         */
        private void btnGuardarDep_Click(object sender, EventArgs e) {
            if (txtDepartamentoTab.Text.Length == 0) {
                txtDepartamentoTab.Focus();
            } else {
                departamento.store();
                txtDepartamentoTab.Focus();
            }
        }

        private void btnActualizarDep_Click(object sender, EventArgs e) {
            if (txtDepartamentoTab.Text.Length == 0) {
                txtDepartamentoTab.Focus();
            } else {
                departamento.update();
                txtDepartamentoTab.Focus();
            }
        }

        private void btnCancelarDep_Click(object sender, EventArgs e) {
            departamento.clean();
            txtDepartamentoTab.Focus();
        }

        private void btnEditarDep_Click(object sender, EventArgs e) {
            departamento.edit();
        }

        private void btnEliminarDep_Click(object sender, EventArgs e) {
            departamento.delete();
            txtDepartamentoTab.Focus();
        }

        private void getProducts_Tick(object sender, EventArgs e)
        {
            productos.index();
        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void label34_Click(object sender, EventArgs e)
        {

        }

        private void lblEntradaCambio_Click(object sender, EventArgs e)
        {

        }

        private void lblDineroInicial_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalEfectivo_Click(object sender, EventArgs e)
        {

        }

        private void label37_Click(object sender, EventArgs e)
        {

        }

        private void label41_Click(object sender, EventArgs e)
        {

        }

        private void lblVentasEfectivo_Click(object sender, EventArgs e)
        {

        }

        private void lblEntradas_Click(object sender, EventArgs e)
        {

        }

        private void lblSalidas_Click(object sender, EventArgs e)
        {

        }

        private void label39_Click(object sender, EventArgs e)
        {

        }

        private void label40_Click(object sender, EventArgs e)
        {

        }

        private void label54_Click(object sender, EventArgs e)
        {

        }

        private void lblTotalDineroCaja_Click(object sender, EventArgs e)
        {

        }
    }
}
