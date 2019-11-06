using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.ViewModel
{
    public class MainViewModel
    {
        #region propiedades
        public LoginViewModel Login { get; set; }
        public RegistrarViewModel Registrar { get; set; }
        public CreateCountViewModel CrearCuenta { get; set; }
        #endregion
        #region constructores
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
            this.Registrar = new RegistrarViewModel();
            this.CrearCuenta = new CreateCountViewModel();
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
