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
	public partial class ResultadoAtlasPage : ContentPage
	{
        private static bool banderaClick;
        ResultadoAtlasViewModel resultadoAtlasViewModel;
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
            //EscuelasClass oEjemploListView1Model = new EscuelasClass();
            resultadoAtlasViewModel = new ResultadoAtlasViewModel();
            listViewResultAtlas.ItemsSource = null;
            listViewResultAtlas.BindingContext = resultadoAtlasViewModel;
            resultadoAtlasViewModel.ConsultarPlanteles();
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    while (resultadoAtlasViewModel.ListPlantelES != null)
                    {
                        resultadoAtlasViewModel.ConsultarPlanteles();
                        if (resultadoAtlasViewModel.ListPlantelES.Count == 0)
                        {
                            actiCargarResultado.IsVisible = false;
                            actiCargarResultado.IsRunning = false;
                            return false;
                        }
                        actiCargarResultado.IsVisible = false;
                        actiCargarResultado.IsRunning = false;
                        listViewResultAtlas.IsVisible = true;
                        listViewResultAtlas.ItemsSource = resultadoAtlasViewModel.ListPlantelES;
                        listViewResultAtlas.ItemSelected += OnClickOpcionSeleccionada;
                        return false;
                    }
                    return true;
                }
                catch (Exception)
                {
                    return true;
                    throw;
                }
            });
        }

        private async void OnClickOpcionSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {
            listViewResultAtlas.SelectedItem = null;
            if (banderaClick)
            {
                var item = e.SelectedItem as PlantelesES;
                if ((item != null))
                {
                    banderaClick = false;
                    await Navigation.PushAsync(new DetalleUniversidadPage(item));
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