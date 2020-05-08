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
            try
            {
               // VerificarInternet();
            }
            catch (Exception ex)
            {
            }
           
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
            try
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
                        }
                        return true;
                    }
                    catch (NullReferenceException ex)
                    {
                        return true;
                    }
                });
            }
            catch (Exception ex)
            {
            }
          
        }

        public async Task SincronizarDetalleCarrera()
        {
            try
            {
                SQLiteConnection conn;
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                conn.CreateTable<DetalleCarreraPlantel>();
                if (!await ObtenerDetalleCarreraES())
                {
                    SincronizarDetalleCarrera();
                }
                if (ListDetalleCarreraES.Count != 0)
                {
                    for (int i = 0; i < ListDetalleCarreraES.Count; i++)
                    {
                        //   conn.Query<DetalleCarreraPlantel>("Delete from DetalleCarreraPlantel");
                        conn.InsertOrReplace(ListDetalleCarreraES[i]);
                    }
                    conn.Close();
                }
                else
                {
                    SincronizarDetalleCarrera();
                }
            }
            catch (Exception ex)
            {
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
                            Sector_productivo = listaCarreras[i].Sector_productivo,
                            Actividades_extracurriculares = listaCarreras[i].Actividades_extracurriculares, 
                            Correo_contacto = listaCarreras[i].Correo_contacto,
                            Cve_nivel_agrupado = listaCarreras[i].Cve_nivel_agrupado,
                            Cve_nivel_carrera = listaCarreras[i].Cve_nivel_carrera,
                            Cve_nivel_estudio = listaCarreras[i].Cve_nivel_estudio,
                            Fecha_expedicion = listaCarreras[i].Fecha_expedicion,
                            Fecha_inicio = listaCarreras[i].Fecha_inicio,
                            Fecha_inscripcion = listaCarreras[i].Fecha_inscripcion,
                            Nombre_contacto = listaCarreras[i].Nombre_contacto,
                            Nombre_region = listaCarreras[i].Nombre_region,
                            Region = listaCarreras[i].Region,
                            Requisitos = listaCarreras[i].Requisitos,
                            Resenia = listaCarreras[i].Resenia,
                            Vinculacion = listaCarreras[i].Vinculacion

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
                App.Current.MainPage.DisplayAlert("Información", "Ha ocurrido un error,  por favor pongase en contacto con el desarrollador.", "Aceptar");

                return false;
            }
        }

        public async Task SincronizarCarrera()
        {
            try
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
                        //conn.Query<CarrerasES>("Delete from CarrerasES");
                        conn.InsertOrReplace(ListCarreraES[i]);
                    }
                    conn.Close();
                }
                else
                {
                    SincronizarCarrera();
                }
            }
            catch (Exception ex)
            {
            }
            
        }
    }
}
