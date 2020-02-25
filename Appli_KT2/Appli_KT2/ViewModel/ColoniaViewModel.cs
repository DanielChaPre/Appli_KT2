﻿using Appli_KT2.Model;
using Appli_KT2.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Appli_KT2.ViewModel
{
    public class ColoniaViewModel : Colonias
    {
        private string cp;
        private List<Colonias> lstColonias = new List<Colonias>();
        public ColoniaViewModel(string cp)
        {
            this.cp = cp;
            ObtenerColonias();
        }

        public List<Colonias> ListColonias
        {
            get;
            set;
        }

        private async void ObtenerColonias()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
                var uri = new Uri(string.Format(conexion.URL + conexion.ObtenerColonia + cp, string.Empty));
                HttpResponseMessage response = await _client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var listaColonia = JsonConvert.DeserializeObject<List<Colonias>>(content);
                    for (int i = 0; i < listaColonia.Count; i++)
                    {
                        var entColonia = new Colonias()
                        {
                            IdColonia = listaColonia[i].IdColonia,
                            Cp = listaColonia[i].Cp,
                            NombreColonia = listaColonia[i].NombreColonia,
                            IdMunicipio = listaColonia[i].IdMunicipio
                        };
                        lstColonias.Add(entColonia);
                    }
                    //this.ListEstados = JsonConvert.DeserializeObject<List<Estados>>(content);
                    this.ListColonias = this.lstColonias;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
