using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class Persona
    {
        public int cve_persona { get; set; }
        public string nombre { get; set; }
        public string apellido_paterno { get; set; }
        public string apellido_materno { get; set; }
        public string rfc { get; set; }
        public string curp { get; set; }
        public string sexo { get; set; }
        public DateTime fecha_nacimiento { get; set; }
        public string numero_telefono { get; set; }
        public string correo_electronico { get; set; }
        public int estado_civil { get; set; }
        public string nacionalidad { get; set; }
        public string municipio { get; set; }
        public DateTime fecha_registro { get; set; }
        public string colonia { get; set; }
        public int cve_grupo_seguridad_usuario { get; set; }
    }
}
