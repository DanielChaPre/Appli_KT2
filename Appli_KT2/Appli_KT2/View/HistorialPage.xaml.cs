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
        private HistorialViewModel historialViewModel;
		public HistorialPage ()
		{
			InitializeComponent ();
		}

        public void LlenarHistorial()
        {
            historialViewModel = new HistorialViewModel();
            listViewHistorial.BindingContext = historialViewModel;
            listViewHistorial.ItemsSource = historialViewModel.lst
        }
	}
}