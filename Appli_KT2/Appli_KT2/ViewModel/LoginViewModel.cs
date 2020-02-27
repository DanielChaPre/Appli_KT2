//using GalaSoft.MvvmLight.Command;
using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.FacebookClient.Abstractions;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
//ñusing Plugin.FacebookClient.Abstractions;
//using Plugin.GoogleClient;
//using Plugin.GoogleClient.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
        private bool alumnoEncontrado = false;
       
        private int idAlumno;

        ConexionWS conexion;
        HttpClient _client;
        private string url;
        private string confirmarcontrasena;


        #endregion
        #region propiedades
        private  IGoogleClientManager _googleClientManager;
        public UserProfile User1 { get; set; } = new UserProfile();
        public string Name
        {
            get => User1.Name;
            set => User1.Name = value;
        }

        public string Email
        {
            get => User1.Email;
            set => User1.Email = value;
        }

        public Uri PictureU
        {
            get => User1.Picture;
            set => User1.Picture = value;
        }

        public bool IsLoggedIn { get; set; }

		public string Token { get; set; }

        public string ConfirmarContrasena
        {
            get { return this.confirmarcontrasena; }

            set
            {
                SetValue(ref this.confirmarcontrasena, value);
            }
        }

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
            App.Current.Properties["usuarioFacebook"] = "";
            App.Current.Properties["usuarioGoogle"] = "";

            IsLoggedIn = false;
        }
      
        #endregion
        #region comandos

        public ICommand CrearCuentaAlumnoCommand
        {
            get
            {
                return new RelayCommand(CrearCuentaAlumno);
            }
        }

        public ICommand RecuperarCommand
        {
            get
            {
                return new RelayCommand(Recuperar);
            }
        }

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

        public ICommand IrCrearCuentaCommand
        {
            get
            {
                return new RelayCommand(IrCrearCuenta);
            }
        }

        public ICommand IniciarFacebookCommand
        {
            get
            {
                return new RelayCommand(IniciarFacebook);
            }
        }

        public ICommand IniciarGoogleCommand
        {
            get
            {
                return new RelayCommand(IniciarGoogle);
            }
        }

        public ICommand IniciarSesionCommand
        {
            get
            {
                return new RelayCommand(IniciarSesion);
            }
        }

        private async void Recuperar()
        {
            MainViewModel.GetInstance().Recuperar = new RecuperarContrasenaViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RecuperarContrasenaPage());
        }

        private async void IniciarSesion()
        {
            MainViewModel.GetInstance().Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new IniciarUsuarioPage());
        }

        private async void ValidarUsuario()
        {
            this.IsRunning = true;
            this.IsEnable = false;
            if (ValidarAlumno())
            {
                BuscarAlumno();
            }
            else
            {
                ValidarUsuarios();
            }
        }

        private async void ValidarUsuarios()
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
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var login = JsonConvert.DeserializeObject<bool>(content);
                    if (login)
                    {
                        this.IsRunning = false;
                        this.IsEnable = true;
                        MainViewModel.GetInstance().Login = new LoginViewModel();
                        await Application.Current.MainPage.Navigation.PushAsync(new IniciarContraseniaPage());
                        Xamarin.Forms.Application.Current.Properties["usuario"] = this.usuario;
                        Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = false;
                    }
                    else
                    {
                        ValidarAlumno();
                    }
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

        private async void ValidarUsuarioAlumno(int idAlumno)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();

                url = conexion.URL + "" + conexion.ValidarUsuarioAlumno + "" + this.idAlumno;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var login = JsonConvert.DeserializeObject<bool>(content);
                    if (login)
                    {
                        this.IsRunning = false;
                        this.IsEnable = true;
                        MainViewModel.GetInstance().Login = new LoginViewModel();
                        await Application.Current.MainPage.Navigation.PushAsync(new IniciarContraseniaPage());
                        Xamarin.Forms.Application.Current.Properties["idAlumno"] = idAlumno;
                        Xamarin.Forms.Application.Current.Properties["usuario"] = this.usuario;
                        Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = true;
                        return;
                    }
                    else
                    {
                        this.IsRunning = false;
                        this.IsEnable = true;
                        MainViewModel.GetInstance().Login = new LoginViewModel();
                        await Application.Current.MainPage.Navigation.PushAsync(new CrearContrasenaAlumnoPAge());
                        Xamarin.Forms.Application.Current.Properties["idAlumno"] = idAlumno;
                        Xamarin.Forms.Application.Current.Properties["usuario"] = this.usuario;
                        Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = true;
                        return;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        private async void BuscarAlumno()
        {
            _client = new HttpClient();
            conexion = new ConexionWS();

            url = conexion.URL + "" + conexion.BuscarAlumnoCurp + "" + this.usuario;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                idAlumno = JsonConvert.DeserializeObject<int>(content);
                if (idAlumno == 0)
                {
                    this.IsRunning = false;
                    this.IsEnable = true;
                    await Application.Current.MainPage.DisplayAlert("Error", "El alumno no se encontro en la base de datos de SUREDSU", "Aceptar");
                    Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = false;
                    return;
                }
               
                ValidarUsuarioAlumno(idAlumno);
                //alumnoEncontrado = true;
                //Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = alumnoEncontrado;
                //this.IsRunning = false;
                //this.IsEnable = true;
                //MainViewModel.GetInstance().Login = new LoginViewModel();
                //if (alumnoEncontrado)
                //{
                //    await Application.Current.MainPage.Navigation.PushAsync(new CrearContrasenaAlumnoPAge());
                //}
                //else
                //    await Application.Current.MainPage.Navigation.PushAsync(new IniciarContraseniaPage());
                //return;
            }
            else
            {
                this.IsRunning = false;
                this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert("Error", ""+response.StatusCode, "Aceptar");
                Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = false;
                return;
            }
        }

        private bool ValidarAlumno()
        {
            if (this.usuario.Length == 18)
            {
                if (!ValidarCurp(this.usuario.ToUpper()))
                {
                    return false;
                }
                else
                {
                    return true;
                    //BuscarAlumno();
                }
                // return true;
            }
            else
                return false;
        }

        public bool ValidarCurp(string curp)
        {
            try
            {
                string regex =
                "^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}" +
                "(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])" +
                "[HM]{1}" +
                "(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)" +
                "[B-DF-HJ-NP-TV-Z]{3}" +
                "[0-9A-Z]{1}[0-9]{1}$";
                if (Regex.IsMatch(curp, @regex))
                {
                    Thread.Sleep(6000);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
                throw;
            }
        }

        private async void ValidarContrasenia()
        {
            _client = new HttpClient();
            conexion = new ConexionWS();
            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa la contraseña", "Accept");
                return;
            }
            Xamarin.Forms.Application.Current.Properties["contrasena"] = this.password;
            var encontrado = Convert.ToBoolean(Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"].ToString());
            if (encontrado)
            {
                if (await VerificarRegistroAlumno())
                {
                    var id = Xamarin.Forms.Application.Current.Properties["idAlumno"];
                    VerificarContrasenaAlumno(" ", id.ToString());
                }
                else
                {
                    CrearCuentaAlumno();
                }
            }
            else
            {
                VerificarContrasena(App.Current.Properties["usuario"].ToString(), "0");
            }
        }

        /**
       * Este método solo se utilizara para poder consultar el nomobre del alumno o de la persona 
       * **/

        public async void CrearCuentaAlumno()
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                var user = Xamarin.Forms.Application.Current.Properties["usuario"].ToString();
                var id = Xamarin.Forms.Application.Current.Properties["idAlumno"];
                App.Current.Properties["tipo_usuario"] = 2;
                url = conexion.URL + "" + conexion.CrearCuenta + " " + "/" + this.password + "/" + id + "/" + "2";
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(content);
                    if (result)
                    {
                        Application.Current.MainPage = new NavigationPage(new MainPage());
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "El usuario no se pudo guardar de manera correcta en la tabla", "Accept");
                        return;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El usuario no se pudo guardar de manera correcta en la tabla", "Accept");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async void VerificarContrasena(string usuario, string idAlumno)
        {
            url = conexion.URL + "" + conexion.ValidarContrasenia + this.password + "/" + usuario + "/" + idAlumno;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<string>>(content);
                if (result.Count != 0)
                {
                    var tipo = result[0];
                    var cveUsuario = result[1];
                    string nombre = result[2];
                    string apePaterno = result[3];

                    App.Current.Properties["tipo_usuario"] = tipo;
                    App.Current.Properties["cveUsuario"] = cveUsuario;
                    App.Current.Properties["nombreUsuario"] = nombre + " " + apePaterno;
                    this.IsRunning = false;
                    this.IsEnable = true;
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    this.IsRunning = false;
                    this.IsEnable = true;
                    await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                    return;
                }
            }
            else
            {
                this.IsRunning = false;
                this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                return;
            }
        }

        public async void VerificarContrasenaAlumno(string usuario, string idAlumno)
        {
            url = conexion.URL + "" + conexion.ValidarContrasenaAlumno + this.password + "/" + usuario + "/" + idAlumno;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<List<string>>(content);
                if (result.Count != 0)
                {
                    var tipo = result[0];
                    var cveUsuario = result[1];
                    var nombre = result[2];
                    var apePaterno = result[3];
                    App.Current.Properties["tipo_usuario"] = tipo;
                    App.Current.Properties["cveUsuario"] = cveUsuario;
                    App.Current.Properties["nombreUsuario"] = nombre + " " + apePaterno;
                    this.IsRunning = false;
                    this.IsEnable = true;
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    this.IsRunning = false;
                    this.IsEnable = true;
                    await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                    return;
                }
            }
            else
            {
                this.IsRunning = false;
                this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                return;
            }
        }

        public async Task<bool> VerificarRegistroAlumno()
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                var id = Xamarin.Forms.Application.Current.Properties["idAlumno"];
                url = conexion.URL + "" + conexion.VerificarRegistroAlumno + this.password + "/" + id;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(content);
                    if (result)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error" + response.StatusCode, "Accept");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error" + ex.Message, "Accept");
                return false;
            }
        }

        private async void IrCrearCuenta()
        {
            MainViewModel.GetInstance().CrearCuenta = new CreateCountViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CreateAccountPage());
        }

        private async void IniciarFacebook()
        {
            try
            {
                IFacebookClient _facebookService = CrossFacebookClient.Current;
                if (_facebookService.IsLoggedIn)
                {
                    _facebookService.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;
                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                            //facebookProfile  es el que tiene los datos de la cuenta de facebook
                            App.Current.Properties["usuarioFacebook"] = facebookProfile.Id;
                            if (await ValidarUsuarioFacebook(facebookProfile.Id))
                            {
                                Application.Current.MainPage = new NavigationPage(new MainPage());
                            }
                            else
                            {
                                //Application.Current.MainPage = new NavigationPage(new MainPage());
                                await Application.Current.MainPage.Navigation.PushModalAsync(new RelacionarUsuarioRedSocialPage());
                            }
                            break;
                        case FacebookActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Canceled", "Ok");
                            break;
                        case FacebookActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Error", "Ok");
                            break;
                        case FacebookActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Facebook Auth", "Unauthorized", "Ok");
                            break;
                    }
                    _facebookService.OnUserData -= userDataDelegate;
                };
                _facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "picture", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await _facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        /*
         * Configurar tanto el facebook como el google en android e ios para poder utilizarlos de manera adecuada
         * */
        private async void IniciarGoogle()
        {
            try
            {
                _googleClientManager = CrossGoogleClient.Current;
            }
            catch (Exception ex)
            {

                throw;
            }
           
            _googleClientManager.OnLogin += OnLoginCompleted;
            try
            {
                await _googleClientManager.LoginAsync();
            }
            catch (GoogleClientSignInNetworkErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInCanceledErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInInvalidAccountErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientSignInInternalErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientNotInitializedErrorException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
            catch (GoogleClientBaseException e)
            {
                await App.Current.MainPage.DisplayAlert("Error", e.Message, "OK");
            }
        }

        private async void OnLoginCompleted(object sender, GoogleClientResultEventArgs<GoogleUser> loginEventArgs)
        {
            if (loginEventArgs.Data != null)
            {
                GoogleUser googleUser = loginEventArgs.Data;
                User1.Name = googleUser.Name;
                User1.Email = googleUser.Email;
                User1.Picture = googleUser.Picture;
                var GivenName = googleUser.GivenName;
                var FamilyName = googleUser.FamilyName;
                App.Current.Properties["usuarioGoogle"] = googleUser.Id;
                IsLoggedIn = true;
                var token = CrossGoogleClient.Current.ActiveToken;
                Token = token;
                if (await ValidarUsuarioGoogle(googleUser.Id))
                {
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    await Application.Current.MainPage.Navigation.PushModalAsync(new RelacionarUsuarioRedSocialPage());
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", loginEventArgs.Message, "OK");
            }

            _googleClientManager.OnLogin -= OnLoginCompleted;

        }

        public void Logout()
        {
            _googleClientManager.OnLogout += OnLogoutCompleted;
            _googleClientManager.Logout();
        }

        private void OnLogoutCompleted(object sender, EventArgs loginEventArgs)
        {
            IsLoggedIn = false;
            User1.Email = "Offline";
            _googleClientManager.OnLogout -= OnLogoutCompleted;
        }

        private async Task<bool> ValidarUsuarioFacebook(string perfil)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ValidarUsuarioFacebook + perfil;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<string>>(content);
                    if (result.Count != 0)
                    {
                        var tipo = result[0];
                        var cveUsuario = result[1];
                        var idAlumno = result[2];
                        var nombre = result[3];
                        var apePaterno = result[4];
                        App.Current.Properties["tipo_usuario"] = tipo;
                        App.Current.Properties["cveUsuario"] = cveUsuario;
                        App.Current.Properties["idAlumno"] = idAlumno;
                        App.Current.Properties["nombreUsuario"] = nombre + " " + apePaterno;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error" + response.StatusCode, "Accept");
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<bool> ValidarUsuarioGoogle(string perfil)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ValidarUsuarioGoogle + perfil;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<string>>(content);
                    if (result.Count != 0)
                    {
                        var tipo = result[0];
                        var cveUsuario = result[1];
                        var idAlumno = result[2];
                        var nombre = result[3];
                        var apePaterno = result[4];
                        App.Current.Properties["tipo_usuario"] = tipo;
                        App.Current.Properties["cveUsuario"] = cveUsuario;
                        App.Current.Properties["idAlumno"] = idAlumno;
                        App.Current.Properties["nombreUsuario"] = nombre + " " + apePaterno;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error" + response.StatusCode, "Accept");
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        #endregion
        internal class NetworkAuthData
        {
            public string Id { get; set; }
            public string Name { get; set; }
            public string Logo { get; set; }
            public string Picture { get; set; }
            public string Background { get; set; }
            public string Foreground { get; set; }
        }

        public class Data
        {
            [JsonProperty("is_silhouette")]
            public bool IsSilhouette { get; set; }
            public int Height { get; set; }
            public string Url { get; set; }  
            public int Width { get; set; }
        }

        public class Picture
        {
            public Data Data { get; set; }
        }

        public class FacebookProfile
        {
            public string Email { get; set; }
            public string Id { get; set; }
            public Picture Picture { get; set; }
            [JsonProperty("last_name")]
            public string LastName { get; set; }
            [JsonProperty("first_name")]
            public string FirstName { get; set; }
            [JsonProperty("user_id")]
            public int UserId { get; set; }
        }

        public class UserProfile : INotifyPropertyChanged
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public Uri Picture { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;
        }
        
    }
}
