using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Rg.Plugins.Popup.Services;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.PlatformConfiguration.TizenSpecific;
using Xamarin.Forms.Xaml;
using static Appli_KT2.View.AtlasPage;

namespace Appli_KT2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleUniversidadPage : ContentPage
    {
         public List<ImagenPlantel> MyDataSource { get; set; }
       // public List<CarouselModel> MyDataSource { get; set; }
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
        public DetalleUniversidadPage(PlantelesES plantelesES)
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
                            ImagenDecodificada = "no_existe_imagen.png" }
                    };
                    //MyDataSource = new List<CarouselModel>()
                    //{new CarouselModel()
                    //    {
                    //        imagen = "no_existe_imagen.png" }
                    //};

                }
                BindingContext = this;
                EntDetallePlantel = new PlantelesES();
                EntDetallePlantel = plantelesES;
                txtNombreEscuela.Text = EntDetallePlantel.NombrePlantelES;
                this.btnEnlace.Text = EntDetallePlantel.DetallePlantel.Url_vinculacion;
                //  this.btndireccion.Source
                AccionarDireccion();
               // AccionarReseña();
                AccionarCarreras();
            }
            catch (Exception ex)
            {
            }

        }

        public void AccionarDireccion()
        {
            try
            {
                this.btndireccion.Source = "ic_arrow_downward.png";
                framedireccion.IsVisible = false;
                btndireccion.Clicked += (s, e) =>
                {
                    if (framedireccion.IsVisible)
                    {
                        this.btndireccion.Source = "ic_arrow_downward.png";
                        framedireccion.IsVisible = false;
                    }
                    else
                    {
                        this.btndireccion.Source = "ic_arrow_upward.png";
                        framedireccion.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }

        //public void AccionarReseña()
        //{
        //    try
        //    {
        //        this.btnresenia.Source = "ic_arrow_downward.png";
        //        frameresenia.IsVisible = false;
        //        btnresenia.Clicked += (s, e) =>
        //        {
        //            if (frameresenia.IsVisible)
        //            {
        //                this.btnresenia.Source = "ic_arrow_downward.png";
        //                frameresenia.IsVisible = false;
        //            }
        //            else
        //            {
        //                this.btnresenia.Source = "ic_arrow_upward.png";
        //                frameresenia.IsVisible = true; ;
        //            }
        //        };
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}

        public void AccionarCarreras()
        {
            try
            {
                this.btncarreras.Source = "ic_arrow_downward.png";
                framecarreras.IsVisible = false;
                btncarreras.Clicked += (s, e) =>
                {
                    if (framecarreras.IsVisible)
                    {
                        this.btncarreras.Source = "ic_arrow_downward.png";
                        framecarreras.IsVisible = false;
                    }
                    else
                    {
                        this.btncarreras.Source = "ic_arrow_upward.png";
                        framecarreras.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                // banderaClick = true;
               // Application.Current.MainPage.DisplayAlert("Alerta", "Si llegara a faltar información, no es problema de la aplicación, si no de la base de datos de SUREDSU", "Aceptar");
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                //  CrearCarruselImagen();
                var sugerencia = Convert.ToInt32(App.Current.Properties["sugerencia"].ToString());
                if (sugerencia == 1)
                {
                    LlenarCarrerasSugerencia();
                }
                else
                {
                    LlenarCarrerasPlantel();
                }
              
                LlenarDireccion();
                App.Current.Properties["idPlnatelES"] = EntDetallePlantel.idPlantelES;
                // mapsViewModel = new MapsViewModel(EntDetallePlantel);
                // listViewCarreras.ItemSelected += OnClickOpcionSeleccionada;
            }
            catch (Exception ex)
            {
            }


        }

     

        private void CrearCarruselImagen(int cvedetalle)
        {
            try
            {
                //if (cvedetalle == 1231)
                //{
                //    MyDataSource = new List<CarouselModel>()
                //    {
                //            new CarouselModel()
                //        {
                //            imagen = "documento_20200430105055_0.png" },
                //            new CarouselModel()
                //        {
                //            imagen = "documento_20200430105055_1.jpg" },
                //            new CarouselModel()
                //        {
                //            imagen = "documento_20200430105252_1.jpg" },
                //            new CarouselModel()
                //        {
                //            imagen = "documento_20200430105252_0.jpg" },
                //            new CarouselModel()
                //        {
                //            imagen = "documento_20200430105252_2.jpg" },

                //    };
                //    BindingContext = this;
                //    iscreatecarrusel = true;

                //}else if (cvedetalle == 1232)
                //{
                //    MyDataSource = new List<CarouselModel>()
                //    {
                //            new CarouselModel()
                //        {
                //            imagen = "documento_20200430105446_1.jpg" },
                //            new CarouselModel()
                //        {
                //            imagen = "documento_20200430105446_0.jpg" },
                //            new CarouselModel()
                //        {
                //            imagen = "documento_20200430105446_2.jpg" },
                //            new CarouselModel()
                //        {
                //            imagen = "documento_20200430105509_1.jpg" },
                //             new CarouselModel()
                //        {
                //            imagen = "documento_20200430105509_0.jpg" }

                //    };
                //    BindingContext = this;
                //    iscreatecarrusel = true;
                //}
                //else
                //{
                //    MyDataSource = new List<CarouselModel>()
                //    {new CarouselModel()
                //        {
                //            imagen = "no_existe_imagen.png" } };
                //}

                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();

                var listaImagenes = conn.Query<ImagenPlantel>("SELECT * FROM ImagenPlantel where Cve_detalle_plantel = ?", "" + cvedetalle + "");
                //  var listaImagenes = conn.Query<ImagenPlantel>("SELECT * FROM ImagenPlantel");
                for (int i = 0; i < listaImagenes.Count; i++)
                {
                    listaImagenes[i].ImagenDecodificada = GetImage(listaImagenes[i].Imagenbase64);
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


            }
            catch (Exception ex)
            {
                //MyDataSource = new List<CarouselModel>()
                //    {new CarouselModel()
                //        {
                //            imagen = "no_existe_imagen.png" } };
                MyDataSource = new List<ImagenPlantel>()
                        {new ImagenPlantel()
                            {
                                ImagenDecodificada = "no_existe_imagen.png" } };
            }

        }

        public Xamarin.Forms.ImageSource GetImage(string strEncoded)
        {
            try
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
            catch (Exception ex)
            {
                return null;
            }

        }

        private async Task<bool> LlenarDireccion()
        {
            try
            {
                lblDireccionPlantel.Text = EntDetallePlantel.DetallePlantel.Domicilio;
                LlenarMapa();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        private void LlenarMapa()
        {
            try
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
            catch (Exception ex)
            {
            }
        }

        private void LlenarCarrerasSugerencia()
        {
            try
            {
                var lplanteles = new List<PlantelesES>();
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                conn.CreateTable<Resultado>();
                //  var resultados = conn.Query<Resultado>("SELECT * FROM Resultado WHERE idAlumno = ?", idAlumno);
                var resultados = conn.Query<Resultado>("SELECT * FROM Resultado");
                //   Aptitud
                conn.CreateTable<Aptitud>();
                var cvesAptitud = conn.Query<Aptitud>("SELECT distinct a.Cve_aptitud  FROM DetalleAptitudCarrera as ac " +
                    "INNER JOIN Aptitud AS a ON a.Cve_aptitud = ac.cve_aptitud" +
                    "  WHERE a.Nombre= ? OR a.Nombre= ? OR a.Nombre= ?", resultados[0].aptitud1, resultados[0].aptitud2, resultados[0].aptitud3);
                conn.CreateTable<PlantelesES>();
                if (cvesAptitud.Count == 1)
                {
                       var csugerenciia = conn.Query<CarrerasES>("SELECT distinct CarrerasES.* FROM DetalleAptitudCarrera as ac " +
                           "INNER JOIN CarrerasES on CarrerasES.idCarreraES = ac.idCarreraES" +
                    "  WHERE ac.cve_aptitud = ? ", cvesAptitud[0].Cve_aptitud);
                    var LstEscuela3 = from a in csugerenciia where a.IdPlantelesES == EntDetallePlantel.idPlantelES select a;
                    listViewCarreras.ItemsSource = LstEscuela3.Cast<CarrerasES>().ToList();
                    listViewCarreras.ItemSelected += obtenerdetallecarrera;

                    // App.Current.Properties["carrerasugerencia"] = csugerenciia[0].idCarreraES;
                }
            }
            catch (Exception ex)
            {
               
            }
        }

        private void BtnEnlace_Clicked(object sender, EventArgs e)
        {
            Button btnEnlace = (Button)sender;
            var text = btnEnlace.Text;
            Device.OpenUri(new System.Uri(text));
        }

        private void LlenarCarrerasPlantel()
        {
            try
            {
           //     var carrerasugerencia = Convert.ToInt32(App.Current.Properties["carrerasugerencia"].ToString());
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                var carrera = App.Current.Properties["Carrera"].ToString();
                var palabraCarrera = App.Current.Properties["PalabraCarrera"].ToString();
                var listaCarreras = new List<CarrerasES>();
                //if (carrerasugerencia != 0)
                //{
                //    listaCarreras = conn.Query<CarrerasES>("SELECT * FROM CarrerasES Where idCarreraES = ?", "" + carrerasugerencia + "");
                //}
                if (!string.IsNullOrEmpty(palabraCarrera))
                {
                    string carreraBuscar = palabraCarrera.Replace(" ", "%");
                    listaCarreras = conn.Query<CarrerasES>("SELECT DISTINCT * FROM CarrerasES Where NombreCarreraES LIKE ?", "%" + carreraBuscar + "%");
                    var LstEscuela3 = from a in listaCarreras where a.IdPlantelesES == EntDetallePlantel.idPlantelES select a;
                    listViewCarreras.ItemsSource = LstEscuela3.Cast<CarrerasES>().ToList();
                    listViewCarreras.ItemSelected += obtenerdetallecarrera;
                    return;
                }
                if (string.IsNullOrEmpty(carrera))
                {
                    listaCarreras = conn.Query<CarrerasES>("SELECT * FROM CarrerasES Where IdPlantelesES = ?", "" + EntDetallePlantel.idPlantelES + "");
                }
                else
                {
                    // listaCarreras = conn.Query<CarrerasES>("SELECT * FROM CarrerasES Where IdPlantelesES = ?, NombreCarreraES = ? ", "" + EntDetallePlantel.idPlantelES + ", "+carrera+"");
                    listaCarreras = conn.Query<CarrerasES>("SELECT * FROM CarrerasES Where NombreCarreraES = ? ", "" + carrera + "");
                }
                //  conn.CreateTable<ImagenPlantel>();
                var LstEscuela2 = from a in listaCarreras where a.IdPlantelesES == EntDetallePlantel.idPlantelES select a;
                listViewCarreras.ItemsSource = LstEscuela2.Cast<CarrerasES>().ToList();
                listViewCarreras.ItemSelected += obtenerdetallecarrera;
            }
            catch (Exception ex)
            {
            }

        }

        private async void obtenerdetallecarrera(object sender, SelectedItemChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                Xamarin.Forms.Image imagen = (Xamarin.Forms.Image)sender;
                var img = imagen.Source.ToString();
                img = img.Replace("File:", "");
                var popupProperties = new PopUpPage(imagen.Source);
              //  popupProperties.CloseWhenBackgroundIsClicked = false;
                await PopupNavigation.PushAsync(popupProperties);
            }
            catch (Exception ex)
            {
            }
        }
    }

    public class CarouselModel
    {
        public ImageSource imagen { get; set; }
    }
}