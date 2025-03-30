using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Drawing;

namespace ManuelWindowsControls
{

    public class IntegerTextBox : TextBox
    {

        #region -> Miembros

        private int _minValue = int.MinValue;
        private int _maxValue = int.MaxValue;
        private bool isUserInteracting;
        private bool isValid = true; // Indica si el valor es válido
        private Color originalBackColor;
        private Color originalForeColor;

        #endregion

        #region -> Propiedades

        [Category("Numeric Properties")]
        [Description("Establece el valor mínimo permitido.")]
        public int MinValue
        {
            get => _minValue;
            set
            {
                _minValue = value;
                if (isUserInteracting) ValidateValue();
            }
        }

        [Category("Numeric Properties")]
        [Description("Establece el valor máximo permitido.")]
        public int MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                if (isUserInteracting) ValidateValue();
            }
        }

        [Category("Appearance")]
        [Description("Color del texto cuando el control tiene el foco.")]
        public Color ColorTextoFoco { get; set; } = Color.Black;

        [Category("Appearance")]
        [Description("Color del fondo cuando el control tiene el foco.")]
        public Color ColorFondoFoco { get; set; } = Color.LightYellow;

        #endregion

        #region -> Constructor

        public IntegerTextBox()
        {
            this.TextAlign = HorizontalAlignment.Right;
            this.KeyPress += IntegerTextBox_KeyPress;
            this.Leave += IntegerTextBox_Leave;
            this.Enter += IntegerTextBox_Enter;
        }

        #endregion

        #region -> Eventos

        private void IntegerTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            // Solo permitir un signo '-' al inicio
            if (e.KeyChar == '-' && (this.Text.Length > 0 || this.SelectionStart > 0))
            {
                e.Handled = true;
            }
        }

        private void IntegerTextBox_Enter(object sender, EventArgs e)
        {
            // Guardar los colores originales antes de cambiarlos
            originalBackColor = this.BackColor;
            originalForeColor = this.ForeColor;

            // Cambiar a los colores definidos cuando tiene el foco
            this.BackColor = ColorFondoFoco;
            this.ForeColor = ColorTextoFoco;

        }

        private void IntegerTextBox_Leave(object sender, EventArgs e)
        {
            if (isUserInteracting)
            {
                ValidateValue();

                // Si el valor es inválido, devolver el foco
                if (!isValid)
                {
                    this.Focus();
                }
            }

            // Restaurar colores originales al perder el foco
            this.BackColor = originalBackColor;
            this.ForeColor = originalForeColor;
        }

        private void ValidateValue()
        {
            // Evitar validación si el campo está vacío
            if (string.IsNullOrWhiteSpace(this.Text))
            {
                isValid = true;
                return;
            }

            if (int.TryParse(this.Text, out int value))
            {
                if (value < _minValue)
                {
                    if (isValid)
                    {
                        MessageBox.Show($"El valor no puede ser menor que {_minValue}.", "Valor fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Media.SystemSounds.Exclamation.Play();
                        isValid = false;
                    }
                }
                else if (value > _maxValue)
                {
                    if (isValid)
                    {
                        MessageBox.Show($"El valor no puede ser mayor que {_maxValue}.", "Valor fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        System.Media.SystemSounds.Exclamation.Play();
                        isValid = false;
                    }
                }
                else
                {
                    isValid = true;
                }
            }
            else
            {
                if (isValid)
                {
                    MessageBox.Show("Entrada inválida. Introduzca un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    System.Media.SystemSounds.Hand.Play();
                    isValid = false;
                }
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            // Si el valor es inválido, bloquear cualquier cambio de foco
            if (!isValid)
            {
                e.SuppressKeyPress = true; // Bloquea la tecla sin propagar el evento
                e.Handled = true;
                return;
            }

            // Si el valor es válido, permitir la navegación sin sonido
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Escape || e.KeyCode == Keys.Tab || e.KeyCode == Keys.ShiftKey)
            {
                e.SuppressKeyPress = true; // Evita el sonido del sistema
                bool forward = e.KeyCode != Keys.ShiftKey && e.Modifiers != Keys.Shift;

                // ESC se comporta como Shift + TAB
                if (e.KeyCode == Keys.Escape)
                {
                    forward = false;
                }

                this.Parent.SelectNextControl(this, forward, true, true, true);
                e.Handled = true;
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            base.OnEnter(e);
            isUserInteracting = true;
            isValid = true;
            //Debug.WriteLine("OnEnter ejecutado"); // Verificar si entra al evento
            this.ForeColor = ColorTextoFoco;
            this.BackColor = ColorFondoFoco;
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);

            if (isUserInteracting)
            {
                ValidateValue();

                // Si el valor es inválido, devolver el foco al control
                if (!isValid)
                {
                    this.Focus();
                }
            }
            this.ForeColor = SystemColors.WindowText; // Restaurar color predeterminado
            this.BackColor = SystemColors.Window; // Restaurar color de fondo predeterminado
        }

        #endregion

    }

}