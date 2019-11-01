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
        #endregion
        #region constructores
        public MainViewModel()
        {
            instance = this;
            this.Login = new LoginViewModel();
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
