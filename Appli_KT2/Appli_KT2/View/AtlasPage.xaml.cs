using Appli_KT2.Model;
using Appli_KT2.Services;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using Plugin.Connectivity;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AtlasPage : ContentPage
    {
        MunicipioViewModel municipiosViewModel = new MunicipioViewModel();
        PlantelESViewModel plantelESViewModel = new PlantelESViewModel();
        CarreraViewModel carreraViewModel = new CarreraViewModel();
        EstadosViewModel estadosViewModel = new EstadosViewModel();
        DetalleUniversidadViewModel detalleUniversidadViewModel = new DetalleUniversidadViewModel();
        MunicipioViewModel municipioViewModel = new MunicipioViewModel();
        PerfilAlumnoViewModel perfilAlumno = new PerfilAlumnoViewModel();
        Estados estados = new Estados();
        Municipios municipios = new Municipios();
        PlantelesES entplantelesES = new PlantelesES();
        CarrerasES carrerasES = new CarrerasES();
        ConexionInternet conexionInternet = new ConexionInternet();
        List<Municipios> ListaMunicipios = new List<Municipios>();
        List<CarrerasES> ListaCarreras = new List<CarrerasES>();
        List<PlantelesES> ListaPlanteles = new List<PlantelesES>();
        ToolbarItem MenuItem1;
        private bool estatusInternet;
        public SQLiteConnection conn;
        private static bool banderaClick = true;
        private int notificacionnoleidas;

        public AtlasPage()
        {
            try
            {
                InitializeComponent();
                App.Current.Properties["carrerasugerencia"] = 0;
                VerificarUsuario();
                btnBuscar.Clicked += BuscarAtlas;
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
                //PruebaNotificacion();
                txtCarrera.Text = "";
                App.Current.Properties["carrerasugerencia"] = 0;
                App.Current.Properties["sugerencia"] = 0;
                var idAlumno = Application.Current.Properties["idAlumno"].ToString();
                if (!idAlumno.Equals("0"))
                {
                    App.Current.Properties["sugerencia"] = 1;
                    await perfilAlumno.SincronizarAptitudAlumno();
                    CargarSugerencias(Convert.ToInt32(idAlumno));
                }
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                txtCarrera.TextChanged += BuscarCarrera;
                VerificarInternet();
                App.Current.Properties["PalabraCarrera"] = "";
                municipios = new Municipios();
                entplantelesES = new PlantelesES();
                carrerasES = new CarrerasES();
                var usuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());
                if (MenuItem1 != null)
                {
                    MostrarNotificacionesNoLeidas();
                }
              //  VerificarUsuario();

            }
            catch (Exception ex)
            {
            }
           
        }

        public async Task ConsultarNotificaciones()
        {
            try
            {

                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var cveUsuario = Xamarin.Forms.Application.Current.Properties["cveUsuario"];
                var url = conexion.URL + "" + conexion.ObtenerNotificacionesNoLeidas1 + cveUsuario;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lstNotificaciones = JsonConvert.DeserializeObject<List<Notificaciones>>(content);
                    notificacionnoleidas = lstNotificaciones.Count;
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async void MostrarNotificacionesNoLeidas()
        {
            await ConsultarNotificaciones();
            DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, this.MenuItem1, notificacionnoleidas.ToString(), Color.Red, Color.White);
        }


        private void MenuItem1_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new NotificacionesPage());
            // DependencyService.Get<IToolbarItemBadgeService>().SetBadge(this, this.MenuItem1, "5", Color.Red, Color.White);
        }

        public void PruebaNotificacion()
        {
            if (ToolbarItems.Count > 0)
            {
               
            }
        }



        private void BtnEnlace_Clicked(object sender, EventArgs e)
        {
            Button btnEnlace = (Button)sender;
            var text = btnEnlace.Text;
            Device.OpenUri(new System.Uri(text));
        }

        private void VerificarInternet()
        {
            var status = 1;
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    if (conexionInternet.VerificarInternet())
                    {
                        if (status == 1)
                        {
                            LlenarFiltros();
                            status = 0;
                            estatusInternet = true;
                        }
                        return true;
                    }
                    else
                    {
                        status = 1;
                        estatusInternet = false;
                        //  
                    }
                    return true;
                }
                catch (NullReferenceException ex)
                {
                    return true;
                }
            });
        }

        private void IniciarSesion()
        {
            try
            {
                ToolbarItem icLogin = new ToolbarItem
                {
                    Text = "Iniciar sesión",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 1
                };
                this.ToolbarItems.Add(icLogin);
                icLogin.Clicked += OnClickLogin;
            }
            catch (Exception ex)
            {
            }

        }

        private void MostarCampanaNoti()
        {
            try
            {
                MenuItem1 = new ToolbarItem
                {
                    Text = "Iniciar sesión",
                    Order = ToolbarItemOrder.Primary,
                    Priority = 0,
                    IconImageSource = "ic_action_notifications_none.png"
                };
                this.ToolbarItems.Add(MenuItem1);
                MenuItem1.Clicked += MenuItem1_Clicked;
                MostrarNotificacionesNoLeidas();
            }
            catch (Exception ex)
            {
            }

        }

        private void CerrarSesion()
        {
            try
            {
                ToolbarItem icClose = new ToolbarItem
                {
                    Text = "Cerrar sesión",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 1
                };
                this.ToolbarItems.Add(icClose);
                icClose.Clicked += OnClickCerrar;
            }
            catch (Exception ex)
            {
            }
           
        }

        private void CompartirAplicaciones()
        {
            try
            {
                ToolbarItem icShare = new ToolbarItem
                {
                    Text = "Compartir Appli-KT",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 0
                };
                this.ToolbarItems.Add(icShare);
                icShare.Clicked += OnClickShare;
            }
            catch (Exception ex)
            {
            }
           
        }

        private void SincronizarDatos()
        {
            try
            {
                ToolbarItem icSincronizar = new ToolbarItem
                {
                    Text = "Sincronizar",
                    Order = ToolbarItemOrder.Secondary,
                    Priority = 0
                };
                this.ToolbarItems.Add(icSincronizar);
                icSincronizar.Clicked += SincronizarClick;
            }
            catch (Exception ex)
            {
            }
        }

        private void SincronizarClick(object sender, EventArgs e)
        {
            Sincronizar();
        }

        private void VerificarUsuario()
        {
            try
            {
                var usuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());

                switch (usuario)
                {
                    case 0:
                        IniciarSesion();
                        SincronizarDatos();
                        break;
                    case 1:
                        MostarCampanaNoti();
                        CerrarSesion();
                        SincronizarDatos();
                        break;
                    case 2:
                        MostarCampanaNoti();
                        CompartirAplicaciones();
                        CerrarSesion();
                        SincronizarDatos();
                        break;
                    case 3:
                        MostarCampanaNoti();
                        CompartirAplicaciones();
                        CerrarSesion();
                        SincronizarDatos();
                        break;
                    case 4:
                        MostarCampanaNoti();
                        CompartirAplicaciones();
                        CerrarSesion();
                        SincronizarDatos();
                        break;
                    case 5:
                        MostarCampanaNoti();
                        CompartirAplicaciones();
                        CerrarSesion();
                        SincronizarDatos();
                        break;
                    case 6:
                        MostarCampanaNoti();
                        CompartirAplicaciones();
                        CerrarSesion();
                        SincronizarDatos();
                        break;
                    case 7:
                        MostarCampanaNoti();
                        CompartirAplicaciones();
                        CerrarSesion();
                        SincronizarDatos();
                        break;
                }
            }
            catch (Exception ex)
            {
            }
           
        }

        private void OnClickCerrar(object sender, EventArgs e)
        {
            try
            {
                App.Current.Properties["usuario"] = "";
                App.Current.Properties["contrasena"] = "";
                App.Current.Properties["idAlumno"] = 0;
                App.Current.Properties["tipo_usuario"] = 0;
                App.Current.Properties["nombreUsuario"] = "Nombre Usuario";
                App.Current.Properties["rolUsuario"] = "";
                App.Current.MainPage.DisplayAlert("Información", "Se ha cerrado sesión", "Aceptar");
                Application.Current.MainPage = new NavigationPage(new MainPage());
            }
            catch (Exception ex)
            {
            }
           
        }

        private void OnClickLogin(object sender, EventArgs e)
        {
            try
            {
                MainViewModel.GetInstance().Login = new LoginViewModel();
                Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
            }
            catch (Exception ex)
            {
            }
           
        }

        private async void OnClickShare(object sender, EventArgs e)
        {
            try
            {
                ShareDialogClass share = new ShareDialogClass();
                await share.ShareUri("https://drive.google.com/drive/folders/1qbc3bjTOXFsyDV5vskAYH4DJNdA-5755?usp=sharing", "Compartir link de descarga de Appli-KT");
            }
            catch (Exception ex)
            {
            }
        }

        private async void BuscarAtlas(object sender, EventArgs e)
        {
            try
            {

                if (municipios.idMunicipio == 0 && entplantelesES.idPlantelES == 0 && carrerasES.IdPlantelesES == 0 && string.IsNullOrEmpty(txtCarrera.Text))
                {
                    await App.Current.MainPage.DisplayAlert("Información", "Debes seleccionar  por los menos uno de los filtros o escribir la carrera que buscas", "Aceptar");
                    return;
                }
                if (municipios.idMunicipio == 0)
                {
                    App.Current.Properties["municipios"] = 0;
                }
                else
                {
                    App.Current.Properties["municipios"] = municipios.idMunicipio;
                }
                if (entplantelesES.idPlantelES == 0)
                {
                    App.Current.Properties["institucion"] = 0;
                }
                else
                {
                    App.Current.Properties["institucion"] = entplantelesES.idPlantelES;
                }
                if (carrerasES.IdPlantelesES == 0)
                {
                    App.Current.Properties["Carrera"] = "";
                }
                else
                {
                    App.Current.Properties["Carrera"] = carrerasES.NombreCarreraES;
                }
                await Application.Current.MainPage.Navigation.PushAsync(new ResultadoAtlasPage());
            }
            catch (Exception ex)
            {
                App.Current.Properties["municipios"] = 0;
                App.Current.Properties["institucion"] = 0;
                App.Current.Properties["Carrera"] = "";
                await Application.Current.MainPage.Navigation.PushAsync(new ResultadoAtlasPage());
            }
          
        }

        private async Task LlenarFiltros()
        {
            try
            {
                // LlenarEstados();
                await LlenarMunicipios();
                 LlenarCarreras();
                 LlenarPlantelesES();
            }
            catch (Exception ex)
            {
            }
           
        }

        private async Task LlenarMunicipios()
        {
            try
            {
                conn.CreateTable<Municipios>();
                var municipio = new Municipios()
                {
                    idEstado = 0,
                    idMunicipio = 0,
                    NombreMunicipio = "Seleccionar Municipio"
                };
                // ListaMunicipios.Add(municipio);
                var mun = (from x in conn.Table<Municipios>() select x).ToList();
                mun = mun.OrderBy(p => p.NombreMunicipio).ToList();
                if (mun == null || mun.Count == 0)
                {
                    var sincronizacion = Convert.ToInt32(App.Current.Properties["sincronizacion"].ToString());
                    if (sincronizacion == 1)
                    {
                        App.Current.Properties["sincronizacion"] = 0;
                        bool accion = await App.Current.MainPage.DisplayAlert("Sincronizar", "Si es la primera vez que instala la aplicación por favor sincronize los datos", "Aceptar", "Cancelar");
                        if (accion)
                        {
                            Sincronizar();
                        }
                    }
                }
                ListaMunicipios.Add(municipio);
                for (int i = 0; i < mun.Count; i++)
                {
                    ListaMunicipios.Add(mun[i]);
                }

                pMunicipio.ItemsSource = ListaMunicipios;
                pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");
                pMunicipio.SelectedIndexChanged += SeleccionarMunicipio;
            }
            catch (Exception ex)
            {
            }
           
        }

        private async void Sincronizar()
        {
            try
            {
                bool accion = await DisplayAlert("Alerta", "La sincronización puede tardar varios minutos, aun así desea sincronizar?", "Aceptar", "Cancelar");
                if (!accion)
                {
                    return;
                }
                frameSugerencias.IsVisible = false;
                frameFiltro.IsVisible = false;
                frameSincronizacion.IsVisible = true;

                await carreraViewModel.SincronizarCarrera();
                await carreraViewModel.SincronizarDetalleCarrera();
                await plantelESViewModel.SincronizarDetallePlantel();
                await plantelESViewModel.SincronizarPlantelesES();
                await municipioViewModel.SincronizarMunicipio();
                await perfilAlumno.SincronizarAptitudAlumno();
                await perfilAlumno.SincronizarAptitudes();
                await perfilAlumno.SincronizarAptitudesCarrera();
                await detalleUniversidadViewModel.SincronizarImagenesPlantel();

              //  await LlenarFiltros();
                frameSincronizacion.IsVisible = false;
                frameSugerencias.IsVisible = false;
                frameFiltro.IsVisible = true;
               await Application.Current.MainPage.Navigation.PushAsync(new SplashPage());
            }
            catch (Exception ex)
            {
            }
            
        }

        private void LlenarPlantelesES()
        {
            try
            {
                conn.CreateTable<PlantelesES>();
                var plantel = new PlantelesES()
                {
                    NombrePlantelES = "Seleccionar Plantel"
                };
                var plantelesEs = (from x in conn.Table<PlantelesES>() select x).ToList();
                plantelesEs = plantelesEs.OrderBy(p => p.NombrePlantelES).ToList();
                ListaPlanteles.Add(plantel);
                for (int i = 0; i < plantelesEs.Count; i++)
                {
                    ListaPlanteles.Add(plantelesEs[i]);
                }
                pPlantelesES.ItemsSource = ListaPlanteles;
                pPlantelesES.ItemDisplayBinding = new Binding("NombrePlantelES");
                pPlantelesES.SelectedIndexChanged += SeleccionarPlanteles;
                BuscarSugerencias();
            }
            catch (Exception ex)
            {
            }
            
        }

        private void LlenarCarreras()
        {
            try
            {
                conn.CreateTable<CarrerasES>();
                var carrera = new CarrerasES()
                {
                    NombreCarreraES = "Seleccionar Carrera"
                };
                var carreras = conn.Query<CarrerasES>("SELECT DISTINCT * FROM CarrerasES GROUP BY NombreCarreraES");
                carreras = carreras.OrderBy(p => p.NombreCarreraES).ToList();
                ListaCarreras.Add(carrera);
                for (int i = 0; i < carreras.Count; i++)
                {
                    ListaCarreras.Add(carreras[i]);
                }
                pCarreras.ItemsSource = ListaCarreras;

                pCarreras.ItemDisplayBinding = new Binding("NombreCarreraES");

                pCarreras.SelectedIndexChanged += SeleccionarCarrera;
            }
            catch (Exception ex)
            {
            }
           
           
        }

        private async void BuscarSugerencias()
        {
            try
            {
                conn.CreateTable<Resultado>();
                var idAlumno = Application.Current.Properties["idAlumno"].ToString();
                if (idAlumno.Equals("0"))
                {
                    return;
                }
                var resultado = conn.Query<Resultado>("SELECT  * FROM Resultado WHERE idAlumno = ?", idAlumno);
                if (resultado != null || resultado.Count != 0)
                {
                    CargarSugerencias(Convert.ToInt32(idAlumno));
                  //  await DisplayAlert("Aviso", "De acuerdo a sus resultados se encontraron carreras que se relacionan a sus aptitudes", "Aceptar");

                }
            }
            catch (Exception ex)
            {
            }
          

        }

        private void CargarSugerencias(int idAlumno)
        {
            try
            {
                ResultadoAtlasViewModel resultadoAtlasViewModel = new ResultadoAtlasViewModel();
                listViewSugerencias.BindingContext = resultadoAtlasViewModel;
                resultadoAtlasViewModel.ConsultarPlantelesDetalleBDSugerencias(idAlumno);
                if (resultadoAtlasViewModel.lstPlanteles.Count == 0)
                {
                }
                frameSugerencias.IsVisible = true;
                listViewSugerencias.IsVisible = true;
                listViewSugerencias.ItemsSource = resultadoAtlasViewModel.lstPlanteles;
                listViewSugerencias.ItemSelected += OnClickOpcionSeleccionada;
            }
            catch (Exception ex)
            {
            }
           
        }

        private async void OnClickOpcionSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                listViewSugerencias.SelectedItem = null;
                if (banderaClick)
                {
                    App.Current.Properties["Carrera"] = "";
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
            catch (Exception ex)
            {
            }
        }

        private void BuscarCarrera(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCarrera.Text))
                {
                    pCarreras.ItemsSource = ListaCarreras;
                    pMunicipio.ItemsSource = ListaMunicipios;
                    pPlantelesES.ItemsSource = ListaPlanteles;
                    App.Current.Properties["PalabraCarrera"] = "";
                }
                else
                {
                    App.Current.Properties["PalabraCarrera"] = txtCarrera.Text;
                    FiltrarPalabraCarrera(txtCarrera.Text);
                    FiltrarPalabraInstitucion(txtCarrera.Text);
                    FiltrarPalabraMunicipio(txtCarrera.Text);
                }
            }
            catch (Exception ex)
            {
            }
          
        }

        private void FiltrarPalabraCarrera(string palabracarrera)
        {
            try
            {
                var carrera = new CarrerasES()
                {
                    NombreCarreraES = "Seleccionar Carrera"
                };
                var listaInstituciones = new List<string>();
                string carreraBuscar = txtCarrera.Text.Replace(" ", "%");
                var listaCarreras = conn.Query<CarrerasES>("SELECT DISTINCT * FROM CarrerasES Where NombreCarreraES LIKE ? COLLATE SQL_Latin1_General_CP1_CI_AI GROUP BY NombreCarreraES", "%" + palabracarrera.ToUpper() + "%");
                List<CarrerasES> carrerasEs = new List<CarrerasES>();
                carrerasEs.Add(carrera);
                for (int i = 0; i < listaCarreras.Count; i++)
                {
                    carrerasEs.Add(listaCarreras[i]);
                }
                pCarreras.ItemsSource = carrerasEs;
            }
            catch (Exception ex)
            {
            }
           
        }

        private void FiltrarPalabraMunicipio(string palabracarrera)
        {
            try
            {
                var listaInstituciones = new List<string>();
                string carreraBuscar = txtCarrera.Text.Replace(" ", "%");
                var listaMunicipios = conn.Query<Municipios>("SELECT DISTINCT Municipios.* FROM CarrerasES " +
                    " INNER JOIN PlantelesES ON PlantelesES.idPlantelES = CarrerasES.IdPlantelesES" +
                    " INNER JOIN Municipios ON Municipios.idMunicipio = PlantelesES.Municipio" +
                    " Where CarrerasES.NombreCarreraES LIKE ?", "%" + carreraBuscar + "%");
                pMunicipio.ItemsSource = listaMunicipios;
            }
            catch (Exception ex)
            {
            }
          
        }

        private void FiltrarPalabraInstitucion(string palabracarrera)
        {
            try
            {
                var listaInstituciones = new List<string>();
                string carreraBuscar = txtCarrera.Text.Replace(" ", "%");
                var listaPlanteles = conn.Query<PlantelesES>("SELECT DISTINCT PlantelesES.* FROM CarrerasES " +
                    " INNER JOIN PlantelesES ON PlantelesES.idPlantelES = CarrerasES.IdPlantelesES" +
                    " Where CarrerasES.NombreCarreraES LIKE ?", "%" + carreraBuscar + "%");
                pPlantelesES.ItemsSource = listaPlanteles;
            }
            catch (Exception ex)
            {
            }
           
        }

        private void SeleccionarMunicipio(object sender, EventArgs e)
        {
            try
            {
                municipios = (Municipios)pMunicipio.SelectedItem;
                if (municipios.idMunicipio == 0)
                {
                    LlenarPlantelesES();
                }
                var planteles = from a in ListaPlanteles where a.Municipio == municipios.idMunicipio select a;
                pPlantelesES.ItemsSource = planteles.Cast<PlantelesES>().ToList();
            }
            catch (Exception ex)
            {
            }
           
        }

        private void SeleccionarPlanteles(object sender, EventArgs e)
        {
            try
            {
                entplantelesES = (PlantelesES)pPlantelesES.SelectedItem;
                //LlenarCarreras();
            }
            catch (Exception ex)
            {
            }
          
        }

        private void SeleccionarCarrera(object sender, EventArgs e)
        {

            try
            {
                App.Current.Properties["PalabraCarrera"] = "";
                carrerasES = (CarrerasES)pCarreras.SelectedItem;

                if (carrerasES.idCarreraES == 0)
                {
                    LlenarMunicipios();
                    LlenarPlantelesES();
                }
                ListaPlanteles  = conn.Query<PlantelesES>("SELECT p.* FROM CarrerasES AS c" +
                        " INNER JOIN PlantelesES AS p on p.idPlantelES = c.IdPlantelesES " +
                        "WHERE c.NombreCarreraES = ?", ""+carrerasES.NombreCarreraES+"");
                pPlantelesES.ItemsSource = ListaPlanteles;
              
                ListaMunicipios = conn.Query<Municipios>("SELECT DISTINCT Municipios.* FROM CarrerasES " +
                        " INNER JOIN PlantelesES on PlantelesES.idPlantelES = CarrerasES.IdPlantelesES " +
                        " INNER JOIN Municipios on Municipios.idMunicipio = PlantelesES. Municipio " +
                        "WHERE CarrerasES.NombreCarreraES = ?", "" + carrerasES.NombreCarreraES + "");
                pMunicipio.ItemsSource = ListaMunicipios;
            }
            catch (Exception ex)
            {

            }

            //LlenarCarreras();
        }

        public void FiltrarMunicipio(List<Municipios> municipios)
        {
            try
            {
                if (estados.IdEstado == 0)
                {
                    pMunicipio.ItemsSource = municipios;
                    return;
                }
                else
                {
                    var municipio = from a in municipios where a.idEstado == 11 select a;
                    pMunicipio.ItemsSource = municipio.Cast<Municipios>().ToList();
                    return;
                }
            }
            catch (Exception ex)
            {
            }
            
        }

        public void FiltrarPlanteles(List<PlantelesES> plantelesEs)
        {
            try
            {
                conn.CreateTable<PlantelesES>();

                if (municipios.idEstado == 0)
                {
                    var details = (from x in conn.Table<PlantelesES>() select x).ToList();
                    pPlantelesES.ItemsSource = details;
                    return;
                }
                else
                {
                    var planteles = from a in plantelesEs where a.Municipio == municipios.idMunicipio select a;
                    pPlantelesES.ItemsSource = planteles.Cast<DetallePlantel>().ToList();
                    return;
                }
            }
            catch (Exception ex)
            {
            }
            
        }

        public void FiltrarCarreras(List<CarrerasES> carreras)
        {
            try
            {
                if (entplantelesES == null || entplantelesES.idPlantelES == 0)
                {
                    pCarreras.ItemsSource = carreras;
                    return;
                }
                else
                {
                    //var alumnosC = lstMunicipios.Where(a => a.IdEstado == 43);
                    var carrera = from a in carreras where a.IdPlantelesES == entplantelesES.idPlantelES select a;
                    pCarreras.ItemsSource = carrera.Cast<CarrerasES>().ToList(); ;
                    return;
                }
            }
            catch (Exception ex)
            {
            }
           
        }

        public class DetalleAptitudCarrera
        {
            private int _idCarreraES;
            private int _cve_aptitud;

            public int idCarreraES { get => _idCarreraES; set => _idCarreraES = value; }
            public int cve_aptitud { get => _cve_aptitud; set => _cve_aptitud = value; }
        }
    }
}