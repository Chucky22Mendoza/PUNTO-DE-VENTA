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

        private ListView listSale;
        private List<Label> labels;

        List<Producto> producto = new List<Producto>();

        private double total = 0;
        private int count = 0;
        private double items = 0;

        public LogicVenta(ListView listSale, List<Label> labels)
        {
            this.listSale = listSale;
            this.labels = labels;
        }

        public void addProduct(string product, double quantity)
        {

            if(product.Length < 8)
            {
                Console.WriteLine("es menor");
                producto = _Producto.Where(obj => obj.codigo.Equals(product)).ToList();
            }
            else
            {
                Console.WriteLine("es mayor");
                producto = _Producto.Where(obj => obj.codigo_barras.Equals(product)).ToList();
            }

            if (producto.Count() == 1)
            {
                items = items + quantity;

                producto.ForEach(obj =>
                {
                    
                    Console.WriteLine(obj.nombre);
                    total = total + (obj.precio * quantity);

                    listSale.Items.Add(obj.codigo);
                    listSale.Items[count].SubItems.Add(obj.nombre);
                    listSale.Items[count].SubItems.Add(quantity.ToString());
                    listSale.Items[count].SubItems.Add(obj.precio.ToString());
                    listSale.Items[count].SubItems.Add((obj.precio * quantity).ToString());
                    count += 1;
                });

                labels[0].Text = "$" + total;
                labels[1].Text = items + " productos en venta actual";

            }
            else
            {
                MessageBox.Show("No se ha encontrado un producto con el código: " + product + ", intente con otro código", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void removeProduct()
        {
            listSale.Items.Clear();
            count = 0;
            items = 0;
            total = 0;
            labels[0].Text = "$" + total;
            labels[1].Text = items + " productos en venta actual";
        }

    }
}
