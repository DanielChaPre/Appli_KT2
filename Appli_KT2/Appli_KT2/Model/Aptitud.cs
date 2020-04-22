using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class Aptitud
    {
        private int cve_aptitud;
        private string nombre;
        private string estatus;

        public int Cve_aptitud { get => cve_aptitud; set => cve_aptitud = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Estatus { get => estatus; set => estatus = value; }
    }
}
