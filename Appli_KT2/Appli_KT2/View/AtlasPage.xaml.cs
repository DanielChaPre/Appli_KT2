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
	public partial class AtlasPage : ContentPage
	{
        MunicipioViewModel municipiosViewModel;
        PlantelESViewModel plantelESViewModel;

        public AtlasPage ()
		{
			InitializeComponent ();
            VerificarUsuario();
            btnBuscar.Clicked += BuscarAtlas;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            LlenarFiltros();
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
            var usuario = (int)App.Current.Properties["tipo_usuario"];

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
            catch (Exception)
            {

            }
        }

        private async void BuscarAtlas(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ResultadoAtlasPage());
        }
   
        private void LlenarFiltros()
        {
            LlenarMunicipios();
            LlenarPlantelesES();
            //LlenarCarreras();
        }

        private void LlenarMunicipios()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    if (municipiosViewModel == null)
                    {
                        municipiosViewModel = new MunicipioViewModel();
                        municipiosViewModel.ObtenerTodosMunicipios();
                    }
                    else
                    {
                       
                        if (municipiosViewModel.ListMunicipios != null || municipiosViewModel.ListMunicipios.Count != 0)
                        {
                            pMunicipio.ItemsSource = municipiosViewModel.ListMunicipios;
                            pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");
                            return false;
                        }
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

        private void LlenarPlantelesES()
        {
            Device.StartTimer(TimeSpan.FromSeconds(5), () => 
            {
                try
                {
                    if (plantelESViewModel == null)
                    {
                        plantelESViewModel = new PlantelESViewModel();
                        plantelESViewModel.ObtenerPlantelES();
                    }
                    else
                    {
                        if (plantelESViewModel.ListPlantelES != null || plantelESViewModel.ListPlantelES.Count != 0)
                        {
                            pPlantelesES.ItemsSource = plantelESViewModel.ListPlantelES;
                            pPlantelesES.ItemDisplayBinding = new Binding("NombreInstitucionES");
                            return false;
                        }
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

        private void LlenarCarreras()
        {

        }
    }
}