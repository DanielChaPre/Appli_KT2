using Appli_KT2.Utils;
using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Text.RegularExpressions;
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

            if (this.usuario.Length == 18)
            {
                if (!ValidarCurp(this.usuario.ToUpper()))
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Si se intento ingresar la curp, esta esta incorrecta", "Accept");
                    return;
                }else
                    Xamarin.Forms.Application.Current.Properties["usuarioAlumno"] = this.usuario;
            }
            else
                Xamarin.Forms.Application.Current.Properties["usuario"] = this.usuario;

            if (this.contrasenia.Length < 8)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "La longitud de la contraseña no puede ser menor a 8 caracteres", "Accept");
                return;
            }
            Xamarin.Forms.Application.Current.Properties["contrasenia"] = this.contrasenia;
            MainViewModel.GetInstance().Registrar = new PerfilPadreViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        public bool ValidarCurp(string curp)
        {
            try
            {
                string regex =
                "^[A-Z]{1}[AEIOU]{1}[A-Z]{2}[0-9]{2}" +
                "(0[1-9]|1[0-2])(0[1-9]|1[0-9]|2[0-9]|3[0-1])" +
                "[HM]{1}" +
                "(AS|BC|BS|CC|CS|CH|CL|CM|DF|DG|GT|GR|HG|JC|MC|MN|MS|NT|NL|OC|PL|QT|QR|SP|SL|SR|TC|TS|TL|VZ|YN|ZS|NE)" +
                "[B-DF-HJ-NP-TV-Z]{3}" +
                "[0-9A-Z]{1}[0-9]{1}$";
                if (Regex.IsMatch(curp, @regex))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return false;
                throw;
            }
        }
        #endregion
    }
}

