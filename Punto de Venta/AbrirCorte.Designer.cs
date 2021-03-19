namespace Punto_de_Venta {
    partial class AbrirCorte {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AbrirCorte));
            this.txtCantidadCaja = new System.Windows.Forms.TextBox();
            this.btnRegistrarCaja = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtCantidadCaja
            // 
            this.txtCantidadCaja.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadCaja.Location = new System.Drawing.Point(15, 89);
            this.txtCantidadCaja.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCantidadCaja.Multiline = true;
            this.txtCantidadCaja.Name = "txtCantidadCaja";
            this.txtCantidadCaja.Size = new System.Drawing.Size(320, 32);
            this.txtCantidadCaja.TabIndex = 63;
            this.txtCantidadCaja.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadCaja_KeyPress);
            // 
            // btnRegistrarCaja
            // 
            this.btnRegistrarCaja.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnRegistrarCaja.Location = new System.Drawing.Point(109, 188);
            this.btnRegistrarCaja.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRegistrarCaja.Name = "btnRegistrarCaja";
            this.btnRegistrarCaja.Size = new System.Drawing.Size(128, 39);
            this.btnRegistrarCaja.TabIndex = 62;
            this.btnRegistrarCaja.Text = "Registrar";
            this.btnRegistrarCaja.UseSelectable = true;
            this.btnRegistrarCaja.Click += new System.EventHandler(this.btnRegistrarCaja_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(104, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(123, 25);
            this.label1.TabIndex = 61;
            this.label1.Text = "Dinero Inicial";
            // 
            // AbrirCorte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 261);
            this.Controls.Add(this.txtCantidadCaja);
            this.Controls.Add(this.btnRegistrarCaja);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AbrirCorte";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCantidadCaja;
        private MetroFramework.Controls.MetroButton btnRegistrarCaja;
        private System.Windows.Forms.Label label1;
    }
}