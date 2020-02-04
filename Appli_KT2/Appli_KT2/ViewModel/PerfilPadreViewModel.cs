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
        private PadreFamilia padreFamilia;
        private Estados _selectedEstado;
        private Municipios _selectedMunicipio;
        private Colonia _selectedColonia;
        private Estados entEstados;
        MetodoHTTP metodosHTTP;
        private bool isrun;
        private bool isvisible;
        private bool isinsertar;
        private bool isacciones;
        private string sexo;
        private string plantelEMS;
        private string curphijo;

        public PerfilPadreViewModel()
        {
            IsRun = true;
            IsVisible = false;
            IsInsertar = false;
            IsAcciones = false;
        }

        public string CurpHijo
        {
            get { return this.curphijo; }
            set
            {
                SetValue(ref this.curphijo, value);
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

        public bool IsVisible
        {
            get { return this.isvisible; }
            set
            {
                SetValue(ref this.isvisible, value);
            }
        }

        public bool IsInsertar
        {
            get { return this.isinsertar; }
            set
            {
                SetValue(ref this.isinsertar, value);
            }
        }

        public bool IsAcciones
        {
            get { return this.isacciones; }
            set
            {
                SetValue(ref this.isacciones, value);
            }
        }

        public string Sexo
        {
            get { return this.sexo; }
            set
            {
                SetValue(ref this.sexo, value);
            }
        }

        public string PlantelEMS
        {
            get { return this.plantelEMS; }
            set
            {
                SetValue(ref this.plantelEMS, value);
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
            }
        }

        private void ObtenerMunicipios()
        {
            App.Current.Properties["NombreEstado"] = _selectedEstado.NombreEstado;
            MainViewModel.GetInstance().Municipio = new MunicipioViewModel();
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

        public async void InsertarPerfil()
        {
            metodosHTTP = new MetodoHTTP();
            conexion = new ConexionWS();
            LlenarDatos();
            string json = JsonConvert.SerializeObject(rootObject);
            dynamic respuesta = metodosHTTP.Post(conexion.URL + conexion.CrearPadreFamilia, json);
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
                LlenarDatos();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.Put(conexion.URL + conexion.ModificarPadreFamilia, json);
                await ConsultarPadreFamilia();
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
                BuscarAlumnoCurp();
                rootObject = new RootObjectPadreFamilia()
                {
                    padreFamilia = new PadreFamilia()
                    {
                       
                       CVE_Padre_Familia = Convert.ToInt32(App.Current.Properties["cvePadreFamilia"].ToString()),
                       IdAlumno =  (int) App.Current.Properties["idAlumnoPadre"],
                       Fecha_Registro = Fecha_Registro,
                       Persona = new Persona()
                       {
                           Cve_Persona = Convert.ToInt32(App.Current.Properties["cvePersona"].ToString()),
                           Nombre = Nombre,
                           Apellido_Paterno = Apellido_Paterno,
                           Apellido_Materno = Apellido_Materno,
                           RFC = "N/A",
                           CURP = "N/A",
                           Sexo = "Sin especificar",
                           Fecha_Nacimiento = "01/01/0001",
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
                    CVE_Padre_Familia = padreFamilia.CVE_Padre_Familia;
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
                    App.Current.Properties["cveUsuario"] = persona.Usuario.Cve_Usuario;
                    App.Current.Properties["cvePersona"] = persona.Cve_Persona;
                    App.Current.Properties["nombreUsuario"] = persona.Nombre + " " + persona.Apellido_Paterno;
                    IsRun = false;
                    IsVisible = true;
                    IsInsertar = false;
                    IsAcciones = true;
                    return true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El usuario no cuenta con información, actualice su información", "Aceptar");
                    App.Current.Properties["cveUsuario"] = 0;
                    App.Current.Properties["cvePersona"] = 0;
                    IsRun = false;
                    IsVisible = true;
                    IsInsertar = true;
                    IsAcciones = false;
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

        public async void BuscarAlumnoCurp()
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                var url = conexion.URL + "" + conexion.BuscarAlumnoCurp + CurpHijo;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var idAlumno = JsonConvert.DeserializeObject<int>(content);
                    App.Current.Properties["idAlumnoPadre"] = idAlumno;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private void SeleccionarSexo(string sexo)
        {
            switch (sexo)
            {
                case "H":
                    Sexo = "Hombre";
                    break;
                case "M":
                    Sexo = "Mujer";
                    break;
                default:
                    Sexo = "Indistinto";
                    break;
            }
        }
        #endregion
    }

    public class RootObjectPadreFamilia
    {
        public PadreFamilia padreFamilia { get; set; }
    }
}
