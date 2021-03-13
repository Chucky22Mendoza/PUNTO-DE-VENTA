using Data;
using Data.Model;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using LinqToDB;
using LinqToDB.Data;
using LinqToDB.SqlQuery;
using Logic.Libreria;
using Logic.Model;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Logic {
    public class LogicCorte : Librerias {
        private List<Label> listLabel;
        private MetroGrid tblDepartamento;
        private MetroGrid tblProductosVentas;
        private MetroButton btnImprimir;
        private MetroButton btnCorte;
        private MetroButton btnInicioCorte;

        public LogicCorte() {

        }

        public LogicCorte(List<Label> listLabel, MetroGrid tblDepartamento, MetroGrid tblProductosVentas, MetroButton btnImprimir, MetroButton btnCorte, MetroButton btnInicioCorte) {
            this.listLabel = listLabel;
            this.tblDepartamento = tblDepartamento;
            this.tblProductosVentas = tblProductosVentas;
            this.btnImprimir = btnImprimir;
            this.btnCorte = btnCorte;
            this.btnInicioCorte = btnInicioCorte;
            limpiarCampos();
        }

        private void limpiarCampos() {
            listLabel.ForEach(lbl => {
                lbl.Text = "$0.00";
            });
        }

        public void index() {
            Corte_Caja corteCaja;
            try {
                int contador = _CorteCaja.Count();
                
                if (contador > 0) {
                    corteCaja = _CorteCaja.ThenByDescending(obj => obj.fecha_corte_inicio).First();
                    
                    if (corteCaja.ganancia == 0) {
                        listLabel[9].Text = "";
                        btnInicioCorte.Enabled = false;
                        btnImprimir.Enabled = false;
                        btnCorte.Enabled = true;
                    } else {
                        listLabel[9].Text = "Favor de registrar el dinero inicial en caja";
                        btnInicioCorte.Enabled = true;
                        btnImprimir.Enabled = true;
                        btnCorte.Enabled = false;
                    }
                } else {
                    listLabel[9].Text = "Favor de registrar el dinero inicial en caja";
                    btnInicioCorte.Enabled = true;
                    btnImprimir.Enabled = false;
                    btnCorte.Enabled = false;
                }
            } catch(SqlException e) {
                Console.WriteLine(e);
                MessageBox.Show("Error al intentar conectar con la base de datos: Logica de corte, ln 71", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void registrarInicioCorte(String cantidad) {
            try {
                _CorteCaja.Value(obj => obj.dinero_inicial, Convert.ToDouble(cantidad)).Insert();

                MessageBox.Show("Dinero Inicial registrado con exito", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            } catch (SqlException e) {
                MessageBox.Show("Error al registrar el dinero incial intente de nuevo, ln 82.", "Error al insertar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        double sumaVentaTotales, sumaGananciaDia, dineroInicialCaja, sumaEntradas, sumaSalidas;
        int idCorte;
        List<DepartamentoVenta> listDepartamentos;
        List<ProductoVenta> listProductos;
        public void setDataGUI() {
            sumaVentaTotales = 0;
            sumaGananciaDia = 0;
            dineroInicialCaja = 0;
            sumaEntradas = 0;
            sumaSalidas = 0;
            idCorte = 0;
            listDepartamentos = new List<DepartamentoVenta>();
            listProductos = new List<ProductoVenta>();

            DateTime dateTime = DateTime.Now;

            Corte_Caja corteCaja = _CorteCaja.ThenByDescending(obj => obj.fecha_corte_inicio).First();
            List<Venta_Producto> ventaProductos = _VentaProducto.ToList();
            List<Producto> productos = _Producto.ToList();
            List<Departamentos> departamentos = _Departamentos.ToList();
            List<Venta> ventas = _Venta.ToList();
            List<Entradas_Salidas> entradas_salidas = _EntradasSalidas.ToList();

            dineroInicialCaja = corteCaja.dinero_inicial;
            idCorte = corteCaja.id_corte_caja;

            using (var db = new Connection()) {
                var queryVentasTotales = db.QueryProc<double>("venta_totales",
                    new DataParameter("fecha_hora", corteCaja.fecha_corte_inicio)
                );

                sumaVentaTotales = queryVentasTotales.ToList()[0];
            }
            
            using (var db = new Connection()) {
                var queryGanancias = db.QueryProc<double>("ganancias",
                    new DataParameter("fecha_hora", corteCaja.fecha_corte_inicio)
                );

                sumaGananciaDia = queryGanancias.ToList()[0];
            }

            using (var db = new Connection()) {
                var queryVentasPorProducto = db.QueryProc<ProductoVenta>("ventas_por_producto",
                    new DataParameter("fecha_hora", corteCaja.fecha_corte_inicio)
                );

                listProductos = queryVentasPorProducto.ToList();
            }

            using (var db = new Connection()) {
                var queryVentasPorDepartamento = db.QueryProc<DepartamentoVenta>("ventas_por_departamento",
                    new DataParameter("fecha_hora", corteCaja.fecha_corte_inicio)
                );

                listDepartamentos = queryVentasPorDepartamento.ToList();
            }

            entradas_salidas.ForEach(io => {
                if (io.date_create > corteCaja.fecha_corte_inicio) {
                    if (io.tipo == 0) {
                        sumaEntradas += io.cantidad;
                    } else if (io.tipo == 1) {
                        sumaSalidas += io.cantidad;
                    }
                }
            });

            listDepartamentos.ForEach(departamento => {
                departamento.ventasStr = parser.getCentavos(Math.Round(departamento.total_departamento, 2).ToString());
            });

            listProductos.ForEach(producto => {
                producto.ventasStr = parser.getCentavos(Math.Round(producto.total_producto, 2).ToString());
            });

            double totalDineroCaja = sumaVentaTotales + (sumaEntradas - sumaSalidas);
            double sumaEntradaEfectivo = sumaVentaTotales + dineroInicialCaja;
            
            totalDineroCaja = Math.Round(totalDineroCaja, 2);
            sumaVentaTotales = Math.Round(sumaVentaTotales, 2);
            sumaGananciaDia = Math.Round(sumaGananciaDia, 2);
            dineroInicialCaja = Math.Round(dineroInicialCaja, 2);
            sumaEntradaEfectivo = Math.Round(sumaEntradaEfectivo, 2);
            sumaEntradas = Math.Round(sumaEntradas, 2);
            sumaSalidas = Math.Round(sumaSalidas, 2);

            listLabel[0].Text = parser.getCentavos(sumaVentaTotales.ToString());
            listLabel[1].Text = parser.getCentavos(sumaGananciaDia.ToString());
            listLabel[2].Text = parser.getCentavos(sumaVentaTotales.ToString());
            listLabel[3].Text = parser.getCentavos(dineroInicialCaja.ToString());
            listLabel[4].Text = parser.getCentavos(sumaEntradaEfectivo.ToString());
            listLabel[5].Text = parser.getCentavos(sumaVentaTotales.ToString());
            listLabel[6].Text = parser.getCentavos(sumaEntradas.ToString());
            listLabel[7].Text = parser.getCentavos(sumaSalidas.ToString());
            listLabel[8].Text = parser.getCentavos(totalDineroCaja.ToString());

            List<DepartamentoVenta> sortedListDepartamentos = listDepartamentos.OrderBy(obj => obj.total_departamento).ToList();

            tblDepartamento.DataSource = sortedListDepartamentos;

            tblDepartamento.Columns[1].DefaultCellStyle.BackColor = Color.White;
            tblDepartamento.Columns[1].DefaultCellStyle.ForeColor = Color.Black;
            tblDepartamento.Columns[1].DefaultCellStyle.SelectionForeColor = Color.White;
            tblDepartamento.Columns[1].DefaultCellStyle.SelectionBackColor = Color.Gray;
            tblDepartamento.Columns[1].Width = 120;

            tblDepartamento.Columns[3].DefaultCellStyle.BackColor = Color.White;
            tblDepartamento.Columns[3].DefaultCellStyle.ForeColor = Color.Black;
            tblDepartamento.Columns[3].DefaultCellStyle.SelectionForeColor = Color.White;
            tblDepartamento.Columns[3].DefaultCellStyle.SelectionBackColor = Color.Gray;
            tblDepartamento.Columns[3].Width = 70;

            tblDepartamento.Columns[0].Visible = false;
            tblDepartamento.Columns[2].Visible = false;

            tblDepartamento.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            tblDepartamento.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tblDepartamento.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DimGray;
            tblDepartamento.RowHeadersWidth = 25;

            List<ProductoVenta> sortedListProductos = listProductos.OrderBy(obj => obj.total_producto).ToList();

            tblProductosVentas.DataSource = sortedListProductos;

            tblProductosVentas.Columns[1].DefaultCellStyle.BackColor = Color.White;
            tblProductosVentas.Columns[1].DefaultCellStyle.ForeColor = Color.Black;
            tblProductosVentas.Columns[1].DefaultCellStyle.SelectionForeColor = Color.White;
            tblProductosVentas.Columns[1].DefaultCellStyle.SelectionBackColor = Color.Gray;
            tblProductosVentas.Columns[1].Width = 120;

            tblProductosVentas.Columns[2].DefaultCellStyle.BackColor = Color.White;
            tblProductosVentas.Columns[2].DefaultCellStyle.ForeColor = Color.Black;
            tblProductosVentas.Columns[2].DefaultCellStyle.SelectionForeColor = Color.White;
            tblProductosVentas.Columns[2].DefaultCellStyle.SelectionBackColor = Color.Gray;
            tblProductosVentas.Columns[2].Width = 100;

            tblProductosVentas.Columns[4].DefaultCellStyle.BackColor = Color.White;
            tblProductosVentas.Columns[4].DefaultCellStyle.ForeColor = Color.Black;
            tblProductosVentas.Columns[4].DefaultCellStyle.SelectionForeColor = Color.White;
            tblProductosVentas.Columns[4].DefaultCellStyle.SelectionBackColor = Color.Gray;
            tblProductosVentas.Columns[4].Width = 70;

            tblProductosVentas.Columns[0].Visible = false;
            tblProductosVentas.Columns[3].Visible = false;

            tblProductosVentas.ColumnHeadersDefaultCellStyle.BackColor = Color.Black;
            tblProductosVentas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            tblProductosVentas.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.DimGray;
            tblProductosVentas.RowHeadersWidth = 25;

            BeginTransactionAsync();
            try {
                _CorteCaja.Where(obj => obj.id_corte_caja == idCorte)
                    .Set(obj => obj.fecha_corte_fin, dateTime)
                    .Set(obj => obj.ganancia, sumaGananciaDia)
                    .Set(obj => obj.ventas_totales, sumaVentaTotales)
                    .Update();
                
                MessageBox.Show("Corte de caja realizado correctamente", "Información", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                CommitTransaction();
                index();
            } catch (SqlException) {
                MessageBox.Show("Error al registrar corte caja intente de nuevo, ln 217.", "Error al insertar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
                RollbackTransaction();
            }
        }

        public void imprimirCorte() {
            DateTime dateTime = DateTime.Now;

            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "CorteCaja_" + dateTime.ToShortDateString().Replace("/", "-") + ".pdf";
            save.Filter = "Formato de documento portátil (PDF) (*.pdf)|*.pdf|Todos los archivos (*.*)|*.*";

            if (save.ShowDialog() == DialogResult.OK) {
                // Creamos el documento
                Document doc = new Document(PageSize.LETTER);
                // Indicamos donde vamos a guardar el documento
                PdfWriter writer = PdfWriter.GetInstance(doc, new FileStream(save.FileName, FileMode.Create));

                // Le colocamos el título y el autor
                // **Nota: Esto no será visible en el documento
                doc.AddTitle("Corte de Caja");
                doc.AddCreator("loginlock22@gmail.com");

                //Abrimos el archivo
                doc.Open();

                // Creamos el tipo de Font que vamos utilizar
                iTextSharp.text.Font _standardFont = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                // Escribimos el encabezamiento en el documento
                Paragraph title = new Paragraph();
                title.Font = FontFactory.GetFont(FontFactory.TIMES, 28f, BaseColor.DARK_GRAY);
                title.Add("Reporte de Corte de Caja");
                title.Alignment = Element.ALIGN_CENTER;

                Paragraph fecha = new Paragraph();
                fecha.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.DARK_GRAY);
                fecha.Add("Fecha: " + dateTime.ToShortDateString());
                fecha.Alignment = Element.ALIGN_CENTER;

                Paragraph hora = new Paragraph();
                hora.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.DARK_GRAY);
                hora.Add("Hora: " + dateTime.ToShortTimeString());
                hora.Alignment = Element.ALIGN_CENTER;
                
                Paragraph paragraph1 = new Paragraph();
                paragraph1.Font = FontFactory.GetFont(FontFactory.TIMES, 16f, BaseColor.BLACK);
                paragraph1.Add("Ventas Totales: " + listLabel[0].Text + "           Ganancia del día: " + listLabel[1].Text);
                paragraph1.Alignment = Element.ALIGN_CENTER;

                Paragraph paragraph3 = new Paragraph();
                paragraph3.Font = FontFactory.GetFont(FontFactory.TIMES, 16f, BaseColor.BLACK);
                paragraph3.Add("Entradas en Efectivo");

                Paragraph paragraph4 = new Paragraph();
                paragraph4.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.GREEN);
                paragraph4.Add("Entrada de Dinero (Cambio): " + listLabel[2].Text);

                Paragraph paragraph5 = new Paragraph();
                paragraph5.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.RED);
                paragraph5.Add("Dinero Inicial en Caja: " + listLabel[3].Text);

                Paragraph paragraph6 = new Paragraph();
                paragraph6.Font = FontFactory.GetFont(FontFactory.TIMES, 14f, BaseColor.BLACK);
                paragraph6.Add("Total: " + listLabel[4].Text);

                Paragraph paragraph7 = new Paragraph();
                paragraph7.Font = FontFactory.GetFont(FontFactory.TIMES, 16f, BaseColor.BLACK);
                paragraph7.Add("Dinero en Caja");

                Paragraph paragraph8 = new Paragraph();
                paragraph8.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.GREEN);
                paragraph8.Add("Ventas en efectivo: " + listLabel[5].Text);

                Paragraph paragraph9 = new Paragraph();
                paragraph9.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.GREEN);
                paragraph9.Add("Entradas: " + listLabel[6].Text);

                Paragraph paragraph10 = new Paragraph();
                paragraph10.Font = FontFactory.GetFont(FontFactory.TIMES, 12f, BaseColor.RED);
                paragraph10.Add("Salidas: " + listLabel[7].Text);

                Paragraph paragraph11 = new Paragraph();
                paragraph11.Font = FontFactory.GetFont(FontFactory.TIMES, 14f, BaseColor.BLACK);
                paragraph11.Add("Total: " + listLabel[8].Text);

                doc.Add(title);
                doc.Add(fecha);
                doc.Add(hora);
                doc.Add(Chunk.NEWLINE);
                doc.Add(paragraph1);
                doc.Add(Chunk.NEWLINE);
                doc.Add(paragraph3);
                doc.Add(paragraph4);
                doc.Add(paragraph5);
                doc.Add(Chunk.NEWLINE);
                doc.Add(paragraph6);
                doc.Add(Chunk.NEWLINE);
                doc.Add(paragraph7);
                doc.Add(paragraph8);
                doc.Add(paragraph9);
                doc.Add(paragraph10);
                doc.Add(Chunk.NEWLINE);
                doc.Add(paragraph11);
                doc.Add(Chunk.NEWLINE);

                // Creamos una tabla 
                PdfPTable table = new PdfPTable(2);
                table.WidthPercentage = 50;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                // Configuramos el título de las columnas de la tabla
                PdfPCell clDepartamento = new PdfPCell(new Phrase("Departamento", _standardFont));
                clDepartamento.Border = 0;
                clDepartamento.BorderWidthBottom = 0.75f;
                clDepartamento.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clVentas = new PdfPCell(new Phrase("Ventas", _standardFont));
                clVentas.Border = 0;
                clVentas.BorderWidthBottom = 0.75f;
                clVentas.HorizontalAlignment = Element.ALIGN_CENTER;

                // Añadimos las celdas a la tabla
                table.AddCell(clDepartamento);
                table.AddCell(clVentas);

                // Llenamos la tabla con información

                List<DepartamentoVenta> departamento = (List<DepartamentoVenta>) tblDepartamento.DataSource;
                
                departamento.ForEach(element => {
                    // Añadimos las celdas a la tabla
                    clDepartamento = new PdfPCell(new Phrase(element.nombre, _standardFont));
                    clVentas = new PdfPCell(new Phrase(element.ventasStr, _standardFont));

                    clDepartamento.HorizontalAlignment = Element.ALIGN_CENTER;
                    clVentas.HorizontalAlignment = Element.ALIGN_CENTER;

                    // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                    table.AddCell(clDepartamento);
                    table.AddCell(clVentas);
                });

                // Creamos una tabla 
                PdfPTable tableP = new PdfPTable(2);
                table.WidthPercentage = 50;
                table.HorizontalAlignment = Element.ALIGN_CENTER;

                // Configuramos el título de las columnas de la tabla
                PdfPCell clProd = new PdfPCell(new Phrase("Producto", _standardFont));
                clProd.Border = 0;
                clProd.BorderWidthBottom = 0.75f;
                clProd.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clCodigo = new PdfPCell(new Phrase("Código", _standardFont));
                clCodigo.Border = 0;
                clCodigo.BorderWidthBottom = 0.75f;
                clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;

                PdfPCell clVentasProd = new PdfPCell(new Phrase("Ventas", _standardFont));
                clVentasProd.Border = 0;
                clVentasProd.BorderWidthBottom = 0.75f;
                clVentasProd.HorizontalAlignment = Element.ALIGN_CENTER;

                // Añadimos las celdas a la tabla
                tableP.AddCell(clProd);
                tableP.AddCell(clCodigo);
                tableP.AddCell(clVentasProd);

                // Llenamos la tabla con información

                List<ProductoVenta> producto = (List<ProductoVenta>)tblProductosVentas.DataSource;

                producto.ForEach(element => {
                    // Añadimos las celdas a la tabla
                    clProd = new PdfPCell(new Phrase(element.nombre, _standardFont));
                    clCodigo = new PdfPCell(new Phrase(element.codigo, _standardFont));
                    clVentasProd = new PdfPCell(new Phrase(element.ventasStr, _standardFont));

                    clProd.HorizontalAlignment = Element.ALIGN_CENTER;
                    clCodigo.HorizontalAlignment = Element.ALIGN_CENTER;
                    clVentasProd.HorizontalAlignment = Element.ALIGN_CENTER;

                    // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                    tableP.AddCell(clProd);
                    tableP.AddCell(clCodigo);
                    tableP.AddCell(clVentasProd);
                });

                // Finalmente, añadimos la tabla al documento PDF y cerramos el documento
                doc.Add(table);
                doc.Add(Chunk.NEWLINE);
                doc.Add(Chunk.NEWLINE);
                doc.Add(tableP);
                doc.Close();
                writer.Close();
            }
        }
    }
}
