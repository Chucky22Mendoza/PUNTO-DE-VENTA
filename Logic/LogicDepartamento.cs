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

namespace Logic {
    public class LogicDepartamento : Librerias {
        private MetroGrid tableDepartamentos;
        private MetroTextBox inputText;

        public LogicDepartamento() {

        }

        public LogicDepartamento(MetroGrid tableDepartamentos, MetroTextBox inputText) {
            this.tableDepartamentos = tableDepartamentos;
            this.inputText = inputText;
        }

        public void index() {
            List<Departamentos> departamentos = new List<Departamentos>();

            try {
                departamentos = _Departamentos.ToList();
                tableDepartamentos.DataSource = departamentos;

                tableDepartamentos.Columns[0].DefaultCellStyle.BackColor = Color.White;
                tableDepartamentos.Columns[0].DefaultCellStyle.ForeColor = Color.Black;
                tableDepartamentos.Columns[0].DefaultCellStyle.SelectionForeColor = Color.White;
                tableDepartamentos.Columns[0].DefaultCellStyle.SelectionBackColor = Color.Gray;
                tableDepartamentos.Columns[0].Width = 100;

                tableDepartamentos.Columns[1].DefaultCellStyle.BackColor = Color.White;
                tableDepartamentos.Columns[1].DefaultCellStyle.ForeColor = Color.Black;
                tableDepartamentos.Columns[1].DefaultCellStyle.SelectionForeColor = Color.White;
                tableDepartamentos.Columns[1].DefaultCellStyle.SelectionBackColor = Color.Gray;
                tableDepartamentos.Columns[1].Width = 300;

                tableDepartamentos.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
                tableDepartamentos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                tableDepartamentos.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DimGray;
                tableDepartamentos.RowHeadersWidth = 25;
            } catch (SqlException e) {
                MessageBox.Show("Error al intentar conectar con la base de datos: Logica de departamentos, ln 52", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void store() {
            if (inputText.Text != "") {
                List<Departamentos> departamentos = new List<Departamentos>();
                departamentos = _Departamentos.Where(obj => obj.nombre.Equals(inputText.Text)).ToList();

                if (departamentos.Count > 0) {
                    MessageBox.Show("Departamento a guardar ya existe.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                } else {
                    BeginTransactionAsync();
                    try {

                        _Departamentos.Value(obj => obj.nombre, inputText.Text).Insert();

                        MessageBox.Show("Departamento " + inputText.Text + " creado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        clean();
                        index();
                        CommitTransaction();
                    } catch (SqlException) {
                        RollbackTransaction();
                        MessageBox.Show("Error al crear el departamento", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            } else {
                MessageBox.Show("Es necesario llenar todos los campos para crear un departamento.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        private static int idDepActualizar;
        public void edit() {
            if (tableDepartamentos.SelectedRows.Count != 0) {
                DataGridViewRow data = this.tableDepartamentos.SelectedRows[0];

                inputText.Text = (string)data.Cells["nombre"].Value;
                idDepActualizar = (int)data.Cells["idDepartamento"].Value;
            }
        }

        public void update() {
            if (inputText.Text != "") {
                BeginTransactionAsync();
                try {
                    _Departamentos.Where(obj => obj.idDepartamento.Equals(idDepActualizar))
                        .Set(obj => obj.nombre, inputText.Text)
                        .Update();

                    MessageBox.Show("Departamento " + inputText.Text + " actualizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clean();
                    index();
                    CommitTransaction();
                }
                catch (SqlException) {
                    RollbackTransaction();
                    MessageBox.Show("Error al actualizar el departamento " + inputText.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else {
                MessageBox.Show("Es necesario seleccionar un departamento para actualizarlo.", "Alerta", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }

        public void delete() {
            if (tableDepartamentos.SelectedRows.Count != 0) {
                DataGridViewRow data = this.tableDepartamentos.SelectedRows[0];

                int id = (int)data.Cells["idDepartamento"].Value;

                DialogResult dialogResult = MessageBox.Show("Esta seguro de eliminar el departamento: " + (string)data.Cells["nombre"].Value, "Confirmación", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes) {
                    BeginTransactionAsync();
                    try {
                        _Departamentos.Where(obj => obj.idDepartamento.Equals(id)).Delete();

                        MessageBox.Show("Departamento " + (string)data.Cells["nombre"].Value + " eliminado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        index();
                        CommitTransaction();
                    }
                    catch (SqlException) {
                        RollbackTransaction();
                        MessageBox.Show("Error al eliminar el departamento " + (string)data.Cells["nombre"].Value, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else if (dialogResult == DialogResult.No) {
                    MessageBox.Show("Eliminación de departamento cancelada.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        public void clean() {
            inputText.Text = "";
        }
    }
}
