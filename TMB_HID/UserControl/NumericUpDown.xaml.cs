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
    /// Lógica de interacción para NumericUpDown.xaml
    /// </summary>
    public partial class NumericUpDown : UserControl
    {
        int _value;
        Type _color;

        public Type ColorButtons { 
            get { 
                return _color; 
            } 
            set { 
                _color = value;
                changeColor(_color);
            } 
        }


        public SolidColorBrush BorderColor { get; set; }
        public SolidColorBrush BackgroundColor { get; set; }
        public int Max
        {
            get;
            set;
        }
        public int Min
        {
            get;
            set;
        }
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                ShowValue();
                RaiseEvent();
            }
        }

        public delegate void ValueChange();
        public event ValueChange onValueChanged;

        public NumericUpDown()
        {
            InitializeComponent();
            /// se debe inicializar las variables públicas si 
            /// es que no se declararon en el XML
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
            else if(tipo == Type.Humedad){
                Up.Background = new SolidColorBrush(Color.FromRgb(85, 169, 233));
                Up.BorderBrush = new SolidColorBrush(Color.FromRgb(43, 96, 151));
                Down.Background = new SolidColorBrush(Color.FromRgb(85, 169, 233));
                Down.BorderBrush = new SolidColorBrush(Color.FromRgb(43, 96, 151));
            }
        }
        private void RaiseEvent()
        {
            if (onValueChanged != null)
                onValueChanged();
        }

        private void Up_Click(object sender, RoutedEventArgs e)
        {
            if (Value <= Max)
            {
                this.Value++;
            }    
        }

        private void Down_Click(object sender, RoutedEventArgs e)
        {
            if (Value >= Min)
            {
                this.Value--;

            }
        }

        private void LostFocus(object sender, RoutedEventArgs e)
        {
            if (!Validation.GetHasError(TextMuestras))
            {
                if (TextMuestras.Text != "" )
                {
                    Value = Convert.ToInt32(TextMuestras.Text);
                }
                else
                {
                    Value = 0;
                }
                ShowValue();
            }
        }

        private void InputValidation_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Validation.GetHasError(TextMuestras))
            {
                Value = Convert.ToInt32(TextMuestras.Text);
            }
            else {
                Value = 0;
            }
        }
        private void ShowValue()
        {
            TextMuestras.Text = Value.ToString();
        }
    }
    public enum Type { 
        Luz,
        Temperatura,
        LuzTemperatura,
        Humedad
    };

 


}
