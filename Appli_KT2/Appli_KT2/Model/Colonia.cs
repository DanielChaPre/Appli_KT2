using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
   public  class Colonia : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int idColonia;
        private string nombreColonia;
        private string cp;
        private int idMunicipio;

        public int IdColonia
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
        public string Cp
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
        public int IdMunicipio
        {
            get
            {
                return idMunicipio;
            }
            set
            {
                idMunicipio = value;
                OnPropertyChanged();
            }
        }
    }
}
