using Appli_KT2.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Appli_KT2.Model
{
    public class ImagenPlantel : BaseViewModel
    {
    
        private int cve_detalle_plantel;
        private int cve_imagen_plantel;
        private int imagen_principal;
        private string ruta;
        private string descripcion;
        private Xamarin.Forms.ImageSource imagenDecodificada;

        public int Cve_detalle_plantel
        {
            get { return this.cve_detalle_plantel; }
            set
            {
                SetValue(ref this.cve_detalle_plantel, value);
            }
        }
        public int Cve_imagen_plantel
        {
            get { return this.cve_imagen_plantel; }
            set
            {
                SetValue(ref this.cve_imagen_plantel, value);
            }
        }
        public int Imagen_principal
        {
            get { return this.imagen_principal; }
            set
            {
                SetValue(ref this.imagen_principal, value);
            }
        }
        public string Ruta
        {
            get { return this.ruta; }
            set
            {
                SetValue(ref this.ruta, value);
            }
        }
        public string Descripcion
        {
            get { return this.descripcion; }
            set
            {
                SetValue(ref this.descripcion, value);
            }
        }
        [JsonIgnore]
        public ImageSource ImagenDecodificada
        {
            get { return this.imagenDecodificada; }
            set
            {
                SetValue(ref this.imagenDecodificada, value);
            }
        }
    }
}
