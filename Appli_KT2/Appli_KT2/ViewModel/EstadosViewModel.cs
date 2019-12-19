using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class EstadosViewModel : Estados
    {
        private string estado;
        private string estadoSeleccionado;
        private List<string> lstNombreEstados = new List<string>();
        private List<Estados> lstEstados = new List<Estados>();
        private List<Estados> listEstados;
        private Estados _selectedEstado;
        private Estados entEstados;
       

        public EstadosViewModel()
        {
            ObtenerEstados();
        }

        public Estados SelectedEstado
        {
            get
            {
                return _selectedEstado;
            }
            set
            {
                _selectedEstado = value;
                OnPropertyChanged();
                //put here your code  
                estadoSeleccionado = "City : " + _selectedEstado.NombreEstado;
                Console.WriteLine("Estado recogido:" + _selectedEstado.NombreEstado);
            }
        }

        public List<string> ListNombreEstados
        {
            get;
            set;
        }

        public List<Estados> ListEstados
        {
            get;
            set;
        }

        public string Estado
        {
            get { return this.estado; }
            set
            {
                estado = value;
                OnPropertyChanged();
            }
        }

        public async void ObtenerEstados()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerEstados, string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaEstados = JsonConvert.DeserializeObject<List<Estados>>(content);
                    for (int i = 0; i < listaEstados.Count; i++)
                    {
                        entEstados = new Estados()
                        {
                            IdEstado = listaEstados[i].IdEstado,
                            NombreEstado = listaEstados[i].NombreEstado,
                            IdPais = listaEstados[i].IdPais
                        };
                        lstEstados.Add(entEstados);
                        lstNombreEstados.Add(listaEstados[i].NombreEstado);
                    }
                    //this.ListEstados = JsonConvert.DeserializeObject<List<Estados>>(content);
                    this.ListEstados = this.lstEstados;
                    this.ListNombreEstados = this.lstNombreEstados;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
