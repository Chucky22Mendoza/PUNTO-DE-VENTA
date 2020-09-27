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
        public void setDataGUI() {
            sumaVentaTotales = 0;
            List<Corte_Caja> corteCaja = _CorteCaja
                .ThenByDescending(obj => obj.fecha_corte_inicio).ToList();
            List<Venta_Producto> ventaProductos = _VentaProducto.ToList();
            List<Producto> productos = _Producto.ToList();

            corteCaja.ForEach(corte => {
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

            listLabel[0].Text = "$" + sumaVentaTotales.ToString();
            listLabel[1].Text = "$" + sumaGananciaDia.ToString();
        }
    }
}
