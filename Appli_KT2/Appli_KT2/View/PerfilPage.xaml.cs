﻿using Appli_KT2.Model;
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
        PerfilAlumnoViewModel perfilAlumno;
        PerfilEmpleadoPlantelViewModel perfilEmpleadoPlantel;
        PerfilEmpleadoViewModel perfilEmpleado;
        PerfilPadreViewModel perfilPadre;
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


        protected override void OnAppearing()
        {
            base.OnAppearing();
            FiltrarPerfil();
            LlenarListasDireccion();
        }

        public void FiltrarPerfil()
        {
             /*
              * 1: Usuario general
              * 2: Estudiante
              * 3: empleado
              * 4: plantel
              * 5: Docente
              * 6: Directivo
              * 7: Padre Familia
              * **/
            switch (this.tipoUsuario)
            {
                case 1:
                    //Usuario General
                    framePreguntaPadre.IsVisible = true;
                    lytPadre.IsVisible = true;
                    lytCurpHijo.IsVisible = false;
                    btnSi.Clicked += BtnSi_Clicked;
                    btnNo.Clicked += BtnNo_Clicked;
                    break;
                case 2://Alumno
                    OcultarPerfiles();
                    CargarPerfilAlumno();
                    break;
                case 3://Empleado
                    //ConsultarEmpleado();
                    //frameDireccion.BindingContext = new PerfilEmpleadoViewModel();
                    //LlenarListasDireccion();
                    //OcultarPerfiles();
                    break;
                case 4://Plantel
                    //LlenarListasDireccion();
                    //frameDireccion.BindingContext = new PerfilEmpleadoPlantelViewModel();
                    break;
                case 5://Docente
                    //frameDireccion.BindingContext = new PerfilPadreViewModel();
                    //LlenarListasDireccion();
                    break;
                case 6://Direcivo

                    break;
                case 7://Padre Familia
                    CargarPerfilPadre();
                    break;
                default:
                    //lytPadre.IsVisible = false;
                    //lytCurpHijo.IsVisible = true;
                    ////ConsultarPadreFamilia();
                    //framePreguntaPadre.IsVisible = false;
                    //framePadre.IsVisible = true;
                    break;
            }
        }

        
        #region Carga de Perfiles

        public void OcultarPerfiles()
        {
            framePreguntaPadre.IsVisible = true;
            lytPadre.IsVisible = false;
            lytCurpHijo.IsVisible = false;
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

        public async void CargarPerfilAlumno()
        {
            perfilAlumno = new PerfilAlumnoViewModel();
            frameAlumno.BindingContext = perfilAlumno;
            frameBotones.BindingContext = perfilAlumno;
            frameDireccion.BindingContext = perfilAlumno;
            frameAlumno.IsVisible = true;
            frameBotones.IsVisible = true;
            frameDireccion.IsVisible = true;
           await perfilAlumno.ConsultarAlumno();
            LlenarListasDireccion();
        }

        public async void CargarPerfilPadre()
        {
            perfilPadre = new PerfilPadreViewModel();
            framePadre.BindingContext = perfilPadre;
            frameBotones.BindingContext = perfilPadre;
            framePadre.IsVisible = true;
            frameBotones.IsVisible = true;
            lytPadre.IsVisible = false;
            lytCurpHijo.IsVisible = true;
            await perfilPadre.ConsultarPadreFamilia();
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
                    if (municipiosViewModel == null)
                    {
                        municipiosViewModel = new MunicipioViewModel();
                    }
                    else
                    {
                        municipiosViewModel.ObtenerMunicipios();
                        if (municipiosViewModel.ListMunicipios != null || municipiosViewModel.ListMunicipios.Count != 0)
                        {
                            pMunicipio.ItemsSource = municipiosViewModel.ListMunicipios;
                            pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");
                            return true;
                        }
                        return true; // True = Repeat again, False = Stop the timer
                    }
                    return true;
                  
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
            CargarPerfilPadre();
        }

        #region Consultas Perfiles

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
                if (empleado != null)
                {

                }
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
        #endregion
    }
}