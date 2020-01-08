using Appli_KT2.ViewModel;
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
	public partial class AtlasPage : ContentPage
	{
		public AtlasPage ()
		{
			InitializeComponent ();
            btnBuscar.Clicked += buscarAtlas;
            icShare.Clicked += OnClickShare;
            icLogin.Clicked += OnClickLogin;

        }

        private void OnClickLogin(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Login = new LoginViewModel();
            Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private async void OnClickShare(object sender, EventArgs e)
        {
            try
            {
                ShareDialogClass share = new ShareDialogClass();
                await share.ShareUri("WWW.HolaMundo.com", "Compartir link de descarga de Appli-KT");
            }
            catch (Exception)
            {


            }
        }

        private async void buscarAtlas(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ResultadoAtlasPage());
        }
    }
}