using Data.Model;
using Logic;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        public static string code = "";

        public Main() {

            InitializeComponent();

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
            tblDepartamento.BackgroundColor = Color.WhiteSmoke;

            corte = new LogicCorte(listLabelCorte, tblDepartamento, btnImprimirCorte, btnCorte);

            corte.index();
        }

        private void Main_Load(object sender, EventArgs e)
        {

            Console.WriteLine(e);

            Encabezados();
            venta.removeProduct();
            this.ActiveControl = txtProductCode;
            this.txtProductCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtProductCode_KeyPress);
            this.txtProductCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtProductCode_KeyDown);

            this.txtQuatinty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtQuatinty_KeyPress_1);
            this.txtQuatinty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtQuatinty_KeyDown_1);
        }

        private void Encabezados()
        {
            //Encabezados del litView
            listSale.View = View.Details;
            listSale.Columns.Add("Codigo", 100, HorizontalAlignment.Left);
            listSale.Columns.Add("Producto", 250, HorizontalAlignment.Left);
            listSale.Columns.Add("Cant", 75, HorizontalAlignment.Right);
            listSale.Columns.Add("Prec", 75, HorizontalAlignment.Right);
            //lvVenta.Columns.Add("Iva", 75,HorizontalAlignment.Right);
            listSale.Columns.Add("Total", 100, HorizontalAlignment.Right);
        }

        private void puntoVentaTabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (puntoVentaTabs.SelectedTab.Text == "Venta")
            {
                txtProductCode.Focus();
            }
            else if (puntoVentaTabs.SelectedTab.Text == "Productos")
            {
                txtCode.Focus();
            } else if(puntoVentaTabs.SelectedTab.Text == "Inventario")
            {
                txtCodigoProducto.Focus();
            }
        }

        private void listSale_Click(object sender, EventArgs e)
        {
            txtProductCode.Focus();
        }

        private void txtProductCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            lblMensaje.Text = "";
            try
            {
                if (!((txtProductCode.Text == "") || (txtQuatinty.Text == "")))
                {
                   

                    if (e.KeyChar == 13)
                    {
                        if (txtProductCode.Text != "" || txtQuatinty.Text != "")
                        {
                            SaveTemp_Ventas(txtProductCode.Text, Convert.ToDouble(txtQuatinty.Text));
                            txtProductCode.Text = "";
                            txtQuatinty.Text = "";
                            txtProductCode.Focus();
                        }

                    }
                }
                else
                {
                    
                    lblMensaje.Text = "Debe introducir una clave de producto y/o una cantidad";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtProductCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RealizaVenta();

            }
        }

        private void txtQuatinty_Click(object sender, EventArgs e)
        {
            if (txtProductCode.Text.Length == 0)
            {
                txtProductCode.Focus();
            }
        }

        private void txtQuatinty_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            try
            {
                lblMensaje.Text = "";

                if (((txtProductCode.Text != "") || (txtQuatinty.Text == "")))
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


                    if (e.KeyChar == 13)
                    {
                        if (txtProductCode.Text != "" || txtQuatinty.Text != "")
                        {
                            SaveTemp_Ventas(txtProductCode.Text, Convert.ToDouble(txtQuatinty.Text));
                            txtProductCode.Text = "";
                            txtQuatinty.Text = "";
                            txtProductCode.Focus();
                        }
                    }
                }
                else
                {
                    lblMensaje.Text = "Debe introducir una clave de producto y/o una cantidad";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void txtQuatinty_KeyDown_1(object sender, KeyEventArgs e)
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
                if (listSale.Items.Count != 0)
                {
                    venta.makeSale();

                    Cobrar cobrar = new Cobrar();
                    cobrar.ShowDialog();
                }
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
    }
}
