using Newtonsoft.Json;
using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

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
        private string costos;
        private string requisitos;
        private string fechas;
        private string reseña;
        private string latitud;
        private string longitud;
        private string ubicacion;
        private string nivel_estudio;
        private string cve_nivel_agrupado;
        private string cve_nivel_estudio;
        private int idplantelesES;
        private Xamarin.Forms.ImageSource imagenDecodificada;

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
        public string Costos
        {
            get
            {
                return costos;
            }
            set
            {
                costos = value;
                OnPropertyChanged();
            }
        }
        public string Requisitos
        {
            get
            {
                return requisitos;
            }
            set
            {
                requisitos = value;
                OnPropertyChanged();
            }
        }
        public string Fechas
        {
            get
            {
                return fechas;
            }
            set
            {
                fechas = value;
                OnPropertyChanged();
            }
        }
        public string Reseña
        {
            get
            {
                return reseña;
            }
            set
            {
                reseña = value;
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
        public string Nivel_estudio
        {
            get
            {
                return nivel_estudio;
            }
            set
            {
                nivel_estudio = value;
                OnPropertyChanged();
            }
        }
        public string Cve_nivel_agrupado
        {
            get

            {
                return cve_nivel_agrupado;
            }
            set
            {
                cve_nivel_agrupado = value;
                OnPropertyChanged();
            }
        }
        
        public string Cve_nivel_estudio
        {
            get
            {
                return cve_nivel_estudio;
            }
            set
            {
                cve_nivel_estudio = value;
                OnPropertyChanged();
            }
        }
      //  [ForeignKey(typeof(PlantelesES))]
        
        public int idPlantelesES
        {
            get
            {
                return idplantelesES;
            }
            set
            {
                idplantelesES = value;
                OnPropertyChanged();
            }
        }

        [JsonIgnore]
        public Xamarin.Forms.ImageSource ImagenDecodificada
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
    }
}
