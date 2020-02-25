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
    public partial class RelacionarUsuarioRedSocialPage : ContentPage
    {
        private RelacionarRedSocialViewModel relacionarRedSocialViewModel;
        public RelacionarUsuarioRedSocialPage()
        {
            InitializeComponent();
            btnAlumno.Clicked += AparecerAlumno;
            btnUsuario.Clicked += AparecerUsuario;
            btnGuardar.Clicked += RegistrarRedSocial;
            btnIngresarAlumno.Clicked += IngresarAlumno;
            btnIngresarUsuario.Clicked += IngresarUsuario;
            cargarAccion.IsVisible = false;
        }

        private async void IngresarUsuario(object sender, EventArgs e)
        {
            relacionarRedSocialViewModel = new RelacionarRedSocialViewModel();
            var usuarioF = App.Current.Properties["usuarioFacebook"].ToString();
            var usuarioG = App.Current.Properties["usuarioGoogle"].ToString();
            var usuario = txtUsuario.Text;
            var contrasena = txtContraseñaAlumno.Text;
            if (!usuarioF.Equals(""))
            {
                if (await relacionarRedSocialViewModel.RelacionarFacebookUsuario(usuarioF, usuario, contrasena))
                {
                    cargarAccion.IsVisible = false;
                    await Application.Current.MainPage.DisplayAlert("Exito", "Se relaciono la red social con su cuenta", "Aceptar");
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    cargarAccion.IsVisible = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo relacionar la red social con su cuenta", "Aceptar");
                }
            }
            else if (!usuarioG.Equals(""))
            {
                if (await relacionarRedSocialViewModel.RelacionarGoogleUsuario(usuarioG, usuario, contrasena))
                {
                    cargarAccion.IsVisible = false;
                    await Application.Current.MainPage.DisplayAlert("Exito", "Se relaciono la red social con su cuenta", "Aceptar");
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    cargarAccion.IsVisible = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo relacionar la red social con su cuenta", "Aceptar");
                }
            }
        }

        private async void IngresarAlumno(object sender, EventArgs e)
        {
            cargarAccion.IsVisible = true;
            relacionarRedSocialViewModel = new RelacionarRedSocialViewModel();
            var usuarioF = App.Current.Properties["usuarioFacebook"].ToString();
            var usuarioG = App.Current.Properties["usuarioGoogle"].ToString();
            var curp = txtAlumno.Text;
            var contrasena = txtContraseñaAlumno.Text;
            if (!usuarioF.Equals(""))
            {
                if (await relacionarRedSocialViewModel.RelacionarFacebookAlumno(usuarioF, curp, contrasena))
                {
                    cargarAccion.IsVisible = false;
                    await Application.Current.MainPage.DisplayAlert("Exito", "Se relaciono la red social con su cuenta", "Aceptar");
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    cargarAccion.IsVisible = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo relacionar la red social con su cuenta", "Aceptar");
                }
            }
            else if (!usuarioG.Equals(""))
            {
                if (await relacionarRedSocialViewModel.RelacionarGoogleAlumno(usuarioG, curp, contrasena))
                {
                    cargarAccion.IsVisible = false;
                    await Application.Current.MainPage.DisplayAlert("Exito", "Se relaciono la red social con su cuenta", "Aceptar");
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    cargarAccion.IsVisible = false;
                    await Application.Current.MainPage.DisplayAlert("Error", "No se pudo relacionar la red social con su cuenta", "Aceptar");
                }
            }
        }

        private async void RegistrarRedSocial(object sender, EventArgs e)
        {

            lytAlumno.IsVisible = false;
            lytUsuario.IsVisible = false;
            await Application.Current.MainPage.DisplayAlert("Prueba", "Prueba de Registro", "Aceptar");
        }

        private async void AparecerUsuario(object sender, EventArgs e)
        {
            lytAlumno.IsVisible = false;
            lytUsuario.IsVisible = true;
           
        }

        private async void AparecerAlumno(object sender, EventArgs e)
        {
            lytAlumno.IsVisible = true;
            lytUsuario.IsVisible = false;
        }
    }
}