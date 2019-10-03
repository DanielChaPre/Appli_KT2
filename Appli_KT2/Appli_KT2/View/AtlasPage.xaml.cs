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
	public partial class AtlasPage : ContentPage
	{
		public AtlasPage ()
		{
			InitializeComponent ();
            btnBuscar.Clicked += buscarAtlas;
		}

        private async void buscarAtlas(object sender, EventArgs e)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ResultadoAtlasPage());
        }
    }
}