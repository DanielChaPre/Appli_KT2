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
	public partial class MasterPage : ContentPage
	{
		public MasterPage ()
		{
			InitializeComponent ();
            btnPerfil.Clicked += irPerfil;
            btnAtlas.Clicked += irAtlas;
            btnHistorial.Clicked += irHistorial;
            btnNotificaciones.Clicked += irNotificaciones;
            btnSuredsu.Clicked += irSuredsu;
        }

        private async void irSuredsu(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await App.MasterD.Detail.Navigation.PushAsync(new SuredsuPage());
        }

        private async void irNotificaciones(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await App.MasterD.Detail.Navigation.PushAsync(new NotificacionesPage());
        }

        private async void irHistorial(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await App.MasterD.Detail.Navigation.PushAsync(new HistorialPage());
        }

        private async void irAtlas(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await App.MasterD.Detail.Navigation.PushAsync(new AtlasPage());
        }

        private async void irPerfil(object sender, EventArgs e)
        {
            App.MasterD.IsPresented = false;
            await App.MasterD.Detail.Navigation.PushAsync(new RegisterPage());
        }
    }
}