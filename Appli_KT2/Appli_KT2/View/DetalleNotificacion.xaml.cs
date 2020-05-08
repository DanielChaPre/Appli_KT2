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
            var labelClick = new TapGestureRecognizer();

            labelClick.Tapped += (s, e) =>
            {
                if (lblEnlace.Text.Contains("https") || lblEnlace.Text.Contains("http"))
                {
                    Device.OpenUri(new System.Uri(lblEnlace.Text));
                }
                else
                {
                    Device.OpenUri(new System.Uri("https/"+lblEnlace.Text));
                }
               
            };
             lblEnlace.GestureRecognizers.Add(labelClick);
            notificacionesViewModel = new NotificacionesViewModel();
            if (entnotificaciones.Estatus.Equals("No leida"))
            {
                notificacionesViewModel.CambiarEstatusNotificacion(entnotificaciones.Cve_notificacion.ToString());
            }
           
        }




        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                LlenarDatos();
                itemEliminar.Clicked += EliminarNotificacion;
            }
            catch (Exception ex)
            {
            }

        }

        private async void EliminarNotificacion(object sender, EventArgs e)
        {
            try
            {
                
                App.Current.Properties["cveNotificacion"] = entnotificaciones.Cve_notificacion;
                if (await notificacionesViewModel.EliminarNotificaciones())
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
            catch (Exception ex)
            {
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
                lblEnlace.Text = entnotificaciones.Url;
            }
            catch (Exception ex)
            {
            }
        }
    }
}