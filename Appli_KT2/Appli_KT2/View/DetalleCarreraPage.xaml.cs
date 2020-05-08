using Appli_KT2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DetalleCarreraPage : ContentPage
	{
        private DetalleCarreraPlantel detalleCarreraPlantel;
        private string nombreEscuela;
        private string nombreCarrera;
		public DetalleCarreraPage (DetalleCarreraPlantel detalleCarreraPlanteles, string escuela, string carrera)
		{
            try
            {
                InitializeComponent();
                this.detalleCarreraPlantel = detalleCarreraPlanteles;
                this.nombreEscuela = escuela;
                this.nombreCarrera = carrera;
                LlenarInformacion();
                AccionarContacto();
                AccionarCostos();
                AccionarDuracion();
                AccionarEgreso();
                AccionarExpedicion();
                AccionarIngreso();
                AccionarInscripcion();
                AccionarModalidad();
                AccionarProductivo();
                AccionarRequisitos();
            }
            catch (Exception ex)
            {
            }
          
			
		}

        public void AccionarIngreso()
        {
            try
            {
                this.btningreso.Source = "ic_arrow_downward.png";
                frameingreso.IsVisible = false;
                btningreso.Clicked += (s, e) =>
                {
                    if (frameingreso.IsVisible)
                    {
                        this.btningreso.Source = "ic_arrow_downward.png";
                        frameingreso.IsVisible = false;
                    }
                    else
                    {
                        this.btningreso.Source = "ic_arrow_upward.png";
                        frameingreso.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }
        public void AccionarEgreso()
        {
            try
            {
                this.btnegreso.Source = "ic_arrow_downward.png";
                frameegreso.IsVisible = false;
                btnegreso.Clicked += (s, e) =>
                {
                    if (frameegreso.IsVisible)
                    {
                        this.btnegreso.Source = "ic_arrow_downward.png";
                        frameegreso.IsVisible = false;
                    }
                    else
                    {
                        this.btnegreso.Source = "ic_arrow_upward.png";
                        frameegreso.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }
        public void AccionarProductivo()
        {
            try
            {
                this.btnproductivo.Source = "ic_arrow_downward.png";
                frameproductivo.IsVisible = false;
                btnproductivo.Clicked += (s, e) =>
                {
                    if (frameproductivo.IsVisible)
                    {
                        this.btnproductivo.Source = "ic_arrow_downward.png";
                        frameproductivo.IsVisible = false;
                    }
                    else
                    {
                        this.btnproductivo.Source = "ic_arrow_upward.png";
                        frameproductivo.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }
        public void AccionarCostos()
        {
            try
            {
                this.btncostos.Source = "ic_arrow_downward.png";
                framecostos.IsVisible = false;
                btncostos.Clicked += (s, e) =>
                {
                    if (framecostos.IsVisible)
                    {
                        this.btncostos.Source = "ic_arrow_downward.png";
                        framecostos.IsVisible = false;
                    }
                    else
                    {
                        this.btncostos.Source = "ic_arrow_upward.png";
                        framecostos.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }
        public void AccionarModalidad()
        {
            try
            {
                this.btnmodalidad.Source = "ic_arrow_downward.png";
                framemodalidad.IsVisible = false;
                btnmodalidad.Clicked += (s, e) =>
                {
                    if (framemodalidad.IsVisible)
                    {
                        this.btnmodalidad.Source = "ic_arrow_downward.png";
                        framemodalidad.IsVisible = false;
                    }
                    else
                    {
                        this.btnmodalidad.Source = "ic_arrow_upward.png";
                        framemodalidad.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }
        public void AccionarDuracion()
        {
            try
            {
                this.btnduracion.Source = "ic_arrow_downward.png";
                frameduracion.IsVisible = false;
                btnduracion.Clicked += (s, e) =>
                {
                    if (frameduracion.IsVisible)
                    {
                        this.btnduracion.Source = "ic_arrow_downward.png";
                        frameduracion.IsVisible = false;
                    }
                    else
                    {
                        this.btnduracion.Source = "ic_arrow_upward.png";
                        frameduracion.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }
        public void AccionarRequisitos()
        {
            try
            {
                this.btnrequisitos.Source = "ic_arrow_downward.png";
                framerequisitos.IsVisible = false;
                btnrequisitos.Clicked += (s, e) =>
                {
                    if (framerequisitos.IsVisible)
                    {
                        this.btnrequisitos.Source = "ic_arrow_downward.png";
                        framerequisitos.IsVisible = false;
                    }
                    else
                    {
                        this.btnrequisitos.Source = "ic_arrow_upward.png";
                        framerequisitos.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }
        public void AccionarExpedicion()
        {
            try
            {
                this.btnexpedi.Source = "ic_arrow_downward.png";
                frameexpedicion.IsVisible = false;
                btnexpedi.Clicked += (s, e) =>
                {
                    if (frameexpedicion.IsVisible)
                    {
                        this.btnexpedi.Source = "ic_arrow_downward.png";
                        frameexpedicion.IsVisible = false;
                    }
                    else
                    {
                        this.btnexpedi.Source = "ic_arrow_upward.png";
                        frameexpedicion.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }
        public void AccionarInscripcion()
        {
            try
            {
                this.btninscrip.Source = "ic_arrow_downward.png";
                frameinscripcion.IsVisible = false;
                btninscrip.Clicked += (s, e) =>
                {
                    if (frameinscripcion.IsVisible)
                    {
                        this.btninscrip.Source = "ic_arrow_downward.png";
                        frameinscripcion.IsVisible = false;
                    }
                    else
                    {
                        this.btninscrip.Source = "ic_arrow_upward.png";
                        frameinscripcion.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }
        public void AccionarContacto()
        {
            try
            {
                this.btnContacto.Source = "ic_arrow_downward.png";
                framecontacto.IsVisible = false;
                btnContacto.Clicked += (s, e) =>
                {
                    if (framecontacto.IsVisible)
                    {
                        this.btnContacto.Source = "ic_arrow_downward.png";
                        framecontacto.IsVisible = false;
                    }
                    else
                    {
                        this.btnContacto.Source = "ic_arrow_upward.png";
                        framecontacto.IsVisible = true; ;
                    }
                };
            }
            catch (Exception)
            {
            }
        }


        public void LlenarInformacion()
        {
            try
            {
                txtCorreoContacto.Text = detalleCarreraPlantel.Correo_contacto;
                txtDuracion.Text = detalleCarreraPlantel.Duracion;
                txtFechaExp.Text = detalleCarreraPlantel.Fecha_expedicion;
                txtFechaIns.Text = detalleCarreraPlantel.Fecha_inscripcion;
                txtCostos.Text = detalleCarreraPlantel.Costos;
                txtModalidad.Text = detalleCarreraPlantel.Modalidad;
                txtNombreCarrea.Text = nombreCarrera;
                txtNombreContacto.Text = detalleCarreraPlantel.Nombre_contacto;
                txtNombreEscuela.Text = nombreEscuela;
                txtPerfilEgreso.Text = detalleCarreraPlantel.Perfil_egreso;
                txtPerfilIngreso.Text = detalleCarreraPlantel.Perfil_ingreso;
               // txtRequisitos.Text = detalleCarreraPlantel.Requisitos;
                txtSectorProductivo.Text = detalleCarreraPlantel.Sector_productivo;
                DesflagmentarRequisitos();
            }
            catch (Exception ex)
            {

            }

        }

        public void DesflagmentarRequisitos()
        {
            var listaRequimientos = new List<ListaRequisitos>();
            char[] delimiterChars = {';' };

            string text = detalleCarreraPlantel.Requisitos;

            string[] words = text.Split(delimiterChars);

            foreach (var word in words)
            {
                if (!word.Equals(" "))
                {
                    if (!string.IsNullOrEmpty(word))
                    {
                        listaRequimientos.Add(new ListaRequisitos()
                        {
                            requisito = word
                        });
                    }
                  
                }
            }

            listViewRequisitos.ItemsSource = listaRequimientos;

        }

        public class ListaRequisitos
        {
            public string requisito { get; set; }
        }
	}
}