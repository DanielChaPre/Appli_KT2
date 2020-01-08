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
            VerificarLogin();
            btnBuscar.Clicked += BuscarAtlas;
            icShare.Clicked += OnClickShare;
        }

        private void VerificarLogin()
        {
            if (string.IsNullOrEmpty(Xamarin.Forms.Application.Current.Properties["usuario"].ToString()))
            {
                ToolbarItem icLogin = new ToolbarItem
                {
                    Text = "Iniciar sesión",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 1
                };
                this.ToolbarItems.Add(icLogin);
                icLogin.Clicked += OnClickLogin;
            }
            else
            {
                ToolbarItem icClose = new ToolbarItem
                {
                    Text = "Cerrar sesión",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 1
                };
                this.ToolbarItems.Add(icClose);
                icClose.Clicked += OnClickCerrar;
            }
        }

        private void OnClickCerrar(object sender, EventArgs e)
        {
            Xamarin.Forms.Application.Current.Properties["usuario"] = "";
            Application.Current.MainPage = new NavigationPage(new MainPage());
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

        private async void BuscarAtlas(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ResultadoAtlasPage());
        }
    }
}