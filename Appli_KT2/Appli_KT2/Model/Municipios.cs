using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class Municipios : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int idMunicipio;
        private string nombreMunicipio;
        private int idEstado;

        public int IdEstado
        {
            get
            {
                return idEstado;
            }
            set
            {
                idEstado = value;
                OnPropertyChanged();
            }
        }
        public string NombreMunicipio
        {
            get
            {
                return nombreMunicipio;
            }
            set
            {
                nombreMunicipio = value;
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

