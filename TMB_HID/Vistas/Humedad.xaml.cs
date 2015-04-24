using HID;
using SistemaLuzTemperatura.conexion;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
using TMB.UserControls;

namespace TMB.Vistas
{
    /// <summary>
    /// Lógica de interacción para Humedad.xaml
    /// </summary>
    public partial class Humedad : Page
    {

        private DispatcherTimer reloj;
        private DispatcherTimer timer;
        /// <summary>
        /// Objeto de la Clase
        /// <see cref="SistemaLuzTemperatura.animaciones.Grafico">Grafico</see> muestras un grafico lineal con los últimos 10 valores obtenidos
        /// </summary>
        private int totalSegundos = 0;
        /// <summary>
        /// Total de segundos trasncurridos en la lectura de datos
        /// </summary>
        private int transcurrido = 0;
        public Dispositivo tarjeta;
        /// <summary>
        /// Intervalos de tiempo entre consultas de datos hacia la tarjeta medidos en segundos
        /// </summary>
        int segundos = 0;
        /// <summary>
        /// Numero de muestras a capturar desde la tarjeta
        /// </summary>
        int muestras = 0;
        /// <summary>
        /// Valor por defecto de los spinner de muestras  e intervalo de tiempo(segundos)
        /// </summary>
        int reset = 1;
        /// <summary>
        /// Valor del numero actual de datos obtenidos desde la tarjeta
        /// </summary>
        int numDato = 0;
        /// <summary>
        /// Coleccion de objetos <see cref="SistemaLuzTemperatura.conexion.DatosLuzTemperatura">DatosLuzTemperatura</see> 
        /// donde se almacenan temporalmente los datos obtenidos
        /// </summary>
        ObservableCollection<DatosLuzTemperatura> Datos;

        public delegate void Desconexion();
        public delegate void Conexion();
   
        public Humedad(Dispositivo _tarjeta)
        {
            InitializeComponent();
            btnExportar.IsEnabled = false;//deshabilita botones Exportar, Limpiar y Detener
            btnLimpiar.IsEnabled = false;
            btnDetener.IsEnabled = false;
            Datos = new ObservableCollection<DatosLuzTemperatura>();
            tarjeta = _tarjeta;
            tarjeta.Conexion += tarjeta_Conexion;
            tarjeta.Desconexion += tarjeta_Desconexion;
            tarjeta.DesconexionSonda += tarjeta_DesconexionSonda;
            //eventos de los temporizadores
            intervalo.onValueChanged += intervalo_onValueChanged;
            numMuestras.onValueChanged += intervalo_onValueChanged;
            
        }

        private void tarjeta_DesconexionSonda()
        {
            this.Dispatcher.BeginInvoke(new Desconexion(desconexionSonda));
        }

        private void desconexionSonda()
        {
            btnDetenerClick(null, null);
            MessageBox.Show("El Sensor de Temperatura está desconectado.", "Sensor de Temperatura", MessageBoxButton.OK, MessageBoxImage.Exclamation);            
        }
        private void intervalo_onValueChanged()
        {
            tiempoProgramado.Content = Temporizador.calcularTiempo(intervalo.Value, numMuestras.Value);
        }

        #region Eventos de conexion
        private void tarjeta_Desconexion()
        {
            this.Dispatcher.BeginInvoke(new Desconexion(desconexion));
        }

        private void desconexion()
        {
            btnDetenerClick(null, null);
            btnIniciar.IsEnabled = false;
        }

        private void tarjeta_Conexion()
        {
            this.Dispatcher.BeginInvoke(new Conexion(conexion));
        }

        private void conexion()
        {
            btnIniciar.IsEnabled = true;
        }

