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
            btnCrearCuenta.Clicked += onclick;
            btnLogin.Clicked += LogIn;
		}

        private async void LogIn(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new IniciarUsuarioPage());
        }

        private async void onclick(object sender, EventArgs e)
        {
           await Application.Current.MainPage.Navigation.PushAsync(new CreateAccountPage());
        }
    }
}