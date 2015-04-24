using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaLuzTemperatura.conexion
{
    /// <summary>
    /// Clase que maneja los datos de las mediciones de Luz y Temperatura, posee la estructura de los datos que se almacenan en tiempo de ejecución en la aplicación como los grados<see cref="dataCelsius"> Celsius</see>,<see cref="dataFahrenheit"> Fahrenheit</see>, <see cref="dataLux"> Lux </see>, la <see cref="dataFecha">Fecha y hora</see> que se midieron los datos, etc.
    /// </summary>
    /// <remarks>Autor: Edison Delgado</remarks>
    public class DatosLuzTemperatura
    {
        /// <summary>
        /// Número del dato registrado, también es la posición asignada en la colección de datos
        /// </summary>
        public int dataNumDato { get; set; }

        /// <summary>
        /// Temperatura en grados Celsius
        /// </summary>
        public String dataCelsius { get; set; }

        /// <summary>
        /// Temperatura en grados Fahrenheit
        /// </summary>
        public String dataFahrenheit { get; set; }

        /// <summary>
        /// Lux
        /// </summary>
        public String dataLux { get; set; }

        /// <summary>
        /// nivel de Luz
        /// </summary>
        public String dataNivelLuz { get; set; }

        /// <summary>
        /// Fecha y hora en la que se capturaron los datos
        /// </summary>
        public String dataFecha { get; set; }

        public String dataHumedad { get; set; }

    }
}
