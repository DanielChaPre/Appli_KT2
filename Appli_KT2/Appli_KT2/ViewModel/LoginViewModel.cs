//using GalaSoft.MvvmLight.Command;
using Appli_KT2.Utils;
using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using Plugin.GoogleClient;
using Plugin.GoogleClient.Shared;
using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Http;
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
        #endregion
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
                    await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                    Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = false;
                    return;
                }
                Xamarin.Forms.Application.Current.Properties["idAlumno"] = idAlumno;
                Xamarin.Forms.Application.Current.Properties["usuario"] = this.usuario;
                Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = true;
                this.IsRunning = false;
                this.IsEnable = true;
                MainViewModel.GetInstance().Login = new LoginViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new IniciarContraseniaPage());
                return;
            }
            else
            {
                this.IsRunning = false;
                this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = false;
                return;
            }
        }

        private void ValidarAlumno()
        {
            if (this.usuario.Length == 18)
            {
                if (!ValidarCurp(this.usuario.ToUpper()))
                {
                    return;
                }
                else
                {
                    BuscarAlumno();
                }
                // return true;
            }
            else
                return;
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
                    VerificarContrasena(" ", id.ToString());
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
        //public async Task<Alumno> ConsultarAlumno(string idAlum)
        //{

        //}

        public async void CrearCuentaAlumno()
        {
            try
            {
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
            catch (Exception)
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
                var result = JsonConvert.DeserializeObject<int>(content);
                if (result != 0)
                {
                    App.Current.Properties["tipo_usuario"] = result;
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

        public void CrearUsuario()
        {

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
                            //var socialLoginData = new NetworkAuthData
                            //{
                            //    Id = facebookProfile.Id,
                            //    Logo = authNetwork.Icon,
                            //    Foreground = authNetwork.Foreground,
                            //    Background = authNetwork.Background,
                            //    Picture = facebookProfile.Picture.Data.Url,
                            //    Name = $"{facebookProfile.FirstName} {facebookProfile.LastName}",
                            //};
                            await App.Current.MainPage.Navigation.PushModalAsync(new MainPage());
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

        private async void IniciarGoogle()
        {
           
            try
            {
                CrossGoogleClient.Current.Logout();
                IGoogleClientManager _googleService = CrossGoogleClient.Current;
                if (!string.IsNullOrEmpty(_googleService.ActiveToken))
                {
                    //Always require user authentication
                    _googleService.Logout();
                }

                EventHandler<GoogleClientResultEventArgs<GoogleUser>> userLoginDelegate = null;
                userLoginDelegate = async (object sender, GoogleClientResultEventArgs<GoogleUser> e) =>
                {
                    switch (e.Status)
                    {
                        case GoogleActionStatus.Completed:
                            #if DEBUG
                            var googleUserString = JsonConvert.SerializeObject(e.Data);
                            Console.WriteLine($"Google Logged in succesfully: {googleUserString}");
                            #endif

                            //var socialLoginData = new NetworkAuthData
                            //{
                            //    Id = e.Data.Id,
                            //    Logo = authNetwork.Icon,
                            //    Foreground = authNetwork.Foreground,
                            //    Background = authNetwork.Background,
                            //    Picture = e.Data.Picture.AbsoluteUri,
                            //    Name = e.Data.Name,
                            //};

                            await App.Current.MainPage.Navigation.PushModalAsync(new MainPage());
                            break;
                        case GoogleActionStatus.Canceled:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Canceled", "Ok");
                            break;
                        case GoogleActionStatus.Error:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Error", "Ok");
                            break;
                        case GoogleActionStatus.Unauthorized:
                            await App.Current.MainPage.DisplayAlert("Google Auth", "Unauthorized", "Ok");
                            break;
                    }

                    _googleService.OnLogin -= userLoginDelegate;
                };

                _googleService.OnLogin += userLoginDelegate;

                await _googleService.LoginAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
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
    }
}
