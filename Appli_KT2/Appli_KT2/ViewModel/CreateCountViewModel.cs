using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class CreateCountViewModel : BaseViewModel
    {
        #region atributos
       
        private string usuario;
        private string contrasenia;
        public ConexionWS conexion;
        HttpClient cliente;
        string url;
        #endregion

        #region propiedades
        public string Usuario
        {
            get { return this.usuario; }
            set
            {
                SetValue(ref this.usuario, value);
            }
        }

        public string Contrasenia
        {
            get { return this.contrasenia; }
            set
            {
                SetValue(ref this.contrasenia, value);
            }
        }
        #endregion
        #region Comandos
        public ICommand CrearCuentaCommand
        {
            get
            {
                return new RelayCommand(CrearCuenta);
            }
        }

        #endregion
        #region Métodos
        private async void CrearCuenta()
        {
            MainViewModel.GetInstance().CrearCuenta = new CreateCountViewModel();
            if (string.IsNullOrEmpty(this.usuario))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese el usuario", "Acceptar");
                return;
            }
            if (string.IsNullOrEmpty(this.contrasenia))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingrese la contraseña", "Acceptar");
                return;
            }

            if (this.contrasenia.Length < 8)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La longitud de la contraseña no puede ser menor a 8 caracteres", "Accept");
                return;
            }

            Xamarin.Forms.Application.Current.Properties["usuario"] = this.usuario;
            Xamarin.Forms.Application.Current.Properties["contrasenia"] = this.contrasenia;
            MainViewModel.GetInstance().Registrar = new RegistrarViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }
        #endregion
    }
}

