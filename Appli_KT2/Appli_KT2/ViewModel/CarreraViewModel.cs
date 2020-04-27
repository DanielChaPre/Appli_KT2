using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class CarreraViewModel : CarrerasES
    {
        private List<CarrerasES> lstCarreraES = new List<CarrerasES>();
        private List<DetalleCarreraPlantel> lstDetalleCarreraES = new List<DetalleCarreraPlantel>();
        private CarreraDataBase carreraDataBase = new CarreraDataBase();

        public CarreraViewModel()
        {
            VerificarInternet();
        }

        public List<CarrerasES> ListCarreraES
        {
            get;
            set;
        }

        public List<DetalleCarreraPlantel> ListDetalleCarreraES
        {
            get;
            set;
        }

        public async Task<bool> ObtenerCarreraES()
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
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void VerificarInternet()
        {
            var status = 1;
            ConexionInternet conexionInternet = new ConexionInternet();
            Device.StartTimer(TimeSpan.FromSeconds(5), () =>
            {
                try
                {

                    if (conexionInternet.VerificarInternet())
                    {
                        if (status == 1)
                        {
                            ObtenerCarreraES();
                            status = 0;
                        }
                        return true;
                    }
                    else
                    {
                        status = 1;
                        Application.Current.MainPage.DisplayAlert("Notificación", "Los filtros no funcionaran por falta de conexión a internet", "Aceptar");
                    }
                    return true;
                }
                catch (NullReferenceException ex)
                {
                    return true;
                }
            });
        }

        public async Task SincronizarDetalleCarrera()
        {
            SQLiteConnection conn;
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            conn.CreateTable<DetalleCarreraPlantel>();
            if (!await ObtenerDetalleCarreraES())
            {
                SincronizarDetalleCarrera();
            }
            if (ListCarreraES.Count != 0)
            {
                for (int i = 0; i < ListCarreraES.Count; i++)
                {
                    conn.InsertOrReplace(ListCarreraES[i]);
                }
            }
            else
            {
                SincronizarDetalleCarrera();
            }
        }

        private async Task<bool> ObtenerDetalleCarreraES()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerDetalleCarrera));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaCarreras = JsonConvert.DeserializeObject<List<DetalleCarreraPlantel>>(content);
                    for (int i = 0; i < listaCarreras.Count; i++)
                    {
                        var entCarreras = new DetalleCarreraPlantel()
                        {
                            Costos = listaCarreras[i].Costos,
                            Cve_detalle_carrera_plantel = listaCarreras[i].Cve_detalle_carrera_plantel,
                            Cve_detalle_plantel = listaCarreras[i].Cve_detalle_plantel,
                            Duracion = listaCarreras[i].Duracion,
                            IdCarreraES = listaCarreras[i].IdCarreraES,
                            Modalidad = listaCarreras[i].Modalidad,
                            Perfil_egreso = listaCarreras[i].Perfil_egreso,
                            Perfil_ingreso = listaCarreras[i].Perfil_ingreso,
                            RVOE1 = listaCarreras[i].RVOE1,
                            Sector_productivo = listaCarreras[i].Sector_productivo

                        };
                        lstDetalleCarreraES.Add(entCarreras);
                    }
                    this.ListDetalleCarreraES = this.lstDetalleCarreraES;
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task SincronizarCarrera()
        {
            SQLiteConnection conn;
            conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
            conn.CreateTable<CarrerasES>();
            if (!await ObtenerCarreraES())
            {
                SincronizarCarrera();
            }
            if (ListCarreraES.Count != 0)
            {
                for (int i = 0; i < ListCarreraES.Count; i++)
                {
                    conn.InsertOrReplace(ListCarreraES[i]);
                }
            }
            else
            {
                SincronizarCarrera();
            }
        }
    }
}
