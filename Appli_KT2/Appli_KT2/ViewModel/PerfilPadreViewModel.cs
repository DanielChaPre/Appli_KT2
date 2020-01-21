using Appli_KT2.Model;
using Appli_KT2.Utils;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class PerfilPadreViewModel : PadreFamilia
    {
        #region Atributos
       
        private ObservableCollection<GrupoSeguridad> perfiles;
        private Persona persona;
        private PadreFamilia padreFamilia;
        private Alumno alumno;
        private bool isalumno;
        private bool ispadre;
        private bool isrun;
        private HttpClient _client;
        private ConexionWS conexion;
        private string url;
        private string codigoPostal;
        private List<string> lstNombreEstados = new List<string>();
        private List<Estados> lstEstados = new List<Estados>();
        private List<Municipios> lstMunicipios = new List<Municipios>();
        private List<Estados> listEstados;
        private Estados _selectedEstado;
        private Municipios _selectedMunicipio;
        private Colonia _selectedColonia;
        private Estados entEstados;

        #endregion

        #region Propiedades

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

        public List<Estados> ListEstados
        {
            get;
            set;
        }

        public List<Municipios> ListMunicipios
        {
            get;
            set;
        }

        public string CodigoPostal
        {
            get { return this.codigoPostal; }
            set
            {
                SetValue(ref this.codigoPostal, value);
            }
        }

        public ObservableCollection<GrupoSeguridad> Perfiles
        {
            get { return this.perfiles; }
            set
            {
                SetValue(ref this.perfiles, value);
            }
        }

        public Persona PersonaR {

            get { return this.persona; }
            set
            {
                SetValue(ref this.persona, value);
            }
        }

        public bool IsRun
        {
            get { return this.isrun; }
            set
            {
                SetValue(ref this.isrun, value);
            }
        }

        public bool IsAlumno
        {
            get { return this.isalumno; }
            set
            {
                SetValue(ref this.isalumno, value);
            }
        }

        public bool IsPadre
        {
            get { return this.ispadre; }
            set
            {
                SetValue(ref this.ispadre, value);
            }
        }
        #endregion

        #region Constructor
        public  PerfilPadreViewModel()
        {
            ObtenerEstados();
            
            this.IsPadre = true;
            this.IsAlumno = false;
            this.IsRun = false;
        }
        #endregion

        #region Comandos
        #region PadreFamilia
        public ICommand RegistrarPerfilCommand
        {
            get
            {
                return new RelayCommand(RegistarPerfilPadre);
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


        #endregion

        #region Metodos
        private  void ObtenerEstados()
        {
             MainViewModel.GetInstance().Estados = new EstadosViewModel();
        }

        private async void ObtenerMunicipios()
        {
            MainViewModel.GetInstance().Municipio = new MunicipioViewModel(this._selectedEstado.NombreEstado);
        }

        private ObservableCollection<GrupoSeguridad> CargarPerfiles()
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

        private async Task<int> BuscarAlumnoCurp(string curp)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
               // url = conexion.URL + "" + conexion.ValidarUsuario + "" + this.usuario;
                url = conexion.URL + "" + conexion.BuscarAlumnoCurp+""+curp;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    int idAlumno = JsonConvert.DeserializeObject<int>(content);
                    return idAlumno;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private async Task<int> BuscarColonia(string colonia)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                // url = conexion.URL + "" + conexion.ValidarUsuario + "" + this.usuario;
                url = conexion.URL + "" + conexion.BuscarColonia + "" + colonia;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    int idColonia = JsonConvert.DeserializeObject<int>(content);
                    return idColonia;
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        private async void RegistarPerfilPadre()
        {
            try
            {
                int idAlumno = await BuscarAlumnoCurp(CURP);
              // checar que pasaria el nombre de la colonia  int idColonia = await BuscarColonia();
                padreFamilia = new PadreFamilia() {
                    IdAlumno = idAlumno,
                    Fecha_Registro = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/")),
                     PersonaP = new Persona()
                    {
                        Nombre = Nombre,
                        Apellido_Paterno = Apellido_Paterno,
                        Apellido_Materno = Apellido_Materno,
                        RFC = RFC,
                        CURP = CURP,
                        Sexo = Sexo,
                        Fecha_Nacimiento = Fecha_Nacimiento,
                        Numero_Telefono = Numero_Telefono,
                        Estado_Civil = Estado_Civil,
                        Nacionalidad = Nacionalidad,
                        Municipio = Municipio,
                         // IdColonia = ,
                         Usuario = new Usuario()
                        {
                            IdAlumno = 0,
                            Nombre_Usuario = Usuario.Nombre_Usuario,
                            Contrasena = Usuario.Contrasena,
                            Estatus = "Activo",
                        }
                    },
                };
                
                if (this.persona == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese información", "Accept");
                    return;
                }
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.CrearPerfil;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var json = JsonConvert.SerializeObject(this.padreFamilia);
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
                if (this.PersonaR == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese informacion", "Accept");
                    return;
                }

                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ModificarPerfil;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var json = JsonConvert.SerializeObject(this.PersonaR);
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
                if (this.PersonaR == null)
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Ingrese informacion", "Accept");
                    return;
                }

                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ModificarPerfil;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var json = JsonConvert.SerializeObject(this.PersonaR);
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
