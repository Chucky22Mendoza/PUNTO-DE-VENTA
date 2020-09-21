using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Logic.Libreria {
    public class TextBoxEvent {
        public void textKeyPress(KeyPressEventArgs e) {
            if (char.IsLetter(e.KeyChar)) {
                e.Handled = false;
            } else if (e.KeyChar == Convert.ToChar(Keys.Enter)) {
                e.Handled = true;
            } else if (char.IsControl(e.KeyChar)) {
                e.Handled = false;
            } else if (char.IsSeparator(e.KeyChar)) {
                e.Handled = false;
            } else {
                e.Handled = true;
            }
        }

        public void numberKeyPress(KeyPressEventArgs e) {
            if (char.IsDigit(e.KeyChar)) {
                e.Handled = false;
            } else if (e.KeyChar == Convert.ToChar(Keys.Enter)) {
                e.Handled = true;
            } else if (char.IsControl(e.KeyChar)) {
                e.Handled = false;
            } else if (char.IsSeparator(e.KeyChar)) {
                e.Handled = true;
            } else {
                e.Handled = true;
            }
        }

        public bool validateEmailFormat(string email) {
            return new EmailAddressAttribute().IsValid(email);
        }
    }
}
