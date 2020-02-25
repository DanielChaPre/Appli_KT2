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
    public class PerfilAlumnoViewModel : Alumno
    {
        private HttpClient _client;
        private ConexionWS conexion;
        private RootObjectAlumno rootObject;
        private Alumno alumno;
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

        public PerfilAlumnoViewModel()
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
            if (string.IsNullOrEmpty(_selectedEstado.NombreEstado))
            {
                App.Current.Properties["NombreEstado"] = "";
                MainViewModel.GetInstance().Municipio = new MunicipioViewModel();
            }
            else
            {
                App.Current.Properties["NombreEstado"] = _selectedEstado.NombreEstado;
                MainViewModel.GetInstance().Municipio = new MunicipioViewModel();
            }
           
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
            IsRun = false;
            IsVisible = true;
            metodosHTTP = new MetodoHTTP();
            conexion = new ConexionWS();
            LlenarDatos();
            string json = JsonConvert.SerializeObject(rootObject);
            dynamic respuesta = metodosHTTP.ActualizarDatos(conexion.URL + conexion.CrearAlumno, json);
            await ConsultarAlumno();
            return;
        }



        private async void DesactivarPerfil()
        {
            try
            {
                IsRun = false;
                IsVisible = true;
                metodosHTTP = new MetodoHTTP();
                conexion = new ConexionWS();
                LlenarDatos();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.Delete(conexion.URL + conexion.EliminarAlumno, json);
                await ConsultarAlumno();
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
                IsRun = false;
                IsVisible = true;
                metodosHTTP = new MetodoHTTP();
                conexion = new ConexionWS();
                LlenarDatos();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.ActualizarDatos(conexion.URL + conexion.ModificarAlumno, json, nuevo_registro);
                await ConsultarAlumno();
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
                rootObject = new RootObjectAlumno()
                {
                    alumno = new Alumno()
                    {
                        IdAlumno = IdAlumno,
                        Nombre1 = Nombre1,
                        ApellidoPaterno1 = ApellidoPaterno1,
                        ApellidoMaterno1 = ApellidoMaterno1,
                        CURP1 = CURP1,
                        Sexo1 = Sexo1,
                        Calle1 = Calle1,
                        NumeroExterior1 = NumeroExterior1,
                        NumeroInterior1 = NumeroInterior1,
                        Email1 = Email1,
                        Celular1 = Celular1,
                        Telefono1 = Telefono1,
                        FOLIOSUREDSU1 = FOLIOSUREDSU1,
                        FolioSUREMS1 = FolioSUREMS1,
                        Municipios = new Municipios()
                        {
                            IdEstado = _selectedMunicipio.IdEstado,
                            IdMunicipio = _selectedMunicipio.IdMunicipio,
                            NombreMunicipio = _selectedMunicipio.NombreMunicipio
                        },
                        Colonias = new Colonias()
                        {
                            Cp = _selectedColonia.Cp,
                            IdColonia = _selectedColonia.IdColonia,
                            IdMunicipio = _selectedColonia.IdMunicipio,
                            NombreColonia = _selectedColonia.NombreColonia,
                        },
                        IdPais = IdPais,
                        ClavePlantelESEC1 = ClavePlantelESEC1,
                        IdPlantelEMS = IdPlantelEMS,
                        Nacionalidad1 = Nacionalidad1
                   }
                };
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async void ConsultarUsuarioAlumno()
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                var idAlumno = App.Current.Properties["idAlumno"];
                var url = conexion.URL + "" + conexion.ConsultarUsuarioAlumno + idAlumno;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var usuario = JsonConvert.DeserializeObject<Usuario>(content);
                    var usuarioAlumno = new Usuario()
                    {
                        Cve_Usuario = usuario.Cve_Usuario,
                        IdAlumno = usuario.IdAlumno,
                        Nombre_Usuario = usuario.Nombre_Usuario,
                        Contrasena = usuario.Contrasena,
                        Fecha_Registro = usuario.Fecha_Registro,
                        Alias_Red = usuario.Alias_Red,
                        Estatus = usuario.Estatus,
                };
                    App.Current.Properties["cveUsuario"] = usuario.Cve_Usuario;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error: " + response.StatusCode + ", existe un error en la petición", "Accept");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error: " + ex.Message, "Accept");
                throw;
            }
        }
        public async Task<bool> ConsultarAlumno()
        {
            IsRun = true;
            IsVisible = false;
            ConsultarUsuarioAlumno();
            _client = new HttpClient();
            conexion = new ConexionWS();
            var idAlumno = App.Current.Properties["idAlumno"];
            var url = conexion.URL + "" + conexion.ConsultarAlumno + idAlumno;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var entalumno = JsonConvert.DeserializeObject<Alumno>(content);
                if (entalumno != null)
                {
                    IdAlumno = entalumno.IdAlumno;
                    Nombre1 = entalumno.Nombre1;
                    ApellidoPaterno1 = entalumno.ApellidoPaterno1;
                    ApellidoMaterno1 = entalumno.ApellidoMaterno1;
                    CURP1 = entalumno.CURP1;
                    Sexo1 = entalumno.Sexo1;
                    SeleccionarSexo(entalumno.Sexo1);
                    Calle1 = entalumno.Calle1;
                    NumeroExterior1 = entalumno.NumeroExterior1;
                    NumeroInterior1 = entalumno.NumeroInterior1;
                    Email1 = entalumno.Email1;
                    Celular1 = entalumno.Celular1;
                    Telefono1 = entalumno.Telefono1;
                    FOLIOSUREDSU1 = entalumno.FOLIOSUREDSU1;
                    FolioSUREMS1 = entalumno.FolioSUREMS1;
                    IdPais = entalumno.IdPais;
                    _selectedColonia = entalumno.Colonias;
                    _selectedMunicipio = entalumno.Municipios;
                    ClavePlantelESEC1 = entalumno.ClavePlantelESEC1;
                    IdPlantelEMS = entalumno.IdPlantelEMS;
                    await ConsultarPlantelEMS(entalumno.IdPlantelEMS);
                    Nacionalidad1 = entalumno.Nacionalidad1;
                    App.Current.Properties["idAlumno"] = entalumno.IdAlumno;
                    App.Current.Properties["nombreUsuario"] = entalumno.Nombre1 + " " + entalumno.ApellidoPaterno1;
                    IsRun = false;
                    IsVisible = true;
                    IsAcciones = true;
                    IsInsertar = false;
                    nuevo_registro = false;
                    return true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "El usuario no cuenta con información, actualice su información", "Aceptar");
                    IsRun = false;
                    IsVisible = true;
                    IsAcciones = false;
                    IsInsertar = true;
                    nuevo_registro = true;
                    return false;
                }
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
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
            var url = conexion.URL + "" + conexion.ObtenerPlantelEMS+idEMS;
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

        private void SeleccionarColonias(int idColonias)
        {
           

        }

        #endregion
    }


    public class RootObjectAlumno
    {
        public Alumno alumno { get; set; }
    }
}
