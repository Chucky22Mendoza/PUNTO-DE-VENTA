using Data.Model;
using Logic.Libreria;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic {
    public class LogicVenta : Librerias {

        private MetroGrid tableSaleProducts;
        private MetroTextBox txtProductCode;
        private List<Label> listLabelSale;
        private List<MetroButton> listButtonSale;
        List<Producto> producto = new List<Producto>();

        private int received = 0;
        private int cash = 0;
        private double total = 0;

        private int items = 0;

        public LogicVenta(MetroGrid tableSaleProducts, MetroTextBox txtProductCode, List<Label> listLabelSale, List<MetroButton> listButtonSale)
        {
            this.tableSaleProducts = tableSaleProducts;
            this.txtProductCode = txtProductCode;
            this.listLabelSale = listLabelSale;
            this.listButtonSale = listButtonSale;
        }

        public void addProduct()
        {

            if(txtProductCode.Text.Length < 8)
            {
                producto = _Producto.Where(obj => obj.codigo.Equals(txtProductCode.Text)).ToList();
            }
            else
            {
                producto = _Producto.Where(obj => obj.codigo_barras.Equals(txtProductCode.Text)).ToList();
            }

            if (producto.Count() > 0)
            {
                items = items + 1;

                producto.ForEach(obj =>
                {

                    total = total + obj.precio;

                    if (obj.tipo_venta != 1)
                    {
                        tableSaleProducts.Rows.Add(obj.codigo_barras, obj.codigo, obj.nombre, obj.precio, obj.descripcion, 1);
                    }
                    else
                    {
                        tableSaleProducts.Rows.Add(obj.codigo_barras, obj.codigo, obj.nombre, obj.precio, obj.descripcion);
                    }

                });

                listLabelSale[2].Text = "$" + total;
                listLabelSale[3].Text = "$" + total;
                listLabelSale[4].Text = items + " productos en venta actual";
            }
            else
            {
                MessageBox.Show("No se ha encontrado un producto con el código: " + txtProductCode.Text + ", intente con otro código", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }




        }

        public void removeProduct()
        {
            if (tableSaleProducts.SelectedRows.Count != 0)
            {
                DataGridViewRow data = tableSaleProducts.SelectedRows[0];

                Console.WriteLine(data);

                total = total - (double)data.Cells["precio"].Value;

                items = items - 1;

                listLabelSale[2].Text = "$" + total;
                listLabelSale[3].Text = "$" + total;
                listLabelSale[4].Text = items + " productos en venta actual";

                tableSaleProducts.Rows.Remove(data);
            }
        }

        public List<Producto> getProductData(string code)
        {

            Console.WriteLine(code);

            List<Producto> producto = new List<Producto>();

            if (code.Length < 8)
            {
                producto = _Producto.Where(obj => obj.codigo.Equals(code)).ToList();
            }
            else
            {
                producto = _Producto.Where(obj => obj.codigo_barras.Equals(code)).ToList();
            }

       

            return producto;

        }
    }
}
