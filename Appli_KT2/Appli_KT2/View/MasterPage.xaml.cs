using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using Plugin.LocalNotification;
using Plugin.LocalNotifications;
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
        private List<PlantillaEnlace> listaPlantillas2 = new List<PlantillaEnlace>();
        private List<string> listaMenu = new List<string>();
        private MainTabbedViewModel mainTabbedViewModel;
        private NotificacionesViewModel notificacionesViewModel = new NotificacionesViewModel();
        private List<Notificaciones> lstnotificaciones = new List<Notificaciones>();
        int notificacionnoleidas;
        public MasterPage()
        {
            try
            {
                InitializeComponent();
               
                this.tipo_usuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());
                //AccesoUsuario();
                lblNombreUsuario.Text = App.Current.Properties["nombreUsuario"].ToString();
              
                mainTabbedViewModel = new MainTabbedViewModel();
                CrearMenuDinamico();
                lblrolUsuario.Text = App.Current.Properties["rolUsuario"].ToString();

            }
            catch (Exception ex)
            {
            }
          
        }

        public async Task ConsultarNotificaciones()
        {
            try
            {
               
                var _client = new HttpClient();
               var conexion = new ConexionWS();
                var cveUsuario = Xamarin.Forms.Application.Current.Properties["cveUsuario"];
                var url = conexion.URL + "" + conexion.ObtenerNotificacionesNoLeidas1 + cveUsuario;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lstNotificaciones = JsonConvert.DeserializeObject<List<Notificaciones>>(content);
                    notificacionnoleidas = lstNotificaciones.Count;
                    for (int i = 0; i < lstNotificaciones.Count; i++)
                    {
                        var entNotificaciones = new Notificaciones()
                        {
                            Cve_notificacion = lstNotificaciones[i].Cve_notificacion,
                            Cve_categoria = lstNotificaciones[i].Cve_categoria,
                            Cve_tipo_notificacion = lstNotificaciones[i].Cve_tipo_notificacion,
                            Fecha_notificacion = lstNotificaciones[i].Fecha_notificacion,
                            Hora_notificacion = lstNotificaciones[i].Hora_notificacion,
                            Responsable = lstNotificaciones[i].Responsable,
                            Texto = lstNotificaciones[i].Texto,
                            Titulo = lstNotificaciones[i].Titulo,
                            Url = lstNotificaciones[i].Url,
                        };
                        lstnotificaciones.Add(entNotificaciones);
                    }
                    return;
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }

        public async void MostrarNotificacionesNoLeidas()
        {
            await ConsultarNotificaciones();

            var notification = new NotificationRequest
            {
                NotificationId = 100,
                Title = "Notificaciones no leídas",
                Description = notificacionnoleidas.ToString(),
                ReturningData = "Dummy data", // Returning data when tapped on notification.
                NotifyTime = DateTime.Now.AddSeconds(1) // Used for Scheduling local notification, if not specified notification will show immediately.
            };
            NotificationCenter.Current.Show(notification);
            //CrossLocalNotifications.Current.Show("Notificaciones sin leer", "5");
            NotificationCenter.Current.NotificationTapped += OnLocalNotificationTapped;
        }

        private void OnLocalNotificationTapped(NotificationTappedEventArgs e)
        {
            App.Current.MainPage.Navigation.PushAsync(new NotificacionesPage());
        }

    //    public void AccesoUsuario()
    //    {
    //        try
    //        {
    //            /*
    //* 1: Usuario general
    //* 2: Estudiante
    //* 3: empleado
    //* 4: plantel
    //* 5: Docente
    //* 6: Directivo
    //* 7: Padre Familia
    //* **/

    //            switch (this.tipo_usuario)
    //            {
    //                case 0:
    //                    btnIniciar.IsVisible = true;
    //                    btnPerfil.IsVisible = false;
    //                    btnHistorial.IsVisible = false;
    //                    btnNotificaciones.IsVisible = false;
    //                    btnSuredsu.IsVisible = true;
    //                    break;
    //                case 1:
    //                    App.Current.Properties["rolUsuario"] = "Usuario General";
    //                    btnIniciar.IsVisible = false;
    //                    btnPerfil.IsVisible = true;
    //                    btnHistorial.IsVisible = false;
    //                    btnNotificaciones.IsVisible = false;
    //                    btnSuredsu.IsVisible = true;
    //                    break;
    //                case 2:
    //                    App.Current.Properties["rolUsuario"] = "Estudiante";
    //                    btnIniciar.IsVisible = false;
    //                    btnPerfil.IsVisible = true;
    //                    btnHistorial.IsVisible = true;
    //                    btnNotificaciones.IsVisible = true;
    //                    btnSuredsu.IsVisible = true;
    //                    break;
    //                case 3:
    //                    App.Current.Properties["rolUsuario"] = "Empleado";
    //                    btnIniciar.IsVisible = false;
    //                    btnPerfil.IsVisible = true;
    //                    btnHistorial.IsVisible = true;
    //                    btnNotificaciones.IsVisible = true;
    //                    btnSuredsu.IsVisible = true;
    //                    break;
    //                case 4:
    //                    App.Current.Properties["rolUsuario"] = "Escuela";
    //                    btnIniciar.IsVisible = false;
    //                    btnPerfil.IsVisible = true;
    //                    btnHistorial.IsVisible = true;
    //                    btnNotificaciones.IsVisible = true;
    //                    btnSuredsu.IsVisible = true;
    //                    break;
    //                case 5:
    //                    App.Current.Properties["rolUsuario"] = "Docente";
    //                    btnIniciar.IsVisible = false;
    //                    btnPerfil.IsVisible = true;
    //                    btnHistorial.IsVisible = true;
    //                    btnNotificaciones.IsVisible = true;
    //                    btnSuredsu.IsVisible = true;
    //                    break;
    //                case 6:
    //                    App.Current.Properties["rolUsuario"] = "Directivo";
    //                    btnIniciar.IsVisible = false;
    //                    btnPerfil.IsVisible = true;
    //                    btnHistorial.IsVisible = true;
    //                    btnNotificaciones.IsVisible = true;
    //                    btnSuredsu.IsVisible = true;
    //                    break;
    //                case 7:
    //                    App.Current.Properties["rolUsuario"] = "Padre de Familia";
    //                    btnIniciar.IsVisible = false;
    //                    btnPerfil.IsVisible = true;
    //                    btnHistorial.IsVisible = true;
    //                    btnNotificaciones.IsVisible = true;
    //                    btnSuredsu.IsVisible = true;
    //                    break;
    //                default:
    //                    App.Current.Properties["rolUsuario"] = "";
    //                    btnPerfil.IsVisible = true;
    //                    btnHistorial.IsVisible = true;
    //                    btnNotificaciones.IsVisible = true;
    //                    btnSuredsu.IsVisible = true;
    //                    break;
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //        }
   
    //    }

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
                        App.Current.Properties["rolUsuario"] = "Usuario General";
                        CrearMenuUsuario();
                        MostrarNotificacionesNoLeidas();
                        break;
                    case 2:
                        App.Current.Properties["rolUsuario"] = "Estudiante";
                        CrearMenuAlumno();
                        MostrarNotificacionesNoLeidas();
                        break;
                    case 3:
                        App.Current.Properties["rolUsuario"] = "Administrador";
                        CrearMenuUsuario();
                        MostrarNotificacionesNoLeidas();
                        break;
                    case 4:
                        App.Current.Properties["rolUsuario"] = "Escuela";
                        CrearMenuUsuario();
                        MostrarNotificacionesNoLeidas();
                        break;
                    case 5:
                        App.Current.Properties["rolUsuario"] = "Docente";
                        CrearMenuUsuario();
                        MostrarNotificacionesNoLeidas();
                        break;
                    case 6:
                        App.Current.Properties["rolUsuario"] = "Directivo";
                        CrearMenuUsuario();
                        MostrarNotificacionesNoLeidas();
                        break;
                    case 7:
                        App.Current.Properties["rolUsuario"] = "Padre de Familia";
                        CrearMenuUsuario();
                        MostrarNotificacionesNoLeidas();
                        break;
                    default:
                        App.Current.Properties["rolUsuario"] = "";
                        CrearMenuUsuario();
                        MostrarNotificacionesNoLeidas();
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }

        private async void BtnCliente_Click(object sender, EventArgs e)
        {

            try
            {
                await Application.Current.MainPage.DisplayAlert("Prueba", "Prueba del click", "Aceptar");
            }
            catch (Exception ex)
            {
            }

            
        }

        private void OcultarMenu()
        {
            try
            {
                btnIniciar.IsVisible = false;
                btnPerfil.IsVisible = false;
                btnHistorial.IsVisible = false;
                btnNotificaciones.IsVisible = false;
                btnSuredsu.IsVisible = false;
            }
            catch (Exception ex)
            {
            }
           
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
                await ObtenerPlantillaUsuarioEnlace();
            }
            catch (Exception)
            {
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
                await ObtenerPlantillaUsuarioEnlace();
            }
            catch (Exception)
            {
            }
        }

        public void LlenarListaMenu(string plantilla)
        {
            try
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
            catch (Exception ex)
            {
            }
            
        }

        public void LlenarListaMenuA(string plantilla)
        {
            try
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
            catch (Exception ex)
            {
            }
         
        }

        public void CrearMenuU(string plantilla, int posicion)
        {
            try
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
            catch (Exception ex)
            {
            }
       
        }

        public void CrearMenuA(string plantilla, int posicion)
        {
            try
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
            catch (Exception ex)
            {
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
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerPlantillaUsuario + cveUsuario));
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

            }
        }

        public async Task ObtenerPlantillaUsuarioEnlace()
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
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerPlantillaEnlace + cveUsuario));
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaPlant = JsonConvert.DeserializeObject<List<PlantillaEnlace>>(content);
                    //var lstplantillas = new List<string>();
                    listaPlantillas2 = null;
                    listaPlantillas2 = listaPlant;
                    GenerarMenuEnlace();
                }
            }
            catch (Exception ex)
            {
            }
        }

        private void GenerarMenuEnlace()
        {
            try
            {
                for (int i = 0; i < listaPlantillas2.Count; i++)
                {
                    Button btnEnlace = new Button();
                    btnEnlace.ImageSource = "ic_school_outline.png";
                    btnEnlace.Text = listaPlantillas2[i].nombre;
                    btnEnlace.BindingContext = mainTabbedViewModel;
                    if ((i % 2) == 0)
                        btnEnlace.BackgroundColor = Color.FromHex("#1b213c");
                    else
                        btnEnlace.BackgroundColor = Color.FromHex("#0a225a");
                    btnEnlace.TextColor = Color.White;
                    btnEnlace.Clicked += (object sender, EventArgs e) =>
                    {
                        var plantilla = btnEnlace.Text;
                        for (int j = 0; j < listaPlantillas2.Count; j++)
                        {

                            if (plantilla.Equals(listaPlantillas2[j].nombre))
                            {
                                //App.Current.MainPage.DisplayAlert("prueba", listaPlantillas2[j].nombre, "Aceptar");
                                App.Current.MainPage.Navigation.PushAsync(new EnlacePage(listaPlantillas2[j].ruta));
                            }
                        }
                    };
                    stlMenu.Children.Add(btnEnlace);
                }
            }
            catch (Exception ex)
            {
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
            }
        }
    }

    public class PlantillaEnlace
    {
        public string nombre { get; set; }
        public string ruta { get; set; }
    }
}