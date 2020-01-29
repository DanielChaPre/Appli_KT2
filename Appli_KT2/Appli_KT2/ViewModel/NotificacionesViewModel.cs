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
    public class NotificacionesViewModel : Notificaciones
    {
        ConexionWS conexion;
        HttpClient _client;
        MetodoHTTP metodoHTTP;
        private string url;
        private List<Notificaciones> lstnotificaciones = new List<Notificaciones>();

        public NotificacionesViewModel()
        {
            ConsultarNotificaciones();
        }

        public List<Notificaciones> Lst_Notificaciones
        {
            get;
            set;
        }

        public async Task<bool> EliminarNotificaciones()
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
                var cveUsuario = Xamarin.Forms.Application.Current.Properties["cveUsuario"];
                var cveNotificacion = Xamarin.Forms.Application.Current.Properties["cveNotificacion"];
                url = conexion.URL + "" + conexion.ConsultarNotificaciones + cveUsuario + "/" + cveNotificacion;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var resultado = JsonConvert.DeserializeObject<bool>(content);
                    if (resultado)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error " + response.StatusCode, "Accept");
                    return false;
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Error " + ex.Message, "Accept");
                return false;
            }
        }

        public async Task<bool> ConsultarNotificaciones()
        {
            try
            {
                _client = new HttpClient();
                conexion = new ConexionWS();
               // var cveUsuario = Xamarin.Forms.Application.Current.Properties["cveUsuario"];
                var cveUsuario = 72;
                url = conexion.URL + "" + conexion.ConsultarNotificaciones+cveUsuario;
                var uri = new Uri(string.Format(@"" + url, string.Empty));
                var response = await _client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var lstNotificaciones = JsonConvert.DeserializeObject<List<Notificaciones>>(content);
                    for (int i = 0; i < lstNotificaciones.Count; i++)
                    {
                        var entNotificaciones = new Notificaciones()
                        {
                            Cve_notificacion = lstNotificaciones[i].Cve_notificacion,
                            Cve_categoria = lstNotificaciones[i].Cve_categoria,
                            Cve_tipo_notificacion = lstNotificaciones[i].Cve_tipo_notificacion,
                            Fecha_notificacion = lstNotificaciones[i].Fecha_notificacion,
                            Hora_notificacion = lstNotificaciones[i].Hora_notificacion,
                            Responsable = lstNotificaciones[i].Responsable,
                            Texto = lstNotificaciones[i].Texto,
                            Titulo = lstNotificaciones[i].Titulo,
                            Url = lstNotificaciones[i].Url,
                            Icon = SeleccionarImagen(lstNotificaciones[i].Cve_tipo_notificacion)
                        };
                        lstnotificaciones.Add(entNotificaciones);
                    }
                    this.Lst_Notificaciones = this.lstnotificaciones;
                    return true;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Error" + response.StatusCode, "Accept");
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw;
            }
        }

        public string SeleccionarImagen(int tipo)
        {
            try
            {
                var imagen = "";
                switch (tipo)
                {
                    case 1:
                        //Oferta educativa
                        imagen = "ic_noti_oe.png";
                        break;
                    case 2:
                        // Suredsu
                        imagen = "ic_noti_s.png";
                        break;
                    default:
                        break;
                }
                return imagen;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
