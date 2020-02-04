using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Appli_KT2.ViewModel
{
    public class PlantelESViewModel : PlantelesES
    {
        private string plantelES;
        private List<PlantelesES> lstPlantelES = new List<PlantelesES>();

        public List<PlantelesES> ListPlantelES
        {
            get;
            set;
        }

        public async void ObtenerPlantelES()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerPlanteles));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaPlanteles = JsonConvert.DeserializeObject<List<PlantelesES>>(content);
                    for (int i = 0; i < listaPlanteles.Count; i++)
                    {
                        var entPlanteles = new PlantelesES()
                        {
                          Activo = listaPlanteles[i].Activo,
                          CarreraES = listaPlanteles[i].CarreraES,
                          ClaveInstitucion = listaPlanteles[i].ClaveInstitucion,
                          ClavePlantel = listaPlanteles[i].ClavePlantel,
                          idPlantelesES = listaPlanteles[i].idPlantelesES,
                          Municipio = listaPlanteles[i].Municipio,
                          NivelAgrupado = listaPlanteles[i].NivelAgrupado,
                          NombreInstitucionES = listaPlanteles[i].NombreInstitucionES,
                          NombrePlantelES = listaPlanteles[i].NombrePlantelES,
                          OPD = listaPlanteles[i].OPD,
                          Sostenimiento = listaPlanteles[i].Sostenimiento,
                          Subsistema = listaPlanteles[i].Subsistema
                        };
                        lstPlantelES.Add(entPlanteles);
                    }
                    this.ListPlantelES = this.lstPlantelES;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
