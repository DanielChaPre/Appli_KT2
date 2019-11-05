using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class GrupoSeguridad
    {
        public int cve_grupo_seguridad { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string estatus { get; set; }
        public DateTime fecha_registro { get; set; }
        public string responsable { get; set; }
        public string cve_grupo_seguridad_plantilla { get; set; }
    }
}
