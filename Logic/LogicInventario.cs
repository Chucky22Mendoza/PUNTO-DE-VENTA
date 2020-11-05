using Data.Model;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LinqToDB;
using LinqToDB.SqlQuery;
using Logic.Libreria;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
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
        string departamentoColumn;
        int nuevaExistencia;

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

            if (query.Count.Equals(1)) {
                query.ForEach(obj => {
                    listLabel[2].Text = obj.descripcion;
                    listLabel[4].Text = obj.existencia.ToString();
                });
            } else {
                listLabel[2].Text = "-";
                listLabel[4].Text = "0";
            }
        }

        private void guardarRegistro() {
            BeginTransactionAsync();
            try {
                List<Producto> productos = _Producto.Where(obj => obj.codigo.Equals(txtCodigoProducto.Text)).ToList();
                productos.ForEach(element => {
                    nuevaExistencia = (int)(element.existencia + Convert.ToInt32(numInventario.Value));
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
        
        public void reporteInventario() {
            DateTime dateTime = DateTime.Now;

            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "ReporteInventario_" + dateTime.ToShortDateString().Replace("/", "-") + ".pdf";
            save.Filter = "Formato de documento portátil (PDF) (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK) {
                // Creamos el documento
                Document doc = new Document(PageSize.LETTER);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(save.FileName, FileMode.Create));

                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Reporte Inventario");
                doc.AddCreator("loginlock22@gmail.com");

                //Abrimos el archivo
                doc.Open();

                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                // Escribimos el encabezamiento en el documento
                Paragraph title = new Paragraph();
                title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f, BaseColor.DARK_GRAY);
                title.Add("Reporte de Inventario");
                title.Alignment = Element.ALIGN_CENTER;

                Paragraph fecha = new Paragraph();
                fecha.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.DARK_GRAY);
                fecha.Add("Fecha: " + dateTime.ToShortDateString());
                fecha.Alignment = Element.ALIGN_CENTER;

                Paragraph hora = new Paragraph();
                hora.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.DARK_GRAY);
                hora.Add("Hora: " + dateTime.ToShortTimeString());
                hora.Alignment = Element.ALIGN_CENTER;

                doc.Add(title);
                doc.Add(fecha);
                doc.Add(hora);
                doc.Add(Chunk.NEWLINE);

                // Creamos una tabla 
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                // Configuramos el título de las columnas de la tabla
                PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", _standardFont));
                clNombre.Border = 0;
                clNombre.BorderWidthBottom = 0.75f;
                clNombre.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clCodigo = new PdfPCell(new Phrase("Código", _standardFont));
                clCodigo.Border = 0;
                clCodigo.BorderWidthBottom = 0.75f;
                clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clCodigoBarras = new PdfPCell(new Phrase("Código de Barras", _standardFont));
                clCodigoBarras.Border = 0;
                clCodigoBarras.BorderWidthBottom = 0.75f;
                clCodigoBarras.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clExistencia = new PdfPCell(new Phrase("Existencia", _standardFont));
                clExistencia.Border = 0;
                clExistencia.BorderWidthBottom = 0.75f;
                clExistencia.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clPrecioVenta = new PdfPCell(new Phrase("Precio Venta", _standardFont));
                clPrecioVenta.Border = 0;
                clPrecioVenta.BorderWidthBottom = 0.75f;
                clPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clPrecioCosto = new PdfPCell(new Phrase("Precio Costo", _standardFont));
                clPrecioCosto.Border = 0;
                clPrecioCosto.BorderWidthBottom = 0.75f;
                clPrecioCosto.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clDepartamento = new PdfPCell(new Phrase("Departamento", _standardFont));
                clDepartamento.Border = 0;
                clDepartamento.BorderWidthBottom = 0.75f;
                clDepartamento.HorizontalAlignment = Element.ALIGN_CENTER;

                // Añadimos las celdas a la tabla
                table.AddCell(clNombre);
                table.AddCell(clCodigo);
                table.AddCell(clCodigoBarras);
                table.AddCell(clExistencia);
                table.AddCell(clPrecioVenta);
                table.AddCell(clPrecioCosto);
                table.AddCell(clDepartamento);

                // Llenamos la tabla con información
                List<Producto> productos = _Producto.ToList();
                List<Departamentos> departamentos = _Departamentos.ToList();

                productos.ForEach(producto => {
                    string precio_venta = parser.getCentavos(Math.Round(producto.precio, 2).ToString());
                    string precio_costo = parser.getCentavos(Math.Round(producto.precio_costo, 2).ToString());

                    departamentos.ForEach(departamento => {
                        if (departamento.idDepartamento == producto.departamento) {
                            departamentoColumn = departamento.nombre;
                        }
                    });

                    // Añadimos las celdas a la tabla
                    clNombre = new PdfPCell(new Phrase(producto.nombre, _standardFont));
                    clCodigo = new PdfPCell(new Phrase(producto.codigo, _standardFont));
                    clCodigoBarras = new PdfPCell(new Phrase(producto.codigo_barras, _standardFont));
                    clExistencia = new PdfPCell(new Phrase(producto.existencia.ToString(), _standardFont));
                    clPrecioVenta = new PdfPCell(new Phrase(precio_venta, _standardFont));
                    clPrecioCosto = new PdfPCell(new Phrase(precio_costo, _standardFont));
                    clDepartamento = new PdfPCell(new Phrase(departamentoColumn, _standardFont));

                    clNombre.HorizontalAlignment = Element.ALIGN_CENTER;
                    clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
                    clCodigoBarras.HorizontalAlignment = Element.ALIGN_CENTER;
                    clExistencia.HorizontalAlignment = Element.ALIGN_CENTER;
                    clPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;
                    clPrecioCosto.HorizontalAlignment = Element.ALIGN_CENTER;
                    clDepartamento.HorizontalAlignment = Element.ALIGN_CENTER;

                    // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                    table.AddCell(clNombre);
                    table.AddCell(clCodigo);
                    table.AddCell(clCodigoBarras);
                    table.AddCell(clExistencia);
                    table.AddCell(clPrecioVenta);
                    table.AddCell(clPrecioCosto);
                    table.AddCell(clDepartamento);
                });
                // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                doc.Add(table);
                doc.Close();
                writer.Close();
            }
        }

        public void reporteBajoInventario() {
            DateTime dateTime = DateTime.Now;

            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "ReporteBajoInventario_" + dateTime.ToShortDateString().Replace("/", "-") + ".pdf";
            save.Filter = "Formato de documento portátil (PDF) (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK) {
                // Creamos el documento
                Document doc = new Document(PageSize.LETTER);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(save.FileName, FileMode.Create));

                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Reporte Bajo Inventario");
                doc.AddCreator("loginlock22@gmail.com");

                //Abrimos el archivo
                doc.Open();

                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                // Escribimos el encabezamiento en el documento
                Paragraph title = new Paragraph();
                title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f, BaseColor.DARK_GRAY);
                title.Add("Reporte de Bajo Inventario");
                title.Alignment = Element.ALIGN_CENTER;

                Paragraph fecha = new Paragraph();
                fecha.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.DARK_GRAY);
                fecha.Add("Fecha: " + dateTime.ToShortDateString());
                fecha.Alignment = Element.ALIGN_CENTER;

                Paragraph hora = new Paragraph();
                hora.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.DARK_GRAY);
                hora.Add("Hora: " + dateTime.ToShortTimeString());
                hora.Alignment = Element.ALIGN_CENTER;

                doc.Add(title);
                doc.Add(fecha);
                doc.Add(hora);
                doc.Add(Chunk.NEWLINE);

                // Creamos una tabla 
                PdfPTable table = new PdfPTable(7);
                table.WidthPercentage = 100;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                // Configuramos el título de las columnas de la tabla
                PdfPCell clNombre = new PdfPCell(new Phrase("Nombre", _standardFont));
                clNombre.Border = 0;
                clNombre.BorderWidthBottom = 0.75f;
                clNombre.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clCodigo = new PdfPCell(new Phrase("Código", _standardFont));
                clCodigo.Border = 0;
                clCodigo.BorderWidthBottom = 0.75f;
                clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clCodigoBarras = new PdfPCell(new Phrase("Código de Barras", _standardFont));
                clCodigoBarras.Border = 0;
                clCodigoBarras.BorderWidthBottom = 0.75f;
                clCodigoBarras.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clExistencia = new PdfPCell(new Phrase("Existencia", _standardFont));
                clExistencia.Border = 0;
                clExistencia.BorderWidthBottom = 0.75f;
                clExistencia.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clMinInventario = new PdfPCell(new Phrase("Mínimo en Inventario", _standardFont));
                clMinInventario.Border = 0;
                clMinInventario.BorderWidthBottom = 0.75f;
                clMinInventario.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clPrecioVenta = new PdfPCell(new Phrase("Precio Venta", _standardFont));
                clPrecioVenta.Border = 0;
                clPrecioVenta.BorderWidthBottom = 0.75f;
                clPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clDepartamento = new PdfPCell(new Phrase("Departamento", _standardFont));
                clDepartamento.Border = 0;
                clDepartamento.BorderWidthBottom = 0.75f;
                clDepartamento.HorizontalAlignment = Element.ALIGN_CENTER;

                // Añadimos las celdas a la tabla
                table.AddCell(clNombre);
                table.AddCell(clCodigo);
                table.AddCell(clCodigoBarras);
                table.AddCell(clExistencia);
                table.AddCell(clMinInventario);
                table.AddCell(clPrecioVenta);
                table.AddCell(clDepartamento);

                // Llenamos la tabla con información
                List<Producto> productos = _Producto.Where(obj => obj.min_inventario >= obj.existencia).ToList();
                List<Departamentos> departamentos = _Departamentos.ToList();

                productos.ForEach(producto => {
                    string precio_venta = parser.getCentavos(Math.Round(producto.precio, 2).ToString());

                    departamentos.ForEach(departamento => {
                        if (departamento.idDepartamento == producto.departamento) {
                            departamentoColumn = departamento.nombre;
                        }
                    });

                    // Añadimos las celdas a la tabla
                    clNombre = new PdfPCell(new Phrase(producto.nombre, _standardFont));
                    clCodigo = new PdfPCell(new Phrase(producto.codigo, _standardFont));
                    clCodigoBarras = new PdfPCell(new Phrase(producto.codigo_barras, _standardFont));
                    clExistencia = new PdfPCell(new Phrase(producto.existencia.ToString(), _standardFont));
                    clMinInventario = new PdfPCell(new Phrase(producto.min_inventario.ToString(), _standardFont));
                    clPrecioVenta = new PdfPCell(new Phrase(precio_venta, _standardFont));
                    clDepartamento = new PdfPCell(new Phrase(departamentoColumn, _standardFont));

                    clNombre.HorizontalAlignment = Element.ALIGN_CENTER;
                    clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
                    clCodigoBarras.HorizontalAlignment = Element.ALIGN_CENTER;
                    clExistencia.HorizontalAlignment = Element.ALIGN_CENTER;
                    clMinInventario.HorizontalAlignment = Element.ALIGN_CENTER;
                    clPrecioVenta.HorizontalAlignment = Element.ALIGN_CENTER;
                    clDepartamento.HorizontalAlignment = Element.ALIGN_CENTER;

                    // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                    table.AddCell(clNombre);
                    table.AddCell(clCodigo);
                    table.AddCell(clCodigoBarras);
                    table.AddCell(clExistencia);
                    table.AddCell(clMinInventario);
                    table.AddCell(clPrecioVenta);
                    table.AddCell(clDepartamento);
                });
                // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                doc.Add(table);
                doc.Close();
                writer.Close();
            }
        }

        public void reporteMovimientos() {
            List<Producto> productos = _Producto.ToList();

            productos.ForEach(producto => {
                Console.WriteLine(producto.nombre);
            });
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
