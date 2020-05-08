using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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
        PerfilPadreViewModel perfilPadre = new PerfilPadreViewModel();
        EstadosViewModel estadosViewModel = new EstadosViewModel();
        MunicipioViewModel municipiosViewModel;
        ColoniaViewModel coloniaViewModel;
        PerfilPadreViewModel perfil = new PerfilPadreViewModel();
        Estados estadoS;
        Colonias entColonia = new Colonias();
        Municipios entmunicipio = new Municipios();
        List<Municipios> listamuni = new List<Municipios>();
        bool cp = true;
        private int tipoUsuario = 0;
        private HttpClient _client;
        private ConexionWS conexion;
        private int idEstado;
        private int idAlumno;


        public RegisterPage()
        {
           
            try
            {
                InitializeComponent();
                this.tipoUsuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());
                OcultarPerfiles();
            }
            catch (KeyNotFoundException ex)
            {
            }
           
        }

        protected override void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                FiltrarPerfil();
                txtCodigoPostal.TextChanged += SeleccionarColonias;
                //   LlenarListasDireccion();
            }
            catch (Exception ex)
            {
            }
           
        }

        public void FiltrarPerfil()
        {
            try
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
                        var preguntarPadre = Convert.ToInt32(App.Current.Properties["padreFamilia"].ToString());
                        if (preguntarPadre == 1)
                        {
                            framePreguntaPadre.IsVisible = true;
                            lytPadre.IsVisible = true;
                            lytCurpHijo.IsVisible = false;
                            btnSi.Clicked += BtnSi_Clicked;
                            btnNo.Clicked += BtnNo_Clicked;
                        }
                        else
                        {
                            CargarPerfilGeneral();
                        }
                        
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
            catch (Exception ex)
            {
               App.Current.Properties["padreFamilia"] = 1;
                FiltrarPerfil();
            }

        }

        #region Carga de Perfiles

        public void OcultarPerfiles()
        {
            try
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
            catch (Exception ex)
            {
            }
           
        }

        public void CargarPerfilGeneral()
        {
            try
            {
                perfilGeneral = new PerfilGeneralViewModel();
                frameUsuarioGeneral.BindingContext = perfilGeneral;
                frameBotones.BindingContext = perfilGeneral;
                ConsultarUsuarioGeneral();
                framePreguntaPadre.IsVisible = false;
                frameUsuarioGeneral.IsVisible = true;
                frameBotones.IsVisible = true;
            }
            catch (Exception ex)
            {
            }
          
        }

        public async void CargarPerfilAlumno()
        {
            try
            {
                perfilAlumno = new PerfilAlumnoViewModel();
                frameAlumno.BindingContext = perfilAlumno;
                frameBotones.BindingContext = perfilAlumno;
                frameDireccion.BindingContext = perfilAlumno;
                OcultarPerfiles();
                frameAlumno.IsVisible = true;
                await perfilAlumno.ConsultarAlumno();
                AsignarDireccion();
                LlenarListasDireccion();
                frameBotones.IsVisible = true;
                frameDireccion.IsVisible = true;
               
               // AsignarDireccion();
                //Crear un metodo que me extraiga la colonia, el codigo postal y el municipio y que lo inserte en el formulario
            }
            catch (Exception ex)
            {
            }
           
        }

        public async void ConsultarAlumno()
        {
            if (await perfilAlumno.ConsultarAlumno())
            {
                ConsultarAlumno();
            }
        }

        private void AsignarDireccion()
        {
            try
            {
                var municipio = perfilAlumno.SelectedMunicipio;
                var CP = App.Current.Properties["acp"].ToString();
              //  var colonia = perfilAlumno.SelectedColonia;

                // pMunicipio.SelectedIndex = municipio.IdMunicipio;
                //  pMunicipio.SelectedItem = municipio;
                txtCodigoPostal.Text = CP;
                pColonia.SelectedItem = entColonia;
                pMunicipio.SelectedItem = entmunicipio;
            }
            catch (Exception ex)
            {
            }
          
        }
        public async void CargarPerfilPadre()
        {
            try
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
            catch (Exception ex)
            {
            }
           
        }

        public void VerificarCurpHijo()
        {
            try
            {
                lytPadre.IsVisible = false;
                lytCurpHijo.IsVisible = true;
                btnBuscarCurp.Clicked += BuscarCurp;
            }
            catch (Exception ex)
            {
            }
            
        }

        private async void BuscarCurp(object sender, EventArgs e)
        {
            try
            {
                var curphijo = txtCurpHijoPadre.Text;
                idAlumno = await perfilPadre.BuscarAlumnoCurp(curphijo);
                CargarPerfilPadre();
            }
            catch (Exception ex)
            {
            }
          
        }

        public async void CargarPerfilEmpleadoPlantel()
        {
            try
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
            catch (Exception ex)
            {
            }

           
        }

        public void LlenarDatosEmpleadoPlantel()
        {
            try
            {
                entNombre.Text = perfilEmpleadoPlantel.Persona.Nombre;
                entApePat.Text = perfilEmpleadoPlantel.Persona.Apellido_Paterno;
                entApeMat.Text = perfilEmpleadoPlantel.Persona.Apellido_Materno;
                entNumTel.Text = perfilEmpleadoPlantel.Persona.Numero_Telefono;
            }
            catch (Exception ex)
            {
            }
            
        }

        public void LlenarDatosEmpleado()
        {
            try
            {
                entNombreEmpleado.Text = perfilEmpleado.Persona.Nombre;
                entApePatEmpleado.Text = perfilEmpleado.Persona.Apellido_Paterno;
                entApeMatEmpleado.Text = perfilEmpleado.Persona.Apellido_Materno;
                entNumTelEmpleado.Text = perfilEmpleado.Persona.Numero_Telefono;
            }
            catch (Exception ex)
            {
            }
           
        }

        public void LlenarPadreFamilia()
        {
            try
            {
                txtNombrePadre.Text = perfilPadre.Persona.Nombre;
                txtApePatPadre.Text = perfilPadre.Persona.Apellido_Paterno;
                txtApeMatPadre.Text = perfilPadre.Persona.Apellido_Materno;
                txtNumTelPadre.Text = perfilPadre.Persona.Numero_Telefono;
            }
            catch (Exception ex)
            {
            }
           
        }
        public async void CargarPerfilEmpleado()
        {
            try
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
            catch (Exception ex)
            {
            }
          
        }

        #endregion

        private async void ConsultarUsuarioGeneral()
        {
            try
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
            catch (Exception ex)
            {
            }
           
        }

        public void LlenarListasDireccion()
        {
            try
            {
                LlenarEstados();
                LlenarMunicipios();
                LlenarColonias();
            }
            catch (Exception ex)
            {
            }
           
        }

        private async Task LlenarEstados()
        {
            try
            {
                if (await estadosViewModel.ObtenerEstados())
                {
                    var index = 0;
                    foreach (var item in estadosViewModel.ListEstados)
                    {
                        if (item.IdEstado == Convert.ToInt32(App.Current.Properties["aestado"].ToString()))
                        {
                            break;
                        }
                        index++;
                    }
                    pEstados.ItemsSource = estadosViewModel.ListEstados;
                    pEstados.ItemDisplayBinding = new Binding("NombreEstado");
                    pEstados.SelectedIndex = index;
                    pEstados.SelectedIndexChanged += SeleccionarEstados;
                    //Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                    //{

                    //    while (estadosViewModel.ListEstados.Count != 0)
                    //    {

                    //    }
                    //    return true; // True = Repeat again, False = Stop the timer
                    //});
                }
               
            }
            catch (Exception ex)
            {
            }
           
        }

        private async Task LlenarMunicipios()
        {
            try
            {
                municipiosViewModel = new MunicipioViewModel();
                if (await municipiosViewModel.ObtenerTodosMunicipios())
                {
                    if (idEstado == 0)
                    {
                        var municipio = from a in municipiosViewModel.ListMunicipios where a.idEstado == 11 select a;
                        var listamunicipios = new List<Municipios>();
                          listamunicipios = municipio.Cast<Municipios>().ToList();
                        listamuni = listamunicipios;
                        var index = 0;
                        foreach (var item in municipio)
                        {
                            if (item.idMunicipio == Convert.ToInt32(App.Current.Properties["amunicipio"].ToString()))
                            {
                                entmunicipio = item;
                                break;
                            }
                            index++;
                        }
                        pMunicipio.ItemsSource = listamunicipios;
                        pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");
                        pMunicipio.SelectedIndex = index;
                    }
                    else
                    {
                        var municipio = from a in municipiosViewModel.ListMunicipios where a.idEstado == idEstado select a;
                        pMunicipio.ItemsSource = municipio.Cast<Municipios>().ToList();
                        pMunicipio.ItemDisplayBinding = new Binding("NombreMunicipio");
                    }
                }
            }
            catch (Exception ex)
            {
            }
         
        }

        private async Task LlenarColonias()
        {
            try
            {
               // txtCodigoPostal.Text = App.Current.Properties["acp"].ToString();
                if (txtCodigoPostal.Text.Length == 5 && cp)
                {
                    var codPost = txtCodigoPostal.Text;
                    coloniaViewModel = new ColoniaViewModel(codPost);
                    cp = false;
                }
                if (await coloniaViewModel.ObtenerColonias())
                {
                    var index = 0;
                    foreach (var item in coloniaViewModel.ListColonias)
                    {
                        if (item.idColonia == Convert.ToInt32(App.Current.Properties["acolonia"].ToString()))
                        {
                            entColonia = item;
                            break;
                        }
                        index++;
                    }
                    pColonia.ItemsSource = coloniaViewModel.ListColonias;
                    pColonia.ItemDisplayBinding = new Binding("NombreColonia");
                    pColonia.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
            }
            
        }

        private async Task LlenarColonias2()
        {
            try
            {
                // txtCodigoPostal.Text = App.Current.Properties["acp"].ToString();
                if (txtCodigoPostal.Text.Length == 5 && cp)
                {
                    var codPost = txtCodigoPostal.Text;
                    coloniaViewModel = new ColoniaViewModel(codPost);
                    cp = false;
                }
                if (await coloniaViewModel.ObtenerColonias())
                {
                    var index = 0;
                    foreach (var item in coloniaViewModel.ListColonias)
                    {
                        if (item.idColonia == Convert.ToInt32(App.Current.Properties["acolonia"].ToString()))
                        {
                            entColonia = item;
                            break;
                        }
                        index++;
                    }
                    var idmunicipio = coloniaViewModel.ListColonias[0].idMunicipio;

                        for (int i = 0; i < listamuni.Count; i++)
                        {
                            if (listamuni[i].idMunicipio == idmunicipio)
                            {
                                pMunicipio.SelectedItem = listamuni[i];
                                break;
                            }
                        }

                    pColonia.ItemsSource = coloniaViewModel.ListColonias;
                    pColonia.ItemDisplayBinding = new Binding("NombreColonia");
                    pColonia.SelectedIndex = index;
                }
            }
            catch (Exception ex)
            {
            }

        }

        private void SeleccionarEstados(object sender, EventArgs e)
        {
            try
            {
                var estado = (Estados)pEstados.SelectedItem;
                //  App.Current.Properties["NombreEstado"] = estado.NombreEstado;
                idEstado = estado.IdEstado;
                LlenarMunicipios();
            }
            catch (Exception ex)
            {
            }
           
        }

        private async void SeleccionarColonias (object sender, EventArgs e)
        {
            try
            {
                if (txtCodigoPostal.Text.Length == 5)
                {
                    cp = true;
                    await LlenarColonias2();
                }

            }
            catch (Exception ex)
            {
            }
          
        }

        private void BtnNo_Clicked(object sender, EventArgs e)
        {
            try
            {
                CargarPerfilGeneral();
                App.Current.Properties["padreFamilia"] = 0;
            }
            catch (Exception ex)
            {
            }
          
        }

        private void BtnSi_Clicked(object sender, EventArgs e)
        {
            try
            {
                VerificarCurpHijo();
                //CargarPerfilPadre();
            }
            catch (Exception ex)
            {
            }
            
        }

    }
}