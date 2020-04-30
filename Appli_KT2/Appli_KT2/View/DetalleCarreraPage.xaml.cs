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
            InitializeComponent();
            this.detalleCarreraPlantel = detalleCarreraPlanteles;
            this.nombreEscuela = escuela;
            this.nombreCarrera = carrera;
            LlenarInformacion();
			
		}

        public void LlenarInformacion()
        {
            try
            {
                txtActividades.Text = detalleCarreraPlantel.Actividades_extracurriculares;
                txtCorreoContacto.Text = detalleCarreraPlantel.Correo_contacto;
                txtDuracion.Text = detalleCarreraPlantel.Duracion;
                txtFechaExp.Text = detalleCarreraPlantel.Fecha_expedicion;
                txtFechaInicio.Text = detalleCarreraPlantel.Fecha_inicio;
                txtFechaIns.Text = detalleCarreraPlantel.Fecha_inscripcion;
                txtCostos.Text = detalleCarreraPlantel.Costos;
                txtModalidad.Text = detalleCarreraPlantel.Modalidad;
                txtNombreCarrea.Text = nombreCarrera;
                txtNombreContacto.Text = detalleCarreraPlantel.Nombre_contacto;
                txtNombreEscuela.Text = nombreEscuela;
                txtPerfilEgreso.Text = detalleCarreraPlantel.Perfil_egreso;
                txtPerfilIngreso.Text = detalleCarreraPlantel.Perfil_ingreso;
                txtRequisitos.Text = detalleCarreraPlantel.Requisitos;
                txtSectorProductivo.Text = detalleCarreraPlantel.Sector_productivo;
            }
            catch (Exception ex)
            {

            }
           
        }
	}
}