using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class Historial : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int cve_historial;
        private string descripcion;
        private string url;
        private string cve_categoria;
        private int cve_usuario;

        public int Cve_historial
        {
            get
            {
                return cve_historial;
            }
            set
            {
                cve_historial = value;
                OnPropertyChanged();
            }
        }

        public string Descripcion
        {
            get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
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

        public int Cve_usuario
        {
            get
            {
                return cve_usuario;
            }
            set
            {
                cve_usuario = value;
                OnPropertyChanged();
            }
        }
    }
}
