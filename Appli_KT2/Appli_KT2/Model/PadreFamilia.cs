using Appli_KT2.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    public class PadreFamilia : Persona, INotifyPropertyChanged
    {
        public int cve_padre_familia;
        public int idAlumno;
        public DateTime fecha_registro;
        public Persona persona;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int CVE_Padre_Familia
        {
            get { return this.cve_padre_familia; }
            set
            {
                this.cve_padre_familia = value;
                OnPropertyChanged();
            }
        }
        public int IdAlumno
        {
            get { return this.idAlumno; }
            set
            {
                this.idAlumno = value;
                OnPropertyChanged();
            }
        }
        public DateTime Fecha_Registro
        {
            get { return this.fecha_registro; }
            set
            {
                this.fecha_registro = value;
                OnPropertyChanged();
            }
        }
        public Persona PersonaP
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
