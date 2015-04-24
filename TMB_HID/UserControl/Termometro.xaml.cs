using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TMB.UserControls
{

    /// <summary>
    /// Lógica de interacción para Termometro.xaml
    /// </summary>
    public partial class Termometro : UserControl
    {
        Mode _mode;
        public Color DefaultColorAnimation { get; set; }

        public Color OnColorAnimation { get; set; }

        public Color ColorAnimationLT { get; set; }

        public Color ColorIcon { get; set; }

        public SolidColorBrush Fill { get; set; }

        public Mode Mode
        {
            get { return _mode; }
            set
            {
                _mode = value;
                if (value == Mode.Icon)
                {
                    SetIconColor();
                }
                else if (value == Mode.AnimationLT)
                {
                    SetAnimationColorLT();
                }
                else { SetAnimationColor(); }

            }
        }


        public Termometro()
        {
            InitializeComponent();
            DefaultColorAnimation = Color.FromRgb(214, 159, 56);
            OnColorAnimation = Color.FromRgb(214, 159, 56);
            ColorIcon = Colors.White;
            ColorAnimationLT = Color.FromRgb(91, 106, 166);
            Fill = new SolidColorBrush(ColorIcon);
        }


        public void RunAnimation(double tmp)
        {
            tmp = (tmp * 1.8) + 80;
            /*animación desde el mercurio en el tmp actual / hasta
             la nueva temperatura*/
            if (tmp > 0)
            {
                DoubleAnimation animacionMercurio = new DoubleAnimation();
                animacionMercurio.From = Mercurio.ActualHeight;
                animacionMercurio.To = tmp;
                animacionMercurio.Duration =
                    new Duration(TimeSpan.FromSeconds(0.5));

                Storyboard.SetTargetName(animacionMercurio, "Mercurio");
                Storyboard.SetTargetProperty(animacionMercurio,
                    new PropertyPath(Rectangle.HeightProperty));
                Storyboard myStoryboard = new Storyboard();
                myStoryboard.Children.Add(animacionMercurio);
                myStoryboard.Begin(this);
            }
        }


        private void SetAnimationColor()
        {
            SolidColorBrush dfColor = new SolidColorBrush(DefaultColorAnimation);
            R1.Fill = dfColor;
            R2.Fill = dfColor;
            R3.Fill = dfColor;
            R4.Fill = dfColor;
            R5.Fill = dfColor;
            R6.Fill = dfColor;
            Envase.Stroke = dfColor;
            Mercurio.Visibility = Visibility.Visible;
            Mercurio1.Visibility = Visibility.Visible;
        }

        private void SetAnimationColorLT()
        {
            SolidColorBrush ltColor = new SolidColorBrush(ColorAnimationLT);
            R1.Fill = ltColor;
            R2.Fill = ltColor;
            R3.Fill = ltColor;
            R4.Fill = ltColor;
            R5.Fill = ltColor;
            R6.Fill = ltColor;
            Envase.Stroke = ltColor;
        }

        private void SetIconColor()
        {
            SolidColorBrush icon = new SolidColorBrush(ColorIcon);
            R1.Fill = icon;
            R2.Fill = icon;
            R3.Fill = icon;
            R4.Fill = icon;
            R5.Fill = icon;
            R6.Fill = icon;
            Envase.Stroke = icon;
            Mercurio.Visibility = Visibility.Hidden;
            Mercurio1.Visibility = Visibility.Hidden;
        }


    }
}

