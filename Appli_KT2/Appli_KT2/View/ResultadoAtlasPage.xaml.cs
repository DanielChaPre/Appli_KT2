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
        List<DetallePlantel> ListaPlanteles = new List<DetallePlantel>();
        PlantelESViewModel plantelESViewModel = new PlantelESViewModel();
        CarreraViewModel carreraViewModel = new CarreraViewModel();
        DetalleUniversidadViewModel detalleUniversidadViewModel = new DetalleUniversidadViewModel();
        MunicipioViewModel municipioViewModel = new MunicipioViewModel();

        public ResultadoAtlasPage ()
		{
			InitializeComponent ();
            banderaClick = true;
        }

        protected override async void OnAppearing()
        {
            frameSincronizacion.IsVisible = false;
            listViewResultAtlas.IsVisible = false;
            actiCargarResultado.IsVisible = true;
            actiCargarResultado.IsRunning = true;
            icSincronizar.Clicked += Sincronizar;
            LlenarLista();
            await Task.Yield();
        }

        private async void Sincronizar(object sender, EventArgs e)
        {
            frameSincronizacion.IsVisible = true;
            await detalleUniversidadViewModel.SincronizarImagenesPlantel();
            await carreraViewModel.SincronizarCarrera();
            await plantelESViewModel.SincronizarDetallePlantel();
            await plantelESViewModel.SincronizarPlantelesES();
            await municipioViewModel.SincronizarMunicipio();
            frameSincronizacion.IsVisible = false;
            LlenarLista();
        }

        public void LlenarLista()
        {
            resultadoAtlasViewModel = new ResultadoAtlasViewModel();
            listViewResultAtlas.ItemsSource = null;
            listViewResultAtlas.BindingContext = resultadoAtlasViewModel;
            resultadoAtlasViewModel.ConsultarPlantelesDetalleBD();
            if (resultadoAtlasViewModel.lstPlanteles.Count == 0)
            {
                actiCargarResultado.IsVisible = false;
                actiCargarResultado.IsRunning = false;
                Application.Current.MainPage.DisplayAlert("Alerta", "No se encontraron universidades, actualize la información", "Aceptar");
               // Application.Current.MainPage.Navigation.PopAsync();
            }
            actiCargarResultado.IsVisible = false;
            actiCargarResultado.IsRunning = false;
            listViewResultAtlas.IsVisible = true;
            FiltrarResultado(resultadoAtlasViewModel.ListPlantelES);
            Application.Current.MainPage.DisplayAlert("Alerta", "Si llegara a faltar información, no es problema de la aplicación, si no de la base de datos de SUREDSU", "Aceptar");
            listViewResultAtlas.ItemsSource = resultadoAtlasViewModel.lstPlanteles;
            listViewResultAtlas.ItemSelected += OnClickOpcionSeleccionada;

            //resultadoAtlasViewModel = new ResultadoAtlasViewModel();

            //Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            //{
            //    try
            //    {
            //        while (resultadoAtlasViewModel.ListPlantelES != null)
            //        {
            //            resultadoAtlasViewModel.ConsultarPlanteles();
            //            if (resultadoAtlasViewModel.ListPlantelES.Count == 0)
            //            {
            //                actiCargarResultado.IsVisible = false;
            //                actiCargarResultado.IsRunning = false;
            //                return false;
            //            }
            //            actiCargarResultado.IsVisible = false;
            //            actiCargarResultado.IsRunning = false;
            //            listViewResultAtlas.IsVisible = true;
            //            FiltrarResultado(resultadoAtlasViewModel.ListPlantelES);
            //            //listViewResultAtlas.ItemsSource = resultadoAtlasViewModel.ListPlantelES;
            //            listViewResultAtlas.ItemSelected += OnClickOpcionSeleccionada;

            //            return false;
            //        }
            //        return true;
            //    }
            //    catch (Exception)
            //    {
            //        return true;
            //        throw;
            //    }
            //});
        }

        private void FiltrarResultado(List<DetallePlantel> lstDetallePlantel)
        {
            var municipio = Convert.ToInt32(App.Current.Properties["municipios"].ToString());
            var institucion = Convert.ToInt32(App.Current.Properties["institucion"].ToString());
            var carrera = Convert.ToInt32(App.Current.Properties["Carrera"].ToString());
            if (municipio !=  0)
            {
                //var LstEscuelas = from a in lstDetallePlantel where a.PlantelesES.Municipio == municipio select a;
                //ListaPlanteles = LstEscuelas.Cast<DetallePlantel>().ToList();
                //pMunicipio.ItemsSource = municipio.Cast<Municipios>().ToList();
            }

            if (institucion != 0)
            {
                if (ListaPlanteles.Count != 0)
                {
                   // var LstEscuela2 = from a in ListaPlanteles where a.PlantelesES.idPlantelES == institucion select a;
                    //ListaPlanteles = LstEscuela2.Cast<DetallePlantel>().ToList();
                }
                else
                {
                    //var LstEscuela2 = from a in lstDetallePlantel where a.PlantelesES.idPlantelES == institucion select a;
                    //ListaPlanteles = LstEscuela2.Cast<DetallePlantel>().ToList();
                }
            }

            if (carrera != 0)
            {
                if (ListaPlanteles.Count != 0)
                {
                    //var LstEscuela2 = from a in ListaPlanteles where a.PlantelesES.idPlantelES == carrera select a;
                    //ListaPlanteles = LstEscuela2.Cast<DetallePlantel>().ToList();
                }
                else
                {
                    //var LstEscuela2 = from a in lstDetallePlantel where a.PlantelesES.idPlantelES == carrera select a;
                    //ListaPlanteles = LstEscuela2.Cast<DetallePlantel>().ToList();
                }
            }

            if (municipio == 0 && institucion == 0 && carrera == 0)
            {
                ListaPlanteles = lstDetallePlantel;
            }


            listViewResultAtlas.ItemsSource = ListaPlanteles;
        }

        private async void OnClickOpcionSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {
            listViewResultAtlas.SelectedItem = null;
            if (banderaClick)
            {
                var item = e.SelectedItem as DetallePlantel;
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