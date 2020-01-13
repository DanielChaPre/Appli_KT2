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
	public partial class ResultadoAtlasPage : ContentPage
	{
        private static bool banderaClick;
        public ResultadoAtlasPage ()
		{
			InitializeComponent ();
            banderaClick = true;
        }
        protected override async void OnAppearing()
        {
            LlenarMenu();
            await Task.Yield();
        }

        public async void LlenarMenu()
        {
            EscuelasClass oEjemploListView1Model = new EscuelasClass();
            listViewEjemplo1.ItemsSource = null;
            listViewEjemplo1.ItemsSource = oEjemploListView1Model.ObtenerMenuEjemplo1();
            listViewEjemplo1.ItemSelected += OnClickOpcionSeleccionada;
        }

        private async void OnClickOpcionSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {
            listViewEjemplo1.SelectedItem = null;
            if (banderaClick)
            {
                var item = e.SelectedItem as MenuEjemplo1;
                if ((item != null) && (item.Habilitado))
                {
                    var oSeleccionado = item.idOpcion;
                    banderaClick = false;
                    switch (oSeleccionado)
                    {
                        case 1:
                            await Navigation.PushAsync(new DetalleUniversidadPage());
                            break;
                        case 2:

                            break;
                        case 3:

                            break;
                    }
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