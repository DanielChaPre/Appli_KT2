using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class PlantelesES
    {
        public int idPlantelesES { get; set; }
        public string ClavePlantel { get; set; }
        public string NombrePlantelES { get; set; }
        public string Subsistema { get; set; }
        public string Sostenimiento { get; set; }
        public string Municipio { get; set; }
        public int Activo { get; set; }
        public string ClaveInstitucion { get; set; }
        public string NombreInstitucionES { get; set; }
        public string OPD { get; set; }
        public string NivelAgrupado { get; set; }
        public string CarreraES { get; set; }
        public DetallePlantel detalle_lantel { get; set; }
    }
}
