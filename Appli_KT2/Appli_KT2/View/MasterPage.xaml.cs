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
        private int tipo_usuario;
		public MasterPage ()
		{
			InitializeComponent ();
            this.tipo_usuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());
            AccesoUsuario();
            lblNombreUsuario.Text = App.Current.Properties["nombreUsuario"].ToString();
        }

        public void AccesoUsuario()
        {
            /*
             * 1: Usuario general
             * 2: Estudiante
             * 3: empleado
             * 4: plantel
             * 5: Docente
             * 6: Directivo
             * 7: Padre Familia
             * **/

            switch (this.tipo_usuario)
            {
                case 0:
                    btnIniciar.IsVisible = true;
                    btnPerfil.IsVisible = false;
                    btnHistorial.IsVisible = false;
                    btnNotificaciones.IsVisible = false;
                    btnSuredsu.IsVisible = true;
                    break;
                case 1:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = false;
                    btnNotificaciones.IsVisible = false;
                    btnSuredsu.IsVisible = true;
                    break;
                case 2:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 3:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 4:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 5:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 6:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 7:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                default:
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
            }
        }
        
    }
}