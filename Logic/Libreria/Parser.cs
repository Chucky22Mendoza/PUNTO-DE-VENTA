using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Libreria {
    public class Parser {
        public string getCentavos(string precio) {
            if (precio.Contains('.')) {
                string[] splitPrecio = precio.Split('.');
                int centavosLen = splitPrecio[1].Length;
                if (centavosLen == 1) {
                    return "$" + precio + "0";
                } else {
                    return "$" + precio;
                }
            } else {
                return "$" + precio + ".00";
            }
        }
    }
}
