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

namespace TestBotones
{
    /// <summary>
    /// Lógica de interacción para WindowsStates.xaml
    /// </summary>
    public partial class WindowsStates : UserControl
    {
        public WindowsStates()
        {
            this.InitializeComponent();
        }

        protected virtual void Minimizar(object sender, System.Windows.RoutedEventArgs e)
        {
            Window parentwin = Window.GetWindow(this);
            parentwin.WindowState = WindowState.Minimized;
        }

        protected virtual void Cerrar(object sender, System.Windows.RoutedEventArgs e)
        {
            Window parentwin = Window.GetWindow(this);
            parentwin.Close();
        }

        protected virtual void Maximizar(object sender, System.Windows.RoutedEventArgs e)
        {
            Window parentwin = Window.GetWindow(this);
            if (parentwin.WindowState == WindowState.Maximized)
            {
                parentwin.WindowState = WindowState.Normal;

            }
            else
            {
                parentwin.WindowState = WindowState.Maximized;
            }
        }


    }
}