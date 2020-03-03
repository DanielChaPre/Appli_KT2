using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Appli_KT2.Utils
{
    public class ConexionInternet
    {
        public bool VerificarInternet()
        {
            var status =  CrossConnectivity.Current.IsConnected ? "Conectado" : "Desconectado";

            if (status.Equals("Conectado"))
            {
                //Application.Current.MainPage.DisplayAlert("Alerta", "Se cuenta con conexión a internet", "Aceptar");
                return true;
            }
            else
            {
                //Application.Current.MainPage.DisplayAlert("Alerta", "No se cuenta con conexión a internet, esto puede provocar que la aplicación no funcione de la manera adecuada", "Aceptar");
                return false;
            }
        }
    }
}
