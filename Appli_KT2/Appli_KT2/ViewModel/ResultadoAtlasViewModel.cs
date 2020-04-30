using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class ResultadoAtlasViewModel  : DetallePlantel
    {
        private string plantelES;
        public List<PlantelesES> lstPlanteles = new List<PlantelesES>();
        public List<PlantelesES> lstDetallePlanteles = new List<PlantelesES>();
        public List<DetallePlantel> lstDetalle = new List<DetallePlantel>();
        public SQLiteConnection conn;

        public List<DetallePlantel> ListPlantelES
        {
            get;
            set;
        }


        public void ConsultarPlantelesDetalleBD()
        {
            try
            {
                ConsultarPlantelesESBD();
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                conn.CreateTable<DetallePlantel>();
                lstDetalle = (from x in conn.Table<DetallePlantel>() select x).ToList();
                for (int i = 0; i < lstPlanteles.Count; i++)
                {
                    for (int j = 0; j < lstDetalle.Count; j++)
                    {
                        if (lstPlanteles[i].idPlantelES == lstDetalle[j].IdPlantelesES)
                        {
                            if (lstDetalle[j].Logo_plantel.Equals("-"))
                            {
                                lstDetalle[j].ImagenDecodificada = "imagenescuela.png";
                            }
                            else
                            {
                                lstDetalle[j].ImagenDecodificada = GetImage(lstDetalle[j].Logo_plantel);
                            }
                           
                            lstPlanteles[i].DetallePlantel = lstDetalle[j];

                        }
                    }
                }
                conn.CreateTable<PlantelesES>();
                for (int p = 0; p < lstPlanteles.Count; p++)
                {
                    //detallePlantelDataBase.SaveItemAsync(ListPlantelES[i]);s
                    conn.InsertOrReplace(lstPlanteles[p]);

                }

                var plantelesEs = (from x in conn.Table<PlantelesES>() select x).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ConsultarPlantelesDetalleBDSugerencias()
        {
            try
            {
                ConsultarPlantelesESBD();
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                conn.CreateTable<DetallePlantel>();
                lstDetalle = (from x in conn.Table<DetallePlantel>() select x).ToList();

                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < lstDetalle.Count; j++)
                    {
                        if (lstPlanteles[i].idPlantelES == lstDetalle[j].IdPlantelesES)
                        {
                            lstDetalle[j].ImagenDecodificada = GetImage(lstDetalle[j].Logo_plantel);
                            lstPlanteles[i].DetallePlantel = lstDetalle[j];
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void ConsultarPlantelesESBD()
        {
            try
            {
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                conn.CreateTable<PlantelesES>();
                lstPlanteles = (from x in conn.Table<PlantelesES>() select x).ToList();
            }
            catch (Exception)
            {

                throw;
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
    }
}
