using Appli_KT2.Model;
using Appli_KT2.Utils;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
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
        [JsonProperty("persona")]
        private Persona entpersona;
        private entpersona objeto;
        private Estados _selectedEstado;
        private Municipios _selectedMunicipio;
        private Colonia _selectedColonia;
        private Estados entEstados;
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

        public Colonia SelectedColonia
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

        private  void ObtenerMunicipios()
        {
            MainViewModel.GetInstance().Municipio = new MunicipioViewModel(this._selectedEstado.NombreEstado);
        }

        #region Commandos

        public ICommand InsertarPerfilCommand
        {
            get
            {
                return new RelayCommand(InsertarPerfil);
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

        private async void InsertarPerfil2()
        {
            try
            {
             
                  conexion = new ConexionWS();
                  var client = new HttpClient();
                    client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                  var content = new StringContent(
                      JsonConvert.SerializeObject(new { Nombre = Nombre, Apellido_Paterno = Apellido_Paterno, Apellido_Materno = Apellido_Materno, Numero_Telefono = Numero_Telefono }));
                  client.Timeout = TimeSpan.FromMilliseconds(2000);
                  var result = await client.PostAsync(conexion.URL + conexion.CrearPerfil, content).ConfigureAwait(false);
                  if (result.IsSuccessStatusCode)
                  {
                      var tokenJson = await result.Content.ReadAsStringAsync();
                  }
                  else
                  {
                      //   response.Content;
                      Console.WriteLine("Error: " + result.ReasonPhrase);
                      Console.WriteLine("Error: " + result.Content);

                      await Application.Current.MainPage.DisplayAlert("Error", @"\t No se pudo guardar la persona " + result.Content.ToString(), "Accept");
                      return;
                  }
            }
            catch (HttpRequestException ex)
            {

                throw;
            }
         
        }

        private async void InsertarPerfil()
        {
            try
            {
                LlenarDatos();

                //if (this.objeto == null)
                if (this.entpersona == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese información", "Aceptar");
                    return;
                }
                _client = new HttpClient();
                conexion = new ConexionWS();
                _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json; charset=utf-8");
                var uri = new Uri(string.Format(@"" + conexion.URL + conexion.CrearPerfil, string.Empty));
              //  var json = JsonConvert.SerializeObject(this.objeto);
                var json = JsonConvert.SerializeObject(entpersona);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _client.PostAsync(uri, content);

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
        }

        private void DesactivarPerfil()
        {
        }

        private async void ActualizarPerfil()
        {
            try
            {
                LlenarDatos();
                if (this.entpersona == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese información", "Aceptar");
                    return;
                }
                _client = new HttpClient();
                conexion = new ConexionWS();
                var  url = conexion.URL + "" + conexion.ModificarPerfil;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var json = JsonConvert.SerializeObject(this.entpersona);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    var cont = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<bool>(cont);
                    if (result)
                    {
                        Console.WriteLine(@"\t .");
                        await Application.Current.MainPage.DisplayAlert("Error", @"\t El usuario se actualizo satisfactoriamente.", "Aceptar");
                        await ConsultarUsuarioGeneral();
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
                    await Application.Current.MainPage.DisplayAlert("Error", @"\t No se pudo guardar la persona "+response.StatusCode, "Accept");
                    return;
                }

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
                //objeto = new entpersona()
                //{
                //    persona = new Persona()
                //    {
                //        Cve_Persona = Convert.ToInt32(App.Current.Properties["cvePersona"].ToString()),
                //        Nombre = Nombre,
                //        Apellido_Paterno = Apellido_Paterno,
                //        Apellido_Materno = Apellido_Materno,
                //        RFC = "N/A",
                //        CURP = "N/A",
                //        Sexo = "Sin especificar",
                //        Fecha_Nacimiento = Fecha_Nacimiento,
                //        Numero_Telefono = Numero_Telefono,
                //        Estado_Civil = 0,
                //        Nacionalidad = "N/A",
                //        Municipio = "N/A",
                //        IdColonia = 0,
                //        Usuario = new Usuario()
                //        {
                //            Cve_Usuario = Convert.ToInt32(App.Current.Properties["cveUsuario"].ToString()),
                //            IdAlumno = 0,
                //            Nombre_Usuario = App.Current.Properties["usuario"].ToString(),
                //            Contrasena = App.Current.Properties["contrasena"].ToString(),
                //            Estatus = "Activo",
                //            Alias_Red = "N/A"
                //        }
                //    }
                //};
                entpersona = new Persona()
                {
                    Cve_Persona = Convert.ToInt32(App.Current.Properties["cvePersona"].ToString()),
                    Nombre = Nombre,
                    Apellido_Paterno = Apellido_Paterno,
                    Apellido_Materno = Apellido_Materno,
                    RFC = "N/A",
                    CURP = "N/A",
                    Sexo = "Sin especificar",
                    Fecha_Nacimiento = Fecha_Nacimiento,
                    Numero_Telefono = Numero_Telefono,
                    Estado_Civil = 0,
                    Nacionalidad = "N/A",
                    Municipio = "N/A",
                    IdColonia = 0,
                    Usuario = new Usuario()
                    {
                        Cve_Usuario = Convert.ToInt32(App.Current.Properties["cveUsuario"].ToString()),
                        IdAlumno = 0,
                        Nombre_Usuario = App.Current.Properties["usuario"].ToString(),
                        Contrasena = App.Current.Properties["contrasena"].ToString(),
                        Estatus = "Activo",
                        Alias_Red = "N/A"
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
                        return true;
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "El usuario no cuenta con información, actualice su información", "Aceptar");
                        App.Current.Properties["cveUsuario"] = 0;
                        App.Current.Properties["cvePersona"] = 0;
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

    public class entpersona
    {
       // [JsonProperty("entpersona")]
        public Persona persona { get; set; }
    }
}
