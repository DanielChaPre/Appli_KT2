using Appli_KT2.Model;
using Appli_KT2.Utils;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class PerfilEmpleadoPlantelViewModel : EmpleadoPlantel
    {
        private HttpClient _client;
        private ConexionWS conexion;
        private RootObjectEmpleadoPlantel rootObject;
        private EmpleadoPlantel empleadoPlantel;
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

        public PerfilEmpleadoPlantelViewModel()
        {
            IsRun = true;
            IsVisible = false;
            IsInsertar = false;
            IsAcciones = false;
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
            dynamic respuesta = metodosHTTP.Post(conexion.URL + conexion.CrearEmpleadoPlantel, json);
            await ConsultarEmpleadoPlantel();
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
                dynamic respuesta = metodosHTTP.Delete(conexion.URL + conexion.EliminarEmpleadoPlantel, json);
                await ConsultarEmpleadoPlantel();
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
                dynamic respuesta = metodosHTTP.Put(conexion.URL + conexion.ModificarEmpleadoPlantel, json);
                await ConsultarEmpleadoPlantel();
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
                rootObject = new RootObjectEmpleadoPlantel()
                {
                    empleadoPlantel = new EmpleadoPlantel()
                    {
                        Cve_Empleado_Plantel = Cve_Empleado_Plantel,
                        IdPlantelesES = IdPlantelesES,
                        Tipo = Tipo,
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

        public async Task<bool> ConsultarEmpleadoPlantel()
        {
            _client = new HttpClient();
            conexion = new ConexionWS();
            var usuario = App.Current.Properties["usuario"];
            var contrasena = App.Current.Properties["contrasenia"];
            var url = conexion.URL + "" + conexion.ConsultarEmpleadoPlantel + usuario + "/" + contrasena;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var empleadoPlantel = JsonConvert.DeserializeObject<EmpleadoPlantel>(content);
                if (empleadoPlantel != null)
                {
                    Cve_Empleado_Plantel = empleadoPlantel.Cve_Empleado_Plantel;
                    IdPlantelesES = empleadoPlantel.IdPlantelesES;
                    Tipo = empleadoPlantel.Tipo;
                    Fecha_Registro = empleadoPlantel.Fecha_Registro;
                    Persona = new Persona()
                    {
                        Cve_Persona = empleadoPlantel.Persona.Cve_Persona,
                        Nombre = empleadoPlantel.Persona.Nombre,
                        Apellido_Paterno = empleadoPlantel.Persona.Apellido_Paterno,
                        Apellido_Materno = empleadoPlantel.Persona.Apellido_Materno,
                        RFC = empleadoPlantel.Persona.RFC,
                        CURP = empleadoPlantel.Persona.CURP,
                        Sexo = empleadoPlantel.Persona.Sexo,
                        Fecha_Nacimiento = empleadoPlantel.Persona.Fecha_Nacimiento,
                        Numero_Telefono = empleadoPlantel.Persona.Numero_Telefono,
                        Estado_Civil = empleadoPlantel.Persona.Estado_Civil,
                        Nacionalidad = empleadoPlantel.Persona.Nacionalidad,
                        IdColonia = empleadoPlantel.Persona.IdColonia,
                        Usuario = new Usuario()
                        {
                            Cve_Usuario = empleadoPlantel.Persona.Usuario.Cve_Usuario,
                            IdAlumno = empleadoPlantel.Persona.Usuario.IdAlumno,
                            Nombre_Usuario = empleadoPlantel.Persona.Usuario.Nombre_Usuario,
                            Contrasena = empleadoPlantel.Persona.Usuario.Contrasena,
                            Fecha_Registro = empleadoPlantel.Persona.Usuario.Fecha_Registro,
                            Estatus = empleadoPlantel.Persona.Usuario.Estatus,
                            Alias_Red = empleadoPlantel.Persona.Usuario.Alias_Red
                        }
                    };
                    App.Current.Properties["cveUsuario"] = empleadoPlantel.Persona.Usuario.Cve_Usuario;
                    App.Current.Properties["cvePersona"] = empleadoPlantel.Persona.Cve_Persona;
                    App.Current.Properties["nombreUsuario"] = empleadoPlantel.Persona.Nombre+" "+empleadoPlantel.Persona.Apellido_Paterno;
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

        private async Task<bool> ConsultarPlantelEMS(int idEMS)
        {
            _client = new HttpClient();
            conexion = new ConexionWS();
            var idAlumno = App.Current.Properties["idAlumno"];
            var url = conexion.URL + "" + conexion.ObtenerPlantelEMS + idEMS;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var entPlantel = JsonConvert.DeserializeObject<PlantelesEMS>(content);
                if (entPlantel != null)
                {
                    PlantelEMS = entPlantel.NombrePlantelEMS;
                    return true;
                }
                else
                    return false;
            }
            else
                return false;
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


    public class RootObjectEmpleadoPlantel
    {
        public EmpleadoPlantel empleadoPlantel { get; set; }
    }
}
