using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Appli_KT2.ViewModel
{
    public  class MunicipioViewModel : Municipios
    {

        private string estado;
        private List<Municipios> lstMunicipios = new List<Municipios>();
        public MunicipioViewModel()
        {
            ObtenerMunicipios();
        }

        public List<Municipios> ListMunicipios
        {
            get;
            set;
        }

        public async void ObtenerMunicipios()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                this.estado = App.Current.Properties["NombreEstado"].ToString();
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
