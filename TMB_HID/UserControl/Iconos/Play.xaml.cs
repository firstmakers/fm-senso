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
    /// Lógica de interacción para Play.xaml
    /// </summary>
    public partial class Play : UserControl
    {
        
        ButtonType _type;
        public static SolidColorBrush LuzTemp = new SolidColorBrush(Color.FromRgb(99, 87, 145));
        public static SolidColorBrush Luz = new SolidColorBrush(Color.FromRgb(98, 156, 58));
        public static SolidColorBrush Temp = new SolidColorBrush(Color.FromRgb(204, 159, 56));
        public ButtonType Tipo { 
            get { return _type; } 
            set { _type = value; 
                changeType(value); 
            } 
        }

		public Play()
		{
			this.InitializeComponent();
		}

        public void changeType(ButtonType value) {

            if (value == ButtonType.LuzTemperatura) {
                Icon.Fill = LuzTemp;
                Background.Stroke = LuzTemp;
            }
            else if (value == ButtonType.Luz)
            {
                Icon.Fill = Luz;
                Background.Stroke = Luz;
            }
            else {
                Icon.Fill = Temp;
                Background.Stroke = Temp;
            }
        }
        public enum ButtonType
        {
            Luz,
            Temperatura,
            LuzTemperatura
        };
	}
}