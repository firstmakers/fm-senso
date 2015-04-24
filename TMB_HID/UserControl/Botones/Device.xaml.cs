using System.Windows.Controls;
using System.Windows.Media;

namespace TMB
{
    /// <summary>
    /// Lógica de interacción para Device.xaml
    /// </summary>
    public partial class Device : UserControl
    {
        SolidColorBrush connected;
        SolidColorBrush disconnected;
        DeviceState _state;

        public DeviceState State
        {
            get { return _state; }
            set
            {
                _state = value;
                if (value == DeviceState.Conectado)
                    Connected();
                else
                    Disconnected();
            }
        }

        public Device()
        {
            InitializeComponent();
            connected = new SolidColorBrush(Color.FromRgb(102, 156, 61));
            disconnected = new SolidColorBrush(Color.FromRgb(252, 0, 0));
        }

        public void Connected()
        {
            Icon1.Fill = connected;
            Icon2.Stroke = connected;
            Icon3.Fill = connected;
        }
        public void Disconnected()
        {
            Icon1.Fill = disconnected;
            Icon2.Stroke = disconnected;
            Icon3.Fill = disconnected;
        }

    }
    public enum DeviceState
    {
        Conectado,
        Desconectado
    }
}
