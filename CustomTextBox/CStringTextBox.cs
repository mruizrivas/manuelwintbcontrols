using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;

namespace ManuelWindowsControls
{

    public class CustomTextBox : TextBox
    {
        public Color ColorFondoFoco { get; set; } = Color.LightYellow;
        public Color ColorTextoFoco { get; set; } = Color.Black;
        private Color originalBackColor;
        private Color originalForeColor;

        [Category("Behavior")]
        [Description("Convierte el texto a mayúsculas automáticamente.")]
        public bool Mayusculas { get; set; } = false;

        [Category("Behavior")]
        [Description("Convierte el texto a minúsculas automáticamente.")]
        public bool Minusculas { get; set; } = false;

        public CustomTextBox()
        {
            this.KeyDown += CustomTextBox_KeyDown;
            this.Enter += CustomTextBox_Enter;
            this.Leave += CustomTextBox_Leave;
            this.TextChanged += CustomTextBox_TextChanged;
        }

        private void CustomTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape)
            {
                e.SuppressKeyPress = true; // Evita el sonido del sistema
                e.Handled = true;

                bool forward = e.KeyCode == Keys.Enter;
                this.Parent.SelectNextControl(this, forward, true, true, true);
            }
        }

        private void CustomTextBox_Enter(object sender, EventArgs e)
        {
            originalBackColor = this.BackColor;
            originalForeColor = this.ForeColor;

            this.BackColor = ColorFondoFoco;
            this.ForeColor = ColorTextoFoco;
        }

        private void CustomTextBox_Leave(object sender, EventArgs e)
        {
            this.BackColor = originalBackColor;
            this.ForeColor = originalForeColor;
        }
    private void CustomTextBox_TextChanged(object sender, EventArgs e)
    {
        if (Mayusculas && !Minusculas)
        {
            this.Text = this.Text.ToUpper();
            this.SelectionStart = this.Text.Length; // Mantiene la posición del cursor
        }
        else if (Minusculas && !Mayusculas)
        {
            this.Text = this.Text.ToLower();
            this.SelectionStart = this.Text.Length;
        }
    }


    }
}
