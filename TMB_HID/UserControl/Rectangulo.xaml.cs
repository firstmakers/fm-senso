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
    /// Lógica de interacción para Rectangulo.xaml
    /// </summary>
    public partial class Rectangulo : UserControl
    {
        RecState _state;
        /// <summary>
        /// Colores por Defecto de la animacion e iconos
        /// </summary>
        public Color DefaultColorAnimation { get; set; }
        public Color OnColorAnimation { get; set; }
        public Color ColorIcon { get; set; }
        public Color ColorAnimationLT { get; set; }

        public RecState State
        {
            set
            {
                _state = value;
                SetColor(_state);
            }
            get { return _state; }
        }

        public Rectangulo()
        {
            this.InitializeComponent();
            DefaultColorAnimation = Color.FromRgb(137, 171, 63);
            OnColorAnimation = Color.FromRgb(252, 218, 23);
            ColorAnimationLT = Color.FromRgb(91, 106, 166);
            ColorIcon = Colors.White;

        }

        public void AnimationRun(bool b)
        {
            //Color xcolor;
            Color color;
            if (b)
            {
                color = OnColorAnimation;
                // xcolor = DefaultColorAnimation;
            }
            else
            {
                color = DefaultColorAnimation;
                //xcolor = OnColorAnimation; 
            }
            rect.Fill = new SolidColorBrush(color);
            /*NameScope.SetNameScope(this, new NameScope());
            //rect.Fill = new SolidColorBrush(color);
            
           // this.RegisterName("RECT", rect.Fill);
           
     
            ColorAnimation ColorAnimation = new ColorAnimation();
            //ColorAnimation.From = xcolor;
            ColorAnimation.To = color;
            ColorAnimation.Duration = TimeSpan.FromSeconds(1);
            Storyboard.SetTargetName(ColorAnimation, "brush");
            Storyboard.SetTargetProperty(
                ColorAnimation, new PropertyPath(SolidColorBrush.ColorProperty));
            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Children.Add(ColorAnimation);
            myStoryboard.Begin(this);*/

        }

        public void SetColor(SolidColorBrush c)
        {
            rect.Fill = c;
        }

        private void SetColor(RecState state)
        {
            if (state == RecState.AnimationOFF)
            {
                rect.Fill = new SolidColorBrush(DefaultColorAnimation);
            }
            else if (state == RecState.AnimationLT)
            {
                rect.Fill = new SolidColorBrush(ColorAnimationLT);
            }
            else
            {
                rect.Fill = new SolidColorBrush(ColorIcon);
            }
        }
    }

    public enum RecState
    {
        Icon,
        AnimationLT,
        AnimationON,
        AnimationOFF
    };
}
