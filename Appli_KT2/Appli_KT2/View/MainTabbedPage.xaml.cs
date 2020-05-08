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
    using Xamarin.Forms.PlatformConfiguration;

    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainTabbedPage : Xamarin.Forms.TabbedPage
    {

        private MainTabbedViewModel mainTabbed;

		public MainTabbedPage()
		{
            try
            {
                InitializeComponent();
                icNotificacion.Clicked += onClickIc;
                icShare.Clicked += OnClickShare;
            }
            catch (Exception ex)
            {
            }
        }

        private async void OnCLickLogin(object sender, EventArgs e)
        {
            try
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
            }
           
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

        private async void onClickIc(object sender, EventArgs e)
        {
            try
            {
                await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NotificacionesPage());
            }
            catch (Exception ex)
            {
            }
           
        }
    }
}