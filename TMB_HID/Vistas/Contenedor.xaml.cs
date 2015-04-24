using SistemaLuzTemperatura.conexion;
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

namespace TMB.Vistas
{
    /// <summary>
    /// Lógica de interacción para Contenedor.xaml
    /// </summary>
    public partial class Contenedor : Page
    {
        public static LuzTemp luzTemp;
        public static Luz luz;
        public static Temp temp;
        public static Menu menu;
        public static Actividades actividades;
        public static Humedad hum;

        private bool isActive;

        private SolidColorBrush BorderLT;
        private SolidColorBrush BorderL;
        private SolidColorBrush BorderT;
        private SolidColorBrush BorderDefault;
        private SolidColorBrush BorderAct;
        private SolidColorBrush BorderHum;
        private string htmenu;
        private string htact;

        private delegate void Desconexion();
        private delegate void Conexion();

        private Dispositivo tarjeta;

        public Contenedor()
        {
            
            InitializeComponent();
            isActive = false;
            tarjeta = new Dispositivo();
            tarjeta.Conexion += HID_Conexion;
            tarjeta.Desconexion += HID_Desconexion;
            luzTemp = new LuzTemp(tarjeta);
            luz = new Luz(tarjeta);
            temp =  new Temp(tarjeta);
            hum = new Humedad(tarjeta);
            menu = new Menu();
            actividades = new Actividades() ;
            Contenido.Content = menu;

            BorderHum = new SolidColorBrush(Color.FromRgb(46,96,233));
            BorderL = new SolidColorBrush(Color.FromRgb(107, 145, 78));
            BorderLT = new SolidColorBrush(Color.FromRgb(101, 108, 143));
            BorderT = new SolidColorBrush(Color.FromRgb(176, 112, 63));
            BorderDefault = new SolidColorBrush(Color.FromRgb(96, 139, 189));
            BorderAct = new SolidColorBrush(Color.FromRgb(106, 178, 230));

            htact = "ACTIVIDADES";
            htmenu = "SISTEMA DE MEDICIÓN DE LUZ, TEMPERATURA Y HUMEDAD";

            Contenido.NavigationService.Navigate(menu);
            menu.buttont.Click += buttont_Click;
            menu.buttonlt.Click += buttonlt_Click;
            menu.buttonl.Click += buttonl_Click;
            menu.buttonact.Click += buttonact_Click;
            menu.buttonhum.Click += buttonh_Click;  
            //eventos de la botonera
            Botonera.lt.Click += buttonlt_Click;
            Botonera.luz.Click += buttonl_Click;
            Botonera.temp.Click += buttont_Click;
            Botonera.hum.Click += buttonh_Click;
            //boton volver
            Back.Button.Click += NavigationHome;
            Version.Content = "Software  " + Constantes.Version();
            if (!tarjeta.conectado)
                Device.Disconnected();

        }

        private void HID_Conexion()
        {
            this.Dispatcher.BeginInvoke(new Conexion(conexion));
        }

        private void conexion()
        {
            Device.Connected();

        }

        private void HID_Desconexion()
        {
            this.Dispatcher.BeginInvoke(new Desconexion(desconexion));
        }

        private void desconexion()
        {
            Device.Disconnected();
            if (!isActive)
            {
                isActive = true;//evita que se cree otro dialogo en la pantalla
                if (MessageBoxResult.OK == MessageBox.Show(Constantes.MENSAJE_TARJETA_DESCONECTADA,
                    "Error de lectura", MessageBoxButton.OK, MessageBoxImage.Error))
                    isActive = false;                         
            }
            
        }

        /// <summary>
        /// Vuelve a la página anterior
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationHome(object sender, RoutedEventArgs e)
        {
            if (!tarjeta.isRunning())
                Contenido.NavigationService.Navigate(menu);
        }

