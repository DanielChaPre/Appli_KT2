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
        private HttpClient _client;
        private ConexionWS conexion;
        private RootObjectPadreFamilia rootObject;
        private Estados _selectedEstado;
        private Municipios _selectedMunicipio;
        private Colonias _selectedColonia;
        MetodoHTTP metodosHTTP;
        private bool isrun;
        private bool isvisible;
        private bool isinsertar;
        private bool isacciones;
        private string sexo;
        private string plantelEMS;
        private string curphijo;
        private bool nuevo_registro;

        public PerfilPadreViewModel()
        {
            IsRun = true;
            IsVisible = false;
            IsInsertar = false;
            IsAcciones = false;
            Persona = new Persona();
        }

        public string CurpHijo
        {
            get { return this.curphijo; }
            set
            {
                curphijo = value;
                OnPropertyChanged();
            }
        }

        public bool IsRun
        {
            get { return this.isrun; }
            set
            {
                isrun = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get { return this.isvisible; }
            set
            {
                isvisible = value;
                OnPropertyChanged();
            }
        }

        public bool IsInsertar
        {
            get { return this.isinsertar; }
            set
            {
                isinsertar = value;
                OnPropertyChanged();
            }
        }

        public bool IsAcciones
        {
            get { return this.isacciones; }
            set
            {
                isacciones = value;
                OnPropertyChanged();
            }
        }

        public string PlantelEMS
        {
            get { return this.plantelEMS; }
            set
            {
                plantelEMS = value;
                OnPropertyChanged();
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
                ObtenerMunicipios();
                OnPropertyChanged();

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
            }
        }

        private void ObtenerMunicipios()
        {
            App.Current.Properties["NombreEstado"] = _selectedEstado.NombreEstado;
            MainViewModel.GetInstance().Municipio = new MunicipioViewModel();
        }

        #region Commandos

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

        public async void InsertarPerfil()
        {
            metodosHTTP = new MetodoHTTP();
            conexion = new ConexionWS();
            if (string.IsNullOrEmpty(Persona.Nombre) || string.IsNullOrEmpty(Persona.Apellido_Paterno) || string.IsNullOrEmpty(Persona.Apellido_Materno) || string.IsNullOrEmpty(Persona.Numero_Telefono))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Uno de los campos esta vacio, todos tienen que estar llenos", "Aceptar");
                return;
            }
            LlenarDatos();
            string json = JsonConvert.SerializeObject(rootObject);
            dynamic respuesta = metodosHTTP.ActualizarDatos(conexion.URL + conexion.CrearPadreFamilia, json);
            await ConsultarPadreFamilia();
            return;
        }

        private async void DesactivarPerfil()
        {
            try
            {
                metodosHTTP = new MetodoHTTP();
                conexion = new ConexionWS();
                LlenarDatos();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.Delete(conexion.URL + conexion.EliminarPadreFamilia, json);
                await ConsultarPadreFamilia();
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
                if (Persona == null || string.IsNullOrEmpty(Persona.Nombre) || string.IsNullOrEmpty(Persona.Apellido_Paterno) || string.IsNullOrEmpty(Persona.Apellido_Materno) || string.IsNullOrEmpty(Persona.Numero_Telefono))
                {
                    await Application.Current.MainPage.DisplayAlert("Alerta", "Uno de los campos esta vacio, todos tienen que estar llenos", "Aceptar");
                    return;
                }
                LlenarDatos();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.ActualizarDatos(conexion.URL + conexion.ModificarPadreFamilia, json, nuevo_registro);
                await ConsultarPadreFamilia();
                return;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Se a encontrado un error por favor comuniquese con el administrador", "Aceptar");
                return;
            }
        }

        private void LlenarDatos()
        {
            try
            {
                //BuscarAlumnoCurp();
                rootObject = new RootObjectPadreFamilia()
                {
                    padreFamilia = new PadreFamilia()
                    {

                       Cve_Padre_Familia = Convert.ToInt32(App.Current.Properties["cvePadreFamilia"].ToString()),
                       IdAlumno =  (int) App.Current.Properties["idAlumnoPadre"],
                       Fecha_Registro = "01/01/0001",
                       Persona = new Persona()
                       {
                           Cve_Persona = Convert.ToInt32(App.Current.Properties["cvePersona"].ToString()),
                           Nombre = Persona.Nombre,
                           Apellido_Paterno = Persona.Apellido_Paterno,
                           Apellido_Materno = Persona.Apellido_Materno,
                           RFC = "N/A",
                           CURP = "N/A",
                           Sexo = "Sin especificar",
                           Fecha_Nacimiento = "0001-01-01",
                           Numero_Telefono = Persona.Numero_Telefono,
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
                    }
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> ConsultarPadreFamilia()
        {
            IsRun = true;
            IsVisible = false;
            _client = new HttpClient();
            conexion = new ConexionWS();
            var usuario = App.Current.Properties["usuario"];
            var contrasena = App.Current.Properties["contrasena"];
            var url = conexion.URL + "" + conexion.ConsultarPadreFamilia + usuario + "/" + contrasena;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var padreFamilia = JsonConvert.DeserializeObject<PadreFamilia>(content);
                if (padreFamilia != null)
                {
                    Cve_Padre_Familia = padreFamilia.Cve_Padre_Familia;
                    IdAlumno = padreFamilia.IdAlumno;
                    Fecha_Registro = padreFamilia.Fecha_Registro;
                    Persona = new Persona()
                    {
                        Cve_Persona = padreFamilia.Persona.Cve_Persona,
                        Nombre = padreFamilia.Persona.Nombre,
                        Apellido_Paterno = padreFamilia.Persona.Apellido_Paterno,
                        Apellido_Materno = padreFamilia.Persona.Apellido_Materno,
                        RFC = padreFamilia.Persona.RFC,
                        CURP = padreFamilia.Persona.CURP,
                        Sexo = padreFamilia.Persona.Sexo,
                        Fecha_Nacimiento = padreFamilia.Persona.Fecha_Nacimiento,
                        Numero_Telefono = padreFamilia.Persona.Numero_Telefono,
                        Estado_Civil = padreFamilia.Persona.Estado_Civil,
                        Nacionalidad = padreFamilia.Persona.Nacionalidad,
                        IdColonia = padreFamilia.Persona.IdColonia,
                        Usuario = new Usuario()
                        {
                            Cve_Usuario = padreFamilia.Persona.Usuario.Cve_Usuario,
                            IdAlumno = padreFamilia.Persona.Usuario.IdAlumno,
                            Nombre_Usuario = padreFamilia.Persona.Usuario.Nombre_Usuario,
                            Contrasena = padreFamilia.Persona.Usuario.Contrasena,
                            Fecha_Registro = padreFamilia.Persona.Usuario.Fecha_Registro,
                            Estatus = padreFamilia.Persona.Usuario.Estatus,
                            Alias_Red = padreFamilia.Persona.Usuario.Alias_Red
                        }
                    };
                    App.Current.Properties["idAlumnoPadre"] = padreFamilia.IdAlumno;
                    App.Current.Properties["cveUsuario"] = padreFamilia.Persona.Usuario.Cve_Usuario;
                    App.Current.Properties["cvePersona"] = padreFamilia.Persona.Cve_Persona;
                    App.Current.Properties["cvePadreFamilia"] = padreFamilia.Cve_Padre_Familia;
                    App.Current.Properties["nombreUsuario"] = padreFamilia.Persona.Nombre + " " + padreFamilia.Persona.Apellido_Paterno;
                    IsRun = false;
                    IsVisible = true;
                    IsInsertar = false;
                    IsAcciones = true;
                    nuevo_registro = false;
                    return true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El usuario no cuenta con información, actualice su información", "Aceptar");
                    App.Current.Properties["cveUsuario"] = 0;
                    App.Current.Properties["cvePersona"] = 0;
                    App.Current.Properties["cvePadreFamilia"] = 0;
                    IsRun = false;
                    IsVisible = true;
                    IsInsertar = true;
                    IsAcciones = false;
                    nuevo_registro = true;
                    return false;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
                App.Current.Properties["cveUsuario"] = 0;
                App.Current.Properties["cvePersona"] = 0;
                IsRun = false;
                IsVisible = true;
                IsInsertar = true;
                IsAcciones = false;
                return false;
            }
        }

        public async Task<int> BuscarAlumnoCurp(string curphijo)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                var url = conexion.URL + "" + conexion.BuscarAlumnoCurp + curphijo;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var idAlumno = JsonConvert.DeserializeObject<int>(content);
                    App.Current.Properties["idAlumnoPadre"] = idAlumno;
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
        #endregion
    }

    public class RootObjectPadreFamilia
    {
        public PadreFamilia padreFamilia { get; set; }
    }
}
