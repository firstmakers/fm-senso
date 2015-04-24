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
    /// Lógica de interacción para Botonera.xaml
    /// </summary>
    public partial class Botonera : UserControl
    {
        SolidColorBrush luzDisable;
        SolidColorBrush luzTempDisable;
        SolidColorBrush tempDisable;

        SolidColorBrush luzEnable;
        SolidColorBrush luzTempEnable;
        SolidColorBrush tempEnable;

        SolidColorBrush humEnable;
        SolidColorBrush humDisable;

        public Botonera()
        {
            this.InitializeComponent();
            luzDisable = new SolidColorBrush(Color.FromRgb(201, 219, 178));//lightgreen
            luzTempDisable = new SolidColorBrush(Color.FromRgb(193, 190, 212));//lightpurple
            tempDisable = new SolidColorBrush(Color.FromRgb(235, 210, 176));//lightorange
            luzEnable = new SolidColorBrush(Color.FromRgb(122, 163, 60));//StrongGreen
            luzTempEnable = new SolidColorBrush(Color.FromRgb(97, 89, 145));//StrongPurple
            tempEnable = new SolidColorBrush(Color.FromRgb(207, 139, 52));//Strongorange
            humEnable = new SolidColorBrush(Color.FromRgb(85,169,233)); //lightblue
            humDisable = new SolidColorBrush(Color.FromRgb(175,210,235));//
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //LuzTempSelected();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //LuzSelected();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //TempSelected();
        }

        public void LuzSelected()
        {
            luz.Background = luzEnable;
            lt.Background = luzTempDisable;
            temp.Background = tempDisable;
            hum.Background = humDisable;
        }
        public void LuzTempSelected()
        {
            luz.Background = luzDisable;
            lt.Background = luzTempEnable;
            temp.Background = tempDisable;
            hum.Background = humDisable;
        }

        public void TempSelected()
        {
            luz.Background = luzDisable;
            lt.Background = luzTempDisable;
            temp.Background = tempEnable;
            hum.Background = humDisable;
        }

        public void HumSelected() 
        {
            hum.Background = humEnable;
            luz.Background = luzDisable;
            lt.Background = luzTempDisable;
            temp.Background = tempDisable;

        }
    }
}