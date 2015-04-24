using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace TMB.Vistas
{
    /// <summary>
    /// Lógica de interacción para Guias.xaml
    /// </summary>
    public partial class Guias : Page
    {
        ObservableCollection<Model> Contenido = new ObservableCollection<Model>();
        public Guias()
        {
            InitializeComponent();
            Contenido.Add(new Model { 
                Actividad = "Edison", 
                Icono = "Imagenes/pdf.jpg" 
            });
            Contenido.Add(new Model { 
                Actividad = "Edison Delgado", 
                Icono = "Imagenes/pdf-ico.png" });
            Grid.ItemsSource = Contenido;
        }
    }
    public class Model {
        public string Actividad { get; set; }
        public string Icono { get; set; }
    }
}
