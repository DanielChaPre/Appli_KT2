using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class DetalleCarreraPlantel
    {
        private int cve_detalle_carrera_plantel;
        private int idCarreraES;
        private string perfil_ingreso;
        private string perfil_egreso;
        private string sector_productivo;
        private string rVOE;
        private string modalidad;
        private string duracion;
        private string costos;
        private int cve_detalle_plantel;

        public int Cve_detalle_carrera_plantel { get => cve_detalle_carrera_plantel; set => cve_detalle_carrera_plantel = value; }
        public int IdCarreraES { get => idCarreraES; set => idCarreraES = value; }
        public string Perfil_ingreso { get => perfil_ingreso; set => perfil_ingreso = value; }
        public string Perfil_egreso { get => perfil_egreso; set => perfil_egreso = value; }
        public string Sector_productivo { get => sector_productivo; set => sector_productivo = value; }
        public string RVOE { get => rVOE; set => rVOE = value; }
        public string Modalidad { get => modalidad; set => modalidad = value; }
        public string Duracion { get => duracion; set => duracion = value; }
        public string Costos { get => costos; set => costos = value; }
        public int Cve_detalle_plantel { get => cve_detalle_plantel; set => cve_detalle_plantel = value; }
    }
}
