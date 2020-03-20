using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.ViewModel
{
    public class MainViewModel
    {
        #region propiedades

        public LoginViewModel Login { get; set; }
        public PerfilGeneralViewModel RegistrarUG { get; set; }
        //public PerfilAlumnoViewModel RegistrarA { get; set; }
        public PerfilEmpleadoPlantelViewModel RegistrarEP { get; set; }
        public PerfilEmpleadoViewModel RegistrarE { get; set; }
        public PerfilPadreViewModel RegistrarP { get; set; }
        public CreateCountViewModel CrearCuenta { get; set; }
        public MainTabbedViewModel MainTabbed { get; set; }
        public NotificacionesViewModel Notificaciones { get; set; }
        public AtlasViewModel Atlas { get; set; } 
        public HistorialViewModel Historial { get; set; }
        public EstadosViewModel Estados { get; set; }
        public MunicipioViewModel Municipio { get; set; }
        public RecuperarContrasenaViewModel Recuperar { get; set; }
        public ResultadoAtlasViewModel Resultado { get; set; }


        #endregion
        #region constructores
        public MainViewModel()
        {
            instance = this;
            MainTabbed = new MainTabbedViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;  
        }
        #endregion
    }
}
