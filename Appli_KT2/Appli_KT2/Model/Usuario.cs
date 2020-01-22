using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class Usuario : BaseViewModel
    {
        private int cve_usuario;
        private int idAlumno;
        private string nombre_usuario;
        private string contrasena;
        private string fecha_registro;
        private string estatus;
        private string alias_red;
        private int tipo_usuario;
        private string ruta_imagen;

        public int Cve_Usuario
        {
            get { return this.cve_usuario; }
            set
            {
                SetValue(ref this.cve_usuario, value);
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
        public string Nombre_Usuario
        {
            get { return this.nombre_usuario; }
            set
            {
                SetValue(ref this.nombre_usuario, value);
            }
        }
        public string Contrasena
        {
            get { return this.contrasena; }
            set
            {
                SetValue(ref this.contrasena, value);
            }
        }
        public string Estatus
        {
            get { return this.estatus; }
            set
            {
                SetValue(ref this.estatus, value);
            }
        }
        public string Fecha_Registro
        {
            get { return this.fecha_registro; }
            set
            {
                SetValue(ref this.fecha_registro, value);
            }
        }
        public string Alias_Red
        {
            get { return this.alias_red; }
            set
            {
                SetValue(ref this.alias_red, value);
            }
        }
        public int Tipo_Usuario
        {
            get { return this.tipo_usuario; }
            set
            {
                SetValue(ref this.tipo_usuario, value);
            }
        }
        public string Ruta_Imagen
        {
            get { return this.ruta_imagen; }
            set
            {
                SetValue(ref this.ruta_imagen, value);
            }
        }

        ////[JsonProperty("CVE_Usuario")]
        //public int Cve_Usuario
        //{
        //    get { return this.cve_usuario; }
        //    set
        //    {
        //        SetValue(ref this.cve_usuario, value);
        //    }
        //}
        ////[JsonProperty("IdAlumno")]
        //public int IdAlumno
        //{
        //    get { return this.idAlumno; }
        //    set
        //    {
        //        SetValue(ref this.idAlumno, value);
        //    }
        //}
        ////[JsonProperty("Nombre_Usuario")]
        //public string Nombre_Usuario
        //{
        //    get { return this.nombre_usuario; }
        //    set
        //    {
        //        SetValue(ref this.nombre_usuario, value);
        //    }
        //}
        ////[JsonProperty("Contrasena")]
        //public string Contrasena
        //{
        //    get { return this.contrasena; }
        //    set
        //    {
        //        SetValue(ref this.contrasena, value);
        //    }
        //}
        ////[JsonProperty("Fecha_Registro")]
        //public DateTime Fecha_Registro
        //{
        //    get { return this.fecha_registro; }
        //    set
        //    {
        //        SetValue(ref this.fecha_registro, value);
        //    }
        //}
        ////[JsonProperty("Estatus")]
        //public string Estatus
        //{
        //    get { return this.estatus; }
        //    set
        //    {
        //        SetValue(ref this.estatus, value);
        //    }
        //}
        ////[JsonProperty("Alias_Red")]
        //public string Alias_Red
        //{
        //    get { return this.alias_red; }
        //    set
        //    {
        //        SetValue(ref this.alias_red, value);
        //    }
        //}
    }
}
