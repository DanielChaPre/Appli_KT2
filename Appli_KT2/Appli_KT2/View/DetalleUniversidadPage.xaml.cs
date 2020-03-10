using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using SQLite;
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
        private PlantelesES EntDetallePlantel;
        DetalleUniversidadViewModel detalleUniversidadViewModel;
        private bool iscreatecarrusel = false;
        private bool iscreatecarreras = false;
        private MapBehavior mapBehavior;
        public List<PlantelesES> lstPlanteles = new List<PlantelesES>();
        public SQLiteConnection conn;
        public DetalleUniversidadPage (PlantelesES plantelesES)
       // public DetalleUniversidadPage ()
		{
            InitializeComponent();
            try
            {
                if (plantelesES.DetallePlantel != null)
                {
                    CrearCarruselImagen(plantelesES.DetallePlantel.Cve_detalle_plantel);
                }
                else
                {
                    MyDataSource = new List<ImagenPlantel>()
                    {new ImagenPlantel()
                        {
                            ImagenDecodificada = "no_existe_imagen.png" } };
                    
                }
                BindingContext = this;
                EntDetallePlantel = new PlantelesES();
                EntDetallePlantel = plantelesES;
                txtNombreEscuela.Text = EntDetallePlantel.NombrePlantelES;
                lstPlanteles.Add(EntDetallePlantel);
                mapBehavior.ItemsSource = lstPlanteles;
            }
            catch (Exception ex)
            {
            }
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
          //  CrearCarruselImagen();
            LlenarCarrerasPlantel();
            LlenarDireccion();
        }

        private void CrearCarruselImagen(int cvedetalle)
        {
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            conn.CreateTable<ImagenPlantel>();
            
            MyDataSource = conn.Query<ImagenPlantel>("SELECT * FROM ImagenPlantel Where Cve_detalle_plantel = ", ""+ cvedetalle + "");
            BindingContext = this;
            iscreatecarrusel = true;
            //Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            //{
            //    try
            //    {
            //        if (detalleUniversidadViewModel == null)
            //        {
            //            detalleUniversidadViewModel = new DetalleUniversidadViewModel();
            //            //detalleUniversidadViewModel.ConsultarImagenesPlanteles(EntDetallePlantel.PlantelesES.idPlantelES);
            //        }
            //        while (detalleUniversidadViewModel.ListImagenes != null)
            //        {
            //            MyDataSource = new List<ImagenPlantel>();
            //            MyDataSource = detalleUniversidadViewModel.ListImagenes;
            //            BindingContext = this;
            //            iscreatecarrusel = true;
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

        private async Task<bool> LlenarDireccion()
        {
            lblDireccionPlantel.Text = EntDetallePlantel.DetallePlantel.Ubicacion;
            LlenarMapa();
            return true;
        }

        private void LlenarMapa()
        {
            var latitud = Convert.ToDouble(EntDetallePlantel.DetallePlantel.Latitud);
            var longitud = Convert.ToDouble(EntDetallePlantel.DetallePlantel.Longitud);
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
                        //detalleUniversidadViewModel.ObtenerCarreraES(EntDetallePlantel.PlantelesES.idPlantelES);
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