        /// <summary>
        /// Cambia el contenido de la página actual por la página de lumonosidad y temperatura.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonlt_Click(object sender, RoutedEventArgs e)
        {
            if (luz.tarjeta.isRunning() || temp.tarjeta.isRunning() || hum.tarjeta.isRunning()) {
                MessageBox.Show("Esta operación no es posible debido ya que se está ejecutando una medición","Medición en curso",MessageBoxButton.OK ,MessageBoxImage.Exclamation);
                
            } else {
                Contenido.NavigationService.Navigate(luzTemp);
                Botonera.LuzTempSelected();
            }
           

        }
        /// <summary>
        /// Cambia el contenido de la página actual por la página de actividades
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonact_Click(object sender, RoutedEventArgs e)
        {
            Contenido.NavigationService.Navigate(actividades);
        }
        /// <summary>
        /// Cambia el contenido de la página actual por la página de luminosidad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonl_Click(object sender, RoutedEventArgs e)
        {
            if (luzTemp.tarjeta.isRunning() || temp.tarjeta.isRunning() || hum.tarjeta.isRunning())
            {
                MessageBox.Show("Esta operación no es posible debido a que se está ejecutando una medición", "Medición en curso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else 
            {
                Contenido.NavigationService.Navigate(luz);
                Botonera.LuzSelected();
            }
        }


        private void buttonh_Click(object sender, RoutedEventArgs e)
        {
            if (luzTemp.tarjeta.isRunning() || temp.tarjeta.isRunning() || luz.tarjeta.isRunning())
            {
                MessageBox.Show("Esta operación no es posible debido a que se está ejecutando una medición", "Medición en curso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                Contenido.NavigationService.Navigate(hum);
                Botonera.HumSelected();
            }
        }


        /// <summary>
        /// Cambia el contenido de la página actual por la página de temperatura
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttont_Click(object sender, RoutedEventArgs e)
        {
            if (luz.tarjeta.isRunning() || luzTemp.tarjeta.isRunning())
            {
                MessageBox.Show("Esta operación no es posible debido a que se está ejecutando una medición", "Medición en curso", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                Contenido.NavigationService.Navigate(temp);
                Botonera.TempSelected();
            }
        }
        /// <summary>
        /// Cambia los colores del borde de la aplicación
        /// </summary>
        /// <param name="border"> color del borde</param>
        private void ChangeBorderColor(SolidColorBrush border)
        {
            Fondo1.BorderBrush = border;
            Fondo2.BorderBrush = border;
            Fondo3.BorderBrush = border;
            Fondo4.BorderBrush = border;
            Fondo5.BorderBrush = border;
        }

        /// <summary>
        /// Cambia la disposoción de los elementos de la interfaz gráfica según la pantalla actual
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Navigated(object sender, NavigationEventArgs e)
        {
            if (e.Content == temp)
            {
                ChangeBorderColor(BorderT);
                Logo.Visibility = Visibility.Visible;
                Header.Visibility = Visibility.Hidden;
                HeaderText.Visibility = Visibility.Hidden;
                Botonera.Visibility = Visibility.Visible;
                Back.Visibility = Visibility.Visible;
            }
            else if (e.Content == luz)
            {
                ChangeBorderColor(BorderL);
                Logo.Visibility = Visibility.Visible;
                Header.Visibility = Visibility.Hidden;
                HeaderText.Visibility = Visibility.Hidden;
                Botonera.Visibility = Visibility.Visible;
                Back.Visibility = Visibility.Visible;
            }
            else if (e.Content == luzTemp)
            {
                ChangeBorderColor(BorderLT);
                Logo.Visibility = Visibility.Visible;
                Header.Visibility = Visibility.Hidden;
                HeaderText.Visibility = Visibility.Hidden;
                Botonera.Visibility = Visibility.Visible;
                Back.Visibility = Visibility.Visible;
            }
            else if (e.Content == actividades)
            {
                ChangeBorderColor(BorderAct);
                Logo.Visibility = Visibility.Visible;
                Header.Visibility = Visibility.Visible;
                HeaderText.Visibility = Visibility.Visible;
                HeaderText.Content = htact;
                Botonera.Visibility = Visibility.Visible;
                Back.Visibility = Visibility.Visible;
            }
            else if(e.Content == hum)
            {
                ChangeBorderColor(BorderHum);
                Logo.Visibility = Visibility.Visible;
                Header.Visibility = Visibility.Hidden;
                HeaderText.Visibility = Visibility.Hidden;
                Botonera.Visibility = Visibility.Visible;
                Back.Visibility = Visibility.Visible;
            }
            else
            {
                ChangeBorderColor(BorderDefault);
                Logo.Visibility = Visibility.Hidden;
                Header.Visibility = Visibility.Visible;
                HeaderText.Visibility = Visibility.Visible;
                HeaderText.Content = htmenu;
                Botonera.Visibility = Visibility.Hidden;
                Back.Visibility = Visibility.Hidden;
            }
        }
        /// <summary>
        /// Desplaza la aplicación sobre la pantalla al mantener presionado el boton izquierdo del mouse.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DragMove(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Window win = Window.GetWindow(this);
            win.DragMove();
        }

        private void ChangeStateWindows(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2) { 
                 Window win = Window.GetWindow(this);
                 if (win.WindowState == WindowState.Maximized)
                     win.WindowState = WindowState.Normal;
                 else
                     win.WindowState = WindowState.Maximized;             
            }
        }
    }
}
