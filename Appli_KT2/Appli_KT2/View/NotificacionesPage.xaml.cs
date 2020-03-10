using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Plugin.LocalNotifications;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NotificacionesPage : ContentPage
	{
         private static bool banderaClick;
        private NotificacionesViewModel notificacionesViewModel;

        public NotificacionesPage()
        {
            InitializeComponent();
         //   Title = "Atlas";
            banderaClick = true;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            actiCargar.IsRunning = true;
            actiCargar.IsVisible = true;
            listViewEjemplo1.IsVisible = false;
            lblnoti.IsVisible = false;
            LlenarMenu();
            await Task.Yield();
        }

        public async void LlenarMenu()
        {
            notificacionesViewModel = new NotificacionesViewModel();
            listViewEjemplo1.ItemsSource = null;
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                while (notificacionesViewModel.Lst_Notificaciones != null || notificacionesViewModel.Lst_Notificaciones.Count != 0)
                {

                    if (notificacionesViewModel.Lst_Notificaciones.Count == 0)
                    {
                        //await Application.Current.MainPage.DisplayAlert("Aviso", "No se encuentran notificaciones existentes para el usuario", "Aceptar")
                        lblnoti.IsVisible = true;
                        actiCargar.IsRunning = false;
                        actiCargar.IsVisible = false;
                        return false;
                    }
                   
                    listViewEjemplo1.IsVisible = true;
                    listViewEjemplo1.ItemsSource = notificacionesViewModel.Lst_Notificaciones;
                    listViewEjemplo1.ItemSelected += OnClickOpcionSeleccionada;
                    //for (int i = 0; i < notificacionesViewModel.Lst_Notificaciones.Count; i++)
                    //{
                    //    if (notificacionesViewModel.Lst_Notificaciones[i].Estatus == 0)
                    //    {

                    //        // this..TextColor=Color.Gray;
                    //        //CrossLocalNotifications.Current.Show("", "");
                    //    }
                    //    else
                    //    {
                    //       //this.lblestatus.TextColor= Color.Black;
                    //    }
                    //}
                    actiCargar.IsRunning = false;
                    actiCargar.IsVisible = false;
                    lblnoti.IsVisible = false;
                    return false;
                }
                return true; // True = Repeat again, False = Stop the timer
            });
        }

        private async void OnClickOpcionSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {
            listViewEjemplo1.SelectedItem = null;
            if (banderaClick)
            {
                var item = e.SelectedItem as Notificaciones;
                if ((item != null))
                {
                    banderaClick = false;
                    await Navigation.PushAsync(new DetalleNotificacion(item));
                    await Task.Run(async () =>
                    {
                        await Task.Delay(500);
                        banderaClick = true;
                    });
                }
            }
        }
    }
}