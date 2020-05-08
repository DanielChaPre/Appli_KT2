using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using PdfSharpCore.Fonts;
using System.Reflection;
using Appli_KT2.Services;
using PdfSharp.Xamarin.Forms;
using PdfSharp.Xamarin.Forms.Delegates;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ResultadoAtlasPage : ContentPage
	{
        private static bool banderaClick;
        ResultadoAtlasViewModel resultadoAtlasViewModel = new ResultadoAtlasViewModel();
        List<PlantelesES> ListaPlanteles = new List<PlantelesES>();
        PlantelESViewModel plantelESViewModel = new PlantelESViewModel();
        CarreraViewModel carreraViewModel = new CarreraViewModel();
        DetalleUniversidadViewModel detalleUniversidadViewModel = new DetalleUniversidadViewModel();
        MunicipioViewModel municipioViewModel = new MunicipioViewModel();
        PerfilAlumnoViewModel perfilAlumno = new PerfilAlumnoViewModel();
        public SQLiteConnection conn = DependencyService.Get<ISQLitePlatform>().GetConnection();

        public ResultadoAtlasPage ()
		{
            try
            {
                InitializeComponent();
                banderaClick = true;
            }
            catch (Exception ex)
            {
            }
           
        }

        protected override async void OnAppearing()
        {
            try
            {
                frameSincronizacion.IsVisible = false;
                listViewResultAtlas.IsVisible = false;
                actiCargarResultado.IsVisible = true;
                actiCargarResultado.IsRunning = true;
                icSincronizar.Clicked += Sincronizar;
                LlenarLista();
                this.icShare.Clicked += IcShare_Clicked;
                await Task.Yield();
            }
            catch (Exception ex)
            {
            }
            
        }

        private void IcShare_Clicked(object sender, EventArgs e)
        {
            try
            {
                //GlobalFontSettings.FontResolver = new FontResolver();
                ////   Android. View rootView = getWindow().getDecorView().findViewById(Android.R.id.content);
                //var document = new PdfDocument();
                //var page = document.AddPage();
                //var gfx = XGraphics.FromPdfPage(page);
                //var font = new XFont("OpenSans", 20,XFontStyle.Italic);

                //gfx.DrawString("Hello World!", font, XBrushes.Black, new XRect(20, 20, page.Width, page.Height), XStringFormats.Center);

                //document.Save("test.pdf");
                var pdf = PDFManager.GeneratePDFFromView(this.listViewResultAtlas);
                DependencyService.Get<IPdfSave>().Save(pdf, "Resultados.pdf");

            }
            catch (Exception ex)
            {
            }
          
        }


        

        private async void Sincronizar(object sender, EventArgs e)
        {
            try
            {
                listViewResultAtlas.IsVisible = false;
                frameSincronizacion.IsVisible = true;

                await carreraViewModel.SincronizarCarrera();
                await carreraViewModel.SincronizarDetalleCarrera();
                await plantelESViewModel.SincronizarDetallePlantel();
                await plantelESViewModel.SincronizarPlantelesES();
                await municipioViewModel.SincronizarMunicipio();
                await perfilAlumno.SincronizarAptitudAlumno();
                await perfilAlumno.SincronizarAptitudes();
                await perfilAlumno.SincronizarAptitudesCarrera();
                await detalleUniversidadViewModel.SincronizarImagenesPlantel();

                frameSincronizacion.IsVisible = false;
                listViewResultAtlas.IsVisible = true;
                LlenarLista();
            }
            catch (Exception ex)
            {
            }
           
        }

        public void LlenarLista()
        {
            try
            {
                listViewResultAtlas.ItemsSource = null;
                listViewResultAtlas.BindingContext = resultadoAtlasViewModel;
                resultadoAtlasViewModel.ConsultarPlantelesDetalleBD();
                if (resultadoAtlasViewModel.lstPlanteles.Count == 0)
                {
                    actiCargarResultado.IsVisible = false;
                    actiCargarResultado.IsRunning = false;
                    Application.Current.MainPage.DisplayAlert("Alerta", "No se encontraron universidades, actualize la información", "Aceptar");
                    // Application.Current.MainPage.Navigation.PopAsync();
                }
                actiCargarResultado.IsVisible = false;
                actiCargarResultado.IsRunning = false;
                listViewResultAtlas.IsVisible = true;
                FiltrarResultado2(resultadoAtlasViewModel.lstPlanteles);
                // FiltrarResultado(resultadoAtlasViewModel.lstPlanteles);
              //  Application.Current.MainPage.DisplayAlert("Alerta", "Si llegara a faltar información, no es a causa de la aplicación", "Aceptar");
                //listViewResultAtlas.ItemsSource = resultadoAtlasViewModel.lstPlanteles;
                listViewResultAtlas.ItemSelected += OnClickOpcionSeleccionada;
                listViewResultAtlas.BindingContext = this;
            }
            catch (Exception ex)
            {
            }
          
            
        }

        private void FiltrarResultado2(List<PlantelesES> lstDetallePlantel)
        {
            try
            {
                var municipio = Convert.ToInt32(App.Current.Properties["municipios"].ToString());
                var institucion = Convert.ToInt32(App.Current.Properties["institucion"].ToString());
                var carrera = App.Current.Properties["Carrera"].ToString();
                var palabra = App.Current.Properties["PalabraCarrera"].ToString();
                List<PlantelesES> ListaPlanteles2 = new List<PlantelesES>();
                ListaPlanteles = new List<PlantelesES>();
                conn.CreateTable<PlantelesES>();
               
                if (municipio != 0)
                {
                    ListaPlanteles2 = conn.Query<PlantelesES>("SELECT p.* FROM  PlantelesES AS p " +
                         " INNER JOIN DetallePlantel AS dp ON dp.IdPlantelesES =  p.IdPlantelES" +
                        " INNER JOIN Municipios AS m ON m.idMunicipio = p.Municipio " +
                        "WHERE  m.idMunicipio = ?", "" + municipio + "");
                    JuntarDetalle(ListaPlanteles2);
                }
                if (institucion != 0)
                {
                    if (ListaPlanteles.Count != 0)
                    {
                        ListaPlanteles2 = conn.Query<PlantelesES>("SELECT p.* FROM CarrerasES AS c" +
                            " INNER JOIN PlantelesES AS p on p.idPlantelES = c.IdPlantelesES " +
                            " INNER JOIN DetallePlantel AS dp ON dp.IdPlantelesES =  p.idPlantelES " +
                            " INNER JOIN Municipios AS m ON m.idMunicipio = p.Municipio " +
                            "WHERE p.idPlantelES = ?", "" + institucion +"");
                        var LstEscuela2 = from a in ListaPlanteles2 where a.Municipio == municipio select a;
                      //  ListaPlanteles = LstEscuela2.Cast<PlantelesES>().ToList();
                        JuntarDetalle(LstEscuela2.Cast<PlantelesES>().ToList());
                    }
                    else
                    {
                        //ListaPlanteles2 = conn.Query<PlantelesES>("SELECT distinct PlantelesES.* FROM CarrerasES AS c" +
                        //    " INNER JOIN PlantelesES on PlantelesES.idPlantelES = c.IdPlantelesES " +
                        //    " WHERE PlantelesES.idPlantelES = ?", ""+ institucion +"");
                        var LstEscuela2 = from a in lstDetallePlantel where a.idPlantelES == institucion select a;
                        ListaPlanteles = LstEscuela2.Cast<PlantelesES>().ToList();
                        JuntarDetalle(ListaPlanteles);
                    }
                }

                if (!string.IsNullOrEmpty(carrera))
                {
                    IEnumerable<PlantelesES> LstEscuela2 = null;
                    IEnumerable<PlantelesES> LstEscuela3 = null;
                    if (ListaPlanteles.Count != 0)
                    {
                        ListaPlanteles2 = conn.Query<PlantelesES>("SELECT p.* FROM CarrerasES AS c" +
                           " INNER JOIN PlantelesES AS p on p.idPlantelES = c.IdPlantelesES " +
                           " INNER JOIN DetallePlantel AS dp ON dp.IdPlantelesES =  p.idPlantelES" +
                           " INNER JOIN Municipios AS m ON m.idMunicipio = p.Municipio " +
                           "WHERE c.NombreCarreraES = ?", "" + carrera + "");
                        if (municipio != 0)
                        {
                           LstEscuela2 = from a in ListaPlanteles2 where a.Municipio == municipio select a;
                        }
                        if (institucion != 0)
                        {
                            if (municipio != 0)
                            {
                                LstEscuela3 = from a in LstEscuela2 where a.idPlantelES == institucion select a;
                            }
                            else
                            {
                                LstEscuela3 = from a in ListaPlanteles2 where a.idPlantelES == institucion select a;
                            }
                        }
                           
                     //   var LstEscuela2 = from a in ListaPlanteles2 where a.Municipio == municipio select a;
                        
                        //  ListaPlanteles = LstEscuela2.Cast<PlantelesES>().ToList();
                        JuntarDetalle(LstEscuela3.Cast<PlantelesES>().ToList());
                    }
                    else
                    {

                        ListaPlanteles2 = conn.Query<PlantelesES>("SELECT p.* FROM CarrerasES AS c" +
                            " INNER JOIN PlantelesES AS p on p.idPlantelES = c.IdPlantelesES " +
                            " INNER JOIN DetallePlantel AS dp ON dp.IdPlantelesES =  p.idPlantelES " +
                            "WHERE c.NombreCarreraES = ?", "" + carrera + "");
                        JuntarDetalle(ListaPlanteles2);
                    }
                }

                if (!string.IsNullOrEmpty(palabra))
                {
                    var listaPlanteles = conn.Query<PlantelesES>("SELECT DISTINCT PlantelesES.* FROM CarrerasES " +
                      " INNER JOIN PlantelesES ON PlantelesES.idPlantelES = CarrerasES.IdPlantelesES" +
                      " Where CarrerasES.NombreCarreraES LIKE ?", "%" + palabra + "%");
                    JuntarDetalle(listaPlanteles);
                    listViewResultAtlas.ItemsSource = ListaPlanteles;
                    return;
                }

                if (municipio == 0 && institucion == 0 && string.IsNullOrEmpty(carrera))
                {
                    ListaPlanteles = lstDetallePlantel;
                }
                listViewResultAtlas.ItemsSource = ListaPlanteles;
            }
            catch (Exception ex)
            {

            }

        }

        public void JuntarDetalle(List<PlantelesES> listaPlanteles)
        {
            try
            {

                conn.CreateTable<DetallePlantel>();
                var lstDetalle = (from x in conn.Table<DetallePlantel>() select x).ToList();
                conn.CreateTable<ImagenPlantel>();
                var lstImagenPlantel = (from x in conn.Table<ImagenPlantel>() select x).ToList();
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
                for (int i = 0; i < listaPlanteles.Count; i++)
                {
                    for (int j = 0; j < lstDetalle.Count; j++)
                    {
                        if (listaPlanteles[i].idPlantelES == lstDetalle[j].IdPlantelesES)
                        {
                            if (lstDetalle[j].Logo_plantel.Equals("-"))
                            {
                                lstDetalle[j].ImagenDecodificada = "imagenescuela.png";
                            }
                            else
                            {
                                lstDetalle[j].ImagenDecodificada = GetImage(lstDetalle[j].Logo_plantel);
                            }
                            listaPlanteles[i].DetallePlantel = lstDetalle[j];

                        }
                    }
                }

                ListaPlanteles = listaPlanteles;

            }
            catch (Exception ex)
            {
            }
        
        }

        public Xamarin.Forms.ImageSource GetImage(string strEncoded)
        {
            try
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
            catch (Exception ex)
            {
                return null;
            }
           
        }

        private void FiltrarResultado(List<PlantelesES> lstDetallePlantel)
        {
            try
            {
                var municipio = Convert.ToInt32(App.Current.Properties["municipios"].ToString());
                var institucion = Convert.ToInt32(App.Current.Properties["institucion"].ToString());
                var carrera = App.Current.Properties["Carrera"].ToString();
                lstDetallePlantel = lstDetallePlantel.OrderBy(p => p.NombrePlantelES).ToList();
                if (municipio != 0)
                {
                    var LstEscuelas = from a in lstDetallePlantel where a.Municipio == municipio select a;
                    ListaPlanteles = LstEscuelas.Cast<PlantelesES>().ToList();
                    //pMunicipio.ItemsSource = municipio.Cast<Municipios>().ToList();
                }

                if (institucion != 0)
                {
                    if (ListaPlanteles.Count != 0)
                    {
                        var LstEscuela2 = from a in ListaPlanteles where a.idPlantelES == institucion select a;
                        ListaPlanteles = LstEscuela2.Cast<PlantelesES>().ToList();
                    }
                    else
                    {
                        var LstEscuela2 = from a in lstDetallePlantel where a.idPlantelES == institucion select a;
                        ListaPlanteles = LstEscuela2.Cast<PlantelesES>().ToList();
                    }
                }

                if (!string.IsNullOrEmpty(carrera))
                {
                    if (ListaPlanteles.Count != 0)
                    {
                        //var LstEscuela2 = from a in ListaPlanteles where a.idPlantelES == carrera select a;
                        //ListaPlanteles = LstEscuela2.Cast<PlantelesES>().ToList();
                        var LstEscuela2 = conn.Query<PlantelesES>("SELECT p.* FROM CarrerasES AS c" +
                           " INNER JOIN PlantelesES AS p on p.idPlantelES = c.IdPlantelesES " +
                           "WHERE c.NombreCarreraES = ?", "" + carrera + "");
                        ListaPlanteles = LstEscuela2;
                    }
                    else
                    {
                        List<PlantelesES> ListaPlanteles2 = new List<PlantelesES>();
                        conn.CreateTable<PlantelesES>();
                        ListaPlanteles2 = conn.Query<PlantelesES>("SELECT p.* FROM CarrerasES AS c " +
                            " INNER JOIN PlantelesES AS p on p.idPlantelES = c.IdPlantelesES " +
                            "WHERE c.NombreCarreraES = ?", "" + carrera + "");
                        ListaPlanteles = ListaPlanteles2;
                        //var LstEscuela2 = from a in lstDetallePlantel where a.idPlantelES == carrera select a;
                        //ListaPlanteles = LstEscuela2.Cast<PlantelesES>().ToList();
                    }
                }

                if (municipio == 0 && institucion == 0 && string.IsNullOrEmpty(carrera))
                {
                    ListaPlanteles = lstDetallePlantel;
                }
                listViewResultAtlas.ItemsSource = ListaPlanteles;
            }
            catch (Exception ex)
            {

            }

        }

        private async void OnClickOpcionSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {
            try
            {
                listViewResultAtlas.SelectedItem = null;
                if (banderaClick)
                {
                    var item = e.SelectedItem as PlantelesES;
                    if ((item != null))
                    {
                        banderaClick = false;
                        await Navigation.PushAsync(new DetalleUniversidadPage(item));
                        await Task.Run(async () =>
                        {
                            await Task.Delay(500);
                            banderaClick = true;
                        });
                    }
                }
            }
            catch (Exception ex)
            {
            }
            
        }

      
    }

    public class FontResolver : IFontResolver
    {
        public string DefaultFontName => throw new NotImplementedException();

        public byte[] GetFont(string faceName)
        {
            using (var ms = new MemoryStream())
            {
                using (var fs = File.Open(faceName, FileMode.Open))
                {
                    fs.CopyTo(ms);
                    ms.Position = 0;
                    return ms.ToArray();
                }
            }
        }
        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName.Equals("OpenSans", StringComparison.CurrentCultureIgnoreCase))
            {
                if (isBold && isItalic)
                {
                    return new FontResolverInfo("OpenSans-BoldItalic.ttf");
                }
                else if (isBold)
                {
                    return new FontResolverInfo("OpenSans-Bold.ttf");
                }
                else if (isItalic)
                {
                    return new FontResolverInfo("OpenSans-Italic.ttf");
                }
                else
                {
                    return new FontResolverInfo("OpenSans-Regular.ttf");
                }
            }
            return null;
        }
    }

}