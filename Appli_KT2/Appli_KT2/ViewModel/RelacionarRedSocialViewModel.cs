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
    public class RelacionarRedSocialViewModel
    {
        private HttpClient _client;
        private ConexionWS conexion;
        private string url;

        public async Task<bool> RelacionarFacebookUsuario(string perfil, string usuario, string contrasena)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.RelacionarFacebookUsuario + perfil+"/"+usuario+"/" +contrasena;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<string>>(content);
                    if (result.Count != 0)
                    {
                        var tipo = result[0];
                        var cveUsuario = result[1];
                        var nombre = result[2];
                        var apePaterno = result[3];
                        App.Current.Properties["tipo_usuario"] = tipo;
                        App.Current.Properties["cveUsuario"] = cveUsuario;
                        App.Current.Properties["nombreUsuario"] = nombre + " " + apePaterno;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error" + response.StatusCode, "Accept");
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RelacionarGoogleUsuario(string perfil, string usuario, string contrasena)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.RelacionarGoogleUsuario + perfil + "/" + usuario + "/" + contrasena;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<string>>(content);
                    if (result.Count != 0)
                    {
                        var tipo = result[0];
                        var cveUsuario = result[1];
                        var nombre = result[2];
                        var apePaterno = result[3];
                        App.Current.Properties["tipo_usuario"] = tipo;
                        App.Current.Properties["cveUsuario"] = cveUsuario;
                        App.Current.Properties["nombreUsuario"] = nombre + " " + apePaterno;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error" + response.StatusCode, "Accept");
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RelacionarFacebookAlumno(string perfil, string curp, string contrasena)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.RelacionarFacebookAlumno + perfil + "/" + curp + "/" + contrasena;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<string>>(content);
                    if (result.Count != 0)
                    {
                        var tipo = result[0];
                        var cveUsuario = result[1];
                        var nombre = result[2];
                        var apePaterno = result[3];
                        App.Current.Properties["tipo_usuario"] = tipo;
                        App.Current.Properties["cveUsuario"] = cveUsuario;
                        App.Current.Properties["nombreUsuario"] = nombre + " " + apePaterno;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error" + response.StatusCode, "Accept");
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> RelacionarGoogleAlumno(string perfil, string curp, string contrasena)
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.RelacionarGoogleAlumno + perfil + "/" + curp + "/" + contrasena;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<List<string>>(content);
                    if (result.Count != 0)
                    {
                        var tipo = result[0];
                        var cveUsuario = result[1];
                        var nombre = result[2];
                        var apePaterno = result[3];
                        App.Current.Properties["tipo_usuario"] = tipo;
                        App.Current.Properties["cveUsuario"] = cveUsuario;
                        App.Current.Properties["nombreUsuario"] = nombre + " " + apePaterno;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error" + response.StatusCode, "Accept");
                    return false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
