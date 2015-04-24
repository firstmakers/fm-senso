using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media.Imaging;

namespace TMB.Vistas
{
    /// <summary>
    /// Lógica de interacción para Actividades.xaml
    /// </summary>
    public partial class Actividades : Page
    {

        string sitio = "http://www.austral38.cl";
        /// <summary>
        /// Contiene la ruta donde se instaló la aplicación
        /// </summary>
        String DIR_INSTALACION = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        /// <summary>
        /// Contiene la ruta donde esta la carpeta con ejercicios
        /// </summary>
        String DIR_EJERCICIOS = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Ejercicios/";
        /// <summary>
        /// 
        /// </summary>
        public ObservableCollection<ContentActivity> Ejercicios { get; set; }
        int indice = -1;
        ObservableCollection<ContentActivity> Contenido = new
        ObservableCollection<ContentActivity>();
        // private ContentActivity Content;
        public Actividades()
        {
            InitializeComponent();
            Ejercicios = getContent();
            Grid.ItemsSource = Ejercicios;
        }





        public ObservableCollection<ContentActivity> getContent()
        {
            ObservableCollection<ContentActivity> list = new ObservableCollection<ContentActivity>();
            int counter = 1;
            string line;
            
            StreamReader file = new StreamReader(DIR_EJERCICIOS + "Actividades.ACT", System.Text.Encoding.Default);
            while ((line = file.ReadLine()) != null)
            {

                string[] actividad = line.Split('-');

                ContentActivity act = new ContentActivity();
                act.Actividad = actividad[1];
                act.Extension = "pdf";
                act.Numero = actividad[0];
                act.Icono = "Imagenes/pdf.png";
                act.FullPath = DIR_EJERCICIOS + counter + ".pdf";

                list.Add(act);
                counter++;
            }

            file.Close();
            return list;
        }



        private void cargarVisorDocumentos()
        {
            try
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = Ejercicios[indice].FullPath;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void AbrirManualUsuario(object sender, RoutedEventArgs e)
        {
            DirectoryInfo oDirectorio = new DirectoryInfo(DIR_EJERCICIOS);
            if (oDirectorio.Exists)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = DIR_INSTALACION + "/Manual/Manual_Usuario.pdf";
                process.Start();
            }
        }


        private void seleccion(object sender, SelectionChangedEventArgs e)
        {
            if (Grid.SelectedIndex != -1)
            {
                this.indice = Grid.SelectedIndex;
                System.Diagnostics.Debug.WriteLine("Se ha selecionado la posicion " + indice);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(sitio);
        }

        private void PdfClick(object sender, RoutedEventArgs e)
        {
            
            if (Grid.SelectedIndex != -1)
            {
                cargarVisorDocumentos();
            }
            Grid.SelectedIndex = -1;
        }
    }
    public class ContentActivity
    {
        public ContentActivity() { }

        public string Nombre { get; set; }
        public string Actividad { get; set; }
        public string Numero { get; set; }
        public string Extension { get; set; }
        public string Icono { get; set; }
        public string FullPath { get; set; }
    }
}

