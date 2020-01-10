using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class MainTabbedViewModel : BaseViewModel
    {

        #region Constructor
        public MainTabbedViewModel()
        {
            // MainViewModel.GetInstance().Registrar = new PerfilPadreViewModel();
        }
        #endregion
        #region Comandos

        public ICommand IrLoginCommand
        {
            get
            {
                return new RelayCommand(IrLogin);
            }
        }

        public ICommand IrPerfilCommand
        {
            get
            {
                return new RelayCommand(IrPerfil);
            }
        }

        public ICommand IrSuredsuCommand
        {
              get
              {
                return new RelayCommand(IrSuredsu);
              }
        }
        public ICommand IrNotificacionesCommand
        {
            get
            {
                return new RelayCommand(IrNotificaciones);
            }
        }

        public ICommand IrAtlasCommand
        {
              get
                {
                return new RelayCommand(IrAtlas);
                }
        }
        public ICommand IrHistorialCommand
        {
              get
                {
                return new RelayCommand(IrHistorial);
                }
        }
        #endregion
        #region Metodos
        public async void IrLogin()
        {
                    MainViewModel.GetInstance().Login = new LoginViewModel();
                    await Application.Current.MainPage.Navigation.PushAsync(new LoginPage());
        }

        public async void IrPerfil()
        {
            //MainViewModel.GetInstance().Registrar = new PerfilPadreViewModel();
            //await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        public async void IrNotificaciones()
        {
            MainViewModel.GetInstance().Notificaciones = new NotificacionesViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new NotificacionesPage());
        }

        public async void IrSuredsu()
        {
           // MainViewModel.GetInstance().Registrar = new PerfilPadreViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new SuredsuPage());
        }

        public async void IrAtlas()
        {
            MainViewModel.GetInstance().Atlas = new AtlasViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AtlasPage());
        }

        public async void IrHistorial()
        {
            MainViewModel.GetInstance().Historial = new HistorialViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new HistorialPage());
        }
        #endregion

    }
}
