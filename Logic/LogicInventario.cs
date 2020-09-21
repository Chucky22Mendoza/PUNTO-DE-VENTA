using Data.Model;
using LinqToDB;
using LinqToDB.SqlQuery;
using Logic.Libreria;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace Logic {
    public class LogicInventario : Librerias {
        private MetroTextBox txtCodigoProducto;
        private NumericUpDown numInventario;
        private List<Label> listLabel;
        
        public LogicInventario(MetroTextBox txtCodigoProducto, NumericUpDown numInventario, List<Label> listLabel) {
            this.txtCodigoProducto = txtCodigoProducto;
            this.numInventario = numInventario;
            this.listLabel = listLabel;
            limpiarCampos();
        }

        public void registrar () {
            if (!txtCodigoProducto.Text.Equals("")) {
                listLabel[0].Text = "Código del producto";
                listLabel[0].ForeColor = Color.Black;
                if (numInventario.Value > 0) {
                    listLabel[5].Text = "Cantidad";
                    listLabel[5].ForeColor = Color.Black;
                    var producto = _Producto.Where(obj => obj.codigo.Equals(txtCodigoProducto.Text)).ToList();
                    if (producto.Count.Equals(1)) {
                        listLabel[0].Text = "Código del producto";
                        listLabel[0].ForeColor = Color.Black;
                        guardarRegistro();
                    } else {
                        listLabel[0].Text = "Código del producto existente requerido";
                        listLabel[0].ForeColor = Color.Red;
                        txtCodigoProducto.Focus();
                    }
                } else {
                    listLabel[5].Text = "Cantidad mayor a 0 requerida";
                    listLabel[5].ForeColor = Color.Red;
                    listLabel[5].Location = new Point(75, 214);
                    numInventario.Focus();
                }
            } else {
                listLabel[0].Text = "Código del producto requerido";
                listLabel[0].ForeColor = Color.Red;
                txtCodigoProducto.Focus();
            }
        }

        public void buscarProducto(string field) {
            List<Producto> query = new List<Producto>();
            
            if (field.Equals("")) {
                query = _Producto.ToList();
            } else {
                query = _Producto.Where(obj => obj.codigo.StartsWith(field)).ToList();
            }

            if (query.Count == 1) {
                query.ForEach(obj => {
                    listLabel[2].Text = obj.descripcion;
                    listLabel[4].Text = obj.existencia.ToString();
                });
            } else {
                listLabel[2].Text = "-";
                listLabel[4].Text = "0";
            }
        }

        int nuevaExistencia;
        private void guardarRegistro() {
            BeginTransactionAsync();
            try {
                List<Producto> productos = _Producto.Where(obj => obj.codigo.Equals(txtCodigoProducto.Text)).ToList();
                productos.ForEach(element => {
                    nuevaExistencia = element.existencia + Convert.ToInt32(Math.Round(numInventario.Value, 0));
                });
                
                _Producto.Where(obj => obj.codigo.Equals(txtCodigoProducto.Text))
                    .Set(obj => obj.existencia, nuevaExistencia)
                    .Update();

                MessageBox.Show("Producto actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                limpiarCampos();
                CommitTransaction();
            } catch (SqlException) {
                RollbackTransaction();
            }
        }

        private void limpiarCampos() {
            listLabel[2].Text = "-";
            listLabel[4].Text = "0";

            listLabel[0].Text = "Código del producto";
            listLabel[0].ForeColor = Color.Black;
            listLabel[5].Text = "Cantidad";
            listLabel[5].ForeColor = Color.Black;
            listLabel[5].Location = new Point(146, 214);
            txtCodigoProducto.Text = "";
            numInventario.Value = 0;
        }
    }
}
