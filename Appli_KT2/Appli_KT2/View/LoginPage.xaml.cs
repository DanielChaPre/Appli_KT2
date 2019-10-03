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
	public partial class LoginPage : ContentPage
	{
		public LoginPage ()
		{
			InitializeComponent ();
            // btnRegistrar.Clicked += onclick;
            // btnLogin.Clicked += OnClickLogin;
            btnCrearCuenta.Clicked += onclick;
            btnLogin.Clicked += LogIn;
		}

        private async void LogIn(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new IniciarUsuarioPage());
        }

        private async void OnClickLogin(object sender, EventArgs e)
        {
            //if (txtUsuario.Text != "admin" && txtPassword.Text != "admin")
            //{
            //    await Application.Current.MainPage.DisplayAlert("Error", "usuario o password incorrecto", "Accept");
            //    return;
            //}

            await Application.Current.MainPage.DisplayAlert("Ok", "Usuario encontrado Bienvenido", "Accept");
            //await Navigation.PushAsync(new MainPage());

        }

        private void onclick(object sender, EventArgs e)
        {
            // Navigation.PushAsync(new RegisterPage());
            //throw new NotImplementedException();
            Navigation.PushAsync(new CreateAccountPage());
        }
    }
}