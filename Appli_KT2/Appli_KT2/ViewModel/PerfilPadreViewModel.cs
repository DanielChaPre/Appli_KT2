using Appli_KT2.Model;
using Appli_KT2.Utils;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class PerfilPadreViewModel : PadreFamilia
    {
        #region Atributos
        private List<Estados> lstEstados = new List<Estados>();
        private ObservableCollection<GrupoSeguridad> perfiles;
        private Persona persona;
        private PadreFamilia padreFamilia;
        private Alumno alumno;
        private bool isalumno;
        private bool ispadre;
        private HttpClient _client;
        private ConexionWS conexion;
        private string url;
        private string codigoPostal;
        private string estado;
        private string estadoSeleccionado;
        private Estados _selectedEstado;
        public Estados SelectedEstado
        {
            get
            {
                return _selectedEstado;
            }
            set
            {
                SetValue(ref _selectedEstado, value);
                //put here your code  
                estadoSeleccionado = "City : " + _selectedEstado.NombreEstado;
            }
        }
        #endregion

        #region Propiedades

        public  Estados[] ListEstados
        {
            get;
            set;
        }

        public string Estado
        {
            get { return this.estado; }
            set
            {
                SetValue(ref this.estado, value);
            }
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
            //CargarPerfiles();
        //    ObtenerEstados();
            this.IsPadre = true;
            this.IsAlumno = false;
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

        #region Alumno
        public ICommand RegistrarAlumnoCommand
        {
            get
            {
                return new RelayCommand(RegistarAlumno);
            }
        }

        public ICommand ModificarAlumnoCommand
        {
            get
            {
                return new RelayCommand(ModificarAlumno);
            }
        }

        public ICommand EliminarAlumnoCommand
        {
            get
            {
                return new RelayCommand(EliminarAlumno);
            }
        }

        public ICommand ConsultarAlumnoCommand
        {
            get
            {
                return new RelayCommand(ConsultarAlumno);
            }
        }
        #endregion

        #endregion

        #region Metodos
        private void RegistarAlumno()
        {
            
        }

        private void ConsultarAlumno()
        {
            
        }

        private void EliminarAlumno()
        {
           
        }

        private void ModificarAlumno()
        {
            
        }

        private async void ObtenerEstados()
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ObtenerEstados;
                var request = HttpWebRequest.Create(string.Format(@""+url, string.Empty));
                request.ContentType = "application/json";
                request.Method = "GET";

                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        Console.Out.WriteLine("Error fetching data. Server returned status code: {0}", response.StatusCode);
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        var content = reader.ReadToEnd();
                        if (string.IsNullOrWhiteSpace(content))
                        {
                            Console.Out.WriteLine("Response contained empty body...");
                        }
                        else
                        {
                            Console.Out.WriteLine("Response Body: \r\n {0}", content);
                        }

                        var estado = JsonConvert.DeserializeObject<Estados>(content);
                    }
                }

                //_client = new HttpClient();
                //conexion = new ConexionWS();
                //// url = conexion.URL + "" + conexion.ValidarUsuario + "" + this.usuario;
                //url = conexion.URL + "" + conexion.ObtenerEstados;
                //var uri = new Uri(string.Format(@"" + url, string.Empty));
                //var response = await _client.GetAsync(uri);

                //if (response.IsSuccessStatusCode)
                //{
                //    var content = await response.Content.ReadAsStringAsync();
                //    List<Estados> lista = JsonConvert.DeserializeObject<List<Estados>>(content.);
                //}
            }
            catch (JsonSerializationException ex)
            {
                throw;
            }
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
                         UsuarioP = new Usuario()
                        {
                            IdAlumno = 0,
                            Nombre_Usuario = Nombre_Usuario,
                            Contrasena = Contrasena,
                            Fecha_Registro = Convert.ToDateTime(DateTime.Now.ToString("yyyy/MM/")),
                            Estatus = "Activo",
                        //    Alias_Red = 
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
