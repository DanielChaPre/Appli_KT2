using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Appli_KT2.Model
{
    public class DetallePlantel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int cve_detalle_plantel;
        private string url_vinculacion;
        private string logo_plantel;
        private string latitud;
        private string longitud;
        private string ubicacion;
        private string domicilio;
        private string telefono;
        private string nombre_corto;
        private int idPlantelesES;
        private string idColonia;
        private int cve_subsistema;
        private ImageSource imagenDecodificada;
        private ImageSource imagenprincipal;

        //private PlantelesES plantelesES; 

        [PrimaryKey]
        public int Cve_detalle_plantel
        {
            get
            {
                return cve_detalle_plantel;
            }
            set
            {
                cve_detalle_plantel = value;
                OnPropertyChanged();
            }
        }
        public string Url_vinculacion
        {
            get
            {
                return url_vinculacion;
            }
            set
            {
                url_vinculacion = value;
                OnPropertyChanged();
            }
        }
        public string Logo_plantel
        {
            get
            {
                return logo_plantel;
            }
            set
            {
                logo_plantel = value;
                OnPropertyChanged();
            }
        }
        public string Latitud
        {
            get
            {
                return latitud;
            }
            set
            {
                latitud = value;
                OnPropertyChanged();
            }
        }
        public string Longitud
        {
            get
            {
                return longitud;
            }
            set
            {
                longitud = value;
                OnPropertyChanged();
            }
        }
        public string Ubicacion
        {
            get
            {
                return ubicacion;
            }
            set
            {
                ubicacion = value;
                OnPropertyChanged();
            }
        }
      
        public int IdPlantelesES
        {
            get
            {
                return idPlantelesES;
            }
            set
            {
                idPlantelesES = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        public ImageSource ImagenDecodificada
        {
            get
            {
                return imagenDecodificada;
            }
            set
            {
                imagenDecodificada = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        [Ignore]
        public ImageSource ImagenPrincipal
        {
            get
            {
                return imagenprincipal;
            }
            set
            {
                imagenprincipal = value;
                OnPropertyChanged();
            }
        }

        public string Domicilio
        {
            get
            {
                return domicilio;
            }
            set
            {
                domicilio = value;
                OnPropertyChanged();
            }
        }
        public string Telefono
        {
            get
            {
                return telefono;
            }
            set
            {
                telefono = value;
                OnPropertyChanged();
            }
        }
        public string Nombre_corto
        {
            get
            {
                return nombre_corto;
            }
            set
            {
                nombre_corto = value;
                OnPropertyChanged();
            }
        }
        public string IdColonia
        {
            get
            {
                return idColonia;
            }
            set
            {
                idColonia = value;
                OnPropertyChanged();
            }
        }
        public int Cve_subsistema
        {
            get
            {
                return cve_subsistema;
            }
            set
            {
                cve_subsistema = value;
                OnPropertyChanged();
            }
        }

        //[JsonIgnore]
        //[Ignore]
        //public PlantelesES PlantelesES
        //{
        //    get
        //    {
        //        return plantelesES;
        //    }
        //    set
        //    {
        //        plantelesES = value;
        //        OnPropertyChanged();
        //    }
        //}
    }
}
