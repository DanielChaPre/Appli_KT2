using SQLite;
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
        private string RVOE;
        private string modalidad;
        private string duracion;
        private string costos;
        private string region;
        private string nombre_region;
        private string requisitos;
        private string nombre_contacto;
        private string correo_contacto;
        private string vinculacion;
        private string resenia;
        private string actividades_extracurriculares;
        private string fecha_expedicion;
        private string fecha_inscripcion;
        private string fecha_inicio;
        private int cve_detalle_plantel;
        private int cve_nivel_estudio;
        private int cve_nivel_agrupado;
        private int cve_nivel_carrera;

        [PrimaryKey]
        public int Cve_detalle_carrera_plantel { get => cve_detalle_carrera_plantel; set => cve_detalle_carrera_plantel = value; }
        public int IdCarreraES { get => idCarreraES; set => idCarreraES = value; }
        public string Perfil_ingreso { get => perfil_ingreso; set => perfil_ingreso = value; }
        public string Perfil_egreso { get => perfil_egreso; set => perfil_egreso = value; }
        public string Sector_productivo { get => sector_productivo; set => sector_productivo = value; }
        public string RVOE1 { get => RVOE; set => RVOE = value; }
        public string Modalidad { get => modalidad; set => modalidad = value; }
        public string Duracion { get => duracion; set => duracion = value; }
        public string Costos { get => costos; set => costos = value; }
        public string Region { get => region; set => region = value; }
        public string Nombre_region { get => nombre_region; set => nombre_region = value; }
        public string Requisitos { get => requisitos; set => requisitos = value; }
        public string Nombre_contacto { get => nombre_contacto; set => nombre_contacto = value; }
        public string Correo_contacto { get => correo_contacto; set => correo_contacto = value; }
        public string Vinculacion { get => vinculacion; set => vinculacion = value; }
        public string Resenia { get => resenia; set => resenia = value; }
        public string Actividades_extracurriculares { get => actividades_extracurriculares; set => actividades_extracurriculares = value; }
        public string Fecha_expedicion { get => fecha_expedicion; set => fecha_expedicion = value; }
        public string Fecha_inscripcion { get => fecha_inscripcion; set => fecha_inscripcion = value; }
        public string Fecha_inicio { get => fecha_inicio; set => fecha_inicio = value; }
        public int Cve_detalle_plantel { get => cve_detalle_plantel; set => cve_detalle_plantel = value; }
        public int Cve_nivel_estudio { get => cve_nivel_estudio; set => cve_nivel_estudio = value; }
        public int Cve_nivel_agrupado { get => cve_nivel_agrupado; set => cve_nivel_agrupado = value; }
        public int Cve_nivel_carrera { get => cve_nivel_carrera; set => cve_nivel_carrera = value; }
    }
}
