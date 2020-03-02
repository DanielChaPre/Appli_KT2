using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Plugin.Connectivity;
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
        DetallePlantel plantelesES = new DetallePlantel();
        CarrerasES carrerasES = new CarrerasES();
        ConexionInternet conexionInternet = new ConexionInternet();


        public AtlasPage ()
		{
			InitializeComponent ();
            VerificarUsuario();
            btnBuscar.Clicked += BuscarAtlas;
           
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
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
                        }
                        return true;
                    }
                    else
                    {
                        status = 1;
                        Application.Current.MainPage.DisplayAlert("Notificación", "Los filtros no funcionaran por falta de conexión a internet", "Aceptar");
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
             if (plantelesES.PlantelesES == null)
            {
                App.Current.Properties["institucion"] = 0;
            }
            else
            {
                App.Current.Properties["institucion"] = plantelesES.PlantelesES.idPlantelES;
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
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    while (municipiosViewModel.ListMunicipios.Count != 0)
                    {
                        var municipio = from a in municipiosViewModel.ListMunicipios where a.idEstado == 11 select a;
                        pMunicipio.ItemsSource = municipio.Cast<Municipios>().ToList();
                        pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");

                        pMunicipio.SelectedIndexChanged += SeleccionarMunicipio;

                        return false;
                    }
                    
                    return true;
                }
                catch (NullReferenceException ex)
                {
                    return true;
                }
            });
        }

        private void LlenarPlantelesES()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () => 
            {
                try
                {

                    while (plantelESViewModel.ListPlantelES.Count != 0)
                    {
                        FiltrarPlanteles(plantelESViewModel.ListPlantelES);
                        pPlantelesES.ItemDisplayBinding = new Binding("PlantelesES.NombrePlantelES");
                        pPlantelesES.SelectedIndexChanged += SeleccionarPlanteles;

                        return false;
                    }

                    return true;
                }
                catch (Exception ex)
                {
                    return true;
                }
            });
        }

        private void LlenarCarreras()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    while (carreraViewModel.ListCarreraES.Count != 0)
                    {
                        FiltrarCarreraras(carreraViewModel.ListCarreraES);
                        pCarreras.ItemDisplayBinding = new Binding("NombreCarreraES");
                        pCarreras.SelectedIndexChanged += SeleccionarCarrera;

                        return false;
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return true;
                    throw;
                }
            });
        }

        //private void LlenarEstados()
        //{
        //    Device.StartTimer(TimeSpan.FromSeconds(5), () =>
        //    {
        //        while (estadosViewModel.ListEstados.Count != 0)
        //        {
        //            pEstado.ItemsSource = estadosViewModel.ListEstados;
        //            pEstado.ItemDisplayBinding = new Binding("NombreEstado");
        //            pEstado.SelectedIndexChanged += SeleccionarEstado;
                   
        //            return false;
        //        }
        //        return true; // True = Repeat again, False = Stop the timer
        //    });
        //}

        //private void SeleccionarEstado(object sender, EventArgs e)
        //{
        //    estados = (Estados)pEstado.SelectedItem;
        //    LlenarMunicipios();
        //}

        private void SeleccionarMunicipio(object sender, EventArgs e)
        {
            municipios = (Municipios)pMunicipio.SelectedItem;
            LlenarPlantelesES();
        }

        private void SeleccionarPlanteles(object sender, EventArgs e)
        {
            plantelesES = (DetallePlantel)pPlantelesES.SelectedItem;
            LlenarCarreras();
        }

        private void SeleccionarCarrera(object sender, EventArgs e)
        {
            carrerasES = (CarrerasES)pCarreras.SelectedItem;
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

        public void FiltrarPlanteles(List<DetallePlantel> plantelesEs)
        {
            if (municipios.idEstado == 0)
            {
                pPlantelesES.ItemsSource = plantelesEs;
                return;
            }
            else
            {
                //var alumnosC = lstMunicipios.Where(a => a.IdEstado == 43);
                var planteles = from a in plantelesEs where a.PlantelesES.Municipio == municipios.idMunicipio select a;
                pPlantelesES.ItemsSource = planteles.Cast<DetallePlantel>().ToList();
                return;
            }
        }

        public void FiltrarCarreraras(List<CarrerasES> carreras)
        {
            if (plantelesES.PlantelesES == null || plantelesES.PlantelesES.idPlantelES == 0)
            {
                pCarreras.ItemsSource = carreras;
                return;
            }
            else
            {
                //var alumnosC = lstMunicipios.Where(a => a.IdEstado == 43);
                var carrera = from a in carreras where a.IdPlantelesES == plantelesES.PlantelesES.idPlantelES select a;
                pCarreras.ItemsSource = carrera.Cast<CarrerasES>().ToList(); ;
                return;
            }
        }
    }
}