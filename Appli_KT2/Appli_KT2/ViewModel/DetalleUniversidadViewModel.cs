using Appli_KT2.Utils;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Appli_KT2.ViewModel
{
    public class DetalleUniversidadViewModel
    {
        public void ConsultarUniversidad()
        {
            try
            {
                var _client = new HttpClient();
                var conexion = new ConexionWS();
            //    var uri = new Uri(string.Format(conexion.URL + conexion.));
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
