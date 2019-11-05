using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class Notificaciones
    {
        public int cve_notificaciones { get; set; }
        public string texto { get; set; }
        public string responsable { get; set; }
        public string cve_categoria { get; set; }
        public string titulo { get; set; }
        public string url { get; set; }
        public string audiencia { get; set; }
        public string tipo_notificaciones { get; set; }
        //public MediosEnvio medio_de_difusion { get; set; }
        public string fecha_notificacion { get; set; }
        public string hora_notificacion { get; set; }
        public int cve_detalle_notificacion { get; set; }
    }
}
