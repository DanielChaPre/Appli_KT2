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
	public partial class MainPage : MasterDetailPage
	{
		public MainPage ()
		{
			InitializeComponent ();
            this.Master = new MasterPage();
           // this.Master = new NavigationPage(new MasterPage());
            this.Detail = new NavigationPage(new AtlasPage());

            App.MasterD = this;
		}
	}
}