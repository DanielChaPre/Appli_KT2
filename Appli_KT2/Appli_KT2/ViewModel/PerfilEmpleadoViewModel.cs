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
    public class PerfilEmpleadoViewModel : Empleado
    {
        private HttpClient _client;
        private ConexionWS conexion;
        private RootObjectEmpleado rootObject;
        private Empleado empleado;
        private Estados _selectedEstado;
        private Municipios _selectedMunicipio;
        private Colonias _selectedColonia;
        private Estados entEstados;
        MetodoHTTP metodosHTTP;
        private bool isrun;
        private bool isvisible;
        private bool isinsertar;
        private bool isacciones;
        private string sexo;
        private string plantelEMS;
        private bool nuevo_registro;

        public PerfilEmpleadoViewModel()
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

        public string Sexo
        {
            get { return this.sexo; }
            set
            {
                sexo = value;
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
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Apellido_Paterno) || string.IsNullOrEmpty(Apellido_Materno) || string.IsNullOrEmpty(Numero_Telefono))
            {
                await Application.Current.MainPage.DisplayAlert("Alerta", "Uno de los campos esta vacio, todos tienen que estar llenos", "Aceptar");
                return;
            }
            LlenarDatos();
            string json = JsonConvert.SerializeObject(rootObject);
            dynamic respuesta = metodosHTTP.ActualizarDatos(conexion.URL + conexion.CrearEmpleado, json);
            await ConsultarEmpleado();
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
                dynamic respuesta = metodosHTTP.Delete(conexion.URL + conexion.EliminarEmpleado, json);
                await ConsultarEmpleado();
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
                dynamic respuesta = metodosHTTP.ActualizarDatos(conexion.URL + conexion.ModificarEmpleado, json, nuevo_registro);
                await ConsultarEmpleado();
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
                var nombre = Nombre;

                rootObject = new RootObjectEmpleado()
                {
                    empleado = new Empleado()
                    {
                      Cve_Empleado = Convert.ToInt32(App.Current.Properties["cveEmpleado"].ToString()),
                      Numero_Empleado = App.Current.Properties["numeroEmpleado"].ToString(),
                      Estatus = "Activo",
                      Fecha_Registro = "01/01/0001",
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

        public async Task<bool> ConsultarEmpleado()
        {
            IsRun = true;
            IsVisible = false;
            _client = new HttpClient();
            conexion = new ConexionWS();
            var usuario = App.Current.Properties["usuario"];
            var contrasena = App.Current.Properties["contrasena"];
            var url = conexion.URL + "" + conexion.ConsultarEmpleado + usuario + "/" + contrasena;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var empleado = JsonConvert.DeserializeObject<Empleado>(content);
                if (empleado != null)
                {
                    Cve_Empleado = empleado.Cve_Empleado;
                    Numero_Empleado = empleado.Numero_Empleado;
                    Estatus = empleado.Estatus;
                    Fecha_Registro = empleado.Fecha_Registro;
                    App.Current.Properties["numeroEmpleado"] = empleado.Numero_Empleado;
                    App.Current.Properties["cveEmpleado"] = empleado.Cve_Empleado;
                    Persona = new Persona()
                    {
                        Cve_Persona = empleado.Persona.Cve_Persona,
                        Nombre = empleado.Persona.Nombre,
                        Apellido_Paterno = empleado.Persona.Apellido_Paterno,
                        Apellido_Materno = empleado.Persona.Apellido_Materno,
                        RFC = empleado.Persona.RFC,
                        CURP = empleado.Persona.CURP,
                        Sexo = empleado.Persona.Sexo,
                        Fecha_Nacimiento = empleado.Persona.Fecha_Nacimiento,
                        Numero_Telefono = empleado.Persona.Numero_Telefono,
                        Estado_Civil = empleado.Persona.Estado_Civil,
                        Nacionalidad = empleado.Persona.Nacionalidad,
                        IdColonia = empleado.Persona.IdColonia,
                        Usuario = new Usuario()
                        {
                            Cve_Usuario = empleado.Persona.Usuario.Cve_Usuario,
                            IdAlumno = empleado.Persona.Usuario.IdAlumno,
                            Nombre_Usuario = empleado.Persona.Usuario.Nombre_Usuario,
                            Contrasena = empleado.Persona.Usuario.Contrasena,
                            Fecha_Registro = empleado.Persona.Usuario.Fecha_Registro,
                            Estatus = empleado.Persona.Usuario.Estatus,
                            Alias_Red = empleado.Persona.Usuario.Alias_Red
                        }
                    };
                    App.Current.Properties["cveUsuario"] = empleado.Persona.Usuario.Cve_Usuario;
                    App.Current.Properties["cvePersona"] = empleado.Persona.Cve_Persona;
                    App.Current.Properties["nombreUsuario"] = empleado.Persona.Nombre + " " + empleado.Persona.Apellido_Paterno;
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
                    App.Current.Properties["numeroEmpleado"] = "";
                    App.Current.Properties["cveEmpleado"] = 0;
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


    public class RootObjectEmpleado
    {
        public Empleado empleado { get; set; }
    }
}
