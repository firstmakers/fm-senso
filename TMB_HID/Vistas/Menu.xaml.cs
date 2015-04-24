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
using HID;
using System.Diagnostics;

namespace TMB.Vistas
{
    /// <summary>
    /// Lógica de interacción para Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {

        Dispositivo tarjeta;
        private delegate void Desconexion();
        private delegate void Conexion();
        public Menu()
        {
            InitializeComponent();
            //mDevice.ChangeState(true);
            tarjeta = new Dispositivo();
            tarjeta.Conexion+=tarjeta_Conexion;
            tarjeta.Desconexion+=tarjeta_Desconexion;
            setStateButtons();

        }
        #region Event Conexion
        private void tarjeta_Desconexion()
        {
            this.Dispatcher.BeginInvoke(new Desconexion(desconexion));
        }

        private void desconexion()
        {
            DisableButtons();
        }

        private void tarjeta_Conexion()
        {
            this.Dispatcher.BeginInvoke(new Conexion(conexion));
        }

        private void conexion()
        {
            EnableButtons();
        }



        #endregion
        private void setStateButtons()
        {
            
            if (tarjeta.conectado)
                EnableButtons();
            else
                DisableButtons();
        }

        private void DisableButtons()
        {
            buttonact.IsEnabled = false;
            buttonl.IsEnabled = false;
            buttonlt.IsEnabled = false;
            buttont.IsEnabled = false;
            buttonhum.IsEnabled = false;
        }

        private void EnableButtons()
        {
            buttonact.IsEnabled = true;
            buttonl.IsEnabled = true;
            buttonlt.IsEnabled = true;
            buttont.IsEnabled = true;
            buttonhum.IsEnabled = true;
        }

        private void MedirLuzTemp(object sender, RoutedEventArgs e)
        {

        }

        private void MedirLuz(object sender, RoutedEventArgs e)
        {

        }

        private void MedirTemperatura(object sender, RoutedEventArgs e)
        {

        }

   
    }
}
