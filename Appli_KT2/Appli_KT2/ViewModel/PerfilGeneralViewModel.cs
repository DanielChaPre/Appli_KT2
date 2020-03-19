using Appli_KT2.Model;
using Appli_KT2.Utils;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class PerfilGeneralViewModel : Persona
    {
        private HttpClient _client;
        private ConexionWS conexion;
        private Persona entpersona;
        private RootObject rootObject;
        private Entpersona objeto;
        private Estados _selectedEstado;
        private Municipios _selectedMunicipio;
        private Colonias _selectedColonia;
        private Estados entEstados;
        MetodoHTTP metodosHTTP;
        private bool nuevo_registro;
        private bool isrun;

        public bool IsRun
        {
            get { return this.isrun; }
            set
            {
                SetValue(ref this.isrun, value);
            }
        }

        public Estados SelectedEstado
        {
            get
            {
                return _selectedEstado;
            }
            set
            {
                _selectedEstado = value;
                OnPropertyChanged();
                //put here your code  
                //estadoSeleccionado = "City : " + _selectedEstado.NombreEstado;
                Console.WriteLine("Estado recogido:" + _selectedEstado.NombreEstado);
                ObtenerMunicipios();
            }
        }

        public Colonias SelectedColonia
        {
            get
            {
                return _selectedColonia;
            }
            set
            {
                _selectedColonia = value;
                OnPropertyChanged();
            }
        }

        public Municipios SelectedMunicipio
        {
            get
            {
                return _selectedMunicipio;
            }
            set
            {
                _selectedMunicipio = value;
                OnPropertyChanged();
                //put here your code  
                //estadoSeleccionado = "City : " + _selectedEstado.NombreEstado;
                Console.WriteLine("Municipio recogido:" + _selectedEstado.NombreEstado);
            }
        }

        private async void ObtenerMunicipios()
        {
            MainViewModel.GetInstance().Municipio = new MunicipioViewModel();
        }

        #region Commandos

        public ICommand InsertarPerfilCommand
        {
            get
            {
               // return new RelayCommand(InsertarPerfil);
                return new RelayCommand(InsertarPersona);
            }
        }

        public ICommand ActualizarPerfilCommand
        {
            get
            {
                return new RelayCommand(ActualizarPerfil);
            }
        }

        public ICommand DesactivarPerfilCommand
        {
            get
            {
                return new RelayCommand(DesactivarPerfil);
            }
        }

        public async void InsertarPersona()
        {
            metodosHTTP = new MetodoHTTP();
            conexion = new ConexionWS();
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido_Paterno) || string.IsNullOrEmpty(Apellido_Materno) || string.IsNullOrEmpty(Numero_Telefono))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Uno de los campos esta vacio, todos tienen que estar llenos", "Aceptar");
                return;
            }
            LlenarDatos();
            string json = JsonConvert.SerializeObject(rootObject);
            dynamic respuesta = metodosHTTP.ActualizarDatos(conexion.URL + conexion.CrearPerfil,json);
            await ConsultarUsuarioGeneral();
            return;
        }

      /*  private async void InsertarPerfil()
        {
            try
            {
                LlenarDatos();

               if (this.rootObject == null)
              //  if (this.entpersona == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese información", "Aceptar");
                    return;
                }
                _client = new HttpClient();
                conexion = new ConexionWS();
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var uri = new Uri(string.Format(@"" + conexion.URL + conexion.CrearPerfil, string.Empty));
                var json = JsonConvert.SerializeObject(this.rootObject);
               // var json = JsonConvert.SerializeObject(entpersona);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uri, content).ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    var cont = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<int>>(cont);
                    if (result.Count != 0 || result != null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", @"\t El usuario se actualizo satisfactoriamente.", "Aceptar");
                        App.Current.Properties["cveUsuario"] = result[0].ToString();
                        App.Current.Properties["cvePersona"] = result[1].ToString();
                        return;
                    }
                    else
                    {
                        Console.WriteLine(@"\t Cliente successfully saved.");
                        await Application.Current.MainPage.DisplayAlert("Error", @"\t El usuario no se actualizo de manera satisfactoria.", "Aceptar");
                        return;
                    }
                    // Debug.WriteLine(@"\tTodoItem successfully saved.");

                }
                else
                {
                 //   response.Content;
                    await Application.Current.MainPage.DisplayAlert("Error", @"\t No se pudo guardar la persona " + response.RequestMessage, "Accept");
                    return;
                } 
            }
            catch (System.NullReferenceException ex)
            {

                throw;
            }
        }*/

        private async void DesactivarPerfil()
        {
            try
            {
                metodosHTTP = new MetodoHTTP();
                conexion = new ConexionWS();
                LlenarDatos();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.Delete(conexion.URL + conexion.EliminarPerfil, json);
                await ConsultarUsuarioGeneral();
                return;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", @"\t Error: " + ex.Message, "Aceptar");
                return;
            }
        }

        private async void ActualizarPerfil()
        {
            try
            {
                metodosHTTP = new MetodoHTTP();
                conexion = new ConexionWS();
                if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido_Paterno) || string.IsNullOrEmpty(Apellido_Materno) || string.IsNullOrEmpty(Numero_Telefono))
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Uno de los campos esta vacio, todos tienen que estar llenos", "Aceptar");
                    return;
                }
                LlenarDatos();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.ActualizarDatos(conexion.URL + conexion.ModificarPerfil, json, nuevo_registro);
                await ConsultarUsuarioGeneral();
                return;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", @"\t Error: " + ex.Message, "Aceptar");
                return;
            }
        }

        private void LlenarDatos()
        {
            try
            {
                rootObject = new RootObject()
                {
                    entpersona = new Entpersona()
                    {
                        Cve_Persona = Convert.ToInt32(App.Current.Properties["cvePersona"].ToString()),
                        Nombre = Nombre,
                        Apellido_Paterno = Apellido_Paterno,
                        Apellido_Materno = Apellido_Materno,
                        RFC = "N/A",
                        CURP = "N/A",
                        Sexo = "Sin especificar",
                        Fecha_Nacimiento = "01-01-0001",
                        Numero_Telefono = Numero_Telefono,
                        Estado_Civil = 0,
                        Nacionalidad = "N/A",
                        Municipio = "1",
                        IdColonia = 0,
                        Usuario = new Usuario()
                        {
                            Cve_Usuario = Convert.ToInt32(App.Current.Properties["cveUsuario"].ToString()),
                            IdAlumno = 0,
                            Nombre_Usuario = App.Current.Properties["usuario"].ToString(),
                            Contrasena = App.Current.Properties["contrasena"].ToString(),
                            Estatus = "Activo",
                            Alias_Red = "N/A",
                            Fecha_Registro = "01/01/0001",
                            Tipo_Usuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString()),
                            Ruta_Imagen = "N/A"
                        }
                    }
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> ConsultarUsuarioGeneral()
        {
            try
            {
                //formularioGeneral.IsVisible = false;
                //actCargaForm.IsRunning = true;
                _client = new HttpClient();
                conexion = new ConexionWS();
                var usuario = App.Current.Properties["usuario"];
                var contrasena = App.Current.Properties["contrasena"];
                var url = conexion.URL + "" + conexion.ConsultarPerfil + usuario + "/" + contrasena;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var persona = JsonConvert.DeserializeObject<Persona>(content);
                    if (persona != null)
                    {
                        Cve_Persona = persona.Cve_Persona;
                        Nombre = persona.Nombre; 
                        Apellido_Paterno = persona.Apellido_Paterno;
                        Apellido_Materno = persona.Apellido_Materno;
                        Numero_Telefono = persona.Numero_Telefono;
                        App.Current.Properties["cveUsuario"] = persona.Usuario.Cve_Usuario;
                        App.Current.Properties["cvePersona"] = persona.Cve_Persona;
                        App.Current.Properties["nombreUsuario"] = persona.Nombre + " " + persona.Apellido_Paterno;
                        nuevo_registro = false;
                        return true;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "El usuario no cuenta con información, actualice su información", "Aceptar");
                        App.Current.Properties["cveUsuario"] = 0;
                        App.Current.Properties["cvePersona"] = 0;
                        nuevo_registro = true;
                        return false;
                    }

                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
                return false;
            }

        }

        #endregion
    }

    public class Entpersona
    {
        public string Nombre { get; set; }
        public int Cve_Persona { get; set; }
        public string Apellido_Paterno { get; set; }
        public string Apellido_Materno { get; set; }
        public string RFC { get; set; }
        public string CURP { get; set; }
        public string Sexo { get; set; }
        public string Fecha_Nacimiento { get; set; }
        public string Numero_Telefono { get; set; }
        public int Estado_Civil { get; set; }
        public string Nacionalidad { get; set; }
        public string Municipio { get; set; }
        public int IdColonia { get; set; }
        public Usuario Usuario { get; set; }
    }

    public class RootObject
    {
        public Entpersona entpersona { get; set; }
    }
}
