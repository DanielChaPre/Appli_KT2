using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.ViewModel
{
    public class MainViewModel
    {
        #region propiedades
        public LoginViewModel Login { get; set; }
        #endregion
        #region constructores
        public MainViewModel()
        {
            this.Login = new LoginViewModel();
        } 
        #endregion
    }
}
