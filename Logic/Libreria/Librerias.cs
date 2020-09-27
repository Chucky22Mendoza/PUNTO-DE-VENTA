using Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Libreria {
    public class Librerias : Connection {
        public CargarImagen cargarImagen = new CargarImagen();
        public TextBoxEvent textBoxEvent = new TextBoxEvent();
        public Parser parser = new Parser();
    }
}
