using Appli_KT2.Model;
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
		public DetalleNotificacion (Notificaciones notificaciones)
		{
			InitializeComponent ();
            this.entnotificaciones = new Notificaciones();
            this.entnotificaciones = notificaciones;
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }


    }
}