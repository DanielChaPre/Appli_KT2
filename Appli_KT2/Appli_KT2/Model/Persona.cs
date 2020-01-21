using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Appli_KT2.Model
{
    [JsonObject]
    public class Persona : BaseViewModel ,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private int cve_persona;
        private string nombre;
        private string apellido_paterno;
        private string apellido_materno;
        private string rfc;
        private string curp;
        private string sexo;
        private DateTime fecha_nacimiento;
        private string numero_telefono;
        private int estado_civil;
        private string nacionalidad;
        private string municipio;
        private int idColonia;
        private Usuario usuario = new Usuario();

        //[JsonProperty("Nombre")]
        public string Nombre {
            get {
                return nombre;
            }
            set
            {
                nombre = value;
                OnPropertyChanged();
            }
        }
      //[JsonProperty("CVE_Persona")]
        public int Cve_Persona
        {
            get { return this.cve_persona; }
            set
            {
                cve_persona = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("Apellido_Paterno")]
        public string Apellido_Paterno
        {
            get { return this.apellido_paterno; }
            set
            {
                this.apellido_paterno = value;
                OnPropertyChanged();
            }
        }
       //[JsonProperty("Apellido_Materno")]
        public string Apellido_Materno
        {
            get { return this.apellido_materno; }
            set
            {
                this.apellido_materno = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("RFC")]
        public string RFC
        {
            get { return this.rfc; }
            set
            {
                this.rfc = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("CURP")]
        public string CURP
        {
            get { return this.curp; }
            set
            {
                this.curp = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("Sexo")]
        public string Sexo
        {
            get { return this.sexo; }
            set
            {
                this.sexo = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("Fecha_Nacimiento")]
        public DateTime Fecha_Nacimiento
        {
            get { return this.fecha_nacimiento; }
            set
            {
                this.fecha_nacimiento = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("Numero_Telefono")]
        public string Numero_Telefono
        {
            get { return this.numero_telefono; }
            set
            {
                this.numero_telefono = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("Estado_Civil")]
        public int Estado_Civil
        {
            get { return this.estado_civil; }
            set
            {
                this.estado_civil = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("Nacionalidad")]
        public string Nacionalidad
        {
            get { return this.nacionalidad; }
            set
            {
                this.nacionalidad = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("Municipio")]
        public string Municipio
        {
            get { return this.municipio; }
            set
            {
                this.municipio = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("IdColonia")]
        public int IdColonia
        {
            get { return this.idColonia; }
            set
            {
                this.idColonia = value;
                OnPropertyChanged();
            }
        }
        //[JsonProperty("Usuario")]
        public Usuario Usuario
        {
            get { return this.usuario; }
            set
            {
                this.usuario = value;
                OnPropertyChanged();
            }
        }


    }
}
