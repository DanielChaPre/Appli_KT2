using SQLite;
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
        private int idmunicipio;
        private string nombreMunicipio;
        private int idestado;

        public int idEstado
        {
            get
            {
                return idestado;
            }
            set
            {
                idestado = value;
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
        [PrimaryKey]
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

