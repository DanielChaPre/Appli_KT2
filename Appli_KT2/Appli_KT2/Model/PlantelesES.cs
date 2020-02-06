using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class PlantelesES : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int _idPlantelesES;
        private string _clavePlantel;
        private string _nombrePlantelES;
        private string _subsistema;
        private string _sostenimiento;
        private string _municipio;
        private string _activo;
        private string _claveInstitucion;
        private string _nombreInstitucionES;
        private string _oPD;
        private string _nivelAgrupado;
        private string _carreraES;

        public int idPlantelesES
        {
            get
            {
                return _idPlantelesES;
            }
            set
            {
                _idPlantelesES = value;
                OnPropertyChanged();
            }
        }
        public string ClavePlantel
        {
            get
            {
                return _clavePlantel;
            }
            set
            {
                _clavePlantel = value;
                OnPropertyChanged();
            }
        }
        public string NombrePlantelES
        {
            get
            {
                return _nombrePlantelES;
            }
            set
            {
                _nombrePlantelES = value;
                OnPropertyChanged();
            }
        }
        public string Subsistema
        {
            get
            {
                return _subsistema;
            }
            set
            {
                _subsistema = value;
                OnPropertyChanged();
            }
        }
        public string Sostenimiento
        {
            get
            {
                return _sostenimiento;
            }
            set
            {
                _sostenimiento = value;
                OnPropertyChanged();
            }
        }
        public string Municipio
        {
            get
            {
                return _municipio;
            }
            set
            {
                _municipio = value;
                OnPropertyChanged();
            }
        }
        public string Activo
        {
            get
            {
                return _activo;
            }
            set
            {
                _activo = value;
                OnPropertyChanged();
            }
        }
        public string ClaveInstitucion
        {
            get
            {
                return _claveInstitucion;
            }
            set
            {
                _claveInstitucion = value;
                OnPropertyChanged();
            }
        }
        public string NombreInstitucionES
        {
            get
            {
                return _nombreInstitucionES;
            }
            set
            {
                _nombreInstitucionES = value;
                OnPropertyChanged();
            }
        }
        public string OPD
        {
            get
            {
                return _oPD;
            }
            set
            {
                _oPD = value;
                OnPropertyChanged();
            }
        }
        public string NivelAgrupado
        {
            get
            {
                return _nivelAgrupado;
            }
            set
            {
                _nivelAgrupado = value;
                OnPropertyChanged();
            }
        }
        public string CarreraES
        {
            get
            {
                return _carreraES;
            }
            set
            {
                _carreraES = value;
                OnPropertyChanged();
            }
        }
    }
}
