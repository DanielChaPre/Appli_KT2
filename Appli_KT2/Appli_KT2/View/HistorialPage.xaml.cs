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
	public partial class HistorialPage : ContentPage
	{
        private static bool banderaClick;
        private HistorialViewModel historialViewModel;
		public HistorialPage ()
		{
			InitializeComponent ();
            banderaClick = true;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            LlenarHistorial();
            await Task.Yield();
        }

        public void LlenarHistorial()
        {
            historialViewModel = new HistorialViewModel();
            listViewHistorial.BindingContext = historialViewModel;
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
             {
                 while (historialViewModel.LstHistorial != null || historialViewModel.LstHistorial.Count != 0)
                 {
                     if (historialViewModel.LstHistorial.Count == 0)
                     {
                         lblnoti.IsVisible = true;
                         actiCargar.IsRunning = false;
                         actiCargar.IsVisible = false;
                         return false;
                     }
                     actiCargar.IsRunning = false;
                     actiCargar.IsVisible = false;
                     lblnoti.IsVisible = false;
                     listViewHistorial.IsVisible = true;
                     listViewHistorial.ItemsSource = historialViewModel.LstHistorial;
                     return false;
                 }
                 return true;
             });
            
        }

        private void BtnAbrir_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            Device.OpenUri(new System.Uri(mi.CommandParameter.ToString()));
        }

        private async void BtnCompartir_Clicked(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            ShareDialogClass share = new ShareDialogClass();
            await share.ShareUri(mi.CommandParameter.ToString(), "Compartir link del documento");
        }

    }
}