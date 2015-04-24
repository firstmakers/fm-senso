using SistemaLuzTemperatura.conexion;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using HID;
using System.Collections.ObjectModel;
using System.Threading;

namespace TMB.Vistas
{
    /// <summary>
    /// Lógica de interacción para Temp.xaml
    /// </summary>
    public partial class Luz : Page
    {
        private DispatcherTimer reloj;
        DispatcherTimer timer;
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
      


        public Luz(Dispositivo _tarjeta)
        {
            InitializeComponent();
            btnExportar.IsEnabled = false; //deshabilita botones Exportar, Limpiar y Detener
            btnLimpiar.IsEnabled = false;
            btnDetener.IsEnabled = false;
            Datos = new ObservableCollection<DatosLuzTemperatura>();
            tarjeta = _tarjeta;
            tarjeta.Conexion += tarjeta_Conexion;
            tarjeta.Desconexion += tarjeta_Desconexion;
            intervalo.onValueChanged+= intervalo_onValueChanged;
            numMuestras.onValueChanged += intervalo_onValueChanged;
           
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
            btnIniciar.IsEnabled = false;
            btnDetenerClick(null, null);
        }

        private void tarjeta_Conexion()
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
                        tarjeta.ComenzarMedicion(Command.Luz);
                        btnIniciar.IsEnabled = false;
                        btnLimpiar.IsEnabled = false;//inhabilita los botones limpiar
                        btnExportar.IsEnabled = false;//Exportar
                        btnDetener.IsEnabled = true;//Habilita el boton Detener
                       

                        this.segundos = intervalo.Value;
                        if (muestras == 0)
                            this.muestras = numMuestras.Value;

                        totalSegundos = segundos * muestras;
                        transcurrido = 0;
                       
                        tiempoProgramado.Content = Temporizador.calcularTiempo(segundos, muestras);
                        Thread.Sleep(200);
                        //inicia el timer de captura de datos
                        timer = new DispatcherTimer();//nuevo timer
                        timer.Tick += OnTimedEvent;
                        timer.Start();//inicio del timer
                        int x = segundos * 1000;
                        timer.Interval = TimeSpan.FromMilliseconds(x);//intervalo de tiempo entre cada tick     
                        setTimer();

                        grillaDatos.DataContext = Datos;//agrego la colección a la grilla de datos
                    }
                    else {
                        tarjeta.DetenerMedicion();
                        btnIniciarClick(sender, e);// llamada recursiva al metodo iniciar
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
                tarjeta_DatosNuevaMedicion(tarjeta.getLight());
            }
            //else
            //{
            //    btnDetenerClick(null, null);
            //    numMuestras.Value = 1;
            //    intervalo.Value = 1;
            //    tiempoRestante.Content = "00:00:00";
            //}

        }



        private void tarjeta_DatosNuevaMedicion(double luz){

            double nivelLux = 0;
            if (luz >= 1)
                nivelLux = Math.Round((Math.Log10(luz) / 4.815) * 100, 1);

            agregarDatos(new DatosLuzTemperatura
                {
                    dataLux = luz.ToString(),
                    dataNivelLuz = nivelLux.ToString(),
                    dataFecha = DateTime.Now.ToString(),
                    //dataNumDato = numDato

                });
                luminosidad.AnimationControl(luz);
                grafico.addLux(luz, nivelLux, numDato);
                this.muestras--;
                //numMuestras.Value = muestras;
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
        ///<param name="luz">
        ///Objeto de tipo Luz contiene el valor Lux y el nivel de la luz
        ///</param>
        private void agregarDatos(DatosLuzTemperatura luz)
        {
            numDato++;//incremento del numero de datos
            luz.dataNumDato = numDato;
            Datos.Add(luz); //Agrega un nuevo objeto con los datos capturados desde la tarjeta
            grillaDatos.ScrollIntoView(luz);
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
            //ampolleta.controlarLuminosidad(0);//intensidad de la ampolleta apagada (0)
            //tarjeta.cerrarConexion();
            luminosidad.SetAnimationColor();

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
            //tiempoEstimado.Content = "00:00:00:00";
            numMuestras.Value = 1;
            intervalo.Value = 1; 
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
        /// <summary>
        /// este timer controla los temporizadores
        /// </summary>
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

                ////cuadro de dialogo que captura la ruta del archivo
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
