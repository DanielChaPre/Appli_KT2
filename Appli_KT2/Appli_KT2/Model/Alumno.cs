using Appli_KT2.ViewModel;
using Newtonsoft.Json;

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
        private string FOLIOSUREDSU;
        private string FolioSUREMS;
        private int idPlantelEMS;
        private string ClavePlantelESEC;
        private string Nacionalidad;
        private int idPais;
        private string idMunicipioPlantel;
        private string idPaisPlantel;
        private int idColonia;
        private int idMunicipio;
        private string cp;
        private int idEstado;


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

        public string FOLIOSUREDSU1
        {
            get { return this.FOLIOSUREDSU; }
            set
            {
                SetValue(ref this.FOLIOSUREDSU, value);
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

        public string Sexo1
        {
            get { return this.Sexo; }
            set
            {
                SetValue(ref this.Sexo, value);
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
        public string Cp
        {
            get { return this.cp; }
            set
            {
                SetValue(ref this.cp, value);
            }
        }
        public int IdEstado
        {
            get { return this.idEstado; }
            set
            {
                SetValue(ref this.idEstado, value);
            }
        }
    }
}
