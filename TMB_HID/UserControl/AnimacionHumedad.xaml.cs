using System;
using System.Collections.Generic;
using System.Text;
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

namespace TMB
{
	/// <summary>
	/// Lógica de interacción para AnimacionHumedad.xaml
	/// </summary>
	public partial class AnimacionHumedad : UserControl
	{
        /// <summary>
        /// Amplitud de la onda
        /// </summary>


        Storyboard s; 

		public AnimacionHumedad()
		{
			this.InitializeComponent();
            s = (Storyboard)TryFindResource("Wave");
            Amplitud.Height = new GridLength(12);
            Altura.Y = 0.60;
		}

        public void Animate(double humidity) {
            if (s != null) {
                if (humidity > 1 && humidity < 20) {
                    //Altura.Y = 0.45;
                    animateY(0.45);
                }
                if (humidity > 20 && humidity < 40)
                {
                    //Altura.Y = 0.25;
                    animateY(0.25);
                } 
                if (humidity > 40 && humidity < 60)
                {
                    //Altura.Y = 0;
                    animateY(0);
                } 
                if (humidity > 60 && humidity < 80)
                {
                    //Altura.Y = -0.10;
                    animateY(-0.15);
                } 
                if (humidity > 80 && humidity < 100)
                {
                    //Altura.Y = -0.20;
                    animateY(-0.25);
                } 
            }
        }

        public void StartAnimation(){
            s.Begin();
        }
        public void StopAnimation() {
            if (s != null)
            {
                animateY(0.60);
                s.Stop();
            }
            
        }

        private void animateY(double value) {
            DoubleAnimation altura = new DoubleAnimation();
            altura.From = Altura.Y;
            altura.To = value;
            altura.Duration =
                new Duration(TimeSpan.FromSeconds(0.4));

            Storyboard.SetTargetName(altura, "Altura");
            Storyboard.SetTargetProperty(altura,
                new PropertyPath(TranslateTransform.YProperty));
            Storyboard myStoryboard = new Storyboard();
            myStoryboard.Children.Add(altura);
            myStoryboard.Begin(this);
        }
	}
}