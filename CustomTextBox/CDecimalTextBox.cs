using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace ManuelWindowsControls
{
    public class DecimalTextBox : TextBox
    {
        #region -> Miembros

        private decimal _minValue = decimal.MinValue;
        private decimal _maxValue = decimal.MaxValue;
        private bool isUserInteracting;
        private bool isValid = true; // Indica si el valor es válido
        private int _decimalPlaces = 2; // Número de decimales a mostrar
        private bool _activarPuntoMiles = false; // Activar formato de miles
        private string _decimalSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator; // Obtenemos el separador decimal
        private string _groupSeparator = CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator; // Obtenemos el separador de miles

        // Propiedades para los colores de texto y fondo cuando el control obtiene el foco
        private Color originalBackColor;
        private Color originalForeColor;

        #endregion

        #region -> Propiedades

        [Category("Numeric Properties")]
        [Description("Establece el valor mínimo permitido.")]
        public decimal MinValue
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
        public decimal MaxValue
        {
            get => _maxValue;
            set
            {
                _maxValue = value;
                if (isUserInteracting) ValidateValue();
            }
        }

        [Category("Numeric Properties")]
        [Description("Establece el número de decimales a mostrar.")]
        public int DecimalPlaces
        {
            get => _decimalPlaces;
            set
            {
                _decimalPlaces = value;
                if (isUserInteracting) ValidateValue();
            }
        }

        [Category("Formatting")]
        [Description("Indica si se debe activar el formato de miles cuando se pierda el foco.")]
        [DefaultValue(false)]
        public bool ActivarPuntoMiles
        {
            get => _activarPuntoMiles;
            set
            {
                _activarPuntoMiles = value;
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

        public DecimalTextBox()
        {
            this.TextAlign = HorizontalAlignment.Right;
            this.KeyPress += DecimalTextBox_KeyPress;
            this.Leave += DecimalTextBox_Leave;
            this.Enter += DecimalTextBox_Enter;
            //this.KeyDown += DecimalTextBox_KeyDown;
        }

        #endregion

        #region -> Eventos

        private void DecimalTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {

            // Si se presiona el punto del teclado numérico, lo sustituimos por la coma (si la coma es el separador decimal)
            if (e.KeyChar == '.' && _decimalSeparator == ",")
            {
                e.KeyChar = ','; // Sustituimos el punto por la coma
            }

            // Aceptar caracteres de control (como BACKSPACE) y números
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != _decimalSeparator[0] && e.KeyChar != '-')
            {
                e.Handled = true;
            }

            // Solo permitir un signo '-' al inicio
            if (e.KeyChar == '-' && (this.Text.Length > 0 || this.SelectionStart > 0))
            {
                e.Handled = true;
            }

            // Solo permitir un punto decimal (o coma dependiendo de la configuración)
            if (e.KeyChar == _decimalSeparator[0] && this.Text.Contains(_decimalSeparator))
            {
                e.Handled = true;
            }

        }

        private void DecimalTextBox_Enter(object sender, EventArgs e)
        {
            isUserInteracting = true;
            isValid = true; // Aseguramos que el valor sea válido cuando entra al control

            // Guardar los colores originales antes de cambiarlos
            originalBackColor = this.BackColor;
            originalForeColor = this.ForeColor;

            // Cambiar a los colores definidos cuando tiene el foco
            this.BackColor = ColorFondoFoco;
            this.ForeColor = ColorTextoFoco;

        }

        private void DecimalTextBox_Leave(object sender, EventArgs e)
        {
            if (isUserInteracting)
            {
                ValidateValue();
                if(!isValid)
                {
                    this.Focus();
                }
                else
                {
                    if (_activarPuntoMiles)
                    {
                        FormatValueWithThousandsSeparator();
                    }
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
                isValid = true; // El valor vacío es considerado válido
                return; // No hacer nada si el campo está vacío
            }

            if (decimal.TryParse(this.Text, out decimal value))
            {
                // Verificar si el valor está fuera de los límites
                if (value < _minValue)
                {
                    MessageBox.Show($"El valor no puede ser menor que {_minValue}.", "Valor fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isValid = false;
                }
                else if (value > _maxValue)
                {
                    MessageBox.Show($"El valor no puede ser mayor que {_maxValue}.", "Valor fuera de rango", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    isValid = false;
                }
                else
                {
                    // Ajustar el valor para que tenga el número correcto de decimales
                    this.Text = value.ToString($"N{_decimalPlaces}");
                    isValid = true;
                }
            }
            else
            {
                MessageBox.Show("Entrada inválida. Introduzca un número válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                isValid = false;
            }
        }

        // Método para formatear el valor con separador de miles
        private void FormatValueWithThousandsSeparator()
        {
            if (decimal.TryParse(this.Text, out decimal value))
            {
                // Aplicar formato de miles si es necesario
                this.Text = value.ToString($"N{_decimalPlaces}");

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
