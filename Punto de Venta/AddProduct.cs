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

namespace Punto_de_Venta
{
    public partial class AddProduct : Form
    {
        private LogicVenta venta = new LogicVenta(Main.tab, null, null, null);

        public static string quantity = "";

        public AddProduct()
        {
            InitializeComponent();

            index();
        }


        public void index()
        {
            string code = Main.code;
            List<Producto> producto = new List<Producto>();
            try
            {
                producto = venta.getProductData(code);

                if(producto.Count > 0)
                {
                    producto.ForEach(obj =>
                    {
                        Console.WriteLine(obj.nombre);
                        lblCodeBar.Text = obj.codigo_barras;
                        lblCode.Text = obj.codigo;
                        lblCurrentExist.Text = obj.existencia.ToString();
                        lblProduct.Text = obj.nombre;
                    });
                }
                else
                {
                    MessageBox.Show("No se ha encontrado un producto con el código: " + code + ", intente con otro código", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            venta.addProduct(Main.code, txtQuantity.Text);
            this.Close();
        }
    }
}
