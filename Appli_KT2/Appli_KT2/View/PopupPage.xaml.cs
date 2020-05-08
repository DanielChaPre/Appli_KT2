using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Appli_KT2.View
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PopUpPage : PopupPage
    {
		public PopUpPage(ImageSource imagen)
		{
			InitializeComponent ();
           // imagen = imagen.Replace(" ", "");
            imagenAmpliada.Source = imagen;
		}

        protected override bool OnBackgroundClicked()
        {
           
            return false;
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
           var p = this.CloseWhenBackgroundIsClicked;
            if (p)
            {
                Navigation.PopPopupAsync();
            }
        }
    }
}