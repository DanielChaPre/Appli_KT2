using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class EmpleadoPlantel: Persona, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int cve_empleado_plantel;
        private int idPlantelesES;
        // 1.- directivo, 2.- Profesor
        private int tipo;
        private DateTime fecha_registro;
        private Persona personaEP;

        public int Cve_empleado_plantel
        {
            get { return cve_empleado_plantel; }
            set
            {
                cve_empleado_plantel = value;
                OnPropertyChanged();
            }
        }
        public int IdPlantelesES
        {
            get { return idPlantelesES; }
            set
            {
                idPlantelesES = value;
                OnPropertyChanged();
            }
        }
        public int Tipo
        {
            get { return tipo; }
            set
            {
                tipo = value;
                OnPropertyChanged();
            }
        }
        public DateTime Fecha_registro
        {
            get { return fecha_registro; }
            set
            {
                fecha_registro = value;
                OnPropertyChanged();
            }
        }
        public Persona PersonaEP
        {
            get { return personaEP; }
            set
            {
                personaEP = value;
                OnPropertyChanged();
            }
        }
    }
}
