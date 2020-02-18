using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class Empleado : Persona, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private int cve_empleado;
        private string numero_empleado;
        private string estatus;
        private string fecha_registro;
        private Persona personaE;

        public int Cve_Empleado
        {
            get { return cve_empleado; }
            set { cve_empleado = value;
                OnPropertyChanged();
            }
        }
        public string Numero_Empleado
        {
            get { return numero_empleado; }
            set
            {
                numero_empleado = value;
                OnPropertyChanged();
            }
        }
        public string Estatus
        {
            get { return estatus; }
            set
            {
                estatus = value;
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
            get { return personaE; }
            set
            {
                personaE = value;
                OnPropertyChanged();
            }
        }
    }
}
