using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Model
{
    public class Resultado
    {
        private string _aptitud1;
        private string _aptitud2;
        private string _aptitud3;
        private int _idAlumno;

        public string aptitud1 { get => _aptitud1; set => _aptitud1 = value; }
        public string aptitud2 { get => _aptitud2; set => _aptitud2 = value; }
        public string aptitud3 { get => _aptitud3; set => _aptitud3 = value; }
        public int idAlumno { get => _idAlumno; set => _idAlumno = value; }
    }
}
