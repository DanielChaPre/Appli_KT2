using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class DetalleUniversidadViewModel : ImagenPlantel
    {
       
        private List<ImagenPlantel> lstImagenes = new List<ImagenPlantel>();
        private List<CarrerasES> lstCarreraES = new List<CarrerasES>();
        private ImagenPlantelDataBase imagenDataBase = new ImagenPlantelDataBase();

        public DetalleUniversidadViewModel()
        {

        }

        public List<ImagenPlantel> ListImagenes
        {
            get;
            set;
        }

        public List<CarrerasES> ListCarreraES
        {
            get;
            set;
        }

        public void ConsultarUniversidad()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();

            }
            catch (Exception ex)
            {

            }
        }

        public async void ObtenerCarreraES(int idplantel)
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerCarrerasPlantel + idplantel));
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

        public async Task<bool> ConsultarImagenesPlanteles(int idPlantel)
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.BuscarImagenesPlantel + idPlantel));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaImagenes = JsonConvert.DeserializeObject<List<ImagenPlantel>>(content);
                    for (int i = 0; i < listaImagenes.Count; i++)
                    {
                        var entImagenes = new ImagenPlantel()
                        {
                            Cve_detalle_plantel = listaImagenes[i].Cve_detalle_plantel,
                            Cve_imagen_plantel = listaImagenes[i].Cve_imagen_plantel,
                            Descripcion = listaImagenes[i].Descripcion,
                            Imagen_principal = listaImagenes[i].Imagen_principal,
                            Ruta = listaImagenes[i].Ruta
                        };
                        lstImagenes.Add(entImagenes);
                    }
                    //for (int i = 0; i < lstImagenes.Count; i++)
                    //{
                    //    lstImagenes[i].ImagenDecodificada = GetImage(lstImagenes[i].Ruta);
                    //}
                    this.ListImagenes = this.lstImagenes;
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

        public async Task<bool> ConsultarImagenesPlanteles()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.BuscarImagenesPlanteles));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaImagenes = JsonConvert.DeserializeObject<List<ImagenPlantel>>(content);
                    for (int i = 0; i < listaImagenes.Count; i++)
                    {
                        var entImagenes = new ImagenPlantel()
                        {
                            Cve_detalle_plantel = listaImagenes[i].Cve_detalle_plantel,
                            Cve_imagen_plantel = listaImagenes[i].Cve_imagen_plantel,
                            Descripcion = listaImagenes[i].Descripcion,
                            Imagen_principal = listaImagenes[i].Imagen_principal,
                            Ruta = listaImagenes[i].Ruta,
                            Imagenbase64 = listaImagenes[i].Imagenbase64
                        };
                        lstImagenes.Add(entImagenes);
                    }
                    //for (int i = 0; i < lstImagenes.Count; i++)
                    //{
                    //    lstImagenes[i].ImagenDecodificada = GetImage(lstImagenes[i].Ruta);
                    //}
                    this.ListImagenes = this.lstImagenes;
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

        public Xamarin.Forms.ImageSource GetImage(string strEncoded)
        {

            if (strEncoded.Equals("-"))
            {
                return null;
            }
            byte[] arrBytImage = Convert.FromBase64String(strEncoded);
            Xamarin.Forms.ImageSource objImage = null;
            objImage = ImageSource.FromStream(() => new MemoryStream(arrBytImage));
            return objImage;
        }

        public async Task<bool> SincronizarImagenesPlantel()
        {
            try
            {
                SQLiteConnection conn;
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                conn.CreateTable<ImagenPlantel>();
                if (!await ConsultarImagenesPlanteles())
                {
                    SincronizarImagenesPlantel();
                }
                if (ListImagenes.Count != 0)
                {
                    for (int i = 0; i < ListImagenes.Count; i++)
                    {
                        conn.InsertOrReplace(ListImagenes[i]);
                    }
                    conn.Close();
                    return true;
                }
                else
                {
                    SincronizarImagenesPlantel();
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
    }
}
