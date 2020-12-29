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
            this.metroCheckBox1 = new MetroFramework.Controls.MetroCheckBox();
            this.metroCheckBox2 = new MetroFramework.Controls.MetroCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtProductCode = new MetroFramework.Controls.MetroTextBox();
            this.SuspendLayout();
            // 
            // metroCheckBox1
            // 
            this.metroCheckBox1.AutoSize = true;
            this.metroCheckBox1.Location = new System.Drawing.Point(75, 69);
            this.metroCheckBox1.Name = "metroCheckBox1";
            this.metroCheckBox1.Size = new System.Drawing.Size(75, 17);
            this.metroCheckBox1.TabIndex = 0;
            this.metroCheckBox1.Text = "Entradas";
            this.metroCheckBox1.UseSelectable = true;
            // 
            // metroCheckBox2
            // 
            this.metroCheckBox2.AutoSize = true;
            this.metroCheckBox2.Location = new System.Drawing.Point(210, 69);
            this.metroCheckBox2.Name = "metroCheckBox2";
            this.metroCheckBox2.Size = new System.Drawing.Size(65, 17);
            this.metroCheckBox2.TabIndex = 1;
            this.metroCheckBox2.Text = "Salidas";
            this.metroCheckBox2.UseSelectable = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(72, 105);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Cantidad";
            // 
            // txtProductCode
            // 
            // 
            // 
            // 
            this.txtProductCode.CustomButton.Image = null;
            this.txtProductCode.CustomButton.Location = new System.Drawing.Point(174, 2);
            this.txtProductCode.CustomButton.Margin = new System.Windows.Forms.Padding(5);
            this.txtProductCode.CustomButton.Name = "";
            this.txtProductCode.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.txtProductCode.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.txtProductCode.CustomButton.TabIndex = 1;
            this.txtProductCode.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.txtProductCode.CustomButton.UseSelectable = true;
            this.txtProductCode.CustomButton.Visible = false;
            this.txtProductCode.Lines = new string[0];
            this.txtProductCode.Location = new System.Drawing.Point(75, 126);
            this.txtProductCode.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductCode.MaxLength = 32767;
            this.txtProductCode.Name = "txtProductCode";
            this.txtProductCode.PasswordChar = '\0';
            this.txtProductCode.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.txtProductCode.SelectedText = "";
            this.txtProductCode.SelectionLength = 0;
            this.txtProductCode.SelectionStart = 0;
            this.txtProductCode.ShortcutsEnabled = true;
            this.txtProductCode.Size = new System.Drawing.Size(200, 28);
            this.txtProductCode.TabIndex = 54;
            this.txtProductCode.UseSelectable = true;
            this.txtProductCode.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.txtProductCode.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // InOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 256);
            this.Controls.Add(this.txtProductCode);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.metroCheckBox2);
            this.Controls.Add(this.metroCheckBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "InOut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Entradas y salidas";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroCheckBox metroCheckBox1;
        private MetroFramework.Controls.MetroCheckBox metroCheckBox2;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroTextBox txtProductCode;
    }
}