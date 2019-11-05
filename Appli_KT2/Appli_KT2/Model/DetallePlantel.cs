using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class DetallePlantel
    {
        public int cve_detalle_plantel { get; set; }
        public string url_vinculacion { get; set; }
        public string imiagen_plantel { get; set; }
        public string logo_plantel { get; set; }
        public string costos { get; set; }
        public string requisitos { get; set; }
        public DateTime fechas { get; set; }
        public string reseña { get; set; }
        public string ubicacion { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string nivel_estudio { get; set; }
        public int cve_imagen_plantel { get; set; }
    }
}
