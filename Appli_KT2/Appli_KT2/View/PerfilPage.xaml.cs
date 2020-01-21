using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RegisterPage : ContentPage
	{
        PerfilGeneralViewModel perfilGeneral;
        EstadosViewModel estadosViewModel = new EstadosViewModel();
        MunicipioViewModel municipiosViewModel;
        ColoniaViewModel coloniaViewModel;
        PerfilPadreViewModel perfil = new PerfilPadreViewModel();
        Estados estadoS;
        bool cp = true;
        private int tipoUsuario = 0;
        private HttpClient _client;
        private ConexionWS conexion;

        public RegisterPage ()
		{
            InitializeComponent ();
            try
            {
                this.tipoUsuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
            OcultarPerfiles();
        }

        private void OnPickerSelectedIndexChanged(object sender, EventArgs e)
        {
            estadoS = (Estados) pEstados.SelectedItem;
            municipiosViewModel = new MunicipioViewModel(estadoS.NombreEstado);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FiltrarPerfil();
           
        }

        public void FiltrarPerfil()
        {
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
                    framePreguntaPadre.IsVisible = true;
                    lytPadre.IsVisible = true;
                    lytCurpHijo.IsVisible = false;
                    btnSi.Clicked += BtnSi_Clicked;
                    btnNo.Clicked += BtnNo_Clicked;
                    break;
                case 2:
                    frameAlumno.IsVisible = true;
                    frameDireccion.BindingContext = new PerfilAlumnoViewModel();
                    LlenarListasDireccion();
                    break;
                case 3:
                    ConsultarEmpleado();
                    frameDireccion.BindingContext = new PerfilEmpleadoViewModel();
                    LlenarListasDireccion();
                    break;
                case 4:
                    LlenarListasDireccion();
                    frameDireccion.BindingContext = new PerfilEmpleadoPlantelViewModel();
                    break;
                case 5:
                    frameDireccion.BindingContext = new PerfilPadreViewModel();
                    LlenarListasDireccion();
                    break;
                case 6:
                    break;
                default:
                    lytPadre.IsVisible = false;
                    lytCurpHijo.IsVisible = true;
                    //ConsultarPadreFamilia();
                    framePreguntaPadre.IsVisible = false;
                    framePadre.IsVisible = true;
                    break;
            }
        }

        
        #region Carga de Perfiles

        public void OcultarPerfiles()
        {
            frameAlumno.IsVisible = false;
            frameBotones.IsVisible = false;
            frameDireccion.IsVisible = false;
            frameEmpleado.IsVisible = false;
            frameEmpleadoPlantel.IsVisible = false;
            framePadre.IsVisible = false;
            frameUsuarioGeneral.IsVisible = false;
        }
        public void CargarPerfilGeneral()
        {
            perfilGeneral = new PerfilGeneralViewModel();
            frameUsuarioGeneral.BindingContext = perfilGeneral;
            frameBotones.BindingContext = perfilGeneral;
            ConsultarUsuarioGeneral();
            framePreguntaPadre.IsVisible = false;
            frameUsuarioGeneral.IsVisible = true;
            frameBotones.IsVisible = true;
          
            
            //frameDireccion.BindingContext = new PerfilGeneralViewModel();
        }
        #endregion

        private async void ConsultarUsuarioGeneral()
        {
            formularioGeneral.IsVisible = false;
            actCargaForm.IsRunning = true;
            if ( await perfilGeneral.ConsultarUsuarioGeneral())
            {
                formularioGeneral.IsVisible = true;
                actCargaForm.IsVisible = false;
                slytInsertar.IsVisible = false;
                slytAcciones.IsVisible = true;
            }
            else
            {
                formularioGeneral.IsVisible = true;
                actCargaForm.IsVisible = false;
                slytInsertar.IsVisible = true;
                slytAcciones.IsVisible = false;
            }

        }

        public void LlenarListasDireccion()
        {
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

            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
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
            CargarPerfilGeneral();
        }

        private void BtnSi_Clicked(object sender, EventArgs e)
        {
            lytPadre.IsVisible = false;
            lytCurpHijo.IsVisible = true;
            //ConsultarPadreFamilia();
            //framePreguntaPadre.IsVisible = false;
            //framePadre.IsVisible = true;
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
                var entAlumno = GetEntAlumno(alumno);
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "usuario incorrecto", "Accept");
                return;
            }
        }

        private static Alumno GetEntAlumno(Alumno alumno)
        {
            return new Alumno()
            {
                IdAlumno = alumno.IdAlumno,
                Nombre1 = alumno.Nombre1,
                ApellidoPaterno1 = alumno.ApellidoPaterno1,
                ApellidoMaterno1 = alumno.ApellidoMaterno1,
                CURP1 = alumno.CURP1,
                Sexo1 = alumno.Sexo1,
                Calle1 = alumno.Calle1,
                NumeroExterior1 = alumno.NumeroExterior1,
                NumeroInterior1 = alumno.NumeroInterior1,
                Email1 = alumno.Email1,
                Celular1 = alumno.Celular1,
                Telefono1 = alumno.Telefono1,
                FOLIOSUREDSU1 = alumno.FOLIOSUREDSU1,
                FolioSUREMS1 = alumno.FolioSUREMS1,
                IdColonia = alumno.IdColonia,
                IdMunicipio = alumno.IdMunicipio,
                IdPais = alumno.IdPais,
                ClavePlantelESEC1 = alumno.ClavePlantelESEC1,
                IdPlantelEMS = alumno.IdPlantelEMS,
                Nacionalidad1 = alumno.Nacionalidad1
            };
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
                        Cve_Persona = padreFamilia.PersonaP.Cve_Persona,
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
                        Usuario = new Usuario()
                        {
                            Cve_Usuario = padreFamilia.PersonaP.Usuario.Cve_Usuario,
                            IdAlumno = padreFamilia.PersonaP.Usuario.IdAlumno,
                            Nombre_Usuario = padreFamilia.PersonaP.Usuario.Nombre_Usuario,
                            Contrasena = padreFamilia.PersonaP.Usuario.Contrasena,
                            Fecha_Registro = padreFamilia.PersonaP.Usuario.Fecha_Registro,
                            Estatus = padreFamilia.PersonaP.Usuario.Estatus,
                            Alias_Red = padreFamilia.PersonaP.Usuario.Alias_Red
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
                    Fecha_registro = empleado.Fecha_registro,
                    PersonaE = new Persona()
                    {
                        Cve_Persona = empleado.PersonaE.Cve_Persona,
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
                        Usuario = new Usuario()
                        {
                            Cve_Usuario = empleado.PersonaE.Usuario.Cve_Usuario,
                            IdAlumno = empleado.PersonaE.Usuario.IdAlumno,
                            Nombre_Usuario = empleado.PersonaE.Usuario.Nombre_Usuario,
                            Contrasena = empleado.PersonaE.Usuario.Contrasena,
                            Fecha_Registro = empleado.PersonaE.Usuario.Fecha_Registro,
                            Estatus = empleado.PersonaE.Usuario.Estatus,
                            Alias_Red = empleado.PersonaE.Usuario.Alias_Red
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
                        Cve_Persona = empleadoPlantel.PersonaEP.Cve_Persona,
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
                        Usuario = new Usuario()
                        {
                            Cve_Usuario = empleadoPlantel.PersonaEP.Usuario.Cve_Usuario,
                            IdAlumno = empleadoPlantel.PersonaEP.Usuario.IdAlumno,
                            Nombre_Usuario = empleadoPlantel.PersonaEP.Usuario.Nombre_Usuario,
                            Contrasena = empleadoPlantel.PersonaEP.Usuario.Contrasena,
                            Fecha_Registro = empleadoPlantel.PersonaEP.Usuario.Fecha_Registro,
                            Estatus = empleadoPlantel.PersonaEP.Usuario.Estatus,
                            Alias_Red = empleadoPlantel.PersonaEP.Usuario.Alias_Red
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