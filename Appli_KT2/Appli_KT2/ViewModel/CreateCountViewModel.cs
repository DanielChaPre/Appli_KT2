using Appli_KT2.Utils;
using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class CreateCountViewModel : BaseViewModel
    {
        #region atributos

        private string usuario;
        private string contrasenia;
        private HttpClient _client;
        public ConexionWS conexion;
        private string url;
        private int idAlumno;

        #endregion

        #region propiedades
        public string Usuario
        {
            get { return this.usuario; }
            set
            {
                SetValue(ref this.usuario, value);
            }
        }

        public string Contrasenia
        {
            get { return this.contrasenia; }
            set
            {
                SetValue(ref this.contrasenia, value);
            }
        }
        #endregion

        #region Comandos
        public ICommand CrearCuentaCommand
        {
            get
            {
                return new RelayCommand(VerificarInternet);
            }
        }

        #endregion

        #region Métodos
        private async void CrearCuenta()
        {
            // MainViewModel.GetInstance().CrearCuenta = new CreateCountViewModel();
            await Application.Current.MainPage.DisplayAlert("Aviso", "Se esta creando la cuenta, espere un momento.", "Aceptar");
            if (string.IsNullOrEmpty(this.usuario))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese el usuario", "Aceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.contrasenia))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese la contraseña", "Aceptar");
                return;
            }

            if (this.usuario.Length == 18)
            {
                if (!ValidarCurp(this.usuario.ToUpper()))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Si se intento ingresar la curp, esta incorrecta", "Aceptar");
                    return;
                }
                else
                {
                    if (this.contrasenia.Length < 8)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "La longitud de la contraseña no puede ser menor a 8 caracteres", "Aceptar");
                        return;
                    }
                    else
                    {
                        if (await VerificarRegistroAlumno())
                        {
                            await Application.Current.MainPage.DisplayAlert("Aviso", "El usuario ya esta registrado", "Aceptar");
                            return;
                        }
                        else
                        {
                            if (await BuscarAlumno())
                                return;
                            RegistrarAlumno();
                        }
                    }
                }
            }
            else
            {
                if (this.contrasenia.Length < 8)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "La longitud de la contraseña no puede ser menor a 8 caracteres", "Aceptar");
                    return;
                }
                else
                {
                    RegistrarPerfil();
                }

            }
                //Xamarin.Forms.Application.Current.Properties["usuario"] = this.usuario;
            #region comentario
            /*
             */
            #endregion
        }

        public void ValidarContraseña(string contraseña)
        {
            Random obj = new Random();
            //Generamos 3 arrays con los distintos caracteres
            string carNormales = "abcdefghijklmnoupqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string carNum = "0123456789";
            string carEsp = "%$#?¿@";
            string pass = contraseña; //Aquí vamos a guardar el pass;
            int norm = 0;  //Contador de caracteres normales
            int esp = 0;   //Contador de car especiales
            int num = 0;   //Contador de car mayusculas

            for (int i = 0; i < pass.Length; i++) //Ponemos hasta 10 porque es la longitud del pass
            {
                int arr = obj.Next(0, 3); //Generamos un valor aleatorio para ver en que array vamos a mirar

                if (arr == 0)
                {
                    if (norm < 4)
                    {
                        pass = pass + carNormales[obj.Next(0, 53)].ToString(); //Seleccionamos un caracter de este array
                        norm = norm + 1;
                    }
                }
                else if (arr == 1)
                {
                    if (num < 4)
                    {
                        pass = pass + carNum[obj.Next(0, 10)].ToString(); //Seleccionamos un caracter de este array
                        num = num + 1;
                    }
                }
                else
                {
                    if (esp < 2)
                    {
                        pass = pass + carEsp[obj.Next(0, 5)].ToString(); //Seleccionamos un caracter de este array
                        esp = esp + 1;
                    }
                }
            }
        }

        private async Task<bool> BuscarAlumno()
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
                if (idAlumno != 0)
                {
                    await Application.Current.MainPage.DisplayAlert("Aviso", "La curp coincide con una curp existente, inicie sesión de manera normal con una contraseña", "Aceptar");
                    Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = false;
                    return true;
                }
                await Application.Current.MainPage.DisplayAlert("Error", "La curp no coincide con ningun registro existente ", "Aceptar");
                Xamarin.Forms.Application.Current.Properties["usuario"] = this.usuario;
                Xamarin.Forms.Application.Current.Properties["contrasena"] = this.contrasenia;
               // MainViewModel.GetInstance().RegistrarA = new PerfilAlumnoViewModel();
                await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
                return false;

            }
            else
            {
                //await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                Xamarin.Forms.Application.Current.Properties["alumnoEncontrado"] = false;
                return false;
            }
        }

        public async Task<bool> VerificarRegistroAlumno()
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                var id = Xamarin.Forms.Application.Current.Properties["idAlumno"];
                url = conexion.URL + "" + conexion.VerificarRegistroAlumno + this.contrasenia+ "/" + id;
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
                    await Application.Current.MainPage.DisplayAlert("Error", "Error" + response.StatusCode, "Aceptar");
                    return false;
                }

            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error" + ex.Message, "Aceptar");
                return false;
            }
        }

        public async void RegistrarAlumno()
        {
            var id = Xamarin.Forms.Application.Current.Properties["idAlumno"];
            url = conexion.URL + "" + conexion.CrearCuenta + "" + this.usuario + "/" + this.contrasenia + "/" + id + "/" + "2";
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(content);
                if (result)
                {
                    Xamarin.Forms.Application.Current.Properties["usuario"] = this.usuario;
                    Xamarin.Forms.Application.Current.Properties["contrasena"] = this.contrasenia;
                   // MainViewModel.GetInstance().RegistrarA = new PerfilAlumnoViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
                
                    //Application.Current.MainPage = new NavigationPage(new MainPage());
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

        public async void RegistrarPerfil()
        {
            _client = new HttpClient();
            conexion = new ConexionWS();
            url = conexion.URL + "" + conexion.CrearCuenta + "" + this.usuario + "/" + this.contrasenia + "/" + "0" + "/" + "1";
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<bool>(content);
                if (result)
                {
                    App.Current.Properties["tipo_usuario"] = 1;
                    App.Current.Properties["usuario"] = this.usuario;
                    App.Current.Properties["contrasena"] = this.contrasenia;
                    await Application.Current.MainPage.DisplayAlert("Exito", "El usuario se guardo de manera exitosa", "Aceptar");
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El usuario no se pudo guardar de manera correcta en la tabla", "Aceptar");
                    return;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "El usuario no se pudo guardar de manera correcta en la tabla", "Aceptar");
                return;
            }
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

        private void VerificarInternet()
        {
            var status = 1;
            ConexionInternet conexionInternet = new ConexionInternet();
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {

                    if (conexionInternet.VerificarInternet())
                    {
                        if (status == 1)
                        {
                            CrearCuenta();
                            status = 0;
                        }
                        return true;
                    }
                    else
                    {
                        status = 1;
                        Application.Current.MainPage.DisplayAlert("Notificación", "La creación de la cuenta fallara debido a la falta de conexión a internet", "Aceptar");
                    }
                    return true;
                }
                catch (NullReferenceException ex)
                {
                    return true;
                }
            });
        }
        #endregion
    }
}

