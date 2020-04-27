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
        private static bool banderaClick = true;
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
               
            }
            catch (Exception ex)
            {
            }
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Application.Current.MainPage.DisplayAlert("Alerta", "Si llegara a faltar información, no es problema de la aplicación, si no de la base de datos de SUREDSU", "Aceptar");
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
          //  CrearCarruselImagen();
            LlenarCarrerasPlantel();
            LlenarDireccion();
            listViewCarreras.ItemSelected += OnClickOpcionSeleccionada;
            banderaClick = true;
        }

        private void CrearCarruselImagen(int cvedetalle)
        {
            try
            {
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();

                var listaImagenes = conn.Query<ImagenPlantel>("SELECT * FROM ImagenPlantel ");
                for (int i = 0; i < listaImagenes.Count; i++)
                {
                    listaImagenes[i].ImagenDecodificada = GetImage(listaImagenes[i].Ruta);
                }
                if (listaImagenes.Count == 0)
                {
                    MyDataSource = new List<ImagenPlantel>()
                    {new ImagenPlantel()
                        {
                            ImagenDecodificada = "no_existe_imagen.png" } };
                }
                else
                {
                    MyDataSource = listaImagenes;
                }
                
                BindingContext = this;
                iscreatecarrusel = true;
            }
            catch (Exception ex)
            {
                MyDataSource = new List<ImagenPlantel>()
                    {new ImagenPlantel()
                        {
                            ImagenDecodificada = "no_existe_imagen.png" } };
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
            lblDireccionPlantel.Text = EntDetallePlantel.DetallePlantel.Ubicacion;
            LlenarMapa();
            return true;
        }

        private void LlenarMapa()
        {
            lstPlanteles.Add(EntDetallePlantel);
            BindingContext = this;
        }

        private void LlenarCarrerasPlantel()
        {
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            //  conn.CreateTable<ImagenPlantel>();

            var listaCarreras = conn.Query<CarrerasES>("SELECT * FROM CarrerasES Where IdPlantelesES = ?", "" + EntDetallePlantel.idPlantelES + "");
            listViewCarreras.ItemsSource = listaCarreras;
            listViewCarreras.ItemSelected += obtenerdetallecarrera;
        }

        private async void obtenerdetallecarrera(object sender, SelectedItemChangedEventArgs e)
        {
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
                    var detalle = conn.Query<DetalleCarreraPlantel>("SELECT * FROM DetalleCarreraPlantel Where IdCarreraES = ?", "" + item.idCarreraES + "");
                    if (detalle.Count == 0)
                    {
                        await DisplayAlert("Información", "No se cuenta con información más detallada", "Aceptar");
                    }
                    else
                    {
                        await Navigation.PushAsync(new DetalleCarreraPage(detalle[1], EntDetallePlantel.NombreInstitucionES, item.NombreCarreraES));

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
        private async void OnClickOpcionSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                listViewCarreras.SelectedItem = null;
                if (banderaClick)
                {
                    var item = e.SelectedItem as CarrerasES;
                    if ((item != null))
                    {
                        banderaClick = false;
                        conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                        var listaCarreras = conn.Query<DetalleCarreraPlantel>("SELECT * FROM DetalleCarreraPlantel Where IdCarreraES = ?", "" + item.idCarreraES + "");

                      
                        await Task.Run(async () =>
                        {
                            await Task.Delay(500);
                            banderaClick = true;
                        });
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
           
        }
    }

    public class CarouselModel
    {
        public ImageSource imagen { get; set; }
    }
}