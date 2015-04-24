using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMB.UserControls;

namespace TMB
{
    /// <summary>
    /// Lógica de interacción para Grafico.xaml
    /// </summary>
    public partial class Grafico : UserControl
    {
        private Input data = new Input();
        LinearAxis AxisY = new LinearAxis();
        // LinearAxis AxisX = new LinearAxis();
        private Chart _tipo;
        private string concat1;
        private string concat2;



        public int Max { get { return data.max; } set { data.max = value; } }
        public int Min { get; set; }

        public double AxisInterval
        {
            get { return (double)AxisY.Interval; }
            set { AxisY.Interval = value; }
        }
        public double MaximunAxis
        {
            get
            {
                return (double)AxisY.Maximum;
            }
            set
            {
                AxisY.Maximum = value;
            }
        }

        public double MinimunAxis
        {
            get
            {
                return (double)AxisY.Minimum;
            }
            set
            {
                AxisY.Minimum = value;
            }
        }
        public Chart Tipo
        {
            get
            {
                return _tipo;
            }
            set
            {
                _tipo = value;
                setChartType(value);
            }
        }

        public Grafico()
        {
            this.InitializeComponent();

            data.max = Max;


            //AxisX.Title = "n° medición";
            // AxisX.Orientation = AxisOrientation.X;


            // lineChart.Axes.Add(AxisX);
            lineChart.Axes.Add(AxisY);
            lineChart.DataContext = data;
        }

        /// <summary>
        /// Configura el Tipo de gráfico y el color del tema correspondiente (luz o temp)
        /// </summary>
        /// <param name="type">tipo de gráfico</param>
        private void setChartType(Chart type)
        {
            if (type == Chart.LuzDefault)
            {
                lineChart.Title = "Medición de luz";
                lineChart.Style = (Style)FindResource("ChartStyleTMB");
                lineChart.FontSize = 8;
                mSerie.Title = "Lux";
                mSerie.LegendItemStyle = (Style)FindResource("LegendItemStyleTMB");
                concat1 = "Lux";
                concat2 = "%";
                //AxisY.Maximum = 17000;

            }
            else if (type == Chart.TemperaturaDefault)
            {
                lineChart.Title = "Medición de temperatura";
                lineChart.Style = (Style)FindResource("ChartStyleTMB");
                mSerie.Title = "Celsius";
                mSerie.LegendItemStyle = (Style)FindResource("LegendItemStyleTMB");
                lineChart.FontSize = 8;
                concat1 = "°C";
                concat2 = "°F";
                //AxisY.Interval = 1;


            }
            else if (type == Chart.Luz)
            {
                lineChart.Title = "Medición de luz";
                lineChart.Style = (Style)FindResource("ChartStyleLuz");
                mSerie.Title = "Lux";
                mSerie.LegendItemStyle = (Style)FindResource("LegendItemStyleLuz");
                lineChart.FontSize = 8;
                concat1 = "Lux";
                concat2 = "%";
                //AxisY.Maximum = 200;
                // AxisY.Interval = 100;

            }else if(type == Chart.Humedad){
                lineChart.Title = "Medición de Humedad";
                lineChart.Style = (Style)FindResource("ChartStyleHum");
                mSerie.Title = "Humedad %";
                mSerie.LegendItemStyle = (Style)FindResource("LegendItemStyleHum");
                lineChart.FontSize = 8;
                concat1 = "%";
                concat2 = "%";
            }
            else
            {
                lineChart.Title = "Medición de temperatura";
                lineChart.Style = (Style)FindResource("ChartStyleTemp");
                mSerie.Title = "Celsius";
                mSerie.LegendItemStyle = (Style)FindResource("LegendItemStyleTemp");
                lineChart.FontSize = 8;
                concat1 = "°C";
                concat2 = "°F";
                //AxisY.Maximum = 40;
                //AxisY.Interval = 1;

            }
            AxisY.Title = concat1;
            AxisY.Orientation = AxisOrientation.Y;

            // AxisY.Minimum = 0;
        }



        public void setPanel(double value1, double value2)
        {
            VP.Content = value1.ToString() + " " + concat1;
            VS.Content = value2.ToString() + " " + concat2;

        }

        /// <summary>
        /// Quita todos los datos de los gráficos
        /// </summary>
        public void clear()
        {
            this.data.clear();
            VP.Content = "0 " + concat1;
            VS.Content = "0 " + concat2;
        }

        public void addLux(double p1, double p2, int pos)
        {
            this.data.Add(pos.ToString(), p1);
            setPanel(p1, p2);

        }

        public void addTemp(double p1, double p2, int pos)
        {
            this.data.Add(pos.ToString(), p1);
            setPanel(p1, p2);
        }

        public void addHum(double p1, int pos) {
            this.data.Add(pos.ToString(), p1);
            setPanel(p1, 0);
        }


    }
    public enum Chart
    {
        Luz,
        Temperatura,
        LuzDefault,
        TemperaturaDefault,
        Humedad
    };
}