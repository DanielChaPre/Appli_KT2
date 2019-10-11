//using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        #region atributos
        private string usuario;
        private string password;
        private bool isRunning;
        private bool isEnable;
        #endregion

        #region propiedades
        public string Usuario {
            get { return this.usuario;}

            set
            {
                SetValue(ref this.usuario, value); 
            }
        }

        public string Password {
            get { return this.password; }
            set
            {
                SetValue(ref this.password, value);
            }
        }

        public bool IsRunning
        {
            get { return this.isRunning; }
            set
            {
                SetValue(ref this.isRunning, value);
            }
        }

        public bool IsRemember { get; set; }

        public bool IsEnable
        {
            get { return this.isEnable; }
            set
            {
                SetValue(ref this.isEnable, value);
            }
        }
        #endregion

        #region constructor
        public LoginViewModel()
        {
            this.IsRemember = true;
            this.IsEnable = true;
        }
        #endregion

        #region comandos
        public ICommand loginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }

        private async void Login()
        {
            if (string.IsNullOrEmpty(this.usuario))
            {
                await Application.Current.MainPage.DisplayAlert("Error","Ingresa el usuario","Accept");
                return;
            }
            if (string.IsNullOrEmpty(this.password))
            {
                await Application.Current.MainPage.DisplayAlert("Error","Ingresa el password","Accept");
                return;
            }

            this.IsRunning = true;
            this.IsEnable = false;

            if (this.usuario != "admin" && this.password != "admin")
            {
                this.IsRunning = false;
                this.IsEnable = true;
                await Application.Current.MainPage.DisplayAlert("Error", "usuario o password incorrecto", "Accept");
                this.password = string.Empty;
                return;
            }

            this.IsRunning = false;
            this.IsEnable = true;

            await Application.Current.MainPage.DisplayAlert("Ok", "Usuario encontrado", "Accept");
        }

        public ICommand registrarCommand { get; set; }
        #endregion
    }
}
