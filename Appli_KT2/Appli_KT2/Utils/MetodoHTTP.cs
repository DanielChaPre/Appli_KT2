using Appli_KT2.View;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Web;
using Xamarin.Forms;

namespace Appli_KT2.Utils
{
    class MetodoHTTP
    {

        public dynamic ActualizarDatos(string url, string json, bool nuevo = true, string autorizacion = null)
        {
            try
            {
                var client = new RestClient(url);
                RestRequest request;
                if (nuevo)
                {
                    request = new RestRequest(Method.POST);
                }
                else
                {
                    request = new RestRequest(Method.PUT);
                }
               
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (autorizacion != null)
                {
                    request.AddHeader("Authorization", autorizacion);
                }

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    dynamic datos = JsonConvert.DeserializeObject(response.Content);
                    Application.Current.MainPage.DisplayAlert("Éxito", "Su información se ha guardado", "Aceptar");
                    return datos;
                }

                return null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //public dynamic Post(string url, string json, string autorizacion = null)
        //{
        //    try
        //    {
        //        var client = new RestClient(url);
        //        var request = new RestRequest(Method.POST);
        //        request.AddHeader("content-type", "application/json");
        //        request.AddParameter("application/json", json, ParameterType.RequestBody);

        //        if (autorizacion != null)
        //        {
        //            request.AddHeader("Authorization", autorizacion);
        //        }

        //        IRestResponse response = client.Execute(request);
        //        dynamic datos = JsonConvert.DeserializeObject(response.Content);
        //        return datos;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        //public dynamic Put(string url, string json, string autorizacion = null)
        //{
        //    try
        //    {
        //        var client = new RestClient(url);
        //        var request = new RestRequest(Method.PUT);
        //        request.AddHeader("content-type", "application/json");
        //        request.AddParameter("application/json", json, ParameterType.RequestBody);

        //        if (autorizacion != null)
        //        {
        //            request.AddHeader("Authorization", autorizacion);
        //        }

        //        IRestResponse response = client.Execute(request);
        //        if (response.IsSuccessful)
        //        {
        //            dynamic datos = JsonConvert.DeserializeObject(response.Content);
                    
        //            return datos;
        //        }

        //        return null;

        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }

        //}

        public dynamic Delete(string url, string json, string autorizacion = null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.DELETE);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (autorizacion != null)
                {
                    request.AddHeader("Authorization", autorizacion);
                }

                IRestResponse response = client.Execute(request);
                if (response.IsSuccessful)
                {
                    dynamic datos = JsonConvert.DeserializeObject(response.Content);
                    App.Current.Properties["usuario"] = "";
                    App.Current.Properties["contrasena"] = "";
                    App.Current.Properties["idAlumno"] = 0;
                    App.Current.Properties["tipo_usuario"] = 0;
                    App.Current.Properties["nombreUsuario"] = "Nombre Usuario";
                    App.Current.Properties["rolUsuario"] = "";
                  //  App.Current.MainPage.DisplayAlert("Información", "Se ha cerrado sesión", "Aceptar");
                    Application.Current.MainPage = new NavigationPage(new MainPage());
                    App.Current.MainPage.Navigation.PopAsync();
                    return datos;
                }
                return null;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public dynamic Get(string url)
        {
            try
            {
                HttpWebRequest myWebRequest = (HttpWebRequest)WebRequest.Create(url);
                myWebRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
                myWebRequest.Credentials = CredentialCache.DefaultCredentials;
                myWebRequest.Proxy = null;
                HttpWebResponse httpWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                Stream myStrem = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(myStrem);

                string datos = HttpUtility.HtmlDecode(streamReader.ReadToEnd());

                dynamic data = JsonConvert.DeserializeObject(datos);

                return data;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
