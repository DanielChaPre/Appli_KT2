using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Appli_KT2.ViewModel
{
    public class CarreraViewModel : CarrerasES
    {
        private List<CarrerasES> lstCarreraES = new List<CarrerasES>();

        public CarreraViewModel()
        {
            ObtenerCarreraES();
        }

        public List<CarrerasES> ListCarreraES
        {
            get;
            set;
        }

        public async void ObtenerCarreraES()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerCarreras));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaCarreras = JsonConvert.DeserializeObject<List<CarrerasES>>(content);
                    for (int i = 0; i < listaCarreras.Count; i++)
                    {
                        var entCarreras = new CarrerasES()
                        {
                            Activa = listaCarreras[i].Activa,
                            CampoAmplio2016 = listaCarreras[i].CampoAmplio2016,
                            CampoAmplioAnterior = listaCarreras[i].CampoAmplioAnterior,
                            CampoEspecifico2016 = listaCarreras[i].CampoEspecifico2016,
                            CampoEspecificoAnterior = listaCarreras[i].CampoEspecificoAnterior,
                            ClaveCarrera = listaCarreras[i].ClaveCarrera,
                            idCarreraES = listaCarreras[i].idCarreraES,
                            IdPlantelesES = listaCarreras[i].IdPlantelesES,
                            Nivel = listaCarreras[i].Nivel,
                            NombreCarreraES = listaCarreras[i].NombreCarreraES

                        };
                        lstCarreraES.Add(entCarreras);
                    }
                    this.ListCarreraES = this.lstCarreraES;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
