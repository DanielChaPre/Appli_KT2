using Appli_KT2.Model;
using Appli_KT2.Utils;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using static Appli_KT2.View.AtlasPage;

namespace Appli_KT2.ViewModel
{
    public class ResultadoAtlasViewModel  : DetallePlantel
    {
        private string plantelES;
        public List<PlantelesES> lstPlanteles = new List<PlantelesES>();
        public List<PlantelesES> lstDetallePlanteles = new List<PlantelesES>();
        public List<DetallePlantel> lstDetalle = new List<DetallePlantel>();
        public List<ImagenPlantel> lstImagenPlantel = new List<ImagenPlantel>();
        public SQLiteConnection conn;

        public List<DetallePlantel> ListPlantelES
        {
            get;
            set;
        }
        public ICommand IrEnlaceCommand
        {
            get
            {
                return new RelayCommand(IrEnlace);
            }
        }
        public void IrEnlace()
        {

        }

        public void ConsultarPlantelesDetalleBD()
        {
            try
            {
                ConsultarPlantelesESBD();
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                conn.CreateTable<DetallePlantel>();
                lstDetalle = (from x in conn.Table<DetallePlantel>() select x).ToList();
                conn.CreateTable<ImagenPlantel>();
                lstImagenPlantel = (from x in conn.Table<ImagenPlantel>() select x).ToList();
                for (int i = 0; i < lstDetalle.Count; i++)
                {

                    var LstEscuela2 = from a in lstImagenPlantel where a.Cve_detalle_plantel == lstDetalle[i].Cve_detalle_plantel select a;
                    var Listaiamgen = LstEscuela2.Cast<ImagenPlantel>().ToList();
                    if (Listaiamgen.Count != 0)
                    {
                        for (int k = 0; k < Listaiamgen.Count; k++)
                        {
                            if (Listaiamgen[k].Imagen_principal == 1)
                            {
                                lstDetalle[i].ImagenPrincipal = GetImage(Listaiamgen[k].Imagenbase64);
                            }
                        }
                        // lstDetalle[i].ImagenPrincipal = GetImage(Listaiamgen[0].Imagenbase64);
                    }
                    else
                    {
                        lstDetalle[i].ImagenPrincipal = "img_universidad.jpg";
                    }
                }
                for (int i = 0; i < lstPlanteles.Count; i++)
                {
                    for (int j = 0; j < lstDetalle.Count; j++)
                    {
                        if (lstPlanteles[i].idPlantelES == lstDetalle[j].IdPlantelesES)
                        {
                            if (lstDetalle[j].Logo_plantel.Equals("-"))
                            {
                                lstDetalle[j].ImagenDecodificada = "img_universidad.jpg";
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
            catch (Exception ex)
            {

            }
        }

        public void ConsultarPlantelesDetalleBDSugerencias(int idAlumno)
        {
            try
            {
                var lplanteles = new List<PlantelesES>();
                conn = DependencyService.Get<ISQLitePlatform>().GetConnection();
                conn.CreateTable<Resultado>();
              //  var resultados = conn.Query<Resultado>("SELECT * FROM Resultado WHERE idAlumno = ?", idAlumno);
                var resultados = conn.Query<Resultado>("SELECT * FROM Resultado");
             //   Aptitud
                conn.CreateTable<Aptitud>();
                var cvesAptitud = conn.Query<Aptitud>("SELECT distinct a.Cve_aptitud  FROM DetalleAptitudCarrera as ac " +
                    "INNER JOIN Aptitud AS a ON a.Cve_aptitud = ac.cve_aptitud" +
                    "  WHERE a.Nombre= ? OR a.Nombre= ? OR a.Nombre= ?", resultados[0].aptitud1, resultados[0].aptitud2, resultados[0].aptitud3);
                conn.CreateTable<PlantelesES>();
                if (cvesAptitud.Count == 1)
                {
                    lstPlanteles = conn.Query<PlantelesES>("SELECT distinct p.* FROM DetalleAptitudCarrera as ac " +
                   " INNER JOIN CarrerasES AS c ON c.idCarreraES = ac.idCarreraES" +
                 " INNER JOIN PlantelesES AS p ON p.idPlantelES = c.IdPlantelesES" +
                 "  WHERE ac.cve_aptitud = ? ", cvesAptitud[0].Cve_aptitud);
                 //   var csugerenciia = conn.Query<DetalleAptitudCarrera>("SELECT ac.* FROM DetalleAptitudCarrera as ac " +
                 //"  WHERE ac.cve_aptitud = ? ", cvesAptitud[0].Cve_aptitud);
                 //   App.Current.Properties["carrerasugerencia"] = csugerenciia[0].idCarreraES;
                }
                else if (cvesAptitud.Count == 2)
                {
                    lstPlanteles = conn.Query<PlantelesES>("SELECT p.* FROM DetalleAptitudCarrera as ac " +
                     " INNER JOIN CarrerasES AS c ON c.idCarreraES = ac.idCarreraES" +
                     " INNER JOIN PlantelesES AS p ON p.idPlantelES = c.IdPlantelesES" +
                    "  WHERE ac.cve_aptitud = ? OR ac.cve_aptitud = ?", cvesAptitud[0].Cve_aptitud, cvesAptitud[1].Cve_aptitud);
                }
                else if (cvesAptitud.Count == 3)
                {
                    lstPlanteles = conn.Query<PlantelesES>("SELECT p.* FROM DetalleAptitudCarrera as ac " +
                   " INNER JOIN CarrerasES AS c ON c.idCarreraES = ac.idCarreraES" +
                   " INNER JOIN PlantelesES AS p ON p.idPlantelES = c.IdPlantelesES" +
                  "  WHERE ac.cve_aptitud = ? OR ac.cve_aptitud = ? OR ac.cve_aptitud = ?", cvesAptitud[0].Cve_aptitud, cvesAptitud[1].Cve_aptitud, cvesAptitud[2].Cve_aptitud);
                }
                else
                {
                    
                    ConsultarPlantelesESBD();
                    conn.CreateTable<DetallePlantel>();
                    lstDetalle = (from x in conn.Table<DetallePlantel>() select x).ToList();
                    conn.CreateTable<ImagenPlantel>();
                    lstImagenPlantel = (from x in conn.Table<ImagenPlantel>() select x).ToList();
                    for (int i = 0; i < lstDetalle.Count; i++)
                    {

                        var LstEscuela2 = from a in lstImagenPlantel where a.Cve_detalle_plantel == lstDetalle[i].Cve_detalle_plantel select a;
                        var Listaiamgen = LstEscuela2.Cast<ImagenPlantel>().ToList();
                        if (Listaiamgen.Count != 0)
                        {
                            for (int k = 0; k < Listaiamgen.Count; k++)
                            {
                                if (Listaiamgen[k].Imagen_principal == 1)
                                {
                                    lstDetalle[i].ImagenPrincipal = GetImage(Listaiamgen[k].Imagenbase64);
                                }
                            }
                        }
                        else
                        {
                            lstDetalle[i].ImagenPrincipal = "img_universidad.jpg";
                        }
                    }
                    for (int i = 0; i < lstPlanteles.Count; i++)
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
                conn.CreateTable<DetallePlantel>();
                lstDetalle = (from x in conn.Table<DetallePlantel>() select x).ToList();
                conn.CreateTable<ImagenPlantel>();
                lstImagenPlantel = (from x in conn.Table<ImagenPlantel>() select x).ToList();
                for (int i = 0; i < lstDetalle.Count; i++)
                {

                    var LstEscuela2 = from a in lstImagenPlantel where a.Cve_detalle_plantel == lstDetalle[i].Cve_detalle_plantel select a;
                    var Listaiamgen = LstEscuela2.Cast<ImagenPlantel>().ToList();
                    if (Listaiamgen.Count != 0)
                    {
                        for (int k = 0; k < Listaiamgen.Count; k++)
                        {
                            if (Listaiamgen[k].Imagen_principal == 1)
                            {
                                lstDetalle[i].ImagenPrincipal = GetImage(Listaiamgen[k].Imagenbase64);
                            }
                        }
                    }
                    else
                    {
                        lstDetalle[i].ImagenPrincipal = "img_universidad.jpg";
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    for (int j = 0; j < lstDetalle.Count; j++)
                    {
                        if (lstPlanteles[i].idPlantelES == lstDetalle[j].IdPlantelesES)
                        {
                            if (lstDetalle[j].Logo_plantel.Equals("-"))
                            {
                                lstDetalle[j].ImagenDecodificada = "img_universidad.jpg";
                            }
                            else
                            {
                                lstDetalle[j].ImagenDecodificada = GetImage(lstDetalle[j].Logo_plantel);
                            }

                            lstPlanteles[i].DetallePlantel = lstDetalle[j];

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ConsultarPlantelesESBD();
                conn.CreateTable<DetallePlantel>();
                lstDetalle = (from x in conn.Table<DetallePlantel>() select x).ToList();
                conn.CreateTable<ImagenPlantel>();
                lstImagenPlantel = (from x in conn.Table<ImagenPlantel>() select x).ToList();
                for (int i = 0; i < lstDetalle.Count; i++)
                {

                    var LstEscuela2 = from a in lstImagenPlantel where a.Cve_detalle_plantel == lstDetalle[i].Cve_detalle_plantel select a;
                    var Listaiamgen = LstEscuela2.Cast<ImagenPlantel>().ToList();
                    if (Listaiamgen.Count != 0)
                    {
                        for (int k = 0; k < Listaiamgen.Count; k++)
                        {
                            if (Listaiamgen[k].Imagen_principal == 1)
                            {
                                lstDetalle[i].ImagenPrincipal = GetImage(Listaiamgen[k].Imagenbase64);
                            }
                        }
                    }
                    else
                    {
                        lstDetalle[i].ImagenPrincipal = "img_universidad.jpg";
                    }
                }
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
