using Appli_KT2.Model;
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
            LlenarMenu();
            await Task.Yield();
        }

        public async void LlenarMenu()
        {
            notificacionesViewModel = new NotificacionesViewModel();
            listViewEjemplo1.ItemsSource = null;
            listViewEjemplo1.ItemsSource = notificacionesViewModel.Lst_Notificaciones;
            listViewEjemplo1.ItemSelected += OnClickOpcionSeleccionada;
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