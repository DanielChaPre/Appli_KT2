using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MasterPage : ContentPage
    {
        private int tipo_usuario;
        private List<string> listaPlantillas = new List<string>();
        private List<string> listaMenu = new List<string>();
        private MainTabbedViewModel mainTabbedViewModel;
        public MasterPage()
        {
            InitializeComponent();
            this.tipo_usuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());
            //AccesoUsuario();
            lblNombreUsuario.Text = App.Current.Properties["nombreUsuario"].ToString();
            mainTabbedViewModel = new MainTabbedViewModel();
            CrearMenuDinamico();
        }

        public void AccesoUsuario()
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

            switch (this.tipo_usuario)
            {
                case 0:
                    btnIniciar.IsVisible = true;
                    btnPerfil.IsVisible = false;
                    btnHistorial.IsVisible = false;
                    btnNotificaciones.IsVisible = false;
                    btnSuredsu.IsVisible = true;
                    break;
                case 1:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = false;
                    btnNotificaciones.IsVisible = false;
                    btnSuredsu.IsVisible = true;
                    break;
                case 2:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 3:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 4:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 5:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 6:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                case 7:
                    btnIniciar.IsVisible = false;
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
                default:
                    btnPerfil.IsVisible = true;
                    btnHistorial.IsVisible = true;
                    btnNotificaciones.IsVisible = true;
                    btnSuredsu.IsVisible = true;
                    break;
            }
        }

        public void CrearMenuDinamico()
        {
            try
            {
                switch (this.tipo_usuario)
                {
                    case 0:
                        btnIniciar.IsVisible = true;
                        btnPerfil.IsVisible = false;
                        btnHistorial.IsVisible = false;
                        btnNotificaciones.IsVisible = false;
                        btnSuredsu.IsVisible = true;
                        break;
                    case 1:
                        CrearMenuUsuario();
                        break;
                    case 2:
                        CrearMenuAlumno();
                        break;
                    case 3:
                        CrearMenuUsuario();
                        break;
                    case 4:
                        CrearMenuUsuario();
                        break;
                    case 5:
                        CrearMenuUsuario();
                        break;
                    case 6:
                        CrearMenuUsuario();
                        break;
                    case 7:
                        CrearMenuUsuario();
                        break;
                    default:
                        CrearMenuUsuario();
                        break;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async void BtnCliente_Click(object sender, EventArgs e)
        {
            
            await Application.Current.MainPage.DisplayAlert("Prueba", "Prueba del click", "Aceptar");
        }

        private void OcultarMenu()
        {
            btnIniciar.IsVisible = false;
            btnPerfil.IsVisible = false;
            btnHistorial.IsVisible = false;
            btnNotificaciones.IsVisible = false;
            btnSuredsu.IsVisible = false;
        }

        public async void CrearMenuAlumno()
        {
                try
                {
                OcultarMenu();
                    await ObtenerPlantillaAlumno();
                for (int i = 0; i < listaPlantillas.Count; i++)
                {
                    LlenarListaMenuA(listaPlantillas[i]);
                }
                for (int i = 0; i < listaMenu.Count; i++)
                {
                    CrearMenuA(listaMenu[i], i);
                }
            }
                catch (Exception)
                {

                    throw;
                }
        }

        public async void CrearMenuUsuario()
        {
            try
            {
                OcultarMenu();
                await ObtenerPlantillaUsuario();
                for (int i = 0; i < listaPlantillas.Count; i++)
                {
                    LlenarListaMenu(listaPlantillas[i]);
                }
                for (int i = 0; i < listaMenu.Count; i++)
                {
                    CrearMenuU(listaMenu[i], i);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void LlenarListaMenu(string plantilla)
        {
            switch (plantilla)
            {
                case "Eventos históricos":
                    listaMenu.Add(plantilla);
                    break;
                case "Mi perfil":
                    listaMenu.Add(plantilla);
                    break;
                case "Buzon de Notificaciones":
                    listaMenu.Add(plantilla);
                    break;
                case "Encuesta SUREDSU":
                    listaMenu.Add(plantilla);
                    break;
                default:
                    break;
            }
        }

        public void LlenarListaMenuA(string plantilla)
        {
            switch (plantilla)
            {
                case "Historial web":
                    listaMenu.Add(plantilla);
                    break;
                case "Mi perfil":
                    listaMenu.Add(plantilla);
                    break;
                case "Buzón de notificaciones":
                    listaMenu.Add(plantilla);
                    break;
                case "Encuesta SUREDSU":
                    listaMenu.Add(plantilla);
                    break;
                default:
                    break;
            }
        }

        public void CrearMenuU(string plantilla, int posicion)
        {
            switch (plantilla)
            {
                case "Historial web":
                    // listaMenu.Add(plantilla);
                    Button btnEventHistorial = new Button();
                    btnEventHistorial.ImageSource = "ic_calendar_text_outline.png";
                    btnEventHistorial.Text = plantilla;
                    btnEventHistorial.BindingContext = mainTabbedViewModel;
                    btnEventHistorial.Command = mainTabbedViewModel.IrHistorialCommand;
                    if((posicion%2)==0)
                        btnEventHistorial.BackgroundColor = Color.FromHex("#1b213c");
                    else
                        btnEventHistorial.BackgroundColor = Color.FromHex("#0a225a");
                    btnEventHistorial.TextColor = Color.White;

                    stlMenu.Children.Add(btnEventHistorial);
                    break;
                case "Mi perfil":
                    Button btnPerfiles = new Button();
                    btnPerfiles.ImageSource = "ic_cuenta.png";
                    btnPerfiles.Text = plantilla;
                    btnPerfiles.BindingContext = mainTabbedViewModel;
                    btnPerfiles.Command = mainTabbedViewModel.IrPerfilCommand;
                    if ((posicion % 2) == 0)
                        btnPerfiles.BackgroundColor = Color.FromHex("#1b213c");
                    else
                        btnPerfiles.BackgroundColor = Color.FromHex("#0a225a");
                    btnPerfiles.TextColor = Color.White;

                    stlMenu.Children.Add(btnPerfiles);
                    break;
                case "Notificaciones":
                    Button btnNotificacion = new Button();
                    btnNotificacion.ImageSource = "ic_notifications.png";
                    btnNotificacion.Text = plantilla;
                    btnNotificacion.BindingContext = mainTabbedViewModel;
                    btnNotificacion.Command = mainTabbedViewModel.IrNotificacionesCommand;
                    if ((posicion % 2) == 0)
                        btnNotificacion.BackgroundColor = Color.FromHex("#1b213c");
                    else
                        btnNotificacion.BackgroundColor = Color.FromHex("#0a225a");
                    btnNotificacion.TextColor = Color.White;

                    stlMenu.Children.Add(btnNotificacion);
                    break;
                case "Encuesta SUREDSU":
                    Button btnEnlaceSuredsu = new Button();
                    btnEnlaceSuredsu.ImageSource = "ic_school_outline.png";
                    btnEnlaceSuredsu.Text = plantilla;
                    btnEnlaceSuredsu.BindingContext = mainTabbedViewModel;
                    btnEnlaceSuredsu.Command = mainTabbedViewModel.IrSuredsuCommand;
                    if ((posicion % 2) == 0)
                        btnEnlaceSuredsu.BackgroundColor = Color.FromHex("#1b213c");
                    else
                        btnEnlaceSuredsu.BackgroundColor = Color.FromHex("#0a225a");
                    btnEnlaceSuredsu.TextColor = Color.White;

                    stlMenu.Children.Add(btnEnlaceSuredsu);
                    break;
                default:
                    break;
            }
        }

        public void CrearMenuA(string plantilla, int posicion)
        {
            switch (plantilla)
            {
                case "Historial web":
                    // listaMenu.Add(plantilla);
                    Button btnEventHistorial = new Button();
                    btnEventHistorial.ImageSource = "ic_calendar_text_outline.png";
                    btnEventHistorial.Text = plantilla;
                    btnEventHistorial.BindingContext = mainTabbedViewModel;
                    btnEventHistorial.Command = mainTabbedViewModel.IrHistorialCommand;
                    if ((posicion % 2) == 0)
                        btnEventHistorial.BackgroundColor = Color.FromHex("#1b213c");
                    else
                        btnEventHistorial.BackgroundColor = Color.FromHex("#0a225a");
                    btnEventHistorial.TextColor = Color.White;

                    stlMenu.Children.Add(btnEventHistorial);
                    break;
                case "Mi perfil":
                    Button btnPerfiles = new Button();
                    btnPerfiles.ImageSource = "ic_cuenta.png";
                    btnPerfiles.Text = plantilla;
                    btnPerfiles.BindingContext = mainTabbedViewModel;
                    btnPerfiles.Command = mainTabbedViewModel.IrPerfilCommand;
                    if ((posicion % 2) == 0)
                        btnPerfiles.BackgroundColor = Color.FromHex("#1b213c");
                    else
                        btnPerfiles.BackgroundColor = Color.FromHex("#0a225a");
                    btnPerfiles.TextColor = Color.White;

                    stlMenu.Children.Add(btnPerfiles);
                    break;
                case "Buzón de notificaciones":
                    Button btnNotificacion = new Button();
                    btnNotificacion.ImageSource = "ic_notifications.png";
                    btnNotificacion.Text = plantilla;
                    btnNotificacion.BindingContext = mainTabbedViewModel;
                    btnNotificacion.Command = mainTabbedViewModel.IrNotificacionesCommand;
                    if ((posicion % 2) == 0)
                        btnNotificacion.BackgroundColor = Color.FromHex("#1b213c");
                    else
                        btnNotificacion.BackgroundColor = Color.FromHex("#0a225a");
                    btnNotificacion.TextColor = Color.White;

                    stlMenu.Children.Add(btnNotificacion);
                    break;
                case "Encuesta SUREDSU":
                    Button btnEnlaceSuredsu = new Button();
                    btnEnlaceSuredsu.ImageSource = "ic_school_outline.png";
                    btnEnlaceSuredsu.Text = plantilla;
                    btnEnlaceSuredsu.BindingContext = mainTabbedViewModel;
                    btnEnlaceSuredsu.Command = mainTabbedViewModel.IrSuredsuCommand;
                    if ((posicion % 2) == 0)
                        btnEnlaceSuredsu.BackgroundColor = Color.FromHex("#1b213c");
                    else
                        btnEnlaceSuredsu.BackgroundColor = Color.FromHex("#0a225a");
                    btnEnlaceSuredsu.TextColor = Color.White;

                    stlMenu.Children.Add(btnEnlaceSuredsu);
                    break;
                default:
                    break;
            }
        }

        public async Task ObtenerPlantillaUsuario()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var cveUsuario = Convert.ToInt32(App.Current.Properties["cveUsuario"].ToString());
                if (cveUsuario == 0)
                {
                    return;
                }
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerPlantillaUsuario+cveUsuario));
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaPlant = JsonConvert.DeserializeObject<List<string>>(content);
                    //var lstplantillas = new List<string>();
                    listaPlantillas = null;
                    listaPlantillas = listaPlant;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task ObtenerPlantillaAlumno()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var idAlumno = Convert.ToInt32(App.Current.Properties["idAlumno"].ToString());
                if (idAlumno == 0)
                {
                    return;
                }
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerPlantillaAlumno + idAlumno));
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaPlant = JsonConvert.DeserializeObject<List<string>>(content);
                    //var lstplantillas = new List<string>();
                    listaPlantillas = null;
                    listaPlantillas = listaPlant;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}