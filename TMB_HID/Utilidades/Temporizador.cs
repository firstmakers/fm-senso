using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLuzTemperatura.conexion
{
    /// <summary>
    /// Esta Clase maneja los temporizadores de la aplicación de la clase <see cref="SistemaLuzTemperatura.LuzTempPage">LuzTempPage</see>
    /// </summary>
    /// <remarks>Autor: Edison Delgado</remarks>
    class Temporizador
    {

        /// <summary>
        /// Calcula el tiempo estimado que tarda la aplicación en realizar una lectura de datos 
        /// </summary>
        /// <param name="segundos">Cantidad de segundos</param>
        /// <param name="muestras">Numero de muestras</param>
        /// <returns>Retorna un String con el tiempo calculado en formato dd:hh:mm:ss</returns>
        public static String calcularTiempo(int segundos, int muestras)
        {
            Int32 totalSegundos = segundos * muestras;
            TimeSpan t2 = TimeSpan.FromSeconds(totalSegundos);
            int t = 0;
            try
            {
                t = Convert.ToInt32(t2.Days);
            }catch(Exception e){
                Debug.WriteLine("Temporizador " + e.Message);
            }
            String t1 = "";
            if (t > 0) t1 += t.ToString()+":";
               t1 += t2.Hours.ToString().PadLeft(2, '0')
                 + ":" + t2.Minutes.ToString().PadLeft(2, '0')
                 + ":" + t2.Seconds.ToString().PadLeft(2, '0');
            return t1;
        }

        /// <summary>
        /// Calcula el tiempo estimado que tarda la aplicación en realizar una lectura de datos
        /// </summary>
        /// <param name="segundos">Cantidad en segundos</param>
        /// <returns>Retorna un String con el tiempo calculado en formato dd:hh:mm:ss</returns>
        public static String calcularTemporizador(int segundos)
        {
            TimeSpan cuentaRegresiva = TimeSpan.FromSeconds(segundos);
            int t1 = 0;
            try
            {
                t1 = Convert.ToInt32(cuentaRegresiva.Days);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Temporizador " + e.Message);
            }

            String t = "";
            if (t1 > 0) t += t1.ToString() + ":";
            t += cuentaRegresiva.Hours.ToString().PadLeft(2, '0')
                + ":" + cuentaRegresiva.Minutes.ToString().PadLeft(2, '0')
                + ":" + cuentaRegresiva.Seconds.ToString().PadLeft(2, '0');
            if (t.Contains("-")) t = "00:00:00";//para números negativos
            return t;
        }

    }
}

