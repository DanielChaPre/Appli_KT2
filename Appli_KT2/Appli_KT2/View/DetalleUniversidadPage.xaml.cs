using Appli_KT2.Model;
using Appli_KT2.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleUniversidadPage : ContentPage
	{
        public List<ImagenPlantel> MyDataSource { get; set; }
      //  public List<CarouselModel> MyDataSource { get; set; }
        public List<CarrerasES> ListaCarreras { get; set; }
        private int _position;
        public int Position { get { return _position; } set { _position = value; OnPropertyChanged(); } }
        private DetallePlantel EntDetallePlantel;
        DetalleUniversidadViewModel detalleUniversidadViewModel;
        private bool iscreatecarrusel = false;
        private bool iscreatecarreras = false;

        public DetalleUniversidadPage (DetallePlantel plantelesES)
       // public DetalleUniversidadPage ()
		{
            InitializeComponent();
            try
            {
                BindingContext = this;
                CrearCarruselImagen();
                EntDetallePlantel = new DetallePlantel();
                EntDetallePlantel = plantelesES;
                var latitud = Convert.ToDouble(EntDetallePlantel.Latitud);
                var longitud = Convert.ToDouble(EntDetallePlantel.Longitud);
                MyMap.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(latitud, longitud), Distance.FromMiles(1)));
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        //protected override void OnAppearing()
        //{
        //    base.OnAppearing();
        //    CrearCarruselImagen();
        //    LlenarCarrerasPlantel();
        //    LlenarDireccion();
        //}

        private void CrearCarruselImagen()
        {

            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                try
                {
                    if (detalleUniversidadViewModel == null)
                    {
                        detalleUniversidadViewModel = new DetalleUniversidadViewModel();
                        detalleUniversidadViewModel.ConsultarImagenesPlanteles(EntDetallePlantel.PlantelesES.idPlantelES);
                    }
                    while (detalleUniversidadViewModel.ListImagenes != null)
                    {
                        MyDataSource = new List<ImagenPlantel>();
                        MyDataSource = detalleUniversidadViewModel.ListImagenes;
                        BindingContext = this;
                        iscreatecarrusel = true;
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

        private async Task<bool> LlenarDireccion()
        {
            lblDireccionPlantel.Text = EntDetallePlantel.Ubicacion;
            LlenarMapa();
            return true;
        }

        private void LlenarMapa()
        {
            var latitud = Convert.ToDouble(EntDetallePlantel.Latitud);
            var longitud = Convert.ToDouble(EntDetallePlantel.Longitud);
            //MyMap.MoveToRegion(
            //MapSpan.FromCenterAndRadius(
            //    new Position(latitud, longitud), Distance.FromMiles(1)));
        }

        private void LlenarCarrerasPlantel()
        {
            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                try
                {
                    if (detalleUniversidadViewModel.ListCarreraES == null)
                    {
                        detalleUniversidadViewModel.ObtenerCarreraES(EntDetallePlantel.PlantelesES.idPlantelES);
                    }
                    while (detalleUniversidadViewModel.ListImagenes != null)
                    {
                        ListaCarreras = new List<CarrerasES>();
                        ListaCarreras = detalleUniversidadViewModel.ListCarreraES;
                        BindingContext = this;
                        iscreatecarreras = true;
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

	}

    public class CarouselModel
    {
        public ImageSource imagen { get; set; }
    }
}