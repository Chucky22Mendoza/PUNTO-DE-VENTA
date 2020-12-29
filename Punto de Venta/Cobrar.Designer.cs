namespace Punto_de_Venta
{
    partial class Cobrar
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
            this.btnOK = new System.Windows.Forms.Button();
            this.txtEfectivo = new System.Windows.Forms.TextBox();
            this.lblCambio = new System.Windows.Forms.Label();
            this.lblEfectivo = new System.Windows.Forms.Label();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalSale = new System.Windows.Forms.Label();
            this.lblCash = new System.Windows.Forms.Label();
            this.btnCancelSale = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BackColor = System.Drawing.Color.Teal;
            this.btnOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOK.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnOK.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOK.Location = new System.Drawing.Point(192, 295);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(117, 54);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "Aceptar";
            this.btnOK.UseVisualStyleBackColor = false;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // txtEfectivo
            // 
            this.txtEfectivo.Location = new System.Drawing.Point(94, 157);
            this.txtEfectivo.Margin = new System.Windows.Forms.Padding(4);
            this.txtEfectivo.Name = "txtEfectivo";
            this.txtEfectivo.Size = new System.Drawing.Size(157, 22);
            this.txtEfectivo.TabIndex = 11;
            this.txtEfectivo.TextChanged += new System.EventHandler(this.txtEfectivo_TextChanged);
            this.txtEfectivo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtEfectivo_KeyPress);
            // 
            // lblCambio
            // 
            this.lblCambio.AutoSize = true;
            this.lblCambio.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblCambio.Location = new System.Drawing.Point(127, 192);
            this.lblCambio.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCambio.Name = "lblCambio";
            this.lblCambio.Size = new System.Drawing.Size(95, 23);
            this.lblCambio.TabIndex = 9;
            this.lblCambio.Text = "Cambio:";
            // 
            // lblEfectivo
            // 
            this.lblEfectivo.AutoSize = true;
            this.lblEfectivo.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold);
            this.lblEfectivo.Location = new System.Drawing.Point(129, 121);
            this.lblEfectivo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblEfectivo.Name = "lblEfectivo";
            this.lblEfectivo.Size = new System.Drawing.Size(93, 23);
            this.lblEfectivo.TabIndex = 8;
            this.lblEfectivo.Text = "Efectivo:";
            // 
            // lblTotal
            // 
            this.lblTotal.AutoSize = true;
            this.lblTotal.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.Location = new System.Drawing.Point(82, 24);
            this.lblTotal.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(190, 23);
            this.lblTotal.TabIndex = 7;
            this.lblTotal.Text = "Cantidad a pagar:";
            // 
            // lblTotalSale
            // 
            this.lblTotalSale.AutoSize = true;
            this.lblTotalSale.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotalSale.Location = new System.Drawing.Point(115, 64);
            this.lblTotalSale.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTotalSale.Name = "lblTotalSale";
            this.lblTotalSale.Size = new System.Drawing.Size(107, 39);
            this.lblTotalSale.TabIndex = 27;
            this.lblTotalSale.Text = "$0.00";
            // 
            // lblCash
            // 
            this.lblCash.AutoSize = true;
            this.lblCash.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCash.Location = new System.Drawing.Point(115, 224);
            this.lblCash.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCash.Name = "lblCash";
            this.lblCash.Size = new System.Drawing.Size(107, 39);
            this.lblCash.TabIndex = 28;
            this.lblCash.Text = "$0.00";
            // 
            // btnCancelSale
            // 
            this.btnCancelSale.BackColor = System.Drawing.Color.Red;
            this.btnCancelSale.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelSale.Font = new System.Drawing.Font("Century Gothic", 9.75F);
            this.btnCancelSale.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnCancelSale.Location = new System.Drawing.Point(58, 295);
            this.btnCancelSale.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancelSale.Name = "btnCancelSale";
            this.btnCancelSale.Size = new System.Drawing.Size(117, 54);
            this.btnCancelSale.TabIndex = 29;
            this.btnCancelSale.Text = "Cancelar";
            this.btnCancelSale.UseVisualStyleBackColor = false;
            this.btnCancelSale.Click += new System.EventHandler(this.btnCancelSale_Click);
            // 
            // Cobrar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(358, 375);
            this.Controls.Add(this.btnCancelSale);
            this.Controls.Add(this.lblCash);
            this.Controls.Add(this.lblTotalSale);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtEfectivo);
            this.Controls.Add(this.lblCambio);
            this.Controls.Add(this.lblEfectivo);
            this.Controls.Add(this.lblTotal);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Cobrar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cobrar";
            this.Load += new System.EventHandler(this.Cobrar_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox txtEfectivo;
        private System.Windows.Forms.Label lblCambio;
        private System.Windows.Forms.Label lblEfectivo;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalSale;
        private System.Windows.Forms.Label lblCash;
        private System.Windows.Forms.Button btnCancelSale;
    }
}