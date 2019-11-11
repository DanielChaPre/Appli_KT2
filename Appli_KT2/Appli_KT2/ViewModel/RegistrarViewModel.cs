using Appli_KT2.Model;
using Appli_KT2.Utils;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace Appli_KT2.ViewModel
{
    public class RegistrarViewModel : BaseViewModel
    {
        #region Atributos
        private List<GrupoSeguridad> perfiles;
        private string b;
        private string c;
        private string d;
        private string e;
        private string f;
        private string g;
        private HttpClient _client;
        private ConexionWS conexion;
        private string url;
        #endregion
        #region Propiedades
        public List<GrupoSeguridad> Perfiles
        {
            get { return this.perfiles; }

            set
            {
                SetValue(ref this.perfiles, value);
            }
        }
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
            CargarPerfiles();
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
        private async void CargarPerfiles()
        {
            try
            {
                this.perfiles = new List<GrupoSeguridad>
                {
                    new GrupoSeguridad(){nombre = "Estudiantes"},
                    new GrupoSeguridad(){nombre = "Planteles/Escuela"},
                    new GrupoSeguridad(){nombre = "Padres de familia"},
                    
                };
                /**Prueba con Web Services
                 * _client = new HttpClient();
                conexion = new ConexionWS();
                url = conexion.URL + "" + conexion.ObtenerGruposSeguridad;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    this.perfiles = JsonConvert.DeserializeObject<List<string>>(content);
                    return;
                }**/
            }
            catch (Exception ex)
            {
                Console.WriteLine(@"\t ERROR: ", ex.Message);
                throw;
            }
        }
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
