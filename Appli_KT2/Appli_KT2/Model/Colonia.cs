using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
   public  class Colonias : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _idcolonia;
        private string nombreColonia;
        private string cp;
        private int idmunicipio;

        public int idColonia
        {
            get
            {
                return _idcolonia;
            }
            set
            {
                _idcolonia = value;
                OnPropertyChanged();
            }
        }

        public string NombreColonia
        {
            get
            {
                return nombreColonia;
            }
            set
            {
                nombreColonia = value;
                OnPropertyChanged();
            }
        }
        public string CP
        {
            get
            {
                return cp;
            }
            set
            {
                cp = value;
                OnPropertyChanged();
            }
        }
        public int idMunicipio
        {
            get
            {
                return idmunicipio;
            }
            set
            {
                idmunicipio = value;
                OnPropertyChanged();
            }
        }
    }
}
