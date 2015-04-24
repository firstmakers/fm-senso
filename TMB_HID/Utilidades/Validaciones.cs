using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace SistemaLuzTemperatura.conexion
{
    /// <summary>
    /// Esta clase contiene alguna validaciones de la interfaz gráfica
    /// </summary>
   public class Validaciones
    {
        int maxSeg = 86399;//segundo, unidad máxima permitida
        int minSeg = 1;//segundo, unidad mínima permitida
        int minMues = 1;//muestras, unidad máxima
        int maxMues = 10000;


        /// <summary>
        /// constructor
        /// </summary>
        public Validaciones()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string validarDatos(String segundos, String muestras)
        {

            string respuesta = "";

            if (segundos.Equals("") || muestras.Equals(""))
            {
                return respuesta = "hay un campo vacío";
            }
            if (!isInt32(segundos) && !isInt32(muestras))
            {
                return respuesta = "Se ha ingresado un valor inválido";
            }
            else
            {
                int s = Convert.ToInt32(segundos);
                int m = Convert.ToInt32(muestras);
                if (s > maxSeg || s < minSeg)
                {
                    return respuesta = "El intervalo de tiempo debe ser un número entre " + minSeg + " segundos  y 24 horas.";
                }
                if (m > maxMues || m < minMues)
                {
                    return respuesta = "Las muestras deben ser un número entre " + minMues + "-" + maxMues + " muestras";
                }
                return respuesta = "1";
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public bool isInt32(String num)
        {
            try
            {
                Int32.Parse(num);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}