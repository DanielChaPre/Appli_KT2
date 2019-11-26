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
            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

             this.BackgroundColor = Color.FromHex("#000F9F");
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
            base.OnAppearing();

            await splashImage.ScaleTo(1,2000);
            await splashImage.ScaleTo(0.5,1500, Easing.Linear);
            //await splashImage.ScaleTo(150,1200, Easing.Linear);
            VerificarLogin();
            VerificarConfiguracionTema();
            Application.Current.MainPage = new NavigationPage(new MainTabbedPage());

        }

        public void VerificarLogin()
        {

        }

        public void VerificarConfiguracionTema()
        {

        }

    }
}