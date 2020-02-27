using Appli_KT2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class PadreFamilia : INotifyPropertyChanged
    {
        private int cve_padre_familia;
        private int idAlumno;
        private string fecha_registro;
        private Persona persona;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Cve_Padre_Familia
        {
            get { return cve_padre_familia; }
            set
            {
                cve_padre_familia = value;
                OnPropertyChanged();
            }
        }
        public int IdAlumno
        {
            get { return idAlumno; }
            set
            {
                idAlumno = value;
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
            get { return persona; }
            set
            {
                persona = value;
                OnPropertyChanged();
            }
            
        }
    }
}
