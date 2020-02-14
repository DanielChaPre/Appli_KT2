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
            IsRun = true;
            IsVisible = false;
            metodosHTTP = new MetodoHTTP();
            conexion = new ConexionWS();
            LlenarDatos();
            string json = JsonConvert.SerializeObject(rootObject);
            dynamic respuesta = metodosHTTP.Post(conexion.URL + conexion.CrearEmpleadoPlantel, json);
            await Application.Current.MainPage.DisplayAlert("Exito", "Se a guadado la información de manera correcta", "Aceptar");
            await ConsultarEmpleadoPlantel();
            return;
        }

        private async void DesactivarPerfil()
        {
            try
            {
                IsRun = true;
                IsVisible = false;
                metodosHTTP = new MetodoHTTP();
                conexion = new ConexionWS();
                LlenarDatos();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.Delete(conexion.URL + conexion.EliminarEmpleadoPlantel, json);
                await Application.Current.MainPage.DisplayAlert("Exito", "Se a eliminado la información de manera correcta", "Aceptar");
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
                IsRun = true;
                IsVisible = false;
                metodosHTTP = new MetodoHTTP();
                conexion = new ConexionWS();
                LlenarDatos();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.Put(conexion.URL + conexion.ModificarEmpleadoPlantel, json);
                await Application.Current.MainPage.DisplayAlert("Exito", "Se a actualizado la información de manera correcta", "Aceptar");
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
                        Tipo = 1,
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
                            Fecha_Nacimiento = "01/01/0001",
                            Numero_Telefono = Persona.Numero_Telefono,
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
            IsRun = true;
            IsVisible = false;
            _client = new HttpClient();
            conexion = new ConexionWS();
            var usuario = App.Current.Properties["usuario"];
            var contrasena = App.Current.Properties["contrasena"];
            var url = conexion.URL + "" + conexion.ConsultarEmpleadoPlantel + usuario + "/" + contrasena;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var empleadoPlantel = JsonConvert.DeserializeObject<EmpleadoPlantel>(content);
                if (empleadoPlantel != null)
                {
                    Tipo = empleadoPlantel.Tipo;
                   
                    Persona = new Persona() {
                        Nombre = empleadoPlantel.Persona.Nombre,
                        Apellido_Paterno = empleadoPlantel.Persona.Apellido_Paterno,
                        Apellido_Materno = empleadoPlantel.Persona.Apellido_Materno,
                        Numero_Telefono = empleadoPlantel.Persona.Numero_Telefono,
                     };
                     App.Current.Properties["tipoEmpleadoP"] = empleadoPlantel.Tipo;
                    App.Current.Properties["cveUsuario"] = empleadoPlantel.Persona.Usuario.Cve_Usuario;
                    App.Current.Properties["cvePersona"] = empleadoPlantel.Persona.Cve_Persona;
                    App.Current.Properties["cveEmpleadoPersona"] = empleadoPlantel.Cve_Empleado_Plantel;
                    App.Current.Properties["nombreUsuario"] = empleadoPlantel.Persona.Nombre + " " + empleadoPlantel.Persona.Apellido_Paterno;
                    IsRun = false;
                    IsVisible = true;
                    IsAcciones = true;
                    IsInsertar = false;
                    return true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El usuario no cuenta con información, actualice su información", "Aceptar");
                    App.Current.Properties["cveUsuario"] = 0;
                    App.Current.Properties["cvePersona"] = 0;
                    App.Current.Properties["cveEmpleadoPersona"] = 0;
                    IsRun = false;
                    IsVisible = true;
                    IsAcciones = false;
                    IsInsertar = true;
                    return false;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
                App.Current.Properties["cveUsuario"] = 0;
                App.Current.Properties["cvePersona"] = 0;
                App.Current.Properties["cveEmpleadoPersona"] = 0;
                IsRun = false;
                IsVisible = true;
                IsAcciones = false;
                IsInsertar = true;
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
                    Persona.Sexo = "Hombre";
                    break;
                case "M":
                    Persona.Sexo = "Mujer";
                    break;
                default:
                    Persona.Sexo = "Indistinto";
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
