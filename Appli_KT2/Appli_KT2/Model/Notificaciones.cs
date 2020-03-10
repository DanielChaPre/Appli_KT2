using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace Appli_KT2.Model
{
    public class Notificaciones : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string cve_categoria;
        private int cve_notificacion;
        private int cve_tipo_notificacion;
        private string estatus;
        private string fecha_notificacion;
        private string hora_notificacion;
        private string responsable;
        private string texto;
        private string titulo;
        private string url;
        [JsonIgnore]
        private string icon;
        [JsonIgnore]
        private Color estatuscolor;

        public string Cve_categoria
        {
            get
            {
                return cve_categoria;
            }
            set
            {
                cve_categoria = value;
                OnPropertyChanged();
            }
        }
        public int Cve_notificacion
        {
            get
            {
                return cve_notificacion;
            }
            set
            {
                cve_notificacion = value;
                OnPropertyChanged();
            }
        }
        public int Cve_tipo_notificacion
        {
            get
            {
                return cve_tipo_notificacion;
            }
            set
            {
                cve_tipo_notificacion = value;
                OnPropertyChanged();
            }
        }
        public string Estatus
        {
            get
            {
                return estatus;
            }
            set
            {
                estatus = value;
                OnPropertyChanged();
            }
        }
        public string Fecha_notificacion
        {
            get
            {
                return fecha_notificacion;
            }
            set
            {
                fecha_notificacion = value;
                OnPropertyChanged();
            }
        }
        public string Hora_notificacion
        {
            get
            {
                return hora_notificacion;
            }
            set
            {
                hora_notificacion = value;
                OnPropertyChanged();
            }
        }
        public string Responsable
        {
            get
            {
                return responsable;
            }
            set
            {
                responsable = value;
                OnPropertyChanged();
            }
        }
        public string Texto
        {
            get
            {
                return texto;
            }
            set
            {
                texto = value;
                OnPropertyChanged();
            }
        }
        public string Titulo
        {
            get
            {
                return titulo;
            }
            set
            {
                titulo = value;
                OnPropertyChanged();
            }
        }

        public string Url
        {
            get
            {
                return url;
            }
            set
            {
                url = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        public string Icon
        {
            get
            {
                return icon;
            }
            set
            {
                icon = value;
                OnPropertyChanged();
            }
        }
        [JsonIgnore]
        public Color EstatusColor
        {
            get
            {
                return estatuscolor;
            }
            set
            {
                estatuscolor = value;
                OnPropertyChanged();
            }
        }
    }
}
