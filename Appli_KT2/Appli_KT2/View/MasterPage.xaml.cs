using Appli_KT2.Utils;
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
        public MasterPage()
        {
            InitializeComponent();
            this.tipo_usuario = Convert.ToInt32(App.Current.Properties["tipo_usuario"].ToString());
            AccesoUsuario();
            lblNombreUsuario.Text = App.Current.Properties["nombreUsuario"].ToString();
            //CrearMenuDinamico();
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
                for (int i = 1; i < 5; i++)
                {
                    Button btnPrueba = new Button();
                    btnPrueba.Text = "prueba " + i;
                    btnPrueba.Clicked += BtnCliente_Click;
                    btnPrueba.BackgroundColor = Color.BlueViolet;
                    btnPrueba.TextColor = Color.White;

                    stlMenu.Children.Add(btnPrueba);
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

        public void CrearMenuAlumno()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void CrearMenuUsuario()
        {
            try
            {

            }
            catch (Exception)
            {

                throw;
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