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
	public partial class DetalleNotificacion : ContentPage
	{
        private Notificaciones entnotificaciones;
        private NotificacionesViewModel notificacionesViewModel;
        public DetalleNotificacion (Notificaciones notificaciones)
		{
			InitializeComponent ();
            this.entnotificaciones = new Notificaciones();
            this.entnotificaciones = notificaciones;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LlenarDatos();
            itemEliminar.Clicked += EliminarNotificacion;
        }

        private async void EliminarNotificacion(object sender, EventArgs e)
        {
            notificacionesViewModel = new NotificacionesViewModel();
            App.Current.Properties["cveNotificacion"] = entnotificaciones.Cve_notificacion;
           if( await notificacionesViewModel.EliminarNotificaciones())
            {
                await Application.Current.MainPage.DisplayAlert("Exito", "Se ha eliminado de " +
                    "manera correcta la notificación", "Aceptar");
                await Navigation.PushAsync(new NotificacionesPage());
                return;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se a podido eliminado de " +
                   "manera correcta la notificación", "Aceptar");
                return;
            }
        }

        public async void LlenarDatos()
        {
            try
            {
                imgIcon.Source = entnotificaciones.Icon;
                lblTitulo.Text = entnotificaciones.Titulo;
                lblFecha.Text = entnotificaciones.Fecha_notificacion;
                lblHora.Text = entnotificaciones.Hora_notificacion;
                lblBody.Text = entnotificaciones.Texto;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }
    }
}