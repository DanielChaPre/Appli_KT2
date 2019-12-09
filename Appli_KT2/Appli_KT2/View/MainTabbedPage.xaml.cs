﻿namespace Appli_KT2.View
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
            InitializeComponent ();
          // // new Xamarin.Forms.TabbedPage().On<Android>().SetIsSwipePagingEnabled(false);
          // // new Xamarin.Forms.TabbedPage().On<Android>().IsSwipePagingEnabled();
          // // new Xamarin.Forms.TabbedPage().On<Android>().EnableSwipePaging();
          //  new Xamarin.Forms.TabbedPage().On<Android>().DisableSwipePaging();

          //  On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
          ////  On<Xamarin.Forms.PlatformConfiguration.Android>().SetIsSwipePagingEnabled(false);
            icNotificacion.Clicked += onClickIc;
            icShare.Clicked += OnClickShare;
           // icLogin.Clicked += OnCLickLogin;
           

          //  Console.WriteLine("####################" + Xamarin.Forms.Application.Current.Properties["prueba"].ToString());
          //pruebaShared();
        }

        private async void OnCLickLogin(object sender, EventArgs e)
        {
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        /*Este método es un ejemplo de como utilizar shared preference y utilizarla en la aplicación*/
     /*   public void pruebaShared()
        {
            Xamarin.Forms.Application.Current.Properties["prueba"] = "Prueba de uso de shared preference";
            if (Xamarin.Forms.Application.Current.Properties.ContainsKey("prueba"))
            {
                Console.WriteLine("P: " + Xamarin.Forms.Application.Current.Properties["prueba"].ToString());
            }

          
        }*/

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
            await Xamarin.Forms.Application.Current.MainPage.Navigation.PushAsync(new NotificacionesPage());
        }
    }
}