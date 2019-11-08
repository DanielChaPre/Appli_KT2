using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Appli_KT2.ViewModel
{
    public class RegistrarViewModel
    {
        #region Atributos
        private string a;
        private string b;
        private string c;
        private string d;
        private string e;
        private string f;
        private string g;
        #endregion
        #region Propiedades
        public string MyProperty { get; set; }
        public string MyProperty1 { get; set; }
        public string MyProperty2 { get; set; }
        public string MyProperty3 { get; set; }
        public string MyProperty4 { get; set; }
        public string MyProperty5 { get; set; }
        #endregion
        #region Constructor
        public RegistrarViewModel()
        {

        }
        #endregion
        #region Comandos
        public ICommand RegistrarPerfilCommand
        {
            get
            {
                return new RelayCommand(RegistarPerfil);
            }
        }

        public ICommand ModificarPerfilCommand
        {
            get
            {
                return new RelayCommand(ModificarPerfil);
            }
        }
        public ICommand EliminarPerfilCommand
        {
            get
            {
                return new RelayCommand(EliminarPerfil);
            }
        }
        public ICommand ConsultarPerfilCommand
        {
            get
            {
                return new RelayCommand(ConsultarPerfil);
            }
        }
        #endregion
        #region Metodos
        private void RegistarPerfil()
        {
            throw new NotImplementedException();
        }

        private void ModificarPerfil()
        {
            throw new NotImplementedException();
        }

        private void EliminarPerfil()
        {
            throw new NotImplementedException();
        }

        private void ConsultarPerfil()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
