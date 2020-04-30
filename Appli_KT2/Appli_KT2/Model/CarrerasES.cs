using SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class CarrerasES : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int idcarreraES;
        private string nombreCarreraES;
        private int idplantelES;
        private string activa;
        private string claveCarrera;
        private string campoAmplio2016;
        private string campoAmplioAnterior;
        private string nivel;
        private string campoEspecifico2016;
        private string campoEspecificoAnterior;

        [PrimaryKey]
        public int idCarreraES
        {
            get
            {
                return idcarreraES;
            }
            set
            {
                idcarreraES = value;
                OnPropertyChanged();
            }
        }
        public string NombreCarreraES
        {
            get
            {
                return nombreCarreraES;
            }
            set
            {
                nombreCarreraES = value;
                OnPropertyChanged();
            }
        }
        public int IdPlantelesES
        {
            get
            {
                return idplantelES;
            }
            set
            {
                idplantelES = value;
                OnPropertyChanged();
            }
        }
        public string Activa
        {
            get
            {
                return activa;
            }
            set
            {
                activa = value;
                OnPropertyChanged();
            }
        }
        public string ClaveCarrera
        {
            get
            {
                return claveCarrera;
            }
            set
            {
                claveCarrera = value;
                OnPropertyChanged();
            }
        }
        public string CampoAmplio2016
        {
            get
            {
                return campoAmplio2016;
            }
            set
            {
                campoAmplio2016 = value;
                OnPropertyChanged();
            }
        }
        public string CampoAmplioAnterior
        {
            get
            {
                return campoAmplioAnterior;
            }
            set
            {
                campoAmplioAnterior = value;
                OnPropertyChanged();
            }
        }
        public string Nivel
        {
            get
            {
                return nivel;
            }
            set
            {
                nivel = value;
                OnPropertyChanged();
            }
        }
        public string CampoEspecifico2016
        {
            get
            {
                return campoEspecifico2016;
            }
            set
            {
                campoEspecifico2016 = value;
                OnPropertyChanged();
            }
        }
        public string CampoEspecificoAnterior
        {
            get
            {
                return campoEspecificoAnterior;
            }
            set
            {
                campoEspecificoAnterior = value;
                OnPropertyChanged();
            }
        }
    }
}
