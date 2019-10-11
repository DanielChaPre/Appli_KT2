namespace Appli_KT2.View
{
    using Appli_KT2.ViewModel;
    using System;
    using System.IO;
    using Xamarin.Essentials;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;
    using static Appli_KT2.ViewModel.ShareDialogClass;
    using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainTabbedPage : Xamarin.Forms.TabbedPage
    {

		public MainTabbedPage()
		{
			InitializeComponent ();
         //   On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            icNotificacion.Clicked += onClickIc;
            icShare.Clicked += OnClickShare;
            icLogin.Clicked += OnCLickLogin;
		}

        private async void OnCLickLogin(object sender, EventArgs e)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private async void OnClickShare(object sender, EventArgs e)
        {
            try
            {
                ShareDialogClass share = new ShareDialogClass();
                await share.ShareUri("WWW.HolaMundo.com", "Compartir Aplicación");
            }
            catch (Exception)
            {

              
            }
        }

        private async void onClickIc(object sender, EventArgs e)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NotificacionesPage());
        }
    }
}