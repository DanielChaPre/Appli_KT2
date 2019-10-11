using Appli_KT2.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Appli_KT2.Infrastructure
{
    public class InstanceLocator
    {
        #region Propiedades
        public MainViewModel Main
        {
            get;
            set;
        }
        #endregion

        #region constructor
        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
        #endregion

    }
}
