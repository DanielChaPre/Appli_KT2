using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.ViewModel;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
			InitializeComponent ();
            banderaClick = true;
        }

        protected override async void OnAppearing()
        {
            frameSincronizacion.IsVisible = false;
            listViewResultAtlas.IsVisible = false;
            actiCargarResultado.IsVisible = true;
            actiCargarResultado.IsRunning = true;
            icSincronizar.Clicked += Sincronizar;
            LlenarLista();
            await Task.Yield();
        }

        private async void Sincronizar(object sender, EventArgs e)
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
            await detalleUniversidadViewModel.SincronizarImagenesPlantel();

            frameSincronizacion.IsVisible = false;
            listViewResultAtlas.IsVisible = true;
            LlenarLista();
        }

        public void LlenarLista()
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
            Application.Current.MainPage.DisplayAlert("Alerta", "Si llegara a faltar información, no es a causa de la aplicación", "Aceptar");
            //listViewResultAtlas.ItemsSource = resultadoAtlasViewModel.lstPlanteles;
            listViewResultAtlas.ItemSelected += OnClickOpcionSeleccionada;
            
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
                        ListaPlanteles2 = conn.Query<PlantelesES>("SELECT distinct PlantelesES.* FROM CarrerasES AS c" +
                            " INNER JOIN PlantelesES on PlantelesES.idPlantelES = c.IdPlantelesES " +
                            " INNER JOIN DetallePlantel AS dp ON dp.IdPlantelesES = PlantelesES.idPlantelES" +
                            " WHERE PlantelesES.idPlantelES = ?", ""+ institucion +"");
                        JuntarDetalle(ListaPlanteles2);
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
            conn.CreateTable<DetallePlantel>();
            var lstDetalle = (from x in conn.Table<DetallePlantel>() select x).ToList();
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
    }
}