using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        
        EstadosViewModel estadosViewModel = new EstadosViewModel();
        MunicipioViewModel municipiosViewModel;
        ColoniaViewModel coloniaViewModel;
        PerfilPadreViewModel perfil = new PerfilPadreViewModel();
        Estados estadoS;
        bool cp = true;
        private int tipoUsuario;
        private HttpClient _client;
        private ConexionWS conexion;

        public RegisterPage ()
		{
            InitializeComponent ();
            frameAlumno.IsVisible = false;
            frameAlumno.IsVisible = false;
            framePreguntaPadre.IsVisible = false;
            this.tipoUsuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());
            pEstados.SelectedIndexChanged += OnPickerSelectedIndexChanged;
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            estadoS = (Estados) pEstados.SelectedItem;
            municipiosViewModel = new MunicipioViewModel(estadoS.NombreEstado);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Thread.Sleep(5000);
            /*
             * 1: usuario general
             * 2: Alumno
             * 3: Empleado
             * 4: 
             * 5: 
             * 6: 
             * 
             * **/
            switch (this.tipoUsuario)
            {
                case 1:
                    lytPadre.IsVisible = true;
                    lytCurpHijo.IsVisible = false;
                    btnSi.Clicked += BtnSi_Clicked;
                    btnNo.Clicked += BtnNo_Clicked;
                    break;
                case 2:
                    frameAlumno.IsVisible = true;
                    break;
                case 3:
                    ConsultarEmpleado();
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
            }


            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                while (estadosViewModel.ListEstados.Count != 0)
                {
                    pEstados.ItemsSource = estadosViewModel.ListEstados;
                    pEstados.ItemDisplayBinding = new Binding("NombreEstado");
                    return false;
                }

                return true; // True = Repeat again, False = Stop the timer
            });

            Device.StartTimer(TimeSpan.FromSeconds (5), () =>
            {
                try
                {
                  
                    if (municipiosViewModel.ListMunicipios != null || municipiosViewModel.ListMunicipios.Count != 0)
                    {
                        pMunicipio.ItemsSource = municipiosViewModel.ListMunicipios;
                        pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");
                        return false;
                    }
                    return true; // True = Repeat again, False = Stop the timer
                }
                catch (NullReferenceException ex)
                {
                    return true;
                }
               
            });

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {
                    if (txtCodigoPostal.Text.Length == 5 && cp)
                    {
                        var codPost = txtCodigoPostal.Text;
                        coloniaViewModel = new ColoniaViewModel(codPost);
                        cp = false;
                    }

                    if (coloniaViewModel.ListColonias != null || coloniaViewModel.ListColonias.Count != 0)
                    {
                        pColonia.ItemsSource = coloniaViewModel.ListColonias;
                        pColonia.ItemDisplayBinding = new Binding("NombreColonia");
                        return false;
                    }
                    return true; // True = Repeat again, False = Stop the timer
                }
                catch (NullReferenceException ex)
                {
                    return true;
                }

            });
        }

        private void BtnNo_Clicked(object sender, EventArgs e)
        {
            framePreguntaPadre.IsVisible = false;
            lytPadre.IsVisible = false;
            lytCurpHijo.IsVisible = false;
            ConsultarUsuarioGeneral();
        }

        private void BtnSi_Clicked(object sender, EventArgs e)
        {
            lytPadre.IsVisible = false;
            lytCurpHijo.IsVisible = true;
            ConsultarPadreFamilia();
            framePreguntaPadre.IsVisible = false;
            framePadre.IsVisible = true;
        }

        #region Consultas Perfiles
        public async void ConsultarAlumno()
        {
            _client = new HttpClient();
            conexion = new ConexionWS();
            var idAlumno = App.Current.Properties["idAlumno"];
            var url = conexion.URL + "" + conexion.ConsultarAlumno + idAlumno;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var alumno = JsonConvert.DeserializeObject<Alumno>(content);
                var entAlumno = new Alumno()
                {
                    idAlumno = alumno.idAlumno,
                    Nombre = alumno.Nombre,
                    ApellidoPaterno = alumno.ApellidoPaterno,
                    ApellidoMaterno = alumno.ApellidoMaterno,
                    CURP = alumno.CURP,
                    Sexo = alumno.Sexo,
                    Calle = alumno.Calle,
                    NumeroExterior = alumno.NumeroExterior,
                    NumeroInterior = alumno.NumeroInterior,
                    Email = alumno.Email,
                    Celular = alumno.Celular,
                    Telefono = alumno.Telefono,
                    FOLIOSUREDSU = alumno.FOLIOSUREDSU,
                    FolioSUREMS = alumno.FolioSUREMS,
                    idColonia = alumno.idColonia,
                    idMunicipio = alumno.idMunicipio,
                    idPais = alumno.idPais,
                    ClavePlantelESEC = alumno.ClavePlantelESEC,
                    idPlantelEMS = alumno.idPlantelEMS,
                    Nacionalidad = alumno.Nacionalidad
                };
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                return;
            }
        }

        public async void ConsultarPadreFamilia()
        {
            _client = new HttpClient();
            conexion = new ConexionWS();
            var usuario = App.Current.Properties["usuario"];
            var contrasena = App.Current.Properties["contrasenia"];
            var url = conexion.URL + "" + conexion.ConsultarPadreFamilia + usuario + "/" + contrasena;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var padreFamilia = JsonConvert.DeserializeObject<PadreFamilia>(content);
                var entPadreFamilia = new PadreFamilia()
                {
                    CVE_Padre_Familia = padreFamilia.CVE_Padre_Familia,
                    IdAlumno = padreFamilia.IdAlumno,
                    Fecha_Registro = padreFamilia.Fecha_Registro,
                    PersonaP = new Persona()
                    {
                        CVE_Persona = padreFamilia.PersonaP.CVE_Persona,
                        Nombre = padreFamilia.PersonaP.Nombre,
                        Apellido_Paterno = padreFamilia.PersonaP.Apellido_Paterno,
                        Apellido_Materno = padreFamilia.PersonaP.Apellido_Materno,
                        RFC = padreFamilia.PersonaP.RFC,
                        CURP = padreFamilia.PersonaP.CURP,
                        Sexo = padreFamilia.PersonaP.Sexo,
                        Fecha_Nacimiento = padreFamilia.PersonaP.Fecha_Nacimiento,
                        Numero_Telefono = padreFamilia.PersonaP.Numero_Telefono,
                        Estado_Civil = padreFamilia.PersonaP.Estado_Civil,
                        Nacionalidad = padreFamilia.PersonaP.Nacionalidad,
                        IdColonia = padreFamilia.PersonaP.IdColonia,
                        UsuarioP = new Usuario()
                        {
                            CVE_Usuario = padreFamilia.PersonaP.UsuarioP.CVE_Usuario,
                            IdAlumno = padreFamilia.PersonaP.UsuarioP.IdAlumno,
                            Nombre_Usuario = padreFamilia.PersonaP.UsuarioP.Nombre_Usuario,
                            Contrasena = padreFamilia.PersonaP.UsuarioP.Contrasena,
                            Fecha_Registro = padreFamilia.PersonaP.UsuarioP.Fecha_Registro,
                            Estatus = padreFamilia.PersonaP.UsuarioP.Estatus,
                            Alias_Red = padreFamilia.PersonaP.UsuarioP.Alias_Red
                        }
                    }
                };
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                return;
            }
        }

        public async void ConsultarEmpleado()
        {
            _client = new HttpClient();
            conexion = new ConexionWS();
            var usuario = App.Current.Properties["usuario"];
            var contrasena = App.Current.Properties["contrasenia"];
            var url = conexion.URL + "" + conexion.ConsultarEmpleado + usuario + "/" + contrasena;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var empleado = JsonConvert.DeserializeObject<Empleado>(content);
                var entEmpleado = new Empleado()
                {
                    Cve_empleado = empleado.Cve_empleado,
                    Numero_empleado = empleado.Numero_empleado,
                    Estatus = empleado.Estatus,
                    Fecha_Registro = empleado.Fecha_Registro,
                    PersonaE = new Persona()
                    {
                        CVE_Persona = empleado.PersonaE.CVE_Persona,
                        Nombre = empleado.PersonaE.Nombre,
                        Apellido_Paterno = empleado.PersonaE.Apellido_Paterno,
                        Apellido_Materno = empleado.PersonaE.Apellido_Materno,
                        RFC = empleado.PersonaE.RFC,
                        CURP = empleado.PersonaE.CURP,
                        Sexo = empleado.PersonaE.Sexo,
                        Fecha_Nacimiento = empleado.PersonaE.Fecha_Nacimiento,
                        Numero_Telefono = empleado.PersonaE.Numero_Telefono,
                        Estado_Civil = empleado.PersonaE.Estado_Civil,
                        Nacionalidad = empleado.PersonaE.Nacionalidad,
                        IdColonia = empleado.PersonaE.IdColonia,
                        UsuarioP = new Usuario()
                        {
                            CVE_Usuario = empleado.PersonaE.UsuarioP.CVE_Usuario,
                            IdAlumno = empleado.PersonaE.UsuarioP.IdAlumno,
                            Nombre_Usuario = empleado.PersonaE.UsuarioP.Nombre_Usuario,
                            Contrasena = empleado.PersonaE.UsuarioP.Contrasena,
                            Fecha_Registro = empleado.PersonaE.UsuarioP.Fecha_Registro,
                            Estatus = empleado.PersonaE.UsuarioP.Estatus,
                            Alias_Red = empleado.PersonaE.UsuarioP.Alias_Red
                        }
                    }
                };
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                return;
            }
        }

        public async void ConsultarEmpleadoPlantel()
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
                var entEmpleadoPlantel = new EmpleadoPlantel()
                {
                    Cve_empleado_plantel = empleadoPlantel.Cve_empleado_plantel,
                    IdPlantelesES = empleadoPlantel.IdPlantelesES,
                    Tipo = empleadoPlantel.Tipo,
                    Fecha_registro = empleadoPlantel.Fecha_registro,
                    PersonaEP = new Persona()
                    {
                        CVE_Persona = empleadoPlantel.PersonaEP.CVE_Persona,
                        Nombre = empleadoPlantel.PersonaEP.Nombre,
                        Apellido_Paterno = empleadoPlantel.PersonaEP.Apellido_Paterno,
                        Apellido_Materno = empleadoPlantel.PersonaEP.Apellido_Materno,
                        RFC = empleadoPlantel.PersonaEP.RFC,
                        CURP = empleadoPlantel.PersonaEP.CURP,
                        Sexo = empleadoPlantel.PersonaEP.Sexo,
                        Fecha_Nacimiento = empleadoPlantel.PersonaEP.Fecha_Nacimiento,
                        Numero_Telefono = empleadoPlantel.PersonaEP.Numero_Telefono,
                        Estado_Civil = empleadoPlantel.PersonaEP.Estado_Civil,
                        Nacionalidad = empleadoPlantel.PersonaEP.Nacionalidad,
                        IdColonia = empleadoPlantel.PersonaEP.IdColonia,
                        UsuarioP = new Usuario()
                        {
                            CVE_Usuario = empleadoPlantel.PersonaEP.UsuarioP.CVE_Usuario,
                            IdAlumno = empleadoPlantel.PersonaEP.UsuarioP.IdAlumno,
                            Nombre_Usuario = empleadoPlantel.PersonaEP.UsuarioP.Nombre_Usuario,
                            Contrasena = empleadoPlantel.PersonaEP.UsuarioP.Contrasena,
                            Fecha_Registro = empleadoPlantel.PersonaEP.UsuarioP.Fecha_Registro,
                            Estatus = empleadoPlantel.PersonaEP.UsuarioP.Estatus,
                            Alias_Red = empleadoPlantel.PersonaEP.UsuarioP.Alias_Red
                        }
                    }
                };
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                return;
            }
        }

        public async void ConsultarUsuarioGeneral()
        {
            _client = new HttpClient();
            conexion = new ConexionWS();
            var usuario = App.Current.Properties["usuario"];
            var contrasena = App.Current.Properties["contrasenia"];
            var url = conexion.URL + "" + conexion.ConsultarPerfil + usuario + "/" + contrasena;
            var uri = new Uri(string.Format(@"" + url, string.Empty));
            var response = await _client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var persona = JsonConvert.DeserializeObject<Persona>(content);
                var entPersona = new Persona()
                {
                    CVE_Persona = persona.CVE_Persona,
                    Nombre = persona.Nombre,
                    Apellido_Paterno = persona.Apellido_Paterno,
                    Apellido_Materno = persona.Apellido_Materno,
                    RFC = persona.RFC,
                    CURP = persona.CURP,
                    Sexo = persona.Sexo,
                    Fecha_Nacimiento = persona.Fecha_Nacimiento,
                    Numero_Telefono = persona.Numero_Telefono,
                    Estado_Civil = persona.Estado_Civil,
                    Nacionalidad = persona.Nacionalidad,
                    IdColonia = persona.IdColonia,
                    UsuarioP = new Usuario()
                    {
                        CVE_Usuario = persona.UsuarioP.CVE_Usuario,
                        IdAlumno = persona.UsuarioP.IdAlumno,
                        Nombre_Usuario = persona.UsuarioP.Nombre_Usuario,
                        Contrasena = persona.UsuarioP.Contrasena,
                        Fecha_Registro = persona.UsuarioP.Fecha_Registro,
                        Estatus = persona.UsuarioP.Estatus,
                        Alias_Red = persona.UsuarioP.Alias_Red
                    }
                };
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                return;
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
                        CVE_Usuario = usuario.CVE_Usuario,
                        IdAlumno = usuario.IdAlumno,
                        Nombre_Usuario = usuario.Nombre_Usuario,
                        Contrasena = usuario.Contrasena,
                        Fecha_Registro = usuario.Fecha_Registro,
                        Alias_Red = usuario.Alias_Red,
                        Estatus = usuario.Estatus
                    };
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
        #endregion
    }
}