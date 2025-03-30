
using ManuelWindowsControls;
using System;

namespace TestInputTextBox
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.decimalTextBox1 = new ManuelWindowsControls.DecimalTextBox();
            this.integerTextBox1 = new ManuelWindowsControls.IntegerTextBox();
            this.customTextBox1 = new ManuelWindowsControls.CustomTextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(102, 32);
            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(148, 26);
            this.textBox1.TabIndex = 1;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(102, 112);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(148, 26);
            this.textBox2.TabIndex = 3;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(102, 194);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(148, 26);
            this.textBox3.TabIndex = 5;
            // 
            // decimalTextBox1
            // 
            this.decimalTextBox1.AcceptsReturn = true;
            this.decimalTextBox1.ActivarPuntoMiles = true;
            this.decimalTextBox1.ColorFondoFoco = System.Drawing.Color.Lavender;
            this.decimalTextBox1.ColorTextoFoco = System.Drawing.Color.Black;
            this.decimalTextBox1.DecimalPlaces = 4;
            this.decimalTextBox1.Location = new System.Drawing.Point(102, 154);
            this.decimalTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.decimalTextBox1.MaxValue = new decimal(new int[] {
            1952020,
            0,
            0,
            131072});
            this.decimalTextBox1.MinValue = new decimal(new int[] {
            -1,
            -1,
            -1,
            -2147483648});
            this.decimalTextBox1.Name = "decimalTextBox1";
            this.decimalTextBox1.Size = new System.Drawing.Size(148, 26);
            this.decimalTextBox1.TabIndex = 6;
            this.decimalTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // integerTextBox1
            // 
            this.integerTextBox1.ColorFondoFoco = System.Drawing.Color.LightYellow;
            this.integerTextBox1.ColorTextoFoco = System.Drawing.Color.Black;
            this.integerTextBox1.Location = new System.Drawing.Point(102, 72);
            this.integerTextBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.integerTextBox1.MaxValue = 10;
            this.integerTextBox1.MinValue = 1;
            this.integerTextBox1.Name = "integerTextBox1";
            this.integerTextBox1.Size = new System.Drawing.Size(148, 26);
            this.integerTextBox1.TabIndex = 2;
            this.integerTextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // customTextBox1
            // 
            this.customTextBox1.ColorFondoFoco = System.Drawing.Color.LightYellow;
            this.customTextBox1.ColorTextoFoco = System.Drawing.Color.Black;
            this.customTextBox1.Location = new System.Drawing.Point(102, 243);
            this.customTextBox1.Mayusculas = false;
            this.customTextBox1.Minusculas = false;
            this.customTextBox1.Name = "customTextBox1";
            this.customTextBox1.Size = new System.Drawing.Size(147, 26);
            this.customTextBox1.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.customTextBox1);
            this.Controls.Add(this.decimalTextBox1);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.integerTextBox1);
            this.Controls.Add(this.textBox1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBox1;
        private ManuelWindowsControls.IntegerTextBox integerTextBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private ManuelWindowsControls.DecimalTextBox decimalTextBox1;
        private CustomTextBox customTextBox1;
    }
}

