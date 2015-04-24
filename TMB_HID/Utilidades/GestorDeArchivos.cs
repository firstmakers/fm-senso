using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace SistemaLuzTemperatura.conexion
{
    /// <summary>
    /// Esta Clase Maneja la Salida de archivos de la aplicación, crea dialogos para guardar los datos.Los datos son almacenados en el equipo en la ruta que el usuario estime conveniente
    /// </summary> 
    /// <remarks>Autor: Edison Delgado</remarks>
    class GestorDeArchivos
    {
        /// <summary>
        /// Separador del archivo CSV
        /// </summary>
        private static String coma = ";";

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        public GestorDeArchivos()
        {
        }

        /// <summary>
        /// Muestra una alerta cuando existen archivos pendientes por guardar
        /// </summary>
        /// <returns>Respuesta del usuario</returns>
        public static String alertSaveDialog()
        {
            String respuesta = "cancel";
            MessageBoxResult rs = MessageBox.Show("Existen datos de mediciones sin guardar\n¿Desea guardar los cambios efectuados?",
                            "Sistema de Medición luz y temperatura",
                            MessageBoxButton.YesNoCancel,
                            MessageBoxImage.Question);
            if (rs == MessageBoxResult.Yes)
            {
                return respuesta = "yes";
            }
            else if (rs == MessageBoxResult.No)
            {
                return respuesta = "no";
            }
            else
            {
                return respuesta;
            }


        }

        /// <summary>
        /// Crea una ventana de dialogo con las extenciones de archivos
        /// </summary>
        /// <returns>Retorna una ventana de dialogo con la extensión y el nombre del archivo por defecto</returns>
        public static Microsoft.Win32.SaveFileDialog saveDialog()
        {

            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            dlg.FileName = "Medición"; // Nombre por defecto del documento
            dlg.DefaultExt = ".csv"; // Extensión por defecto
            dlg.Filter = "Archivo (.csv)|*.csv"; // Filter files by extension 
            return dlg;
        }


        /// <summary>
        /// Lee una cabecera de un datagrid
        /// </summary>
        /// <param name="grid">Grid de datos</param>
        /// <returns>Retorna un String con los nombres de la cabecera</returns>
        public static String getHeader(System.Windows.Controls.DataGrid grid)
        {
            String header = "";
            // iteración de la grilla de datos para obtener los encabezados
            for (int i = 0; i < grid.Columns.Count; i++)
            {
                header += grid.Columns[i].Header;
                if (i != grid.Columns.Count)
                {
                    header += coma;
                }
            }
            return header;
        }
        /// <summary>
        /// Exporta un grid de datos en formato CSV
        /// </summary>
        /// <param name="sw">Archivo con la ruta de escritura</param>
        /// <param name="Datos">Objeto con los datos capturados</param>
        public static void ExportCsv(StreamWriter sw, ObservableCollection<DatosLuzTemperatura> Datos)
        {

            // se agrega una nueva linea
            sw.Write(sw.NewLine);
            int count = Datos.Count;
            //iteración de la colección de objetos
            for (int i = 0; i < count; i++)
            {
                DatosLuzTemperatura d = Datos[i];
                sw.Write(d.dataNumDato + coma);
                //si el objeto contiene datos vacios, escribirá
                //solamente aquellos que contengan datos
                if (d.dataCelsius != null)
                {
                    sw.Write(d.dataCelsius + coma);
                }
                if (d.dataFahrenheit != null)
                {
                    sw.Write(d.dataFahrenheit + coma);
                }
                if (d.dataLux != null)
                {
                    sw.Write(d.dataLux + coma);
                }
                if (d.dataNivelLuz != null)
                {
                    sw.Write(d.dataNivelLuz + coma);
                }
                if (d.dataFecha != null)
                {
                    sw.Write(d.dataFecha + coma);
                }
                // nueva linea
                sw.Write(sw.NewLine);
            }

            sw.Flush();
            sw.Close();
        }
    }
}


