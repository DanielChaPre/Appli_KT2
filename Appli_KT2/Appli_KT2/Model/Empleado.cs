using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class Empleado
    {
        public int cve_empleado { get; set; }
        public string numero_empleado { get; set; }
        public string estatus { get; set; }
        public DateTime fecha_registro { get; set; }
        public Persona persona { get; set; }
    }
}
