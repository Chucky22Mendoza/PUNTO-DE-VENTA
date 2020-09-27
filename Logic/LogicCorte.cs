using Data;
using Data.Model;
using LinqToDB;
using LinqToDB.Common;
using Logic.Libreria;
using MetroFramework.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic {
    public class LogicCorte : Librerias {
        private List<Label> listLabel;
        private MetroGrid tblDepartamento;

        public LogicCorte(List<Label> listLabel, MetroGrid tblDepartamento) {
            this.listLabel = listLabel;
            this.tblDepartamento = tblDepartamento;
            limpiarCampos();
        }

        private void limpiarCampos() {
            listLabel.ForEach(lbl => {
                lbl.Text = "$0.00";
            });
            tblDepartamento.Rows.Clear();
        }

        double sumaVentaTotales;
        double sumaGananciaDia;
        double dineroInicialCaja;
        public void setDataGUI() {
            sumaVentaTotales = 0;
            sumaGananciaDia = 0;

            List<Corte_Caja> corteCaja = _CorteCaja
                .ThenByDescending(obj => obj.fecha_corte_inicio).ToList();
            List<Venta_Producto> ventaProductos = _VentaProducto.ToList();
            List<Producto> productos = _Producto.ToList();

            corteCaja.ForEach(corte => {
                dineroInicialCaja = corte.dinero_inicial;
                ventaProductos.ForEach(venta => {
                    if(venta.fecha_registro > corte.fecha_corte_inicio) {
                        sumaVentaTotales += venta.total;
                        productos.ForEach(producto => {
                            if (producto.id_producto == venta.id_producto) {
                                sumaGananciaDia += (producto.precio - producto.precio_costo) * venta.cantidad;
                            }
                        });
                    }
                });
            });

            sumaVentaTotales = Math.Round(sumaVentaTotales, 2);
            sumaGananciaDia = Math.Round(sumaGananciaDia, 2);
            dineroInicialCaja = Math.Round(dineroInicialCaja, 2);

            listLabel[0].Text = parser.getCentavos(sumaVentaTotales.ToString());
            listLabel[1].Text = parser.getCentavos(sumaGananciaDia.ToString());
            listLabel[3].Text = parser.getCentavos(dineroInicialCaja.ToString());
        }

        public void imprimirCorte() {

        }
    }
}
