using Newtonsoft.Json;
using RestSharp;
using System;
using System.IO;
using System.Net;
using System.Web;

namespace Appli_KT2.Utils
{
    class MetodoHTTP
    {
        public dynamic Post(string url, string json, string autorizacion = null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.POST);
                request.AddHeader("content-type", "application/json");
                request.AddParameter("application/json", json, ParameterType.RequestBody);

                if (autorizacion != null)
                {
                    request.AddHeader("Authorization", autorizacion);
                }

                IRestResponse response = client.Execute(request);
                dynamic datos = JsonConvert.DeserializeObject(response.Content);
                return datos;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public dynamic Put(string url, string json, string autorizacion = null)
        {
            try
            {
                var client = new RestClient(url);
                var request = new RestRequest(Method.PUT);
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
                    
                    return datos;
                }

                return null;

            }
            catch (Exception ex)
            {

                throw;
            }

        }

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
                dynamic datos = JsonConvert.DeserializeObject(response.Content);
                return datos;

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
