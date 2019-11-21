using Appli_KT2.Model;
using Appli_KT2.Utils;
using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
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
        HttpClient cliente;
        string[] validado;
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

            if (this.usuario.Length == 18)
            {
                if (!ValidarCurp(this.usuario))
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
            MainViewModel.GetInstance().Registrar = new RegistrarViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterPage());
        }

        public bool ValidarCurp(string usuario)
        {
            try
            {
                var re = " ([A - Z][AEIOUX][A - Z]{ 2}\n{ 2} (?: 0[1 - 9] | 1[0 - 2])(?:0[1 - 9] |[12]\n | 3[01])[HM](?:AS | B[CS] | C[CLMSH] | D[FG] | G[TR] | HG | JC | M[CNS] | N[ETL] | OC | PL | Q[TR] | S[PLR] | T[CSL] | VZ | YN | ZS)[B - DF - HJ - NP - TV - Z]{ 3}[A-Z\n])(\n)";
                //validado = curp.match(re);
                //MatchCollection match = Regex.Matches(re, usuario);
                MatchCollection validado = Regex.Matches(re, usuario);
                if (validado == null)
                    return false;
                int digitoVerificador(string curp17)
                {
                    var diccionario = "0123456789ABCDEFGHIJKLMNÑOPQRSTUVWXYZ";
                    var lngSuma = 0.0;
                    var  lngDigito = 0.0;
                    for (int i = 0; i < 17; i++)
                    {
                        lngSuma = lngSuma + diccionario.IndexOf(curp17) * (18 - i);
                    }
                    lngDigito = 10 - lngSuma % 10;
                    if (lngDigito == 10)
                        return 10;
                    return Convert.ToInt32(lngDigito);
                }
                if (Convert.ToInt16(validado[2]) != digitoVerificador(validado[1].ToString()))
                    return false;
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        #endregion
    }
}

