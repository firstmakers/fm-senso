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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TMB.UserControls
{
    /// <summary>
    /// Lógica de interacción para Luminosidad.xaml
    /// </summary>
    public partial class Luminosidad : UserControl
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
                else
                {
                    SetAnimationColor();
                }

            }
        }

        public Luminosidad()
        {
            this.InitializeComponent();
            DefaultColorAnimation = Color.FromRgb(137, 171, 63);
            OnColorAnimation = Color.FromRgb(252, 218, 23);
            ColorIcon = Colors.White;
            ColorAnimationLT = Color.FromRgb(91, 106, 166);
            Fill = new SolidColorBrush(ColorIcon);
        }

        public void AnimationControl(double value)
        {
            if (value <= 50)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(false);
                R3.AnimationRun(false);
                R4.AnimationRun(false);
                R5.AnimationRun(false);
                R6.AnimationRun(false);
                R7.AnimationRun(false);
                R8.AnimationRun(false);
                R9.AnimationRun(false);
                R10.AnimationRun(false);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 50 && value <= 100)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(false);
                R4.AnimationRun(false);
                R5.AnimationRun(false);
                R6.AnimationRun(false);
                R7.AnimationRun(false);
                R8.AnimationRun(false);
                R9.AnimationRun(false);
                R10.AnimationRun(false);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 100 && value <= 200)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(false);
                R5.AnimationRun(false);
                R6.AnimationRun(false);
                R7.AnimationRun(false);
                R8.AnimationRun(false);
                R9.AnimationRun(false);
                R10.AnimationRun(false);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 200 && value <= 500)
            {

                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(false);
                R6.AnimationRun(false);
                R7.AnimationRun(false);
                R8.AnimationRun(false);
                R9.AnimationRun(false);
                R10.AnimationRun(false);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }

            else if (value > 500 && value <= 1000)
            {

                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(true);
                R6.AnimationRun(false);
                R7.AnimationRun(false);
                R8.AnimationRun(false);
                R9.AnimationRun(false);
                R10.AnimationRun(false);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }

            else if (value > 1000 && value <= 2000)
            {

                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(true);
                R6.AnimationRun(true);
                R7.AnimationRun(false);
                R8.AnimationRun(false);
                R9.AnimationRun(false);
                R10.AnimationRun(false);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 2000 && value <= 3000)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(true);
                R6.AnimationRun(true);
                R7.AnimationRun(true);
                R8.AnimationRun(false);
                R9.AnimationRun(false);
                R10.AnimationRun(false);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 3000 && value <= 5000)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(true);
                R6.AnimationRun(true);
                R7.AnimationRun(true);
                R8.AnimationRun(true);
                R9.AnimationRun(false);
                R10.AnimationRun(false);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 5000 && value <= 10000)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(true);
                R6.AnimationRun(true);
                R7.AnimationRun(true);
                R8.AnimationRun(true);
                R9.AnimationRun(true);
                R10.AnimationRun(false);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 10000 && value <= 20000)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(true);
                R6.AnimationRun(true);
                R7.AnimationRun(true);
                R8.AnimationRun(true);
                R9.AnimationRun(true);
                R10.AnimationRun(true);
                R11.AnimationRun(false);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 20000 && value <= 30000)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(true);
                R6.AnimationRun(true);
                R7.AnimationRun(true);
                R8.AnimationRun(true);
                R9.AnimationRun(true);
                R10.AnimationRun(true);
                R11.AnimationRun(true);
                R12.AnimationRun(false);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 30000 && value <= 40000)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(true);
                R6.AnimationRun(true);
                R7.AnimationRun(true);
                R8.AnimationRun(true);
                R9.AnimationRun(true);
                R10.AnimationRun(true);
                R11.AnimationRun(true);
                R12.AnimationRun(true);
                RingCenter.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (value > 40000)
            {
                R1.AnimationRun(true);
                R2.AnimationRun(true);
                R3.AnimationRun(true);
                R4.AnimationRun(true);
                R5.AnimationRun(true);
                R6.AnimationRun(true);
                R7.AnimationRun(true);
                R8.AnimationRun(true);
                R9.AnimationRun(true);
                R10.AnimationRun(true);
                R11.AnimationRun(true);
                R12.AnimationRun(true);
                RingCenter.Fill = new SolidColorBrush(OnColorAnimation);
            }
        }


        private void SetAnimationColorLT()
        {
            /* SolidColorBrush color = new SolidColorBrush(ColorAnimationLT);
             R1.brush = color;
             R2.brush = color;
             R3.brush = color;
             R4.brush = color;
             R5.brush = color;
             R6.brush = color;
             R7.brush = color;
             R8.brush = color;
             R9.brush = color;
             R10.brush = color;
             R11.brush = color;
             R12.brush = color;
             RingCenter.Fill = color;*/
        }

        public void SetIconColor()
        {
            SolidColorBrush color = new SolidColorBrush(ColorIcon);
            R1.SetColor(color);
            R2.SetColor(color);
            R3.SetColor(color);
            R4.SetColor(color);
            R5.SetColor(color);
            R6.SetColor(color);
            R7.SetColor(color);
            R8.SetColor(color);
            R9.SetColor(color);
            R10.SetColor(color);
            R11.SetColor(color);
            R12.SetColor(color);
            RingCenter.Fill = color;
        }
        public void SetAnimationColor()
        {
            SolidColorBrush color = new SolidColorBrush(DefaultColorAnimation);
            R1.SetColor(color);
            R2.SetColor(color);
            R3.SetColor(color);
            R4.SetColor(color);
            R5.SetColor(color);
            R6.SetColor(color);
            R7.SetColor(color);
            R8.SetColor(color);
            R9.SetColor(color);
            R10.SetColor(color);
            R11.SetColor(color);
            R12.SetColor(color);
            RingCenter.Fill = color;
        }
    }
    public enum Mode
    {
        Icon,
        AnimationLT,
        Animation
    };
}