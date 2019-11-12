using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Utils
{
    public class ConexionWS
    {
        private string url = "http://applikt.utleon.edu.mx/pruebaApplikt/Service1.svc";
        private string validarUsuario = "/validarUsuario/";
        private string validarContasenia = "/validarContrasenia/";
        private string crearCuenta = "/crearCuenta";
        private string crearPerfil = "/perfil";
        private string modificarPerfil = "/perfil";
        private string eliminarPerfil = "/perfil";
        private string consultarPerfil = "/perfil/";
        private string consultarNotificaciones = "/notificacion";
        private string obtenerPlanteles = "/planteles";
        private string obtenerCarreras = "/carreras";
        private string obtenerGruposSeguridad = "/gruposeguridad";

        public string URL
        {
            get { return this.url; }
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

        public string ObtenerGruposSeguridad { get => this.obtenerGruposSeguridad;}
    }
}
