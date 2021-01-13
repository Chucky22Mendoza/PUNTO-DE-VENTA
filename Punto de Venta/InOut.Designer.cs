namespace Punto_de_Venta
{
    partial class InOut
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InOut));
            this.label1 = new System.Windows.Forms.Label();
            this.rbIn = new MetroFramework.Controls.MetroRadioButton();
            this.rbOut = new MetroFramework.Controls.MetroRadioButton();
            this.btnRegistrarInOut = new MetroFramework.Controls.MetroButton();
            this.txtCantidadInOut = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cantidad";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // rbIn
            // 
            this.rbIn.AutoSize = true;
            this.rbIn.BackColor = System.Drawing.Color.Transparent;
            this.rbIn.Checked = true;
            this.rbIn.Location = new System.Drawing.Point(75, 71);
            this.rbIn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbIn.Name = "rbIn";
            this.rbIn.Size = new System.Drawing.Size(69, 17);
            this.rbIn.TabIndex = 55;
            this.rbIn.TabStop = true;
            this.rbIn.Text = "Entrada";
            this.rbIn.UseSelectable = true;
            // 
            // rbOut
            // 
            this.rbOut.AutoSize = true;
            this.rbOut.Location = new System.Drawing.Point(205, 71);
            this.rbOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.rbOut.Name = "rbOut";
            this.rbOut.Size = new System.Drawing.Size(59, 17);
            this.rbOut.TabIndex = 56;
            this.rbOut.Text = "Salida";
            this.rbOut.UseSelectable = true;
            // 
            // btnRegistrarInOut
            // 
            this.btnRegistrarInOut.BackColor = System.Drawing.SystemColors.Desktop;
            this.btnRegistrarInOut.Location = new System.Drawing.Point(100, 181);
            this.btnRegistrarInOut.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRegistrarInOut.Name = "btnRegistrarInOut";
            this.btnRegistrarInOut.Size = new System.Drawing.Size(151, 39);
            this.btnRegistrarInOut.TabIndex = 59;
            this.btnRegistrarInOut.Text = "Registrar";
            this.btnRegistrarInOut.UseSelectable = true;
            this.btnRegistrarInOut.Click += new System.EventHandler(this.btnRegistrarInOut_Click);
            // 
            // txtCantidadInOut
            // 
            this.txtCantidadInOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadInOut.Location = new System.Drawing.Point(75, 126);
            this.txtCantidadInOut.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtCantidadInOut.Multiline = true;
            this.txtCantidadInOut.Name = "txtCantidadInOut";
            this.txtCantidadInOut.Size = new System.Drawing.Size(191, 32);
            this.txtCantidadInOut.TabIndex = 60;
            this.txtCantidadInOut.TextChanged += new System.EventHandler(this.txtCantidadInOut_TextChanged);
            this.txtCantidadInOut.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtCantidadInOut_KeyPress_1);
            // 
            // InOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 256);
            this.Controls.Add(this.txtCantidadInOut);
            this.Controls.Add(this.btnRegistrarInOut);
            this.Controls.Add(this.rbOut);
            this.Controls.Add(this.rbIn);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "InOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroRadioButton rbIn;
        private MetroFramework.Controls.MetroRadioButton rbOut;
        private MetroFramework.Controls.MetroButton btnRegistrarInOut;
        private System.Windows.Forms.TextBox txtCantidadInOut;
    }
}