using Appli_KT2.Model;
using Appli_KT2.Utils;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class RegistrarViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<GrupoSeguridad> perfiles;
        private Usuario usuario;
        private string c;
        private string d;
        private string e;
        private string f;
        private string g;
        private HttpClient _client;
        private ConexionWS conexion;
        private string url;
        #endregion
        #region Propiedades
        public ObservableCollection<GrupoSeguridad> Perfiles
        {
            get { return this.perfiles; }

            set
            {
                SetValue(ref this.perfiles, value);
            }
        }

        public Usuario Usuario { get; set; }

        public string MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }
        public string MyProperty3 { get; set; }
        public string MyProperty4 { get; set; }
        public string MyProperty5 { get; set; }
        #endregion
        #region Constructor
        public RegistrarViewModel()
        {
            CargarPerfiles();
        }
        #endregion
        #region Comandos
        public ICommand RegistrarPerfilCommand
        {
            get
            {
                return new RelayCommand(RegistarPerfil);
            }
        }

        public ICommand ModificarPerfilCommand
        {
            get
            {
                return new RelayCommand(ModificarPerfil);
            }
        }
        public ICommand EliminarPerfilCommand
        {
            get
            {
                return new RelayCommand(EliminarPerfil);
            }
        }
        public ICommand ConsultarPerfilCommand
        {
            get
            {
                return new RelayCommand(ConsultarPerfil);
            }
        }

       
        #endregion
        #region Metodos
        private  ObservableCollection<GrupoSeguridad> CargarPerfiles()
        {
            try
            {
              this.perfiles = new ObservableCollection<GrupoSeguridad>
                {
                    new GrupoSeguridad(){nombre = "Estudiantes"},
                    new GrupoSeguridad(){nombre = "Planteles/Escuela"},
                    new GrupoSeguridad(){nombre = "Padres de familia"},
                    
                };

                return this.perfiles;
                /**Prueba con Web Services
                 * _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ObtenerGruposSeguridad;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    this.perfiles = JsonConvert.DeserializeObject<List<string>>(content);
                    return;
                }**/
            }
            catch (Exception ex)
            {
                
                Console.WriteLine(@"\t ERROR: ", ex.Message);
                return null;
                throw;
            }
        }
        private async void RegistarPerfil()
        {
            try
            {
                if (this.Usuario == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese informacion", "Accept");
                    return;
                }
               
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.CrearPerfil;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var json = JsonConvert.SerializeObject(this.Usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    // Debug.WriteLine(@"\tTodoItem successfully saved.");
                    Console.WriteLine(@"\t Cliente successfully saved.");
                    await Application.Current.MainPage.DisplayAlert("Error", @"\t La persona se guarda satisfactoriamente.", "Accept");
                    return;
                }
                
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", @"\t No se pudo guardar la persona", "Accept");
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", @"\t Error: "+ex.Message, "Accept");
                return;
            }
        }

        private async void ModificarPerfil()
        {
            try
            {
                if (this.Usuario == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese informacion", "Accept");
                    return;
                }

                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ModificarPerfil;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var json = JsonConvert.SerializeObject(this.Usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                response = await _client.PutAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    // Debug.WriteLine(@"\tTodoItem successfully saved.");
                    Console.WriteLine(@"\t Cliente successfully saved.");
                    await Application.Current.MainPage.DisplayAlert("Error", @"\t La persona se guarda satisfactoriamente.", "Accept");
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", @"\t No se pudo guardar la persona", "Accept");
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", @"\t Error: " + ex.Message, "Accept");
                return;
            }
        }

        private async void EliminarPerfil()
        {
            try
            {
                if (this.Usuario == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese informacion", "Accept");
                    return;
                }

                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ModificarPerfil;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var json = JsonConvert.SerializeObject(this.Usuario);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                //Cambiar los parametros en los web services para la eliminacion del perfil
                response = await _client.DeleteAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    // Debug.WriteLine(@"\tTodoItem successfully saved.");
                    Console.WriteLine(@"\t Cliente successfully saved.");
                    await Application.Current.MainPage.DisplayAlert("Error", @"\t La persona se guarda satisfactoriamente.", "Accept");
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", @"\t No se pudo guardar la persona", "Accept");
                    return;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", @"\t Error: " + ex.Message, "Accept");
                return;
            }
        }

        private void ConsultarPerfil()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
