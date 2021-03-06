﻿using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Appli_KT2.ViewModel
{
    public class HistorialViewModel : Historial
    {
        private HttpClient _client;
        private ConexionWS conexion;
        private List<Historial> lsthistorialUsuario = new List<Historial>();
        private bool isvisible;
        private bool isrun;

        public bool IsRun
        {
            get { return this.isrun; }
            set
            {
                isrun = value;
                OnPropertyChanged();
            }
        }

        public bool IsVisible
        {
            get { return this.isvisible; }
            set
            {
                isvisible = value;
                OnPropertyChanged();
            }
        }

        public HistorialViewModel()
        {
            var idAlumno = App.Current.Properties["idAlumno"].ToString();
            if (idAlumno.Equals("0"))
            {
                ConsultarHistorial();
            }
            else
            {
                ConsultarHistorialAlumno(idAlumno);
            }
          
        }

        public List<Historial> LstHistorial
        {
            get;
            set;
        }

        public async void ConsultarHistorialAlumno(string idAlumno)
        {
            try
            {
                IsRun = true;
                IsVisible = false;
                _client = new HttpClient();
                conexion = new ConexionWS();
                var url = conexion.URL + "" + conexion.ObtenerHistorialAlumno + idAlumno;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lstHistorial = JsonConvert.DeserializeObject<List<Historial>>(content);
                    for (int i = 0; i < lstHistorial.Count; i++)
                    {
                        var entHistorial = new Historial()
                        {
                            Cve_categoria = lstHistorial[i].Cve_categoria,
                            Cve_usuario = lstHistorial[i].Cve_usuario,
                            Cve_historial = lstHistorial[i].Cve_historial,
                            Url = lstHistorial[i].Url,
                            Descripcion = lstHistorial[i].Descripcion,
                            Fecha_registro = lstHistorial[i].Fecha_registro,
                            Titulo = lstHistorial[i].Titulo
                        };
                        lsthistorialUsuario.Add(entHistorial);
                    }
                    this.LstHistorial = this.lsthistorialUsuario;
                    IsRun = false;
                    IsVisible = true;
                    return;
                }
                else
                {
                    IsRun = false;
                    IsVisible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                IsRun = false;
                IsVisible = true;
                return;
            }
        }

        public async void ConsultarHistorial()
        {
            try
            {
                IsRun = true;
                IsVisible = false;
                var cveUsuario = App.Current.Properties["cveUsuario"].ToString();
                _client = new HttpClient();
                conexion = new ConexionWS();
                var url = conexion.URL + "" + conexion.Consultarhistorial + cveUsuario;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lstHistorial = JsonConvert.DeserializeObject<List<Historial>>(content);
                    for (int i = 0; i < lstHistorial.Count; i++)
                    {
                        var entHistorial = new Historial()
                        {
                            Cve_categoria = lstHistorial[i].Cve_categoria,
                            Cve_usuario = lstHistorial[i].Cve_usuario,
                            Cve_historial = lstHistorial[i].Cve_historial,
                            Url = lstHistorial[i].Url,
                            Descripcion = lstHistorial[i].Descripcion,
                            Fecha_registro = lstHistorial[i].Fecha_registro,
                            Titulo = lstHistorial[i].Titulo
                        };
                        lsthistorialUsuario.Add(entHistorial);
                    }
                    this.LstHistorial = this.lsthistorialUsuario;
                    IsRun = false;
                    IsVisible = true;
                    return;
                }
                else
                {
                    IsRun = false;
                    IsVisible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                IsRun = false;
                IsVisible = true;
                return;
            }
        }
    }
}
