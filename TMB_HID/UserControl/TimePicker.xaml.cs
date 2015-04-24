using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TMB
{
    /// <summary>
    /// Lógica de interacción para TimePicker.xaml
    /// </summary>
    public partial class TimePicker : UserControl
    {
        int _value;
        Type _color;

        public Type ColorButtons
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                changeColor(_color);
            }
        }

        public int Value {
            get {
                return _value;
            }
            set {
                _value = value;
                ShowValue();
                RaiseEvent();
            }
        }
        public int Max { 
            get; 
            set; 
        }
        public int Min { 
            get;
            set; 
        }
        public bool isValid
        {
            get;
            set;
        }
        private int hour;
        private int second;
        private int minute;
       // private bool isPressed = false;


        public delegate void ValueChange();
        public event ValueChange onValueChanged;
        public TimePicker()
        {
            InitializeComponent();
            Value = 0;
            //Min = 0;
            //Max = 10000;
            ShowValue();
        }
        private void changeColor(Type tipo)
        {
            if (tipo == Type.Luz)
            {
                Up.Background = new SolidColorBrush(Color.FromRgb(137, 171, 63));
                Up.BorderBrush = new SolidColorBrush(Color.FromRgb(98, 156, 58));
                Down.Background = new SolidColorBrush(Color.FromRgb(137, 171, 63));
                Down.BorderBrush = new SolidColorBrush(Color.FromRgb(98, 156, 58));
            }
            else if (tipo == Type.Temperatura)
            {
                Up.Background = new SolidColorBrush(Color.FromRgb(214, 159, 56));
                Up.BorderBrush = new SolidColorBrush(Color.FromRgb(204, 106, 31));
                Down.Background = new SolidColorBrush(Color.FromRgb(214, 159, 56));
                Down.BorderBrush = new SolidColorBrush(Color.FromRgb(204, 106, 31));
            }
            else if (tipo == Type.LuzTemperatura)
            {
                Up.Background = new SolidColorBrush(Color.FromRgb(91, 106, 166));
                Up.BorderBrush = new SolidColorBrush(Color.FromRgb(99, 87, 145));
                Down.Background = new SolidColorBrush(Color.FromRgb(91, 106, 166));
                Down.BorderBrush = new SolidColorBrush(Color.FromRgb(99, 87, 145));

            }
            else if (tipo == Type.Humedad)
            {
                Up.Background = new SolidColorBrush(Color.FromRgb(85, 169, 233));
                Up.BorderBrush = new SolidColorBrush(Color.FromRgb(43, 96, 151));
                Down.Background = new SolidColorBrush(Color.FromRgb(85, 169, 233));
                Down.BorderBrush = new SolidColorBrush(Color.FromRgb(43, 96, 151));
            }
        }
        private void Down_Click(object sender, RoutedEventArgs e)
        {
            Decrement();
            ShowValue();
        }

        private void Decrement()
        {
            if (Value >= Min)
            {
                this.Value--;

            }

        }
        private void RaiseEvent() {
            if (onValueChanged != null)
                onValueChanged();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            Increment();
            ShowValue();
        }

        private void Increment()
        {
                if (Value <= Max)
                {
                    this.Value++;
                }           
        }

        public void ChangeColor(SolidColorBrush background, SolidColorBrush border) {
            Up.Background = background;
            Up.BorderBrush = border;
        }

        private void ShowValue() {
            hour = (Value / 3600);
            minute = ((Value - hour * 3600) / 60);
            second = Value - (hour * 3600 + minute * 60);

            TextHours.Text = hour.ToString().PadLeft(2, '0');
            TextMinutes.Text = minute.ToString().PadLeft(2, '0');
            TextSeconds.Text = second.ToString().PadLeft(2, '0');
        }

        private void InputValidation_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(TextHours!=null && TextMinutes!=null && TextSeconds!=null){
                if (Validation.GetHasError(TextSeconds) &&
                    Validation.GetHasError(TextMinutes) &&
                    Validation.GetHasError(TextHours))
                {
                    isValid = false;
                    Value = 0;
                }
                else
                {
                    isValid = true;
                }
          
            }
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Validation.GetHasError(TextSeconds) &&
                    !Validation.GetHasError(TextMinutes) &&
                    !Validation.GetHasError(TextHours))
            {
                if (TextHours.Text != "" && TextMinutes.Text != "" && TextSeconds.Text != "")
                {
                    hour = Convert.ToInt32(TextHours.Text) * 3600;
                    minute = Convert.ToInt32(TextMinutes.Text) * 60;
                    Value = hour + minute + Convert.ToInt32(TextSeconds.Text);
                }
                else
                {
                    Value = 0;
                }
                ShowValue();
            }
            else {
                Value = 0;
                ShowValue();
                //MessageBox.Show("invalido");
            }
        }
       
    }
    public class Validacion {
        string _horas;
        string _minutos;
        string _segundos;
        string _muestras;


        public Validacion()
        {
            Horas = "0";
            Minutos = "0";
            Segundos = "1";
            _muestras = "1";
        }

        public string Horas { 
            get { return _horas; } 
            set { _horas = value; } 
        }
        public string Minutos { 
            get { return _minutos; }
            set { _minutos = value; } 
        }
        public string Segundos {
            get { return _segundos; }
            set { _segundos = value; } 
        }

        public string Muestras{
            get { return _muestras; }
            set { _muestras = value; }
        }



    }

    public class RangeRule : ValidationRule
    {
        private int _min;
        private int _max;

        public RangeRule()
        {
        }

        public int Min
        {
            get { return _min; }
            set { _min = value; }
        }

        public int Max
        {
            get { return _max; }
            set { _max = value; }
        }

        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            int rule = 0;

            try
            {
                if (((string)value).Length > 0)
                    rule = Int32.Parse((String)value);
            }
            catch (Exception e)
            {
                return new ValidationResult(false, "La entrada " + e.Message);
            }

            if ((rule < Min) || (rule > Max))
            {
                return new ValidationResult(false,
                  "Por favor ingrese un número entre: " + Min + " - " + Max + ".");
            }
            else
            {
                return new ValidationResult(true, null);
            }
        }
    }
}
