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
            try
            {
                InitializeComponent();
                banderaClick = true;
            }
            catch (Exception ex)
            {
            }
           
        }

        protected async override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                LlenarHistorial();
                await Task.Yield();
            }
            catch (Exception ex)
            {
            }
          
        }

        public void LlenarHistorial()
        {
            try
            {
                historialViewModel = new HistorialViewModel();
                listViewHistorial.BindingContext = historialViewModel;
                listViewHistorial.IsVisible = false;
                actiCargar.IsRunning = true;
                actiCargar.IsVisible = true;
                lblhistorial.IsVisible = false;
                Device.StartTimer(TimeSpan.FromSeconds(8), () =>
                {
                    while (historialViewModel.LstHistorial != null || historialViewModel.LstHistorial.Count != 0)
                    {
                        if (historialViewModel.LstHistorial.Count == 0)
                        {
                            lblhistorial.IsVisible = true;
                            actiCargar.IsRunning = false;
                            actiCargar.IsVisible = false;
                            return false;
                        }
                        actiCargar.IsRunning = false;
                        actiCargar.IsVisible = false;
                        lblhistorial.IsVisible = false;
                        listViewHistorial.IsVisible = true;
                        listViewHistorial.ItemsSource = historialViewModel.LstHistorial;
                        return false;
                    }
                    return true;
                });

            }
            catch (Exception ex)
            {
            }
            
        }

        private void BtnAbrir_Clicked(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);
                Device.OpenUri(new System.Uri("https://applikt.utleon.edu.mx/" + mi.CommandParameter.ToString()));
            }
            catch (Exception ex)
            {
            }
           
        }

        private async void BtnCompartir_Clicked(object sender, EventArgs e)
        {
            try
            {
                var mi = ((MenuItem)sender);
                ShareDialogClass share = new ShareDialogClass();
                await share.ShareUri("https://applikt.utleon.edu.mx/" + mi.CommandParameter.ToString(), "Compartir link del documento");
            }
            catch (Exception ex)
            {
            }
            
        }

    }
}