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
        Estados estados = new Estados();
        Municipios municipios = new Municipios();
        PlantelesES plantelesES = new PlantelesES();
        CarrerasES carrerasES = new CarrerasES();
        ConexionInternet conexionInternet = new ConexionInternet();
        private bool estatusInternet;
        public SQLiteConnection conn;


        public AtlasPage ()
		{
			InitializeComponent ();
            VerificarUsuario();
            btnBuscar.Clicked += BuscarAtlas;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            txtCarrera.TextChanged += BuscarCarrera;
            VerificarInternet();
            if (estatusInternet)
            {
                Application.Current.MainPage.DisplayAlert("Alerta", "Se cuenta con acceso a internet","Aceptar");
            }else
                Application.Current.MainPage.DisplayAlert("Alerta", "Algunas funciones no funcionaran de manera correcta", "Aceptar");

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
                Text = "Conmpartir Aplicación",
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
             if (plantelesES.idPlantelES == null)
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
            LlenarPlantelesES();
            LlenarCarreras();
        }

        private void LlenarMunicipios()
        {
            conn.CreateTable<Municipios>();
            
            var details = (from x in conn.Table <Municipios>() select x).ToList();
            pMunicipio.ItemsSource = details;
            pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");

             pMunicipio.SelectedIndexChanged += SeleccionarMunicipio;
        }

        private void LlenarPlantelesES()
        {
                conn.CreateTable<PlantelesES>();
                var details = (from x in conn.Table<PlantelesES>() select x).ToList();
                pPlantelesES.ItemsSource = details;
                pPlantelesES.ItemDisplayBinding = new Binding("NombrePlantelES");
            pPlantelesES.SelectedIndexChanged += SeleccionarPlanteles;
           
        }

        private void LlenarCarreras()
        {
            conn.CreateTable<CarrerasES>();
            var details = (from x in conn.Table<CarrerasES>() select x).ToList();
            pCarreras.ItemsSource = details;
            pCarreras.ItemDisplayBinding = new Binding("NombreCarreraES");

            pCarreras.SelectedIndexChanged += SeleccionarCarrera;
           
        }

        private void BuscarCarrera(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtCarrera.Text))
            {
                pCarreras.ItemsSource = carreraViewModel.ListCarreraES;
            }
            else
            {
                var listaInstituciones = new List<string>();
                string carreraBuscar = txtCarrera.Text.Replace(" ", "%");
                var listaCarreras = conn.Query<CarrerasES>("SELECT DISTINCT * FROM CarrerasES Where NombreCarreraES LIKE ?", "%" + carreraBuscar + "%");
                pCarreras.ItemsSource = listaCarreras;
            }
        }

        private void SeleccionarMunicipio(object sender, EventArgs e)
        {
            municipios = (Municipios)pMunicipio.SelectedItem;
            LlenarPlantelesES();
        }

        private void SeleccionarPlanteles(object sender, EventArgs e)
        {
            plantelesES = (PlantelesES)pPlantelesES.SelectedItem;
            LlenarCarreras();
        }

        private void SeleccionarCarrera(object sender, EventArgs e)
        {
            try
            {
                carrerasES = (CarrerasES)pCarreras.SelectedItem;
                var listaPlanteles = conn.Query<PlantelesES>("SELECT p.* FROM CarrerasES AS c" +
                        " INNER JOIN PlantelesES AS p on p.idPlantelES = c.IdPlantelesES " +
                        "WHERE c.NombreCarreraES = ?",""+carrerasES.NombreCarreraES+"");
                pPlantelesES.ItemsSource = listaPlanteles;
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