        #endregion
        private void btnIniciarClick(object sender, RoutedEventArgs e)
        {
            if (tarjeta.conectado)
            {
                string validacion = new Validaciones().validarDatos(intervalo.Value.ToString(), numMuestras.Value.ToString());//validaciones de los datos de entrada
                //si las validaciones son correctas
                if (validacion.Equals("1"))
                {
                    if (!tarjeta.isRunning())
                    {
                        tarjeta.ComenzarMedicion(Command.Humedad);
                        btnIniciar.IsEnabled = false;
                        btnLimpiar.IsEnabled = false;//inhabilita los botones limpiar
                        btnExportar.IsEnabled = false;//Exportar
                        btnDetener.IsEnabled = true;//Habilita el boton Detener

                        this.segundos = intervalo.Value;
                        if(muestras==0)
                            this.muestras = numMuestras.Value;
                       
                        totalSegundos = segundos * muestras;
                        transcurrido = 0;
                        
                        
                        //inicia el timer de captura de datos
                        Thread.Sleep(1200);
                        timer = new DispatcherTimer();//nuevo timer
                        timer.Tick += OnTimedEvent;
                        timer.Start();//inicio del timer
                        int x = segundos * 1000;
                        timer.Interval = TimeSpan.FromMilliseconds(x);//intervalo de tiempo entre cada tick     
                        setTimer();
                        humedad.StartAnimation();
                        grillaDatos.DataContext = Datos;//agrego la colección a la grilla de datos
                    }
                    else {
                        tarjeta.DetenerMedicion();
                        //btnIniciarClick(sender, e);// llamada recursiva al metodo iniciar
                    }
                }
                else
                {
                    numMuestras.Value = reset;
                    intervalo.Value = reset;
                    MessageBox.Show(validacion, "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show(Constantes.MENSAJE_TARJETA_DESCONECTADA, "Error de lectura",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        ///<summary>
        ///Evento de temporizador que obtiene los datos 
        ///de la tarjeta cada X segundos
        ///</summary>
        private void OnTimedEvent(object sender, EventArgs e)
        {
            //se obtienen los datos
            if (muestras > 0)
            {
                double hum = tarjeta.getHum();
                if (hum == 400)
                    return;
                tarjeta_DatosNuevaMedicion(hum);
            }
        }


        private void tarjeta_DatosNuevaMedicion(double hum)
        {
             hum = Math.Round((hum /10.23) , 1);
                agregarDatos(new DatosLuzTemperatura
                {
                    dataHumedad = hum.ToString(),
                    dataFecha = DateTime.Now.ToString(),
                    //dataNumDato = numDato

                });
                humedad.Animate(hum);//aca la animación*******************************************************************
                grafico.addHum(hum, numDato);

                this.muestras--;
                //numMuestras.Value = muestras.ToString();
                /*condición para que la última iteración no se demore en entregar 
                 * el resultado, evita que la aplicación se cuelgue cuando las muestras 
                 * se toman en un intervalo de segundos muy amplio
                */
                if (muestras == 0 && timer != null)
                {
                    timer.Interval = TimeSpan.FromSeconds(1); //intervalo de tiempo a 1 segundo para el ultimo tick del timer
                    reloj.Interval = TimeSpan.FromSeconds(1);
                    btnDetenerClick(null, null); //llamada al metodo detener
                    tiempoRestante.Content = "00:00:00";
                    tiempoRecorrido.Content = "00:00:00";
                    //numMuestras.Value = reset;
                    //intervalo.Value = reset;
                }
            
        }

        ///<summary>
        ///Agrega los datos a la colección 
        ///</summary>
        ///<param name="Temp">
        ///Objeto de tipo Luz contiene el valor Lux y el nivel de la luz
        ///</param>
        private void agregarDatos(DatosLuzTemperatura Hum)
        {
            numDato++;//incremento del numero de datos
            Hum.dataNumDato = numDato;
            Datos.Add(Hum);//Agrega un nuevo objeto con los datos capturados desde la tarjeta
            grillaDatos.ScrollIntoView(Hum);
        }


        ///<summary>
        ///Evento click del botón Detener, Detiene la captura de datos 
        ///de la tarjeta
        ///</summary>
        private void btnDetenerClick(object sender, RoutedEventArgs e)
        {
            btnIniciar.IsEnabled = true;
            if (tarjeta.isRunning())
                tarjeta.DetenerMedicion();
            if (timer != null && reloj != null)
            {
                timer.Stop();//detención del timer
                reloj.Stop();//temporizadores
            }
            if (Datos.Count != 0)
            {
                btnExportar.IsEnabled = true;//habilitar los botones para 
                btnLimpiar.IsEnabled = true;//exportar archivos y limpiar
            }

            //this.timer = null;
            humedad.StopAnimation();

        }

        ///<summary>
        ///Evento click del botón Limpiar
        ///</summary>
        private void btnLimpiarClick(object sender, RoutedEventArgs e)
        {
            if (sender != btnLimpiar)//si la llamada NO viene desde el botón limpiar
            {
                limpiar();
            }
            else if (MessageBox.Show(Constantes.MENSAJE_LIMPIAR,
                "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes && sender == btnLimpiar)
            {
                limpiar();
            }
            //biniciar.Content = "INICIAR";
        }

        private void limpiar()
        {
            tiempoRestante.Content = "00:00:00";
            tiempoRecorrido.Content = "00:00:00";
            tiempoProgramado.Content = "00:00:00";
            numMuestras.Value = reset;
            intervalo.Value = reset;
            numDato = 0;
            muestras = 0;
            grafico.clear();
            //grafico.clear();//limpia el grafico de lineas

            if (Datos != null)
            {
                Datos.Clear();//limpia los datos almacenados
                grillaDatos.Items.Refresh();//refresca los datos de la grilla
                btnExportar.IsEnabled = false;//inhabilita el boton de exportar archivos
            }
        }

        private void setTimer()
        {
            reloj = new DispatcherTimer();
            reloj.Tick += new EventHandler(Temporizadores);
            reloj.Start();
            reloj.Interval = TimeSpan.FromMilliseconds(1000);
        }
        private void Temporizadores(object sender, EventArgs e)
        {
            //Tiempo restante
            tiempoRestante.Content = Temporizador.calcularTemporizador(totalSegundos - segundos);
            totalSegundos--;
            //Tiempo transcurrido
            tiempoRecorrido.Content = Temporizador.calcularTemporizador(transcurrido);
            transcurrido++;
        }

        ///<summary>
        ///Evento del botón exportar archivo, guarda un archivo en formato CSV
        ///compatible con excel
        ///</summary>
        private void btnExportarClick(object sender, RoutedEventArgs e)
        {
            if (Datos != null)
            {
                //cuadro de dialogo que captura la ruta del archivo
                Microsoft.Win32.SaveFileDialog dlg = GestorDeArchivos.saveDialog();
                Nullable<bool> result = dlg.ShowDialog();//resultado de la ventana que guarda los archivos
                // se procesa si el resultado es guardar
                if (result == true)
                {
                    try
                    {
                        StreamWriter sw = new StreamWriter(dlg.FileName, false, Encoding.Default);//archivo de salida con la ruta y el nombre del archivo
                        sw.Write(GestorDeArchivos.getHeader(grillaDatos));//obtiene las cabeceras de la grilla de datos
                        GestorDeArchivos.ExportCsv(sw, Datos);//guarda el archivo en formato CSV
                        btnLimpiarClick(null, null);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Error de Exportación", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                }

            }

        }
    }
}
