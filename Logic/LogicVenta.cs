using Data;
using Data.Model;
using LinqToDB;
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
        List<TempVenta> tempVentas = new List<TempVenta>();
        List<Venta> ventas = new List<Venta>();

        private double total = 0;
        private int count = 0;
        private double items = 0;

        public LogicVenta(ListView listSale, List<Label> labels)
        {
            this.listSale = listSale;
            this.labels = labels;
        }

        public LogicVenta()
        {

        }

        public bool checkDeposito()
        {
            bool res = false;

            try
            {
                List<Corte_Caja> corteCaja = _CorteCaja.ThenByDescending(obj => obj.fecha_corte_fin).ToList();

                corteCaja.ForEach(corte =>
                {
                    if (corte.ganancia == 0)
                    {
                        res = true;
                    }
                });

                return res;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error al conectar con la base de datos, ln 60.", "Error de conección", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return res;
            }
        }

        public void addProduct(string product, double quantity)
        {
            producto = _Producto.Where(obj => obj.codigo.Equals(product)).ToList();

            if (producto.Count() == 1)
            {

                double checkExistencia = 0;

                producto.ForEach(obj =>
                {
                    checkExistencia = obj.existencia;
                });

                if(checkExistencia > 0 && checkExistencia <= quantity)
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

                    tempVentas = _TempVenta.Where(obj => obj.codigo.Equals(product)).ToList();

                    if (tempVentas.Count() > 0)
                    {
                        Console.WriteLine("Ya existe este producto en la venta.");
                        double nuevaCantidad = 0;

                        tempVentas.ForEach(obj =>
                        {
                            nuevaCantidad = quantity + obj.cantidad;
                        });

                        _TempVenta.Where(obj => obj.codigo.Equals(product))
                        .Set(obj => obj.cantidad, nuevaCantidad)
                        .Update();
                    }
                    else
                    {
                        _TempVenta.Value(obj => obj.codigo, product)
                               .Value(obj => obj.total, total)
                               .Value(obj => obj.cantidad, quantity)
                               .Insert();
                    }

                } else
                {
                    MessageBox.Show("Alerta el producto no cuenta con sufuciente existencia en stock para este pedido.", "Alrta de stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            else
            {
                MessageBox.Show("No se ha encontrado un producto con el código: " + product + ", intente con otro código", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void removeProduct()
        {
            try
            {
                listSale.Items.Clear();
                count = 0;
                items = 0;
                total = 0;
                labels[0].Text = "$" + total;
                labels[1].Text = items + " productos en venta actual";
            }catch(Exception e)
            {
                MessageBox.Show("Error al intentar conectar con la base de datos: Logica de venta, ln 107", "Error de conexión", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }
        }

        public void makeSale()
        {
            /**
             * Estado: 0 - Pendiente
             *         1 - Realizada
             *         2 - Cancelada
             */

            _Venta.Value(obj => obj.total, total)
                  .Value(obj => obj.estado, 0)
                  .Value(obj => obj.total_articulos, items)
                  .Insert();

            removeProduct();
        }

        public void completeSale()
        {
            int idVenta = 0;

            using (var db = new Connection())
            {
                var query = from v in db._Venta
                            orderby v.id_venta descending
                            select v;
                ventas = query.ToList();
            }

            idVenta = ventas.First().id_venta;

            tempVentas = _TempVenta.ToList();

            tempVentas.ForEach(obj =>
            {

                double existenciaOld = 0;
                double existenciaNew = 0;

                using (var db = new Connection())
                {
                    var query = from p in db._Producto
                                where p.codigo == obj.codigo
                                select p;
                    producto = query.ToList();
                }

                existenciaOld = producto.First().existencia;

                existenciaNew = existenciaOld - obj.cantidad;


                _VentaProducto.Value(obj2 => obj2.id_venta, idVenta)
                              .Value(obj2 => obj2.id_producto, obj.codigo)
                              .Value(obj2 => obj2.cantidad, obj.cantidad)
                              .Value(obj2 => obj2.total, obj.total)
                              .Insert();


                _Producto.Where(obj2 => obj2.codigo.Equals(obj.codigo))
                           .Set(obj2 => obj2.existencia, existenciaNew)
                           .Update();
            });

            _TempVenta.Delete();

            _Venta.Where(obj => obj.id_venta.Equals(idVenta))
                    .Set(obj => obj.estado, 1)
                    .Update();

            MessageBox.Show("Venta realizada", "Venta exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        
        public void cancelSale()
        {
            int idVenta = 0;

            using (var db = new Connection())
            {
                var query = from v in db._Venta
                            orderby v.id_venta descending
                            select v;
                ventas = query.ToList();
            }

            idVenta = ventas.First().id_venta;

            _TempVenta.Delete();

            _Venta.Where(obj => obj.id_venta.Equals(idVenta))
                    .Set(obj => obj.estado, 2)
                    .Update();

            MessageBox.Show("Venta cancelada", "Cancelación exitosa", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void registerInOut(int tipo, string cantidad)
        {
            try
            {
                _EntradasSalidas.Value(obj => obj.tipo, tipo)
                                .Value(obj => obj.cantidad, Convert.ToDouble(cantidad))
                                .Insert();

                MessageBox.Show("Entrada/Salida registrada con exito", "Registro exitoso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch(Exception e)
            {
                MessageBox.Show("Error al registrar la entrada o salida intente de nuevo, ln 231.", "Error al insertar datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public double getTotalSale()
        {
            double totalSale = 0;

            using (var db = new Connection())
            {
                var query = from v in db._Venta
                            orderby v.id_venta descending
                            select v;
                ventas = query.ToList();
            }

            totalSale = ventas.First().total;

            return totalSale;
        }
    }
}
