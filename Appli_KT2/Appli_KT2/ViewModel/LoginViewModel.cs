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
              
                var user = Xamarin.Forms.Application.Current.Properties["usuario"].ToString();
                var id = Xamarin.Forms.Application.Current.Properties["idAlumno"];
                url = conexion.URL + "" + conexion.CrearCuenta + "" + user + "/" + this.password + "/" + id;
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
            else
            {
                url = conexion.URL + "" + conexion.ValidarContrasenia + "" + this.password;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(content);
                    if (result)
                    {
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
        }

        public void CrearUsuario()
        {

        }

        private async void IrCrearCuenta()
        {
            MainViewModel.GetInstance().CrearCuenta = new CreateCountViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new CreateAccountPage());
        }

        private void IniciarFacebook()
        {
            throw new NotImplementedException();
        }

        private void IniciarGoogle()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
