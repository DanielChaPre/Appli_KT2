using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Appli_KT2.View
{
	public class SplashPage : ContentPage
	{
        Image splashImage;
        Image secondImage;
		public SplashPage ()
		{
            NavigationPage.SetHasNavigationBar(this, false);

            var sub = new AbsoluteLayout();
            splashImage = new Image
            {
                //Source = "ic_sices.png",
                Source = "appli_kt_logo_som.png",
                WidthRequest = 300,
                HeightRequest = 300
            };
            secondImage = new Image
            {
                Source = "sices.png",
                WidthRequest = 200,
                HeightRequest = 200
            };
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(secondImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(secondImage, new Rectangle(0.5, 0.8, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);
            sub.Children.Add(secondImage);

             this.BackgroundColor = Color.FromHex("#110791");
          //  this.BackgroundColor = Color.White;
            this.Content = sub;
		}
        
        /*Nota...
         *
         *Agregar un icono de carga o un activityindicator para que el usuario vea que eta interactuando con la aplicacion 
         * y que se esta cargando el login y la configuraciones del tema
         * **/

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();

                await splashImage.ScaleTo(1, 2000);
                await splashImage.ScaleTo(0.8, 1200, Easing.Linear);
                //await splashImage.ScaleTo(150,1200, Easing.Linear);
                VerificarLogin();
                VerificarConfiguracionTema();
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            catch (Exception ex)
            {
                
            }
           
        }

        public void VerificarLogin()
        {
            try
            {
                if (string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["usuario"].ToString()))
                {
                }
                else if (string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["contrasena"].ToString()))
                {

                }
            }
            catch (Exception ex)
            {

            }
        }

        public void VerificarConfiguracionTema()
        {

        }

    }
}