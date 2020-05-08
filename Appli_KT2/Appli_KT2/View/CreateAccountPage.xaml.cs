using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateAccountPage : ContentPage
	{
        public CreateAccountPage()
        {
            try
            {
                InitializeComponent();
                // lblUsuario.TranslateTo(0, 5, 0);
                //txtUsuario.Focused += animacionUsu;
                //txtContraseña.Focused += animacionPass;
            }
            catch (Exception ex)
            {
            }

        }

        private void animacionPass(object sender, FocusEventArgs e)
        {
            try
            {
                if (txtContraseña.IsFocused)
                {
                    lblContraseña.TranslateTo(0, 3, 100);
                    lblUsuario.TranslateTo(0, 40, 100);
                }
            }
            catch (Exception ex)
            {
               // App.Current.MainPage.DisplayAlert("Información", "Ha ocurrido un error,  por favor pongase en contacto con el desarrollador.", "Aceptar");
            }

        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                // txtUsuario.Focus();
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Información", "Ha ocurrido un error,  por favor pongase en contacto con el desarrollador.", "Aceptar");
            }

        }
        public void animacionUsu(object sender, FocusEventArgs e)
        {
            try
            {
                if (txtUsuario.IsFocused)
                {
                    lblUsuario.TranslateTo(0, 3, 100);
                    lblContraseña.TranslateTo(0, 40, 100);
                }
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("Información", "Ha ocurrido un error,  por favor pongase en contacto con el desarrollador.", "Aceptar");
            }

            
        }
    }
}