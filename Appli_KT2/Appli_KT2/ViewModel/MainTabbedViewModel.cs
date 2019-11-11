using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class MainTabbedViewModel:BaseViewModel
    {

        public MainTabbedViewModel()
        {

        }
        #region Comandos

        public ICommand IrLoginCommand
        {
            get
            {
                return new RelayCommand(IrLogin);
            }
        }
        #endregion
        #region Metodos
        public async void IrLogin()
        {
            MainViewModel.GetInstance().Login = new LoginViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }
        #endregion

    }
}
