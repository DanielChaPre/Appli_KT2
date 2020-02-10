using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
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
        public MunicipioViewModel()
        {
          //  ObtenerMunicipios();
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
                            IdMunicipio = listaMunicipios[i].IdMunicipio,
                            NombreMunicipio = listaMunicipios[i].NombreMunicipio,
                            IdEstado = listaMunicipios[i].IdEstado
                        };
                        lstMunicipios.Add(entMunicipios);
                       
                    }
                    //this.ListEstados = JsonConvert.DeserializeObject<List<Estados>>(content);
                    this.ListMunicipios = this.lstMunicipios;
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Aceptar");
                return false;
            }
        }

        public async void ObtenerMunicipios()
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
                    for (int i = 0; i < listaMunicipios.Count; i++)
                    {
                        var entMunicipios = new Municipios()
                        {
                            IdMunicipio = listaMunicipios[i].IdMunicipio,
                            NombreMunicipio = listaMunicipios[i].NombreMunicipio,
                            IdEstado = listaMunicipios[i].IdEstado
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
                throw;
            }
        }

    }
}
