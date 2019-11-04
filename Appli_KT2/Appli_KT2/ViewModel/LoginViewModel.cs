//using GalaSoft.MvvmLight.Command;
using Appli_KT2.Utils;
using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
namespace Appli_KT2.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region atributos
        private string usuario;
        private string password;
        private bool isRunning;
        private bool isEnable;
        #endregion
        ConexionWS conexion;
        HttpClient _client;
        private string url;
        #region propiedades
        public string User
        {
            get { return this.usuario; }

            set
            {
                SetValue(ref this.usuario, value);
            }
        }

        public string Password
        {

            get { return this.password; }
            set
            {
                SetValue(ref this.password, value);
            }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set
            {
                SetValue(ref this.isRunning, value);
            }
        }

        public bool IsRemember { get; set; }

        public bool IsEnable
        {
            get { return this.isEnable; }
            set
            {
                SetValue(ref this.isEnable, value);
            }
        }
        #endregion

        #region constructor
        public LoginViewModel()
        {
            this.IsRemember = true;
            this.IsEnable = true;
        }
        #endregion

        #region comandos
        public ICommand ValidarUsuarioCommand
        {
            get
            {
                return new RelayCommand(ValidarUsuario);
            }
        }

        public ICommand ValidarPasswordCommand
        {
            get
            {
                return new RelayCommand(ValidarContrasenia);
            }
        }

        private async void ValidarUsuario()
        {
            try
            {
                if (string.IsNullOrEmpty(this.User))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingresa el usuario", "Accept");
                    return;
                }
                this.IsRunning = true;
                this.IsEnable = false;
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ValidarUsuario + "" + this.usuario;
                var uri = new Uri(string.Format(@""+ url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    this.IsRunning = false;
                    this.IsEnable = true;
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new IniciarContraseniaPage());
                }     
                else
                {
                    this.IsRunning = false;
                    this.IsEnable = true;
                    await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                    return;
                 }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                throw;
            }
        }

        private async void ValidarContrasenia()
        {
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa la contraseña", "Accept");
                return;
            }
            this.IsRunning = true;
            this.IsEnable = false;
            _client = new HttpClient();
            conexion = new ConexionWS();
            url = conexion.URL + "" + conexion.ValidarContrasenia + "" + this.password;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                this.IsRunning = false;
                this.IsEnable = true;

                MainViewModel.GetInstance().Registrar = new RegistrarViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
            }
            else
            {
                this.IsRunning = false;
                this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                return;
            }
        }

        /*  
         *  
         *  private async void Login()
          {
              if (string.IsNullOrEmpty(this.usuario))
              {
                  await Application.Current.MainPage.DisplayAlert("Error","Ingresa el usuario","Accept");
                  return;
              }
              if (string.IsNullOrEmpty(this.password))
              {
                  await Application.Current.MainPage.DisplayAlert("Error","Ingresa el password","Accept");
                  return;
              }

              this.IsRunning = true;
              this.IsEnable = false;

              if (this.usuario != "admin" && this.password != "admin")
              {
                  this.IsRunning = false;
                  this.IsEnable = true;
                  await Application.Current.MainPage.DisplayAlert("Error", "usuario o password incorrecto", "Accept");
                  this.password = string.Empty;
                  return;
              }

              this.IsRunning = false;
              this.IsEnable = true;


              await Application.Current.MainPage.DisplayAlert("Ok", "Usuario encontrado", "Accept");
          }*/

        public ICommand registrarCommand { get; set; }
        #endregion
    }
}
