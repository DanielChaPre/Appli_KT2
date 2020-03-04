using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Appli_KT2.ViewModel
{
    public class PlantelESViewModel : DetallePlantel
    {
        private string plantelES;
        private List<PlantelesES> lstPlantelES = new List<PlantelesES>();
        private DetallePlantelDataBase detallePlantelDataBase = new DetallePlantelDataBase();

        public PlantelESViewModel()
        {
            ObtenerPlantelES();
        }

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
                                //  CarreraES = listaPlanteles[i].CarreraES,
                                ClaveInstitucion = listaPlanteles[i].ClaveInstitucion,
                                ClavePlantel = listaPlanteles[i].ClavePlantel,
                                idPlantelES = listaPlanteles[i].idPlantelES,
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

        //public async void ObtenerPlantelES()
        //{
        //    try
        //    {
        //        var _client = new HttpClient();
        //        var conexion = new ConexionWS();
        //        var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerPlanteles));
        //        HttpResponseMessage response = await _client.GetAsync(uri);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var content = await response.Content.ReadAsStringAsync();
        //            var listaPlanteles = JsonConvert.DeserializeObject<List<DetallePlantel>>(content);
        //            for (int i = 0; i < listaPlanteles.Count; i++)
        //            {
        //                var entPlanteles = new DetallePlantel()
        //                {
        //                    Costos = listaPlanteles[i].Costos,
        //                    Cve_detalle_plantel = listaPlanteles[i].Cve_detalle_plantel,
        //                    Cve_nivel_agrupado = listaPlanteles[i].Cve_nivel_agrupado,
        //                    Cve_nivel_estudio = listaPlanteles[i].Cve_nivel_estudio,
        //                    Fechas = listaPlanteles[i].Fechas,
        //                    Latitud = listaPlanteles[i].Latitud,
        //                    Logo_plantel = listaPlanteles[i].Logo_plantel,
        //                    Longitud = listaPlanteles[i].Longitud,
        //                    Nivel_estudio = listaPlanteles[i].Nivel_estudio,
        //                    Requisitos = listaPlanteles[i].Requisitos,
        //                    Reseña = listaPlanteles[i].Reseña,
        //                    Ubicacion = listaPlanteles[i].Ubicacion,
        //                    Url_vinculacion = listaPlanteles[i].Url_vinculacion,
        //                    PlantelesES = new PlantelesES()
        //                    {
        //                        Activo = listaPlanteles[i].PlantelesES.Activo,
        //                        //  CarreraES = listaPlanteles[i].CarreraES,
        //                        ClaveInstitucion = listaPlanteles[i].PlantelesES.ClaveInstitucion,
        //                        ClavePlantel = listaPlanteles[i].PlantelesES.ClavePlantel,
        //                        idPlantelES = listaPlanteles[i].PlantelesES.idPlantelES,
        //                        Municipio = listaPlanteles[i].PlantelesES.Municipio,
        //                        NivelAgrupado = listaPlanteles[i].PlantelesES.NivelAgrupado,
        //                        NombreInstitucionES = listaPlanteles[i].PlantelesES.NombreInstitucionES,
        //                        NombrePlantelES = listaPlanteles[i].PlantelesES.NombrePlantelES,
        //                        OPD = listaPlanteles[i].PlantelesES.OPD,
        //                        Sostenimiento = listaPlanteles[i].PlantelesES.Sostenimiento,
        //                        Subsistema = listaPlanteles[i].PlantelesES.Subsistema
        //                    }
        //                };
        //                lstPlantelES.Add(entPlanteles);
        //            }
        //            this.ListPlantelES = this.lstPlantelES;
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }
        //}

        public void SincronizarDetallePlantel()
        {
            if (ListPlantelES.Count != 0)
            {
                for (int i = 0; i < ListPlantelES.Count; i++)
                {
                   //detallePlantelDataBase.SaveItemAsync(ListPlantelES[i]);s
                }
            }
            else
            {
                SincronizarDetallePlantel();
            }
        }
    }
}
