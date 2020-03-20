using Appli_KT2.Utils;
using Appli_KT2.View;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class RecuperarContrasenaViewModel : BaseViewModel
    {
        #region atributos
        private string password;
        private bool isRunning;
        private string nuevaContrasena;
        private HttpClient _client;
        private ConexionWS conexion;
        private string url;

        public string Password {
            get { return this.password; }
            set
            {
                SetValue(ref this.password, value);
            }
        }
        public string NuevaContrasena
        {
            get { return this.nuevaContrasena; }
            set
            {
                SetValue(ref this.nuevaContrasena, value);
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
        #endregion

        #region Constructor
        public RecuperarContrasenaViewModel()
        {
            this.IsRunning = false;
        }
        #endregion

        #region Commandos
        public ICommand RecuperarContrasenaCommand
        {
            get
            {
                return new RelayCommand(RecuperarContrasena);
            }
        }

        private async void RecuperarContrasena()
        {
            try
            {
                //Quien lo envia
                var fromAddress = new MailAddress("danchavez197@gmail.com", "From Name");
                // Quien lo resive
                var toAddress = new MailAddress(Password, "To Name");
                //Contraseña del correo quien lo envia
                const string fromPassword = "Daniel1998";
                //Titulo del correo
                const string subject = "Recuperar Contraseña";
                var usuario = Xamarin.Forms.Application.Current.Properties["cveUsuario"].ToString();
                var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(usuario);
                var usuarioBase64 = System.Convert.ToBase64String(plainTextBytes);
                usuarioBase64.Replace("+","-");
                usuarioBase64.Replace("/", "_");
                usuarioBase64.Replace("=", "");
                var url = "https://applikt.utleon.edu.mx/index.php?accion=cmVjdXBlcmFyX2NvbnRyYXNlbmE&usuario=" + usuarioBase64 + "";
                string body = url;

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error: "+ex.Message, "Accept");
            }
        }

        private bool VerificarNuevaContrasena()
        {
            try
            {
                if (this.password.Equals(this.nuevaContrasena))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
