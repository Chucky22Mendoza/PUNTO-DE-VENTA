using Data.Model;
using LinqToDB;
using LinqToDB.SqlQuery;
using Logic.Libreria;
using System;
using MetroFramework.Controls;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic
{
    public class LogicProductos : Librerias
    {

        private MetroGrid tableProducts;
        private List<MetroTextBox> listInput;
        private List<MetroComboBox> listCombo;
        private List<NumericUpDown> listNumeric;

        public LogicProductos(MetroGrid tableProducts, List<MetroTextBox> listInput, List<MetroComboBox> listCombo, List<NumericUpDown> listNumeric)
        {
            this.tableProducts = tableProducts;
            this.listInput = listInput;
            this.listCombo = listCombo;
            this.listNumeric = listNumeric;
        }

        public void index()
        {
            List<Producto> productos = new List<Producto>();
            List<Departamentos> departamentos = new List<Departamentos>();
            List<TipoVenta> tipoVentas = new List<TipoVenta>();


            productos = _Producto.ToList();
            tableProducts.DataSource = productos;

            tipoVentas = _Tipo.ToList();
            listCombo[0].DataSource = tipoVentas;
            listCombo[0].DisplayMember = "tipo";
            listCombo[0].ValueMember = "idTipo";

            departamentos = _Departamentos.ToList();
            listCombo[1].DataSource = departamentos;
            listCombo[1].DisplayMember = "nombre";
            listCombo[1].ValueMember = "idDepartamento";

        }

        public void store()
        {
            if (listInput[0].Text != "")
            {
                List<Producto> productos = new List<Producto>();
                productos = _Producto.Where(obj => obj.codigo_barras.Equals(listInput[0].Text)).ToList();

                if (productos.Count > 0)
                {
                    MessageBox.Show("Producto a guardar ya existe.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    BeginTransactionAsync();
                    try
                    {

                        _Producto.Value(obj => obj.nombre, listInput[1].Text)
                            .Value(obj => obj.descripcion, listInput[3].Text)
                            .Value(obj => obj.codigo_barras, listInput[0].Text)
                            .Value(obj => obj.codigo, listInput[2].Text)
                            .Value(obj => obj.precio, (double)listNumeric[1].Value)
                            .Value(obj => obj.existencia, (double)listNumeric[3].Value)
                            .Value(obj => obj.precio_costo, (double)listNumeric[0].Value)
                            .Value(obj => obj.precio_mayoreo, (double)listNumeric[2].Value)
                            .Value(obj => obj.min_inventario, (double)listNumeric[4].Value)
                            .Value(obj => obj.tipo_venta, listCombo[0].SelectedValue)
                            .Value(obj => obj.departamento, listCombo[1].SelectedValue)
                            .Insert();

                        MessageBox.Show("Producto " + listInput[1].Text + " creado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clean();
                        index();
                        CommitTransaction();
                    }
                    catch (SqlException)
                    {
                        RollbackTransaction();
                        MessageBox.Show("Error al crear el proyecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            else
            {
                MessageBox.Show("Es necesario llenar todos los campos para crear un producto.", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public void edit()
        {
            double doubleVal;
            string doubleAsString;
            decimal decimalVal;

            if (tableProducts.SelectedRows.Count != 0)
            {
                DataGridViewRow data = this.tableProducts.SelectedRows[0];

                listInput[0].Text = (string)data.Cells["codigo_barras"].Value;

                listInput[1].Text = (string)data.Cells["nombre"].Value;

                listInput[2].Text = (string)data.Cells["codigo"].Value;

                listInput[3].Text = (string)data.Cells["descripcion"].Value;

                listCombo[0].SelectedValue = data.Cells["tipo_venta"].Value;

                doubleVal = (double)data.Cells["precio_costo"].Value;
                doubleAsString = doubleVal.ToString("R");
                decimalVal = decimal.Parse(doubleAsString);
                listNumeric[0].Value = decimalVal;

                doubleVal = (double)data.Cells["precio"].Value;
                doubleAsString = doubleVal.ToString("R");
                decimalVal = decimal.Parse(doubleAsString);
                listNumeric[1].Value = decimalVal;

                doubleVal = (double)data.Cells["precio_mayoreo"].Value;
                doubleAsString = doubleVal.ToString("R");
                decimalVal = decimal.Parse(doubleAsString);
                listNumeric[2].Value = decimalVal;

                listCombo[1].SelectedValue = data.Cells["departamento"].Value;

                doubleVal = (double)data.Cells["existencia"].Value;
                doubleAsString = doubleVal.ToString("R");
                decimalVal = decimal.Parse(doubleAsString);
                listNumeric[3].Value = decimalVal;

                doubleVal = (double)data.Cells["min_inventario"].Value;
                doubleAsString = doubleVal.ToString("R");
                decimalVal = decimal.Parse(doubleAsString);
                listNumeric[4].Value = decimalVal;
            }
        }

        public void update()
        {
            if (listInput[0].Text != "")
            {
                DateTime localDate = DateTime.Now;
                BeginTransactionAsync();
                try
                {

                    _Producto.Where(obj => obj.codigo_barras.Equals(listInput[0].Text))
                        .Set(obj => obj.nombre, listInput[1].Text)
                        .Set(obj => obj.descripcion, listInput[3].Text)
                        .Set(obj => obj.codigo_barras, listInput[0].Text)
                        .Set(obj => obj.codigo, listInput[2].Text)
                        .Set(obj => obj.precio, (double)listNumeric[1].Value)
                        .Set(obj => obj.existencia, (double)listNumeric[3].Value)
                        .Set(obj => obj.fecha_actualizacion, localDate)
                        .Set(obj => obj.precio_costo, (double)listNumeric[0].Value)
                        .Set(obj => obj.precio_mayoreo, (double)listNumeric[2].Value)
                        .Set(obj => obj.min_inventario, (double)listNumeric[4].Value)
                        .Set(obj => obj.tipo_venta, listCombo[0].SelectedValue)
                        .Set(obj => obj.departamento, listCombo[1].SelectedValue)
                        .Update();

                    MessageBox.Show("Producto " + listInput[1].Text + " actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clean();
                    index();
                    CommitTransaction();
                }
                catch (SqlException)
                {
                    RollbackTransaction();
                    MessageBox.Show("Error al actualizar el producto " + listInput[1].Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Es necesario seleccionar un producto para actualizarlo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public void delete()
        {
            if (tableProducts.SelectedRows.Count != 0)
            {
                DataGridViewRow data = this.tableProducts.SelectedRows[0];

                string codigo = (string)data.Cells["codigo_barras"].Value;

                DialogResult dialogResult = MessageBox.Show("Esta seguro de eliminar el producto: " + (string)data.Cells["nombre"].Value, "Confirmación", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    BeginTransactionAsync();
                    try
                    {
                        _Producto.Where(obj => obj.codigo_barras.Equals(codigo)).Delete();

                        MessageBox.Show("Producto " + (string)data.Cells["nombre"].Value + " eliminado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        index();
                        CommitTransaction();
                    }
                    catch (SqlException)
                    {
                        RollbackTransaction();
                        MessageBox.Show("Error al eliminar el producto " + (string)data.Cells["nombre"].Value, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (dialogResult == DialogResult.No)
                {
                    MessageBox.Show("Eliminación de producto cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void clean()
        {
            listInput[0].Text = "";

            listInput[1].Text = "";

            listInput[2].Text = "";

            listInput[3].Text = "";

            listCombo[0].SelectedValue = 1;

            listNumeric[0].Value = 0;

            listNumeric[1].Value = 0;

            listNumeric[2].Value = 0;

            listCombo[1].SelectedValue = 1;

            listNumeric[3].Value = 0;

            listNumeric[4].Value = 0;
        }
    }

}
