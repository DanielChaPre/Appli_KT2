using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class EmpleadoPlantel: INotifyPropertyChanged
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
        private string fecha_registro;
        private Persona personaEP;

        public int Cve_Empleado_Plantel
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
        public string Fecha_Registro
        {
            get { return fecha_registro; }
            set
            {
                fecha_registro = value;
                OnPropertyChanged();
            }
        }
        public Persona Persona
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
