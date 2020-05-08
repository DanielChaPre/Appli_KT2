using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using static Appli_KT2.View.AtlasPage;

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
        private int id_estado, id_municipio, id_colonia;
        private string cp;
        private Estados entEstados;
        private Resultado resultadoAptitud;
        public Alumno entalumno = new Alumno();
        MetodoHTTP metodosHTTP;
        private bool isrun;
        private bool isvisible;
        private bool isinsertar;
        private bool isacciones;
        private string sexo;
        private string plantelEMS;
        private bool nuevo_registro;
        private List<Aptitud> ListAptitudes = new List<Aptitud>();
        private List<DetalleAptitudCarrera> ListAptitudesCarrera = new List<DetalleAptitudCarrera>();

        public PerfilAlumnoViewModel()
        {
            IsRun = true;
            IsVisible = false;
            IsInsertar = false;
            IsAcciones = false;
            GC.Collect();
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
        public int Id_Colonia
        {
            get
            {
                return id_colonia;
            }
            set
            {
                id_colonia = value;
                OnPropertyChanged();
            }
        }
        public int Id_municipio
        {
            get
            {
                return id_municipio;
            }
            set
            {
                id_municipio = value;
                OnPropertyChanged();
            }
        }
        public int Id_estado
        {
            get
            {
                return id_estado;
            }
            set
            {
                id_estado = value;
                OnPropertyChanged();
            }
        }

        public  string CP
        {
            get
            {
                return cp;
            }
            set
            {
                cp = value;
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
                ConsultarAlumno();
                return;
            }
            catch (Exception ex)
            {
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
                LlenarDatos2();
                string json = JsonConvert.SerializeObject(rootObject);
                dynamic respuesta = metodosHTTP.ActualizarDatos(conexion.URL + conexion.ModificarAlumno, json, nuevo_registro);
                if( await ConsultarAlumno())
                     Application.Current.MainPage = new NavigationPage(new MainPage());
                return;
            }
            catch (Exception ex)
            {
                return;
            }
        }

        private void LlenarDatos2()
        {
            try
            {
                rootObject = new RootObjectAlumno();
                rootObject.alumno = new Alumno();

                rootObject.alumno.IdAlumno = IdAlumno;
                rootObject.alumno.Nombre1 = Nombre1;
                rootObject.alumno.ApellidoPaterno1 = ApellidoPaterno1;
                rootObject.alumno.ApellidoMaterno1 = ApellidoMaterno1;
                rootObject.alumno.CURP1 = CURP1;
                rootObject.alumno.Sexo1 = Sexo1;
                rootObject.alumno.Calle1 = Calle1;
                rootObject.alumno.NumeroExterior1 = NumeroExterior1;
                rootObject.alumno.NumeroInterior1 = NumeroInterior1;
                rootObject.alumno.Email1 = Email1;
                rootObject.alumno.Celular1 = Celular1;
                rootObject.alumno.Telefono1 = Telefono1;
                rootObject.alumno.FOLIOSUREDSU1 = FOLIOSUREDSU1;
                rootObject.alumno.FolioSUREMS1 = FolioSUREMS1;
                rootObject.alumno.IdPais = IdPais;
                rootObject.alumno.ClavePlantelESEC1 = ClavePlantelESEC1;
                rootObject.alumno.IdPlantelEMS = IdPlantelEMS;
                rootObject.alumno.Nacionalidad1 = Nacionalidad1;
                rootObject.alumno.IdColonia = SelectedColonia.idColonia;
                rootObject.alumno.IdMunicipio = SelectedMunicipio.idMunicipio;
                rootObject.alumno.IdEstado = SelectedEstado.IdEstado;
                rootObject.alumno.Cp = SelectedColonia.CP;
            }
            catch (Exception ex)
            {

                throw;
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
                }
            }
            catch (Exception ex)
            {
            }
        }

        public async Task<bool> ConsultarAlumno()
        {
            try
            {
                IsRun = true;
                IsVisible = false;
               // ConsultarUsuarioAlumno();
                _client = new HttpClient();
                conexion = new ConexionWS();
                var idAlumno = App.Current.Properties["idAlumno"];
                var url = conexion.URL + "" + conexion.ConsultarAlumno + idAlumno;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                   // GC.Collect();
                    var content = await response.Content.ReadAsStringAsync();
                    var alumno = JsonConvert.DeserializeObject<Alumno>(content);
                    App.Current.Properties["acolonia"] = alumno.IdColonia;
                    App.Current.Properties["aestado"] = alumno.IdEstado;
                    App.Current.Properties["amunicipio"] = alumno.IdMunicipio;
                    App.Current.Properties["acp"] = alumno.Cp;
                    if (alumno != null)
                    {
                        IdAlumno =alumno.IdAlumno;
                        Nombre1 = alumno.Nombre1;
                        ApellidoPaterno1 = alumno.ApellidoPaterno1;
                        ApellidoMaterno1 = alumno.ApellidoMaterno1;
                        CURP1 = alumno.CURP1;
                        Sexo1 = alumno.Sexo1;
                        SeleccionarSexo(alumno.Sexo1);
                        Calle1 =alumno.Calle1;
                        NumeroExterior1 = alumno.NumeroExterior1;
                        NumeroInterior1 = alumno.NumeroInterior1;
                        Email1 =alumno.Email1;
                        Celular1 = alumno.Celular1;
                        Telefono1 = alumno.Telefono1;
                        FOLIOSUREDSU1 = alumno.FOLIOSUREDSU1;
                        FolioSUREMS1 = alumno.FolioSUREMS1;
                        IdPais = alumno.IdPais;
                        IdColonia = alumno.IdColonia;
                        IdMunicipio = alumno.IdMunicipio;
                        Cp = alumno.Cp;
                        ClavePlantelESEC1 = alumno.ClavePlantelESEC1;
                        IdPlantelEMS = alumno.IdPlantelEMS;
                        await ConsultarPlantelEMS(alumno.IdPlantelEMS);
                        Nacionalidad1 = alumno.Nacionalidad1;
                        App.Current.Properties["idAlumno"] = alumno.IdAlumno;
                        App.Current.Properties["nombreUsuario"] = alumno.Nombre1 + " " + alumno.ApellidoPaterno1;
                      
                        IsRun = false;
                        IsVisible = true;
                        IsAcciones = true;
                        IsInsertar = false;
                        nuevo_registro = false;
                        entalumno = alumno;
                        return true;
                    }
                    else
                    {
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
                    IsRun = false;
                    IsVisible = true;
                    IsAcciones = false;
                    IsInsertar = true;
                    return false;
                }
            }
            catch (Exception ex)
            {
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

        public async Task SincronizarAptitudAlumno()
        {
            var idAlumno = App.Current.Properties["idAlumno"].ToString();
            if (idAlumno.Equals("0"))
            {
                return;
            }
            SQLiteConnection conn;
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            conn.CreateTable<Resultado>();
            if (!await ObtenerResultadoAptitud())
            {
                SincronizarAptitudAlumno();
            }
            if (resultadoAptitud != null)
            {

                conn.InsertOrReplace(resultadoAptitud);
                conn.Close();
            }
            else
            {
                SincronizarAptitudAlumno();
            }
        }

        public async Task SincronizarAptitudes()
        {
            SQLiteConnection conn;
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            conn.CreateTable<Aptitud>();
            if (!await ObtenerAptitudes())
            {
                SincronizarAptitudes();
            }
            if (ListAptitudes != null)
            {
                for (int i = 0; i < ListAptitudes.Count; i++)
                {
                    conn.InsertOrReplace(ListAptitudes[i]);
                }
                conn.Close();
            }
            else
            {
                SincronizarAptitudes();
            }
        }

        public async Task SincronizarAptitudesCarrera()
        {
            SQLiteConnection conn;
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            conn.CreateTable<DetalleAptitudCarrera>();
            if (!await ObtenerAptitudesCarrera())
            {
                SincronizarAptitudesCarrera();
            }
            if (ListAptitudesCarrera != null)
            {
               // conn.Query<>;
                for (int i = 0; i < ListAptitudesCarrera.Count; i++)
                {
                    conn.InsertOrReplace(ListAptitudesCarrera[i]);
                }
                conn.Close();
            }
            else
            {
                SincronizarAptitudes();
            }
        }

        private async Task<bool> ObtenerAptitudesCarrera()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerAptitudCarrera));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    var aptitudCarrera = JsonConvert.DeserializeObject<List<DetalleAptitudCarrera>>(content);
                    if (aptitudCarrera != null)
                    {
                        for (int i = 0; i < aptitudCarrera.Count; i++)
                        {
                            var aptitud = new DetalleAptitudCarrera()
                            {
                                cve_aptitud = aptitudCarrera[i].cve_aptitud,
                                idCarreraES = aptitudCarrera[i].idCarreraES
                            };
                            ListAptitudesCarrera.Add(aptitud);
                        }
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private async Task<bool> ObtenerResultadoAptitud()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var idAlumno = App.Current.Properties["idAlumno"].ToString();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerAptitudesAlumno+idAlumno));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                   
                    var content = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<Resultado>(content);
                    if (resultado != null)
                    {
                        resultadoAptitud = new Resultado()
                        {
                            idAlumno = resultado.idAlumno,
                            aptitud1 = resultado.aptitud1,
                            aptitud2 = resultado.aptitud2,
                            aptitud3 = resultado.aptitud3
                        };
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        private async Task<bool> ObtenerAptitudes()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerAptitudes));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {

                    var content = await response.Content.ReadAsStringAsync();
                    var aptitudes = JsonConvert.DeserializeObject<List<Aptitud>>(content);
                    for (int i = 0; i < aptitudes.Count; i++)
                    {
                        var aptitud = new Aptitud()
                        {
                            Cve_aptitud = aptitudes[i].Cve_aptitud,
                            Estatus = aptitudes[i].Estatus,
                            Nombre = aptitudes[i].Nombre
                        };
                        ListAptitudes.Add(aptitud);
                    }
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion
    }


    public class RootObjectAlumno
    {
        public Alumno alumno { get; set; }
    }
}
