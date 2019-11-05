using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class Usuario
    {
        public int cve_usuario { get; set; }

        public string nombre_usuario { get; set; }

        public string contraseña { get; set; }

        public DateTime fecha_registro { get; set; }

        public string estatus { get; set; }

        public string rol { get; set; }

        public Persona persona { get; set; }
    }
}
