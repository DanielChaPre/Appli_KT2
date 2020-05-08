using Appli_KT2.View;
using Appli_KT2.ViewModel;
using Plugin.LocalNotifications;
using System;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;
using Badge.Plugin;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Appli_KT2
{
    public partial class App : Xamarin.Forms.Application
    {
        NotificacionesViewModel notificacionesViewModel = new NotificacionesViewModel();
        public static MasterDetailPage MasterD { get; set; }

        public App()
        {
            InitializeComponent();
            // pruebaShared();
          //  RecibirNotificaciones();


        }
        protected override void OnStart()
        {
          this.MainPage = new NavigationPage(new SplashPage()); 
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        public async void RecibirNotificaciones()
        {
            CrossBadge.Current.SetBadge(5, "Notificaciones");
            //Device.StartTimer(TimeSpan.FromSeconds(10), () =>
            //{
            //    while (notificacionesViewModel.Lst_Notificaciones != null || notificacionesViewModel.Lst_Notificaciones.Count != 0)
            //    {

            //        if (notificacionesViewModel.Lst_Notificaciones.Count == 0)
            //        {
            //            //await Application.Current.MainPage.DisplayAlert("Aviso", "No se encuentran notificaciones existentes para el usuario", "Aceptar")
            //            return true;
            //        }

            //        //for (int i = 0; i < notificacionesViewModel.Lst_Notificaciones.Count; i++)
            //        //{
            //        //    if (notificacionesViewModel.Lst_Notificaciones[i].Estatus == 0)
            //        //    {
            //        //        CrossLocalNotifications.Current.Show(notificacionesViewModel.Lst_Notificaciones[i].Titulo, notificacionesViewModel.Lst_Notificaciones[i].Texto);
            //        //    }
            //        //}
            //        return true;
            //    }
            //    return true; // True = Repeat again, False = Stop the timer
            //});
        }
    }
}
