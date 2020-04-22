using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Plugin.Connectivity;
using SQLite;
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
        PlantelesES plantelesES = new PlantelesES();
        CarrerasES carrerasES = new CarrerasES();
        ConexionInternet conexionInternet = new ConexionInternet();
        List<Municipios> ListaMunicipios = new List<Municipios>();
        List<CarrerasES> ListaCarreras = new List<CarrerasES>();
        List<PlantelesES> ListaPlanteles = new List<PlantelesES>();
        private bool estatusInternet;
        public SQLiteConnection conn;
        private static bool banderaClick = true;

        public AtlasPage()
        {
            InitializeComponent();
            VerificarUsuario();
            btnBuscar.Clicked += BuscarAtlas;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            txtCarrera.TextChanged += BuscarCarrera;
            VerificarInternet();
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
            ToolbarItem icLogin = new ToolbarItem
            {
                Text = "Iniciar sesión",
                Order = ToolbarItemOrder.Secondary,
                Priority = 1
            };
            this.ToolbarItems.Add(icLogin);
            icLogin.Clicked += OnClickLogin;
        }

        private void CerrarSesion()
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

        private void CompartirAplicaciones()
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

        private void VerificarUsuario()
        {
            var usuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());

            switch (usuario)
            {
                case 0:
                    IniciarSesion();
                    break;
                case 1:
                    CerrarSesion();
                    break;
                case 2:
                    CompartirAplicaciones();
                    CerrarSesion();
                    break;
                case 3:
                    CompartirAplicaciones();
                    CerrarSesion();
                    break;
                case 4:
                    CompartirAplicaciones();
                    CerrarSesion();
                    break;
                case 5:
                    CompartirAplicaciones();
                    CerrarSesion();
                    break;
                case 6:
                    CompartirAplicaciones();
                    CerrarSesion();
                    break;
                case 7:
                    CompartirAplicaciones();
                    CerrarSesion();
                    break;
            }
        }

        private void OnClickCerrar(object sender, EventArgs e)
        {
            App.Current.Properties["usuario"] = "";
            App.Current.Properties["contrasena"] = "";
            App.Current.Properties["idAlumno"] = 0;
            App.Current.Properties["tipo_usuario"] = 0;
            App.Current.Properties["nombreUsuario"] = "Nombre Usuario";
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        private void OnClickLogin(object sender, EventArgs e)
        {
            MainViewModel.GetInstance().Login = new LoginViewModel();
            Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        private async void OnClickShare(object sender, EventArgs e)
        {
            try
            {
                ShareDialogClass share = new ShareDialogClass();
                await share.ShareUri("WWW.HolaMundo.com", "Compartir link de descarga de Appli-KT");
            }
            catch (Exception ex)
            {

            }
        }

        private async void BuscarAtlas(object sender, EventArgs e)
        {
            if (municipios.idMunicipio == 0)
            {
                App.Current.Properties["municipios"] = 0;
            }
            else
            {
                App.Current.Properties["municipios"] = municipios.idMunicipio;
            }
            if (plantelesES.idPlantelES == 0)
            {
                App.Current.Properties["institucion"] = 0;
            }
            else
            {
                App.Current.Properties["institucion"] = plantelesES.idPlantelES;
            }
            if (carrerasES.IdPlantelesES == 0)
            {
                App.Current.Properties["Carrera"] = 0;
            }
            else
            {
                App.Current.Properties["Carrera"] = carrerasES.IdPlantelesES;
            }
            await Application.Current.MainPage.Navigation.PushAsync(new ResultadoAtlasPage());
        }

        private void LlenarFiltros()
        {
            // LlenarEstados();
            LlenarMunicipios();
            LlenarCarreras();
            LlenarPlantelesES();
        }

        private async void LlenarMunicipios()
        {
            conn.CreateTable<Municipios>();
            var municipio = new Municipios()
            {
                idEstado = 0,
                idMunicipio = 0,
                NombreMunicipio = "Seleccionar Municipio"
            };
           // ListaMunicipios.Add(municipio);
            var mun= (from x in conn.Table<Municipios>() select x).ToList();
            mun = mun.OrderBy(p => p.NombreMunicipio).ToList();
            if (mun == null || mun.Count == 0)
            {
                var sincronizacion = Convert.ToInt32(App.Current.Properties["sincronizacion"].ToString());
                if (sincronizacion == 1)
                {
                    App.Current.Properties["sincronizacion"] = 0;
                    bool accion = await DisplayAlert("Sincronizar", "Si es la primera vez que instala la aplicación por favor sincronize los datos", "Aceptar", "Cancelar");
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

        private async void Sincronizar()
        {
            bool accion = await DisplayAlert("Alerta", "La sincronización puede tardar varios minutos, aun así desea sincronizar?", "Aceptar", "Cancelar");
            if (!accion)
            {
                return;
            }
            frameSugerencias.IsVisible = false;
            frameFiltro.IsVisible = false;
            frameSincronizacion.IsVisible = true;
            await detalleUniversidadViewModel.SincronizarImagenesPlantel();
            await carreraViewModel.SincronizarCarrera();
            await carreraViewModel.SincronizarDetalleCarrera();
            await plantelESViewModel.SincronizarDetallePlantel();
            await plantelESViewModel.SincronizarPlantelesES();
            await municipioViewModel.SincronizarMunicipio();
            await perfilAlumno.SincronizarAptitudAlumno();
            await perfilAlumno.SincronizarAptitudes();

            frameSincronizacion.IsVisible = false;
            frameSugerencias.IsVisible = false;
            frameFiltro.IsVisible = true;
            LlenarFiltros();
        }

        private void LlenarPlantelesES()
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

        private void LlenarCarreras()
        {
            conn.CreateTable<CarrerasES>();
            var carrera = new CarrerasES()
            {
                NombreCarreraES = "Seleccionar Carrera"
            };
            var carreras = (from x in conn.Table<CarrerasES>() select x).ToList();
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

        private async void BuscarSugerencias()
        {
            conn.CreateTable<Resultado>();
            var idAlumno = Application.Current.Properties["idAlumno"].ToString();
            if (idAlumno.Equals("0"))
            {
                return;
            }
            var resultado = conn.Query<Resultado>("SELECT * FROM Resultado WHERE idAlumno = ?", idAlumno);
            if (resultado != null || resultado.Count != 0)
            {
                CargarSugerencias();
                await DisplayAlert("Aviso", "De acuerdo a sus resultados se encontraron carreras que se relacionan a sus aptitudes", "Aceptar");
                
            }

        }

        private void CargarSugerencias()
        {
            ResultadoAtlasViewModel resultadoAtlasViewModel = new ResultadoAtlasViewModel();
            //listViewSugerencias.IsVisible = true;
            listViewSugerencias.BindingContext = resultadoAtlasViewModel;
            resultadoAtlasViewModel.ConsultarPlantelesDetalleBDSugerencias();
            if (resultadoAtlasViewModel.lstPlanteles.Count == 0)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "No se encontraron universidades, actualize la información", "Aceptar");
                // Application.Current.MainPage.Navigation.PopAsync();
            }
            frameSugerencias.IsVisible = true;
            listViewSugerencias.IsVisible = true;
            //FiltrarResultado(resultadoAtlasViewModel.lstPlanteles);
            Application.Current.MainPage.DisplayAlert("Alerta", "Si llegara a faltar información, no es a causa de la aplicación", "Aceptar");
            listViewSugerencias.ItemsSource = resultadoAtlasViewModel.lstPlanteles;
            listViewSugerencias.ItemSelected += OnClickOpcionSeleccionada;
        }

        private async void OnClickOpcionSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {

            listViewSugerencias.SelectedItem = null;
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

        private void BuscarCarrera(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCarrera.Text))
            {
                pCarreras.ItemsSource = ListaCarreras;
            }
            else
            {
                var carrera = new CarrerasES()
                {
                    NombreCarreraES = "Seleccionar Carrera"
                };
                var listaInstituciones = new List<string>();
                string carreraBuscar = txtCarrera.Text.Replace(" ", "%");
                var listaCarreras = conn.Query<CarrerasES>("SELECT DISTINCT * FROM CarrerasES Where NombreCarreraES LIKE ?", "%" + carreraBuscar + "%");
                List<CarrerasES> carrerasEs = new List<CarrerasES>();
                carrerasEs.Add(carrera);
                for (int i = 0; i < listaCarreras.Count; i++)
                {
                    carrerasEs.Add(listaCarreras[i]);
                }
                pCarreras.ItemsSource = carrerasEs;
            }
        }

        private void SeleccionarMunicipio(object sender, EventArgs e)
        {
            municipios = (Municipios)pMunicipio.SelectedItem;
            if (municipios.idMunicipio == 0)
            {
                LlenarPlantelesES();
            }
            var planteles = from a in ListaPlanteles where a.Municipio == municipios.idMunicipio select a;
            pPlantelesES.ItemsSource = planteles.Cast<PlantelesES>().ToList();
        }

        private void SeleccionarPlanteles(object sender, EventArgs e)
        {
            plantelesES = (PlantelesES)pPlantelesES.SelectedItem;
            //LlenarCarreras();
        }

        private void SeleccionarCarrera(object sender, EventArgs e)
        {
            try
            {
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
              
                ListaMunicipios = conn.Query<Municipios>("SELECT m.* FROM PlantelesES AS p" +
                        " INNER JOIN Municipios AS m on m.idMunicipio = p. Municipio" +
                        "WHERE c.NombreCarreraES = ?", "" + carrerasES.NombreCarreraES + "");
                pMunicipio.ItemsSource = ListaMunicipios;
            }
            catch (Exception ex)
            {
            }
          
            //LlenarCarreras();
        }

        public void FiltrarMunicipio(List<Municipios> municipios)
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

        public void FiltrarPlanteles(List<PlantelesES> plantelesEs)
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

        public void FiltrarCarreras(List<CarrerasES> carreras)
        {
            if (plantelesES == null || plantelesES.idPlantelES == 0)
            {
                pCarreras.ItemsSource = carreras;
                return;
            }
            else
            {
                //var alumnosC = lstMunicipios.Where(a => a.IdEstado == 43);
                var carrera = from a in carreras where a.IdPlantelesES == plantelesES.idPlantelES select a;
                pCarreras.ItemsSource = carrera.Cast<CarrerasES>().ToList(); ;
                return;
            }
        }
    }
}