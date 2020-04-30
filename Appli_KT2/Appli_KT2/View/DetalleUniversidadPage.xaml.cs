using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
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
       // public List<ImagenPlantel> MyDataSource { get; set; }
          public List<CarouselModel> MyDataSource { get; set; }
        public List<CarrerasES> ListaCarreras { get; set; }
        private int _position;
        public int Position { get { return _position; } set { _position = value; OnPropertyChanged(); } }
        private PlantelesES EntDetallePlantel;
        DetalleUniversidadViewModel detalleUniversidadViewModel;
        private bool iscreatecarrusel = false;
        private bool iscreatecarreras = false;
        private MapBehavior mapBehavior;
        MapsViewModel mapsViewModel;
        public List<PlantelesES> lstPlanteles = new List<PlantelesES>();
        public SQLiteConnection conn;
        private static bool banderaClick = true;
        public int repeticiones = 1;
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
                    //MyDataSource = new List<ImagenPlantel>()
                    //{new ImagenPlantel()
                    //    {
                    //        ImagenDecodificada = "no_existe_imagen.png" }
                    //};
                    MyDataSource = new List<CarouselModel>()
                    {new CarouselModel()
                        {
                            imagen = "no_existe_imagen.png" }
                    };

                }
                BindingContext = this;
                EntDetallePlantel = new PlantelesES();
                EntDetallePlantel = plantelesES;
                txtNombreEscuela.Text = EntDetallePlantel.NombrePlantelES;
               
            }
            catch (Exception ex)
            {
            }
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
           // banderaClick = true;
            Application.Current.MainPage.DisplayAlert("Alerta", "Si llegara a faltar información, no es problema de la aplicación, si no de la base de datos de SUREDSU", "Aceptar");
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
          //  CrearCarruselImagen();
            LlenarCarrerasPlantel();
            LlenarDireccion();
            App.Current.Properties["idPlnatelES"] = EntDetallePlantel.idPlantelES;
           // mapsViewModel = new MapsViewModel(EntDetallePlantel);
           // listViewCarreras.ItemSelected += OnClickOpcionSeleccionada;

        }

        private void CrearCarruselImagen(int cvedetalle)
        {
            try
            {
                if (cvedetalle == 1231 || cvedetalle == 1232)
                {
                    MyDataSource = new List<CarouselModel>()
                    {
                            new CarouselModel()
                        {
                            imagen = "utl_.jpg" },
                            new CarouselModel()
                        {
                            imagen = "utl2.png" },
                            new CarouselModel()
                        {
                            imagen = "utl3.png" },
                            new CarouselModel()
                        {
                            imagen = "utl4.png" }

                    };
                    BindingContext = this;
                    iscreatecarrusel = true;
                }
                else
                {
                    MyDataSource = new List<CarouselModel>()
                    {new CarouselModel()
                        {
                            imagen = "no_existe_imagen.png" } };
                }
                //conn = DependencyService.Get<ISQLitePlatform>().GetConnection();

                ////var listaImagenes = conn.Query<ImagenPlantel>("SELECT * FROM ImagenPlantel where Cve_detalle_plantel = ?", ""+cvedetalle+"");
                //var listaImagenes = conn.Query<ImagenPlantel>("SELECT * FROM ImagenPlantel");
                //for (int i = 0; i < listaImagenes.Count; i++)
                //{
                //    listaImagenes[i].ImagenDecodificada = GetImage(listaImagenes[i].Imagenbase64);
                //}
                //if (listaImagenes.Count == 0)
                //{
                //    MyDataSource = new List<ImagenPlantel>()
                //    {new ImagenPlantel()
                //        {
                //            ImagenDecodificada = "no_existe_imagen.png" } };
                //}
                //else
                //{
                //    MyDataSource = listaImagenes;
                //}
                
              
            }
            catch (Exception ex)
            {
                MyDataSource = new List<CarouselModel>()
                    {new CarouselModel()
                        {
                            imagen = "no_existe_imagen.png" } };
            }
           
        }

        public Xamarin.Forms.ImageSource GetImage(string strEncoded)
        {

            if (strEncoded.Equals("-"))
            {
                return null;
            }
            byte[] arrBytImage = Convert.FromBase64String(strEncoded);
            Xamarin.Forms.ImageSource objImage = null;
            objImage = ImageSource.FromStream(() => new MemoryStream(arrBytImage));
            return objImage;
        }

        private async Task<bool> LlenarDireccion()
        {
            lblDireccionPlantel.Text = EntDetallePlantel.DetallePlantel.Domicilio;
            LlenarMapa();
            return true;
        }

        private void LlenarMapa()
        {
            Pin pin = new Pin
            {
                Label = EntDetallePlantel.NombreInstitucionES,
                Address = EntDetallePlantel.DetallePlantel.Domicilio,
                Type = PinType.Place,
                Position = new Position(Convert.ToDouble(EntDetallePlantel.DetallePlantel.Latitud), Convert.ToDouble(EntDetallePlantel.DetallePlantel.Longitud))
            };

            pin.InfoWindowClicked += async (s, args) =>
            {
                string pinName = ((Pin)s).Label;
                await DisplayAlert("Info Window Clicked", $"The info window was clicked for {pinName}.", "Ok");
            };
            MapView.Pins.Add(pin);
            MapView.MoveToRegion(
                MapSpan.FromCenterAndRadius(
                    new Position(Convert.ToDouble(EntDetallePlantel.DetallePlantel.Latitud), Convert.ToDouble(EntDetallePlantel.DetallePlantel.Longitud)), Distance.FromMiles(1)));
        }

        private void LlenarCarrerasPlantel()
        {
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            var carrera = App.Current.Properties["Carrera"].ToString();
            var listaCarreras = new List<CarrerasES>();
            if (string.IsNullOrEmpty(carrera))
            {
                listaCarreras = conn.Query<CarrerasES>("SELECT * FROM CarrerasES Where IdPlantelesES = ?", "" + EntDetallePlantel.idPlantelES + "");
            }
            else {
               // listaCarreras = conn.Query<CarrerasES>("SELECT * FROM CarrerasES Where IdPlantelesES = ?, NombreCarreraES = ? ", "" + EntDetallePlantel.idPlantelES + ", "+carrera+"");
                listaCarreras = conn.Query<CarrerasES>("SELECT * FROM CarrerasES Where NombreCarreraES = ? ", ""+carrera+"");
            }
            //  conn.CreateTable<ImagenPlantel>();
            var LstEscuela2 = from a in listaCarreras where a.IdPlantelesES == EntDetallePlantel.idPlantelES select a;
            listViewCarreras.ItemsSource = LstEscuela2.Cast<CarrerasES>().ToList();
            listViewCarreras.ItemSelected += obtenerdetallecarrera;
        }

        private async void obtenerdetallecarrera(object sender, SelectedItemChangedEventArgs e)
        {
            if (repeticiones == 0)
            {
                repeticiones = 1;
                return;
            }
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            listViewCarreras.SelectedItem = null;
            if (banderaClick)
            {
                var item = e.SelectedItem as CarrerasES;
                if ((item != null))
                {
                    banderaClick = false;
                    //  await Navigation.PushAsync(new DetalleUniversidadPage(item));
                    //DetalleCarreraPlantel detalle = new DetalleCarreraPlantel();
                    var detalle = conn.Query<DetalleCarreraPlantel>("SELECT * FROM DetalleCarreraPlantel Where IdCarreraES = ? ", "" + item.idCarreraES + "");
                    if (detalle.Count == 0)
                    {
                        await DisplayAlert("Información", "No se cuenta con información más detallada", "Aceptar");
                    }
                    else
                    {
                        repeticiones = 0;
                        await Navigation.PushAsync(new DetalleCarreraPage(detalle[0], EntDetallePlantel.NombreInstitucionES, item.NombreCarreraES));

                    }
                    // await DisplayAlert("Información", detalle[0].Costos, "Aceptar");
                    await Task.Run(async () =>
                    {
                        await Task.Delay(500);
                        banderaClick = true;
                    });
                }
            }
        }
        //private async void OnClickOpcionSeleccionada(object sender, SelectedItemChangedEventArgs e)
        //{
        //    try
        //    {
        //        listViewCarreras.SelectedItem = null;
        //        if (banderaClick)
        //        {
        //            var item = e.SelectedItem as CarrerasES;
        //            if ((item != null))
        //            {
        //                banderaClick = false;
        //                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
        //                var listaCarreras = conn.Query<DetalleCarreraPlantel>("SELECT * FROM DetalleCarreraPlantel Where IdCarreraES = ?", "" + item.idCarreraES + "");

                      
        //                await Task.Run(async () =>
        //                {
        //                    await Task.Delay(500);
        //                    banderaClick = true;
        //                });
        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
           
        //}
    }

    public class CarouselModel
    {
        public ImageSource imagen { get; set; }
    }
}