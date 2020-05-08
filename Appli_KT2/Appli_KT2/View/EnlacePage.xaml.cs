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
	public partial class EnlacePage : ContentPage
	{
		public EnlacePage (string enlace)
		{
            try
            {
                InitializeComponent();
                webviewenlace.Source = enlace;
            }
            catch (Exception ex)
            {
            }
           
		}
	}
}