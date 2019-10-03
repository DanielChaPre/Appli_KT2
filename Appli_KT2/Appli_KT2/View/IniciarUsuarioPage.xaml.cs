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
	public partial class IniciarUsuarioPage : ContentPage
	{
		public IniciarUsuarioPage ()
		{
			InitializeComponent ();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            txtUsuario.Focus();
            btnNext.Clicked += Next;
        }

        private async void Next(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new IniciarContraseniaPage());
        }
    }
}