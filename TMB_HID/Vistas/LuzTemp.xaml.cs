using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using HID;
using SistemaLuzTemperatura.conexion;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace TMB.Vistas
{
    /// <summary>
    /// Lógica de interacción para LuzTemp.xaml
    /// </summary>
    public partial class LuzTemp : Page
    {

        /// <summary>
        /// Este Atributo inicializa un Timer que controla los intervalos de tiempo en que la aplicación consulta un dato a la tarjeta
        /// </summary>
        private DispatcherTimer timer;
        /// <summary>
        /// Controla los temporizadores del tiempo transcurrido, restante y tiempo total
        /// </summary>
        private DispatcherTimer reloj;
        /// <summary>
        /// Objeto de la Clase
        /// <see cref="SistemaLuzTemperatura.conexion.Tarjeta">Tarjeta</see> se instancia para leer los datos desde la tarjeta
        /// </summary>
        public Dispositivo tarjeta;
        /// <summary>
        /// Intervalos de tiempo entre consultas de datos hacia la tarjeta medidos en segundos
        /// </summary>
        private int segundos = 0;
        /// <summary>
        /// Numero de muestras a capturar desde la tarjeta
        /// </summary>
        private int muestras = 0;
        /// <summary>
        /// Total de segundos que se ejecuta la lectura de datos
        /// </summary>
        private int totalSegundos = 0;
        /// <summary>
        /// Total de segundos trasncurridos en la lectura de datos
        /// </summary>
        private int transcurrido = 0;
        /// <summary>
        /// Valor por defecto de los spinner de muestras  e intervalo de tiempo(segundos)
        /// </summary>
        private int reset = 1;
        /// <summary>
        /// Valor del numero actual de datos obtenidos desde la tarjeta
        /// </summary>
        private int numDato = 0;
        /// <summary>
        /// Coleccion de objetos <see cref="SistemaLuzTemperatura.conexion.DatosLuzTemperatura">DatosLuzTemperatura</see> 
        /// donde se almacenan temporalmente los datos obtenidos
        /// </summary>
        ObservableCollection<DatosLuzTemperatura> Datos;

        public delegate void Desconexion();
        public delegate void Conexion();
        public delegate void NuevoDato();

        public LuzTemp(Dispositivo _tarjeta)
        {
            InitializeComponent();

            Datos = new ObservableCollection<DatosLuzTemperatura>();//crea el objeto que contiene los datos capturados
            tarjeta =_tarjeta;
            intervalo.onValueChanged += intervalo_onValueChanged;
            numMuestras.onValueChanged += intervalo_onValueChanged;
            tarjeta.Conexion += tarjeta_Conexion;
            tarjeta.DesconexionSonda += tarjeta_DesconexionSonda;//suscripción al evento de desconexión de la sonda de temperatura
            tarjeta.Desconexion += tarjeta_desconexion;//suscripcion al evento que proporciona los datos
           // tarjeta.SerialDesconnection += DesconexionHandler;//suscripcion al evento de desconexión
            establecerEstadoInicial();
        }
        private void tarjeta_DesconexionSonda()
        {
            this.Dispatcher.BeginInvoke(new Desconexion(desconexionSonda));
        }
        private void desconexionSonda()
        {
            btnDetenerClick(null, null);
            //MessageBox.Show("El Sensor de Temperatura está desconectado.", "Sensor de Temperatura", MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void intervalo_onValueChanged()
        {
            tiempoProgramado.Content = Temporizador.calcularTiempo(intervalo.Value, numMuestras.Value);
        }
        private void tarjeta_desconexion()
        {
            this.Dispatcher.BeginInvoke(new Desconexion(desconexion));
        }

        private void desconexion()
        {
            btnDetenerClick(null, null);
            btnIniciar.IsEnabled = false;
            Debug.WriteLine("finalizando lectura de datos");
        }

        private void tarjeta_Conexion()
        {
            this.Dispatcher.BeginInvoke(new Conexion(conexion));
        }

        private void conexion()
        {
            btnIniciar.IsEnabled = true;
        }

        private void establecerEstadoInicial()
        {
            btnExportar.IsEnabled = false; //deshabilita los botones Exportar,Limpiar y Detener
            btnLimpiar.IsEnabled = false;
            btnDetener.IsEnabled = false;
            if (tarjeta.conectado)
            {
                btnIniciar.IsEnabled = true;
            }
            else
            {
                btnIniciar.IsEnabled = false;
            }
        }


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
                        tarjeta.ComenzarMedicion(Command.LuzTemperatura);

                        this.segundos = intervalo.Value;
                        if(muestras==0)
                            this.muestras = numMuestras.Value;
                        btnIniciar.IsEnabled = false;
                        btnExportar.IsEnabled = false;//deshabilita los botones de exportar y limpiar
                        btnLimpiar.IsEnabled = false;
                        btnDetener.IsEnabled = true;//habilita el boton detener
                        
                        totalSegundos = segundos * muestras;
                        transcurrido = 0;
                        tiempoProgramado.Content = Temporizador.calcularTiempo(segundos, muestras);
                        Thread.Sleep(1200);
                        //inicia el timer de captura de datos
                        timer = new DispatcherTimer();
                        timer.Tick += OnTimedEvent;
                        timer.Start();
                        int x = segundos * 1000;//corrección de retraso de los timers
                        timer.Interval = TimeSpan.FromMilliseconds(x);
                        setTimer();

                        grillaDatos.DataContext = Datos;
                    }
                    else {
                        tarjeta.DetenerMedicion();
                        btnIniciarClick(sender, e);// llamada recursiva al metodo iniciar
                    }
                }
                else
                {
                    numMuestras.Value= reset;
                    intervalo.Value= reset;
                    MessageBox.Show(validacion, "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show(Constantes.MENSAJE_TARJETA_DESCONECTADA, "Error de lectura", MessageBoxButton.OK, MessageBoxImage.Error
                           );
            }
        }

        private void tarjeta_DatosNuevaMedicion(double luz, double temp)//implementar dispatcher!!!!!!
        {

            double nivelLux = 0;
            if (luz >= 1)
                nivelLux = Math.Round((Math.Log10(luz) / 4.815) * 100, 1);

            double fahrenheit = Math.Round(((temp * 9 / 5) + 32), 1);
                //se muestran los valores en la interfaz gráfica
            agregarDatos(new DatosLuzTemperatura
            {
                    dataCelsius = temp.ToString(),
                    dataFahrenheit = fahrenheit.ToString(),
                    dataLux = luz.ToString(),
                    dataNivelLuz = nivelLux.ToString(),
                    dataFecha = DateTime.Now.ToString(),
                    //dataNumDato = numDato

            });

                //se aplican las animaciones del gráfico
                graficoLuminosidad.addLux(luz, nivelLux, numDato);
                graficoTemperatura.addTemp(temp, fahrenheit, numDato);

                this.muestras--;
                //numMuestras.Text = muestras.ToString();
                /*condición para que la última iteración no se demore en entregar 
                 * el resultado, evita que la aplicación se cuelgue cuando las muestras 
                 * se toman en un intervalo de segundos muy amplio
                */
                if (muestras == 0 && timer != null)
                {
                    timer.Interval = TimeSpan.FromSeconds(1);
                    reloj.Interval = TimeSpan.FromSeconds(1);
                    btnDetenerClick(this,null);
                    tiempoRestante.Content = "00:00:00";
                    tiempoRecorrido.Content = "00:00:00";
                    //numMuestras.Value = reset;
                    //intervalo.Value = reset;

                }
            
        }

        ///<summary>
        ///Evento de temporizador que obtiene los datos 
        ///de la tarjeta cada X segundos
        ///</summary>
        private void OnTimedEvent(object sender, EventArgs e)
        {
            double temp = 0;
            double luz = 0;
            //se obtienen los datos
            if (muestras > 0)
            {
                //tarjeta.WriteCommand(Comando.LuzTemperatura);//consulta el dato
                luz = tarjeta.getLight();
                temp = tarjeta.getTemp();
                if (temp == 400)
                    return;
                tarjeta_DatosNuevaMedicion(luz, temp);
            }
  
        }

        ///<summary>
        ///Agrega los datos a la colección 
        ///</summary>
        ///<param name="data">
        ///Objeto de tipo Luz contiene el valor Lux y el nivel de la luz
        ///</param>
        private void agregarDatos(DatosLuzTemperatura data)
        {
            numDato++;
            data.dataNumDato = numDato;
            Datos.Add(data);
            grillaDatos.ScrollIntoView(data);
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
                timer.Stop();
                reloj.Stop();

            }
            if (Datos.Count != 0)
            {
                btnExportar.IsEnabled = true;
                btnLimpiar.IsEnabled = true;

            }


            this.timer = null;
            this.reloj = null;
            //ampolleta.controlarLuminosidad(0);
            //termometro.controlarTemperatura(0);

            
        }


        ///<summary>
        ///Evento click del botón Limpiar
        ///</summary>
        private void btnLimpiarClick(object sender, RoutedEventArgs e)
        {
            if (sender != btnLimpiar)
            {
                limpiar();
            }
            else if (MessageBox.Show("¿Desea limpiar los datos almacenados?",
                "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes && sender == btnLimpiar)
            {
                limpiar();
            }

        }

        /// <summary>
        /// Limpia las variables de la interfaz gráfica
        /// </summary>
        private void limpiar()
        {
            //mostrarPaneles("0", "0", "0", "0");
            tiempoRestante.Content = "00:00:00:00";
            tiempoRecorrido.Content = "00:00:00:00";
            tiempoProgramado.Content = "00:00:00:00";
            //caluloTiempo.Content = "00:00:00:00";
            numMuestras.Value = reset;
            intervalo.Value = reset;
            numDato = 0;
            muestras = 0;
            graficoLuminosidad.clear();
            graficoTemperatura.clear();
            if (Datos != null)
            {
                Datos.Clear();
                grillaDatos.Items.Refresh();
                btnExportar.IsEnabled = false;
            }
        }

        ///<summary>
        ///Evento click del botón calcular tiempo, calcula el tiempo estimado 
        ///para la captura de datos la captura de datos de la tarjeta
        /////</summary>
        //private void btnCalcularTiempoClick(object sender, RoutedEventArgs e)
        //{
        //    calcularTiempo();

        //}

        /// <summary>
        /// 
        ///// </summary>
        //private void calcularTiempo()
        //{
        //    string validacion = new Validaciones().validarDatos(udsegundos.Text, numMuestras.Text);//validaciones de los datos de entrada
        //    //si las validaciones son correctas
        //    if (validacion.Equals("1"))
        //    {
        //        caluloTiempo.Content = Temporizador.calcularTiempo(Convert.ToInt32(udsegundos.Text),
        //                                                    Convert.ToInt32(numMuestras.Text));
        //    }
        //    else
        //    {
        //        numMuestras.Text = reset;
        //        udsegundos.Text = reset;
        //        MessageBox.Show(validacion, "Atención", MessageBoxButton.OK, MessageBoxImage.Warning);
        //    }
        //}

        ///<summary>
        ///Controla los relojes  de temporización de la aplicación
        ///</summary>
        private void setTimer()
        {
            reloj = new DispatcherTimer();
            reloj.Tick += new EventHandler(Temporizadores);
            reloj.Start();
            reloj.Interval = TimeSpan.FromMilliseconds(1000);
        }

        ///<summary>
        ///Controla el temporizador de la aplicación
        ///</summary>
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
        private void exportarArchivoClick(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            if (Datos != null)
            {
                //cuadro de dialogo que captura la ruta del archivo
                Microsoft.Win32.SaveFileDialog dlg = GestorDeArchivos.saveDialog();
                Nullable<bool> result = dlg.ShowDialog();
                // se procesa si el resultado es guardar
                if (result == true)
                {
                    try
                    {
                        StreamWriter sw = new StreamWriter(dlg.FileName, false, Encoding.Default);
                        sw.Write("N°; Celsius; Farhenheit; Lux; Nivel Luz; Fecha | Hora");
                        sw.WriteLine();
                        GestorDeArchivos.ExportCsv(sw, Datos);
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
