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
using System.Windows.Shapes;
using SistemaLuzTemperatura.conexion;

namespace TMB
{
    /// <summary>
    /// Lógica de interacción para SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        public SplashScreen()
        {
            InitializeComponent();
            
            version.Text = Constantes.Version();
            Copyright.Text = Constantes.Copyright;

        }



        //private void Win_Loaded(object sender, RoutedEventArgs e)
        //{
        //    loadingThread = new Thread(load);
        //    loadingThread.Start();
        //}

        //private void load() {
        //    BeginStoryboard(loadAnimation);
        //}

    }
}
