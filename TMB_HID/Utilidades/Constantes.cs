using System;
using System.Reflection;
namespace SistemaLuzTemperatura.conexion
{
    /// <summary>
    /// Esta Clase Contiene la mayoría las contastes de la aplicación, como los comandos de lectura de la tarjeta, textos de los mensajes desplegados por la interfaz, titulos de los gráficos entre otros.
    /// </summary>
    /// <remarks>
    /// Autor: Edison Delgado A.
    /// </remarks>
    static class Constantes
    {
        /// <summary>
        /// Desfase de lectura de tarjeta y temporizador en milisegundos
        /// </summary>
        public static int DESFASE_LECTURA_TARJETA = 200;
        /// <summary>
        ///Mensaje de la interfaz, cuando se desconecta la tarjeta del puerto USB 
        /// </summary>
        public static String MENSAJE_TARJETA_DESCONECTADA = "No es posible encontrar la tarjeta de monitoreo, verifique que se encuentra conectada";
        /// <summary>
        /// Mensaje de la interfaz, Cuando se pulsa o se llama al botón Limpiar
        /// </summary>
        public static String MENSAJE_LIMPIAR = "¿Desea limpiar los datos almacenados?";
        /// <summary>
        /// Título con el cual se instancia el gráfico de temperatura de la interfaz gráfica
        /// </summary>
        public static String TITULO_GRAFICO_TEMP = "Gráfico Medición de Temperaturas";
        /// <summary>
        /// Título con el cual se instancia el gráfico de Luz de la interfaz gráfica
        /// </summary>
        public static String TITULO_GRAFICO_LUX = "Gráfico Medición de Luz"; 
        /// <summary>
        /// Comando de la tarjeta física para leer una dato de temperatura
        /// </summary>
        public static String LEER_TEMPERATURA = "C";
        /// <summary>
        /// Comando de la tarjeta física para leer una dato de temperatura y luz simultáneamente
        /// </summary>
        public static String LEER_TEMPERATURA_LUX = "A";
        /// <summary>
        /// Comando de la tarjeta física para leer una dato de Luz
        /// </summary>
        public static String LEER_LUX = "D";
        /// <summary>
        /// Comando de la tarjeta física para leer la version del Firmware
        /// </summary>
        public static String VERSION_TARJETA = "V";
        /// <summary>
        /// Comando de la tarjeta física para Actualizar el firmware (no se utiliza actualmente)
        /// </summary>
        public static String ACTUALIZAR_FIRMWARE = "B";


        public static Version VersionAssembly() {
           return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
        }

        public static string Version()
        {
            string version = "";
            Version ver = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            return version = "v." + ver.Major.ToString() + "." + ver.Minor.ToString(); ;
        }

        public static string Copyright
        {
            get{
               // Get all Copyright attributes on this assembly
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
               // If there aren't any Copyright attributes, return an empty string
                if (attributes.Length == 0)
                    return "";
                // If there is a Copyright attribute, return its value
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
           }
     
        }
    }
}
