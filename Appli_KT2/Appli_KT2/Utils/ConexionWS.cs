using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Utils
{
    public class ConexionWS
    {
        private string url = "http://applikt.utleon.edu.mx/webservicesapplikt/Service1.svc";
        private string url2 = "http://localhost:50681/Service1.svc/";
        private string validarUsuario = "/validarUsuario/";
        private string validarUsuarioAlumno = "/validarUsuarioAlumno/";
        private string validarContasenia = "/validarContrasenia/";
        private string validarContrasenaAlumno = "/validarContraseniaAlumno/";
        private string crearCuenta = "/crearCuenta/";
        private string crearPerfil = "/perfil";
        private string modificarPerfil = "/perfil";
        private string eliminarPerfil = "/perfil";
        private string consultarPerfil = "/perfil/";
        private string crearEmpleado = "/empleado";
        private string modificarEmpleado = "/empleado";
        private string eliminarEmpleado = "/empleado";
        private string consultarEmpleado = "/empleado/";
        private string crearEmpleadoPlantel = "/empleadoplantel";
        private string modificarEmpleadoPlantel = "/empleadoplantel";
        private string eliminarEmpleadoPlantel = "/empleadoplantel";
        private string consultarEmpleadoPlantel = "/empleadoplantel/";
        private string crearPadreFamilia = "/padrefamilia";
        private string modificarPadreFamilia = "/padrefamilia";
        private string eliminarPadreFamilia = "/padrefamilia";
        private string consultarPadreFamilia = "/padrefamilia/";
        private string crearAlumno = "/alumno";
        private string modificarAlumno = "/alumno";
        private string eliminarAlumno = "/alumno";
        private string consultarAlumno = "/alumno/";
        private string consultarNotificaciones = "/notificacion/";
        private string eliminarNotificaciones = "/notificacion/";
        private string obtenerPlanteles = "/planteles";
        private string obtenerPlantelEMS = "/buscarplantelEMS/";
        private string obtenerCarreras = "/carreras";
        private string obtenerGruposSeguridad = "/gruposeguridad";
        private string obtenerColonia = "/colonia/";
        private string obtenerMunicipio = "/municipios";
        private string obtenerEstados = "/estados";
        private string obtenerPaises = "/paises";
        private string buscarAlumnoCurp = "/alumnocurp/";
        private string buscarColonua = "/coloniaid/";
        private string obtenerMunicipiosEstado = "/municipios/";
        private string recuperarContrasena = "/recuperarContrasena/";
        private string verificarRegistroAlumno = "/verificarAlumno/";
        private string consultarUsuarioAlumno = "/usuarioalumno/";
        private string consultarhistorial = "/historial/";
        private string consultarDetallePlantel = "/buscardetalleplantel/";
        private string validarUsuarioFacebook = "/validarUsuarioFacebook/";
        private string validarUsuarioGoogle = "/validarUsuarioGoogle/";
        private string relacionarFacebookUsuario = "/relacionarFacebookUsuario/";
        private string relacionarGoogleUsuario = "/relacionarGoogleUsuario/";
        private string relacionarFacebookAlumno = "/relacionarFacebookAlumno/";
        private string relacionarGoogleAlumno = "/relacionarGoogleAlumno/";
        private string buscarImagenesPlantel = "/imagenesplantel/";
        private string obtenerCarrerasPlantel = "/carrerasplantel/";
        private string buscarImagenesPlanteles = "/imagenesplanteles";


        public string URL
        {
            get { return this.url; }
        }

        public string URL2
        {
            get { return this.url2; }
        }

        public string ValidarUsuario
        {
            get {return this.validarUsuario;}
        }

        public string ValidarContrasenia
        {
            get {return this.validarContasenia;}
        }
        public string CrearCuenta
        {
            get {return this.crearCuenta;}
        }
         public string CrearPerfil
            {
                get {return this.crearPerfil;}
            }
         public string ModificarPerfil
            {
                get {return this.modificarPerfil;}
            }
         public string EliminarPerfil
            {
                get {return this.eliminarPerfil;}
            }
        public string ConsultarPerfil
            {
                get {return this.consultarPerfil;
}
            }
        public string ConsultarNotificaciones
        {
            get {return this.consultarNotificaciones ;}
        }
        public string ObtenerPlanteles
        {
            get {return this.obtenerPlanteles;}
        }
        public string ObtenerCarreras
        {
            get {return this.obtenerCarreras ;}
        }

        public string ObtenerGruposSeguridad {
            get {return this.obtenerGruposSeguridad;}
        }

        public string CrearEmpleado { get { return this.crearEmpleado; } }
        public string ModificarEmpleado { get => modificarEmpleado; }
        public string EliminarEmpleado { get => eliminarEmpleado;}
        public string ConsultarEmpleado { get => consultarEmpleado;}
        public string CrearEmpleadoPlantel { get => crearEmpleadoPlantel;}
        public string ModificarEmpleadoPlantel { get => modificarEmpleadoPlantel; }
        public string EliminarEmpleadoPlantel { get => eliminarEmpleadoPlantel;}
        public string ConsultarEmpleadoPlantel { get => consultarEmpleadoPlantel;}
        public string CrearPadreFamilia { get => crearPadreFamilia; }
        public string ModificarPadreFamilia { get => modificarPadreFamilia;}
        public string EliminarPadreFamilia { get => eliminarPadreFamilia;}
        public string ConsultarPadreFamilia { get => consultarPadreFamilia;}
        public string CrearAlumno { get => crearAlumno; }
        public string ModificarAlumno { get => modificarAlumno;}
        public string EliminarAlumno { get => eliminarAlumno; }
        public string ConsultarAlumno { get => consultarAlumno;  }
        public string ObtenerColonia { get => obtenerColonia;  }
        public string ObtenerMunicipio { get => obtenerMunicipio;}
        public string ObtenerEstados { get => obtenerEstados;}
        public string ObtenerPaises { get => obtenerPaises;}
        public string BuscarAlumnoCurp { get => buscarAlumnoCurp; set => buscarAlumnoCurp = value; }
        public string BuscarColonia { get => buscarColonua; set => buscarColonua = value; }
        public string ObtenerMunicipiosEstado { get => obtenerMunicipiosEstado; set => obtenerMunicipiosEstado = value; }
        public string RecuperarContrasena { get => recuperarContrasena; set => recuperarContrasena = value; }
        public string VerificarRegistroAlumno { get => verificarRegistroAlumno; set => verificarRegistroAlumno = value; }
        public string ConsultarUsuarioAlumno { get => consultarUsuarioAlumno; set => consultarUsuarioAlumno = value; }
        public string ObtenerPlantelEMS { get => obtenerPlantelEMS; set => obtenerPlantelEMS = value; }
        public string EliminarNotificaciones { get => eliminarNotificaciones; set => eliminarNotificaciones = value; }
        public string ValidarContrasenaAlumno { get => validarContrasenaAlumno; set => validarContrasenaAlumno = value; }
        public string Consultarhistorial { get => consultarhistorial; set => consultarhistorial = value; }
        public string ConsultarDetallePlantel { get => consultarDetallePlantel; set => consultarDetallePlantel = value; }
        public string ValidarUsuarioAlumno { get => validarUsuarioAlumno; set => validarUsuarioAlumno = value; }
        public string ValidarUsuarioFacebook { get => validarUsuarioFacebook; set => validarUsuarioFacebook = value; }
        public string ValidarUsuarioGoogle { get => validarUsuarioGoogle; set => validarUsuarioGoogle = value; }
        public string RelacionarFacebookUsuario { get => relacionarFacebookUsuario; set => relacionarFacebookUsuario = value; }
        public string RelacionarGoogleUsuario { get => relacionarGoogleUsuario; set => relacionarGoogleUsuario = value; }
        public string RelacionarFacebookAlumno { get => relacionarFacebookAlumno; set => relacionarFacebookAlumno = value; }
        public string RelacionarGoogleAlumno { get => relacionarGoogleAlumno; set => relacionarGoogleAlumno = value; }
        public string BuscarImagenesPlantel { get => buscarImagenesPlantel; set => buscarImagenesPlantel = value; }
        public string ObtenerCarrerasPlantel { get => obtenerCarrerasPlantel; set => obtenerCarrerasPlantel = value; }
        public string BuscarImagenesPlanteles { get => buscarImagenesPlanteles; set => buscarImagenesPlanteles = value; }
    }
}
