using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public  class MunicipioViewModel : Municipios
    {

        private string estado;
        private List<Municipios> lstMunicipios = new List<Municipios>();
        private MunicipioDataBase municipioDataBase = new MunicipioDataBase();
        public MunicipioViewModel()
        {
        }

        public List<Municipios> ListMunicipios
        {
            get;
            set;
        }

        public async Task<bool> ObtenerTodosMunicipios()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerMunicipio));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaMunicipios = JsonConvert.DeserializeObject<List<Municipios>>(content);
                    for (int i = 0; i < listaMunicipios.Count; i++)
                    {
                        var entMunicipios = new Municipios()
                        {
                            idMunicipio = listaMunicipios[i].idMunicipio,
                            NombreMunicipio = listaMunicipios[i].NombreMunicipio,
                            idEstado = listaMunicipios[i].idEstado
                        };
                        lstMunicipios.Add(entMunicipios);
                       
                    }
                    //this.ListEstados = JsonConvert.DeserializeObject<List<Estados>>(content);
                    this.ListMunicipios = this.lstMunicipios;
                    return true;
                }

                return false;
            }
            catch (HttpRequestException ex)
            {
             //   await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
                return false;
            }
        }

        public async Task ObtenerMunicipios()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                this.estado = App.Current.Properties["NombreEstado"].ToString();
                if (string.IsNullOrEmpty(estado))
                {
                    return;
                }
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerMunicipiosEstado + estado, string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaMunicipios = JsonConvert.DeserializeObject<List<Municipios>>(content);
                    lstMunicipios = new List<Municipios>();
                    ListMunicipios = null;
                    for (int i = 0; i < listaMunicipios.Count; i++)
                    {
                        var entMunicipios = new Municipios()
                        {
                            idMunicipio = listaMunicipios[i].idMunicipio,
                            NombreMunicipio = listaMunicipios[i].NombreMunicipio,
                            idEstado = listaMunicipios[i].idEstado
                        };
                        lstMunicipios.Add(entMunicipios);
                    }
                    //this.ListEstados = JsonConvert.DeserializeObject<List<Estados>>(content);
                    this.ListMunicipios = this.lstMunicipios;
                    
                  //  return true;
                }
            }
            catch (Exception ex)
            {

            }
        }

        public async Task SincronizarMunicipio()
        {
            SQLiteConnection conn;
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            conn.CreateTable<Municipios>();
            App.Current.Properties["NombreEstado"] = 11;
            await ObtenerTodosMunicipios();
            var municipio = from a in ListMunicipios where a.idEstado == 11 select a;
            var listaMunicipios  = municipio.Cast<Municipios>().ToList();
            if (listaMunicipios.Count != 0)
            {
                for (int i = 0; i < listaMunicipios.Count; i++)
                {
                    conn.InsertOrReplace(listaMunicipios[i]);
                }
                conn.Close();
            }
            else
            {
                SincronizarMunicipio();
            }
        }

    }
}
