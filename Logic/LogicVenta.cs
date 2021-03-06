﻿using Data;
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

        public void addProduct(string product, double quantity)
        {

                producto = _Producto.Where(obj => obj.codigo.Equals(product)).ToList();

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

                tempVentas =  _TempVenta.Where(obj => obj.codigo.Equals(product)).ToList();

                if(tempVentas.Count() > 0)
                {
                    Console.WriteLine("ya existe este productoi en la venta");
                    double nuevaCantidad = 0;

                    tempVentas.ForEach(obj =>
                    {
                        Console.WriteLine(nuevaCantidad);
                        nuevaCantidad = quantity + obj.cantidad;
                        Console.WriteLine(nuevaCantidad);
                    });

                    //Console.WriteLine(nuevaCantidad);

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

                

            }
            else
            {
                MessageBox.Show("No se ha encontrado un producto con el código: " + product + ", intente con otro código", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void removeProduct()
        {
            _TempVenta.Delete();
            listSale.Items.Clear();
            count = 0;
            items = 0;
            total = 0;
            labels[0].Text = "$" + total;
            labels[1].Text = items + " productos en venta actual";
        }

        public void makeSale()
        {
            int idVenta = 0;
            /**
             * Estado: 0 - Pendiente
             *         1 - Realizada
             *         2 - Cancelada
             */

            _Venta.Value(obj => obj.total, total)
                  .Value(obj => obj.estado, 0)
                  .Value(obj => obj.total_articulos, items)
                  .Insert();

            using (var db = new Connection())
            {
                var query = from v in db._Venta
                            orderby v.id_venta descending
                            select v;
                ventas = query.ToList();
            }

            idVenta = ventas.First().id_venta;

            Console.WriteLine(idVenta);

            tempVentas = _TempVenta.ToList();

            tempVentas.ForEach(obj =>
            {
                _VentaProducto.Value(obj2 => obj2.id_venta, idVenta)
                              .Value(obj2 => obj2.id_producto, obj.codigo)
                              .Value(obj2 => obj2.cantidad, obj.cantidad)
                              .Value(obj2 => obj2.total, obj.total)
                              .Insert();
            });

            removeProduct();


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
