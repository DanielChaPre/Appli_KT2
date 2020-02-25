using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        private int idEstado;
        private int idAlumno;


        public RegisterPage()
        {
            InitializeComponent();
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
            txtCodigoPostal.TextChanged += SeleccionarColonias;
            //   LlenarListasDireccion();
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
                    OcultarPerfiles();
                    CargarPerfilEmpleado();
                    break;
                case 4://Plantel
                    break;
                case 5://Docente
                    OcultarPerfiles();
                    CargarPerfilEmpleadoPlantel();
                    break;
                case 6://Direcivo
                    CargarPerfilEmpleadoPlantel();
                    break;
                case 7://Padre Familia
                    CargarPerfilPadre();
                    break;
                default:
                    break;
            }
        }

        #region Carga de Perfiles

        public void OcultarPerfiles()
        {
            framePreguntaPadre.IsVisible = false;
            lytPadre.IsVisible = false;
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
        }

        public async void CargarPerfilAlumno()
        {
            perfilAlumno = new PerfilAlumnoViewModel();
            frameAlumno.BindingContext = perfilAlumno;
            frameBotones.BindingContext = perfilAlumno;
            frameDireccion.BindingContext = perfilAlumno;
            OcultarPerfiles();
            frameAlumno.IsVisible = true;
            await perfilAlumno.ConsultarAlumno();
            frameBotones.IsVisible = true;
            frameDireccion.IsVisible = true;
            LlenarListasDireccion();
            AsignarDireccion();
            //Crear un metodo que me extraiga la colonia, el codigo postal y el municipio y que lo inserte en el formulario
        }

        private void AsignarDireccion()
        {
            var municipio = perfilAlumno.SelectedMunicipio;
            var CP = perfilAlumno.SelectedColonia.Cp;
            var colonia = perfilAlumno.SelectedColonia;

           // pMunicipio.SelectedIndex = municipio.IdMunicipio;
          //  pMunicipio.SelectedItem = municipio;
            txtCodigoPostal.Text = CP;
            pColonia.SelectedItem = colonia;
        }
        public async void CargarPerfilPadre()
        {
            OcultarPerfiles();
            framePadre.BindingContext = perfilPadre;
            frameBotones.BindingContext = perfilPadre;
            framePadre.IsVisible = true;
            
            lytPadre.IsVisible = false;
            if (await perfilPadre.ConsultarPadreFamilia())
            {
                this.actCargaFormPadre.IsVisible = perfilPadre.IsRun;
                this.actCargaFormPadre.IsRunning = perfilPadre.IsRun;
                this.formularioPadreFamilia.IsVisible = perfilPadre.IsVisible;
                this.btnEliminar.IsVisible = perfilPadre.IsAcciones;
                LlenarPadreFamilia();
            }
            else
            {
                this.actCargaFormPadre.IsVisible = perfilPadre.IsRun;
                this.actCargaFormPadre.IsRunning = perfilPadre.IsRun;
                this.formularioPadreFamilia.IsVisible = perfilPadre.IsVisible;
                this.btnEliminar.IsVisible = perfilPadre.IsAcciones;
            }
            frameBotones.IsVisible = true;
        }

        public void VerificarCurpHijo()
        {
            lytPadre.IsVisible = false;
            lytCurpHijo.IsVisible = true;
            btnBuscarCurp.Clicked += BuscarCurp;

            perfilPadre = new PerfilPadreViewModel();
        }

        private async void BuscarCurp(object sender, EventArgs e)
        {
            var curphijo = txtCurpHijoPadre.Text;
            idAlumno = await perfilPadre.BuscarAlumnoCurp(curphijo);
            CargarPerfilPadre();
        }

        public async void CargarPerfilEmpleadoPlantel()
        {
            OcultarPerfiles();
            perfilEmpleadoPlantel = new PerfilEmpleadoPlantelViewModel();
            this.frameEmpleadoPlantel.BindingContext = perfilEmpleadoPlantel;
            this.frameBotones.BindingContext = perfilEmpleadoPlantel;
            frameEmpleadoPlantel.IsVisible = true;
            if (await perfilEmpleadoPlantel.ConsultarEmpleadoPlantel())
            {
                this.actCargaFormEmpleadoP.IsVisible = perfilEmpleadoPlantel.IsRun;
                this.actCargaFormEmpleadoP.IsRunning = perfilEmpleadoPlantel.IsRun;
                this.formularioEmpleadoPlantel.IsVisible = perfilEmpleadoPlantel.IsVisible;
                this.btnEliminar.IsVisible = perfilEmpleadoPlantel.IsAcciones;
                // this.slytInsertar.IsVisible = perfilEmpleadoPlantel.IsInsertar;
                LlenarDatosEmpleadoPlantel();
            }
            else
            {
                this.actCargaFormEmpleadoP.IsVisible = perfilEmpleadoPlantel.IsRun;
                this.actCargaFormEmpleadoP.IsRunning = perfilEmpleadoPlantel.IsRun;
                this.formularioEmpleadoPlantel.IsVisible = perfilEmpleadoPlantel.IsVisible;
                this.btnEliminar.IsVisible = perfilEmpleadoPlantel.IsAcciones;
                //this.slytInsertar.IsVisible = perfilEmpleadoPlantel.IsInsertar;
            }
            frameBotones.IsVisible = true;
            //slytInsertar.IsVisible = Binding(perfilEmpleadoPlantel.IsInsertar);
        }

        public void LlenarDatosEmpleadoPlantel()
        {
            entNombre.Text = perfilEmpleadoPlantel.Persona.Nombre;
            entApePat.Text = perfilEmpleadoPlantel.Persona.Apellido_Paterno;
            entApeMat.Text = perfilEmpleadoPlantel.Persona.Apellido_Materno;
            entNumTel.Text = perfilEmpleadoPlantel.Persona.Numero_Telefono;
        }

        public void LlenarDatosEmpleado()
        {
            entNombreEmpleado.Text = perfilEmpleado.Persona.Nombre;
            entApePatEmpleado.Text = perfilEmpleado.Persona.Apellido_Paterno;
            entApeMatEmpleado.Text = perfilEmpleado.Persona.Apellido_Materno;
            entNumTelEmpleado.Text = perfilEmpleado.Persona.Numero_Telefono;
        }

        public void LlenarPadreFamilia()
        {
            txtNombrePadre.Text = perfilPadre.Persona.Nombre;
            txtApePatPadre.Text = perfilPadre.Persona.Apellido_Paterno;
            txtApeMatPadre.Text = perfilPadre.Persona.Apellido_Materno;
            txtNumTelPadre.Text = perfilPadre.Persona.Numero_Telefono;
        }
        public async void CargarPerfilEmpleado()
        {
            OcultarPerfiles();
            perfilEmpleado = new PerfilEmpleadoViewModel();
            this.frameEmpleado.BindingContext = perfilEmpleado;
            this.frameBotones.BindingContext = perfilEmpleado;
            frameEmpleado.IsVisible = true;
            if (await perfilEmpleado.ConsultarEmpleado())
            {
                this.actCargaFormEmpleado.IsVisible = perfilEmpleado.IsRun;
                this.formularioEmpleado.IsVisible = perfilEmpleado.IsVisible;
                this.btnEliminar.IsVisible = perfilEmpleado.IsAcciones;
                //this.slytInsertar.IsVisible = perfilEmpleado.IsInsertar;
                LlenarDatosEmpleado();
            }
            else
            {
                this.actCargaFormEmpleado.IsVisible = perfilEmpleado.IsRun;
                this.formularioEmpleado.IsVisible = perfilEmpleado.IsVisible;
                this.btnEliminar.IsVisible = perfilEmpleado.IsAcciones;
                // this.slytInsertar.IsVisible = perfilEmpleado.IsInsertar;
            }
            frameBotones.IsVisible = true;
        }

        #endregion

        private async void ConsultarUsuarioGeneral()
        {
            formularioGeneral.IsVisible = false;
            actCargaFormUsuario.IsRunning = true;
            if (await perfilGeneral.ConsultarUsuarioGeneral())
            {
                formularioGeneral.IsVisible = true;
                actCargaFormUsuario.IsVisible = false;
                // slytInsertar.IsVisible = false;
                btnEliminar.IsVisible = true;
            }
            else
            {
                formularioGeneral.IsVisible = true;
                actCargaFormUsuario.IsVisible = false;
                // slytInsertar.IsVisible = true;
                btnEliminar.IsVisible = false;
            }
        }

        public void LlenarListasDireccion()
        {
            LlenarEstados();
            LlenarMunicipios();
            LlenarColonias();
        }

        private void LlenarEstados()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                while (estadosViewModel.ListEstados.Count != 0)
                {
                    var index = 0;
                    foreach (var item in estadosViewModel.ListEstados)
                    {
                        if (item.IdEstado == perfilAlumno.SelectedMunicipio.IdEstado)
                        {
                            break;
                        }
                        index++;
                    }
                    pEstados.ItemsSource = estadosViewModel.ListEstados;
                    pEstados.ItemDisplayBinding = new Binding("NombreEstado");
                    pEstados.SelectedIndex = index;
                    pEstados.SelectedIndexChanged += SeleccionarEstados;
                    return false;
                }
                return true; // True = Repeat again, False = Stop the timer
            });
        }

        private void LlenarMunicipios()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                try
                {
                    if (municipiosViewModel == null)
                    {
                        municipiosViewModel = new MunicipioViewModel();
                        App.Current.Properties["NombreEstado"] = "";
                    }
                    else
                    {
                        municipiosViewModel.ObtenerTodosMunicipios();
                        if (municipiosViewModel.ListMunicipios != null || municipiosViewModel.ListMunicipios.Count != 0)
                        {
                            if (idEstado == 0)
                            {
                                var municipio = from a in municipiosViewModel.ListMunicipios where a.IdEstado == 11 select a;
                                // var indexItem = perfilAlumno.SelectedMunicipio.IdMunicipio;
                                var index = 0;
                                foreach (var item in municipio)
                                {
                                    if (item.IdMunicipio == perfilAlumno.SelectedMunicipio.IdMunicipio)
                                    {
                                        break;
                                    }
                                    index++;
                                }
                              //  var index = from b in municipiosViewModel.ListMunicipios where b.IdMunicipio == perfilAlumno.SelectedMunicipio.IdMunicipio select b;
                                pMunicipio.ItemsSource = municipio.Cast<Municipios>().ToList();
                                pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");
                                pMunicipio.SelectedIndex = index;
                               // pMunicipio.SelectedItem = perfilAlumno.SelectedMunicipio;
                               // pMunicipio.SelectedItem = perfilAlumno.SelectedMunicipio.IdMunicipio;
                               // pMunicipio.SelectedItem = perfilAlumno.SelectedMunicipio.NombreMunicipio;
                                return false;
                            }
                            else
                            {
                                var municipio = from a in municipiosViewModel.ListMunicipios where a.IdEstado == idEstado select a;
                                pMunicipio.ItemsSource = municipio.Cast<Municipios>().ToList();
                                pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");
                                return false;
                            }
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
        }

        private void LlenarColonias()
        {
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
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
                        var index = 0;
                        foreach (var item in coloniaViewModel.ListColonias)
                        {
                            if (item.IdColonia == perfilAlumno.SelectedColonia.IdColonia)
                            {
                                break;
                            }
                            index++;
                        }
                        pColonia.ItemsSource = coloniaViewModel.ListColonias;
                        pColonia.ItemDisplayBinding = new Binding("NombreColonia");
                        pColonia.SelectedIndex = index;
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

        private void SeleccionarEstados(object sender, EventArgs e)
        {
            var estado = (Estados)pEstados.SelectedItem;
          //  App.Current.Properties["NombreEstado"] = estado.NombreEstado;
            idEstado = estado.IdEstado;
            LlenarMunicipios();
        }

        private void SeleccionarColonias (object sender, EventArgs e)
        {
            if (txtCodigoPostal.Text.Length == 5)
            {
                LlenarColonias();
            }
        }

        private void BtnNo_Clicked(object sender, EventArgs e)
        {
            CargarPerfilGeneral();
        }

        private void BtnSi_Clicked(object sender, EventArgs e)
        {
            VerificarCurpHijo();
            //CargarPerfilPadre();
        }

    }
}