using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class HistorialViewModel : Historial
    {
        private HttpClient _client;
        private ConexionWS conexion;
        private List<Historial> lstnotificaciones = new List<Historial>();

        public async Task<bool> ConsultarHistorial()
        {
            try
            {
                var cveUsuario = App.Current.Properties["cveUsuario"].ToString();
                _client = new HttpClient();
                conexion = new ConexionWS();
                var url = conexion.URL + "" + conexion.Consultarhistorial + cveUsuario;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lstHistorial = JsonConvert.DeserializeObject<List<Historial>>(content);
                    for (int i = 0; i < lstHistorial.Count; i++)
                    {
                        var entHistorial = new Historial()
                        {
                            Cve_categoria = lstHistorial[i].Cve_categoria,
                            Cve_usuario = lstHistorial[i].Cve_usuario,
                            Cve_historial = lstHistorial[i].Cve_historial,
                            Url = lstHistorial[i].Url,
                            Descripcion = lstHistorial[i].Descripcion,
                        };
                        lstnotificaciones.Add(entHistorial);
                    }
                    return true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", response.StatusCode.ToString(), "Aceptar");
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
