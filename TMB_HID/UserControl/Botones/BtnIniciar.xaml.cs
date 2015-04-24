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
    /// Lógica de interacción para BtnIniciar.xaml
    /// </summary>
    public partial class BtnIniciar : UserControl
    {
        TMB.Play.ButtonType _type;
        public TMB.Play.ButtonType Type { 
            get { return _type; } 
            set { 
                _type = value;
                setTypeButton(value);
        
            } 
        }

        SolidColorBrush BackgroundLuz;
        SolidColorBrush BackgroundTemp;
        SolidColorBrush BackgroundLuzTemp;
        public BtnIniciar()
        {
            InitializeComponent();
            BackgroundLuz = new SolidColorBrush(Color.FromRgb(137,171,63));
            BackgroundLuzTemp = new SolidColorBrush(Color.FromRgb(100, 105, 156));
            BackgroundTemp = new SolidColorBrush(Color.FromRgb(214, 159, 56));

        }


        private void setTypeButton(Play.ButtonType value)
        {
            if(value == Play.ButtonType.Temperatura){
                Background.BorderBrush = TMB.Play.Temp;
                Background.Background = BackgroundTemp;
                Icon.Tipo = Play.ButtonType.Temperatura;

            }else if(value == Play.ButtonType.Luz){
                Background.BorderBrush = TMB.Play.Luz;
                Background.Background = BackgroundLuz;
                Icon.Tipo = Play.ButtonType.Luz;
            }else{
                Background.BorderBrush = TMB.Play.LuzTemp;
                Background.Background = BackgroundLuzTemp;
                Icon.Tipo = Play.ButtonType.LuzTemperatura;
            }
        } 
        //redondea los bordes de las
        private void sizeChanged(object sender, SizeChangedEventArgs e)
        {
            //double actualHeight = Background.Height;
            Background.CornerRadius = new CornerRadius(Background.ActualHeight/2);

        }

        private void mouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
        	
        }
    }
}


