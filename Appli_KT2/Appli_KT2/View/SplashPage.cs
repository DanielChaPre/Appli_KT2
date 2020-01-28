using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Appli_KT2.View
{
	public class SplashPage : ContentPage
	{
        Image splashImage;
        Image secondImage;
        ActivityIndicator activityIndicator;
        Label lblCarga;
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

            activityIndicator = new ActivityIndicator
            {
                Color = Color.Orange
            };

            lblCarga = new Label
            {
                TextColor = Color.White,
                FontSize = 25,
                Text = "Cargando...",
                IsVisible = false
                
            };

            AbsoluteLayout.SetLayoutFlags(splashImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage, new Rectangle(0.5, 0.3, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(secondImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(secondImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(lblCarga, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(lblCarga, new Rectangle(0.5, 0.7, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(activityIndicator, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(activityIndicator, new Rectangle(0.5, 0.8, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);
            sub.Children.Add(secondImage);
          //  sub.Children.Add(progressBar);
            sub.Children.Add(activityIndicator);
            sub.Children.Add(lblCarga);

             this.BackgroundColor = Color.FromHex("#110791");
          //  this.BackgroundColor = Color.White;
            this.Content = sub;
		}
        
        /*Nota...
         *
         *Agregar un icono de carga o un activityindicator para que el usuario vea que eta interactuando con la aplicacion 
         * y que se esta cargando el login y la configuraciones del tema
         * **/

            /*
             * Crear un metodo junto con un label indicando que esta cargando tanto el inicio de sesion
             * commo la configuracion esto para darle versatibilidad y elegancion en la presentacion
             * 
             */

        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                lblCarga.IsVisible = true;
                activityIndicator.IsRunning = true;
                await splashImage.ScaleTo(1, 2000);
                await splashImage.ScaleTo(0.8, 1200, Easing.Linear);
                lblCarga.IsVisible = false;
                activityIndicator.IsRunning = false;
                VerificarLogin();
                //VerificarConfiguracionTema();
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            catch (Exception ex)
            {
                
            }
        }

        public void SesionPrueba()
        {
            App.Current.Properties["usuario"] = "";
            App.Current.Properties["contrasena"] = "D@niel1998";
            App.Current.Properties["idAlumno"] = "53408";
            App.Current.Properties["tipo_usuario"] = 2;
            App.Current.Properties["nombreUsuario"] = "Daniel Chavez";
            return;
        }

        public void VaciarSesion()
        {
            App.Current.Properties["usuario"] = "";
            App.Current.Properties["contrasena"] = "";
            App.Current.Properties["idAlumno"] = 0;
            App.Current.Properties["tipo_usuario"] = 0;
            App.Current.Properties["nombreUsuario"] = "Nombre Usuario";
            return;
        }

        public void VerificarLogin()
        {
            try
            {
                if (string.IsNullOrEmpty(App.Current.Properties["tipo_usuario"].ToString()))
                {
                    VaciarSesion();
                    return;
                }
               else if (string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["usuario"].ToString()))
                {
                    VaciarSesion();
                    return;
                }
                else if (string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["contrasena"].ToString()))
                {
                    VaciarSesion();
                    return;
                }
            }
            catch (Exception ex)
            {
                VaciarSesion();
                VerificarLogin();
                return;
            }
        }

        public void VerificarConfiguracionTema()
        {

        }

    }
}