using Appli_KT2.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class Alumno : BaseViewModel
    {
        private int idAlumno;
        private string Nombre;
        private string ApellidoPaterno;
        private string ApellidoMaterno;
        private string CURP;
        private string Sexo;
        private string Calle;
        private string NumeroExterior;
        private string NumeroInterior;
        private string Email;
        private string Celular;
        private string Telefono;
        private int OtroCicloEnProceso;
        private string MotivoNoEstudiar1;
        private string MotivoNoEstudiar2;
        private string MotivoNoEstudiar3;
        private string MeGustariaEstudiar;
        private string FOLIOSUREDSU;
        private string FolioSUREMS;
        private string Password;
        private int SeguirEstudiando;
        private int idColonia;
        private int idPlantelEMS;
        private string ClavePlantelESEC;
        private int idCarreraES1;
        private int idCarreraES2;
        private int idCarreraES3;
        private string Nacionalidad;
        private string TEMP_CP;
        private int Paso;
        private string UID_Firebase;
        private string Actualizaciones;
        private string PregutaActual;
        private int Finalizo;
        private int TerminosAceptadso;
        private int idMunicipio;
        private int idPais;
        private string OtraColonia;
        private string idMunicipioPlantel;
        private string idPaisPlantel;
        private string OtroPlantel;
        private string FechaRegistro;
        private string EmailValido;

        public string Actualizaciones1
        {
            get { return this.Actualizaciones; }
            set
            {
                SetValue(ref this.Actualizaciones, value);
            }
        }
        public string ApellidoMaterno1
        {
            get { return this.ApellidoMaterno; }
            set
            {
                SetValue(ref this.ApellidoMaterno, value);
            }
        }
        public string ApellidoPaterno1
        {
            get { return this.ApellidoPaterno; }
            set
            {
                SetValue(ref this.ApellidoPaterno, value);
            }
        }
        public string CURP1
        {
            get { return this.CURP; }
            set
            {
                SetValue(ref this.CURP, value);
            }
        }
        public string Calle1
        {
            get { return this.Calle; }
            set
            {
                SetValue(ref this.Calle, value);
            }
        }
        public string Celular1
        {
            get { return this.Celular; }
            set
            {
                SetValue(ref this.Celular, value);
            }
        }
        public string ClavePlantelESEC1
        {
            get { return this.ClavePlantelESEC; }
            set
            {
                SetValue(ref this.ClavePlantelESEC, value);
            }
        }
        public string Email1
        {
            get { return this.Email; }
            set
            {
                SetValue(ref this.Email, value);
            }
        }
        public string EmailValido1
        {
            get { return this.EmailValido; }
            set
            {
                SetValue(ref this.EmailValido, value);
            }
        }
        public string FOLIOSUREDSU1
        {
            get { return this.FOLIOSUREDSU; }
            set
            {
                SetValue(ref this.FOLIOSUREDSU, value);
            }
        }
        public string FechaRegistro1
        {
            get { return this.FechaRegistro; }
            set
            {
                SetValue(ref this.FechaRegistro, value);
            }
        }
        public int Finalizo1
        {
            get { return this.Finalizo; }
            set
            {
                SetValue(ref this.Finalizo, value);
            }
        }
        public string FolioSUREMS1
        {
            get { return this.FolioSUREMS; }
            set
            {
                SetValue(ref this.FolioSUREMS, value);
            }
        }
        public int IdAlumno
        {
            get { return this.idAlumno; }
            set
            {
                SetValue(ref this.idAlumno, value);
            }
        }
        public int IdCarreraES1
        {
            get { return this.idCarreraES1; }
            set
            {
                SetValue(ref this.idCarreraES1, value);
            }
        }
        public int IdCarreraES2
        {
            get { return this.idCarreraES2; }
            set
            {
                SetValue(ref this.idCarreraES2, value);
            }
        }
        public int IdCarreraES3
        {
            get { return this.idCarreraES3; }
            set
            {
                SetValue(ref this.idCarreraES3, value);
            }
        }
        public int IdColonia
        {
            get { return this.idColonia; }
            set
            {
                SetValue(ref this.idColonia, value);
            }
        }
        public int IdMunicipio
        {
            get { return this.idMunicipio; }
            set
            {
                SetValue(ref this.idMunicipio, value);
            }
        }
        public string IdMunicipioPlantel
        {
            get { return this.idMunicipioPlantel; }
            set
            {
                SetValue(ref this.idMunicipioPlantel, value);
            }
        }
        public int IdPais
        {
            get { return this.idPais; }
            set
            {
                SetValue(ref this.idPais, value);
            }
        }
        public string IdPaisPlantel
        {
            get { return this.idPaisPlantel; }
            set
            {
                SetValue(ref this.idPaisPlantel, value);
            }
        }
        public int IdPlantelEMS
        {
            get { return this.idPlantelEMS; }
            set
            {
                SetValue(ref this.idPlantelEMS, value);
            }
        }
        public string MeGustariaEstudiar1
        {
            get { return this.MeGustariaEstudiar; }
            set
            {
                SetValue(ref this.MeGustariaEstudiar, value);
            }
        }
        public string MotivoNoEstudiar11
        {
            get { return this.MotivoNoEstudiar1; }
            set
            {
                SetValue(ref this.MotivoNoEstudiar1, value);
            }
        }
        public string MotivoNoEstudiar21
        {
            get { return this.MotivoNoEstudiar2; }
            set
            {
                SetValue(ref this.MotivoNoEstudiar2, value);
            }
        }
        public string MotivoNoEstudiar31
        {
            get { return this.MotivoNoEstudiar3; }
            set
            {
                SetValue(ref this.MotivoNoEstudiar3, value);
            }
        }
        public string Nacionalidad1
        {
            get { return this.Nacionalidad; }
            set
            {
                SetValue(ref this.Nacionalidad, value);
            }
        }
        public string Nombre1
        {
            get { return this.Nombre; }
            set
            {
                SetValue(ref this.Nombre, value);
            }
        }
        public string NumeroExterior1
        {
            get { return this.NumeroExterior; }
            set
            {
                SetValue(ref this.NumeroExterior, value);
            }
        }
        public string NumeroInterior1
        {
            get { return this.NumeroInterior; }
            set
            {
                SetValue(ref this.NumeroInterior, value);
            }
        }
        public string OtraColonia1
        {
            get { return this.OtraColonia; }
            set
            {
                SetValue(ref this.OtraColonia, value);
            }
        }
        public int OtroCicloEnProceso1
        {
            get { return this.OtroCicloEnProceso; }
            set
            {
                SetValue(ref this.OtroCicloEnProceso, value);
            }
        }
        public string OtroPlantel1
        {
            get { return this.OtroPlantel; }
            set
            {
                SetValue(ref this.OtroPlantel, value);
            }
        }
        public int Paso1
        {
            get { return this.Paso; }
            set
            {
                SetValue(ref this.Paso, value);
            }
        }
        public string Password1
        {
            get { return this.Password; }
            set
            {
                SetValue(ref this.Password, value);
            }
        }
        public string PregutaActual1
        {
            get { return this.PregutaActual; }
            set
            {
                SetValue(ref this.PregutaActual, value);
            }
        }
        public int SeguirEstudiando1
        {
            get { return this.SeguirEstudiando; }
            set
            {
                SetValue(ref this.SeguirEstudiando, value);
            }
        }
        public string Sexo1
        {
            get { return this.Sexo; }
            set
            {
                SetValue(ref this.Sexo, value);
            }
        }
        public string TEMP_CP1
        {
            get { return this.TEMP_CP; }
            set
            {
                SetValue(ref this.TEMP_CP, value);
            }
        }
        public string Telefono1
        {
            get { return this.Telefono; }
            set
            {
                SetValue(ref this.Telefono, value);
            }
        }
        public int TerminosAceptadso1
        {
            get { return this.TerminosAceptadso; }
            set
            {
                SetValue(ref this.TerminosAceptadso, value);
            }
        }
        public string UID_Firebase1
        {
            get { return this.UID_Firebase; }
            set
            {
                SetValue(ref this.UID_Firebase, value);
            }
        }
    }
}
